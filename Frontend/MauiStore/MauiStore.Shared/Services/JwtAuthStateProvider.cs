// MauiStore.Web/Services/JwtAuthStateProvider.cs
using MauiStore.Web.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

public sealed class JwtAuthStateProvider : AuthenticationStateProvider
{
    private readonly ITokenStorageService _storage;
    private static readonly ClaimsPrincipal Anonymous = new(new ClaimsIdentity());

    // Allow small clock skew (e.g., 60 seconds)
    private static readonly TimeSpan AllowedClockSkew = TimeSpan.FromSeconds(60);

    public JwtAuthStateProvider(ITokenStorageService storage) => _storage = storage;

    public async Task<string?> GetTokenAsync()
    {
        try
        {
            return await _storage.GetTokenAsync();
        }
        catch
        {
            return null;
        }
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string? token = null;
        try
        {
            token = await _storage.GetTokenAsync();
        }
        catch
        {
            // In MAUI/initialization never throw; fallback to anonymous.
            return new AuthenticationState(Anonymous);
        }

        if (string.IsNullOrWhiteSpace(token))
            return new AuthenticationState(Anonymous);

        // Parse JWT and validate expiration (without signature validation here)
        var principal = TryBuildPrincipalFromJwt(token, out var isExpired);
        if (principal is null || isExpired)
        {
            // If token is invalid/expired, clear storage and return anonymous
            await SafeClearAsync();
            return new AuthenticationState(Anonymous);
        }

        return new AuthenticationState(principal);
    }

    public async Task MarkUserAsAuthenticatedAsync(string token, string? userName = null)
    {
        // Persist token first
        await _storage.SetTokenAsync(token);

        // Build principal from token claims; fallback to optional userName if no Name claim exists
        var principal = TryBuildPrincipalFromJwt(token, out var isExpired)
                        ?? BuildMinimalPrincipal(userName);

        if (isExpired)
        {
            // If expired at the moment of setting, treat as logged out
            await SafeClearAsync();
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(Anonymous)));
            return;
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
    }

    public async Task MarkUserAsLoggedOutAsync()
    {
        await SafeClearAsync();
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(Anonymous)));
    }

    // ---------------- helpers ----------------

    /// <summary>
    /// Parses the JWT and returns a ClaimsPrincipal. No signature validation is performed here.
    /// Returns null if parsing fails. Also outputs expiration status.
    /// </summary>
    private static ClaimsPrincipal? TryBuildPrincipalFromJwt(string token, out bool isExpired)
    {
        isExpired = false;

        try
        {
            var handler = new JwtSecurityTokenHandler();

            // ReadJwtToken does NOT validate signature; it's fine for client-side claims usage.
            var jwt = handler.ReadJwtToken(token);

            // Check expiration using 'ValidTo' (UTC). Allow small clock skew.
            if (jwt.ValidTo != DateTime.MinValue &&
                DateTime.UtcNow - AllowedClockSkew > jwt.ValidTo)
            {
                isExpired = true;
                return null;
            }

            // Build identity using all claims present in the JWT
            // Backend sets: Sub, Jti, NameIdentifier, Name, Email, Role(s), plus custom claims.
            var identity = new ClaimsIdentity(jwt.Claims, authenticationType: "jwt");

            // Ensure Name is present: if not, try common fallbacks (optional)
            if (!identity.HasClaim(c => c.Type == ClaimTypes.Name))
            {
                var name = jwt.Claims.FirstOrDefault(c =>
                               c.Type == "name" ||
                               c.Type == JwtRegisteredClaimNames.Name ||
                               c.Type == JwtRegisteredClaimNames.UniqueName ||
                               c.Type == "preferred_username")?.Value;

                if (!string.IsNullOrWhiteSpace(name))
                    identity.AddClaim(new Claim(ClaimTypes.Name, name));
            }

            // Ensure NameIdentifier from 'sub' if missing (optional)
            if (!identity.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
            {
                var sub = jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
                if (!string.IsNullOrWhiteSpace(sub))
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, sub));
            }



            return new ClaimsPrincipal(identity);
        }
        catch
        {
            // Any parsing/format error -> treat as invalid token
            return null;
        }
    }

    /// <summary>
    /// Minimal principal if token couldn't be parsed but we still want a non-anonymous identity (rare).
    /// </summary>
    private static ClaimsPrincipal BuildMinimalPrincipal(string? userName)
    {
        var claims = new List<Claim>();
        if (!string.IsNullOrWhiteSpace(userName))
            claims.Add(new Claim(ClaimTypes.Name, userName));

        return new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
    }

    private async Task SafeClearAsync()
    {
        try { await _storage.ClearAsync(); } catch { /* ignore */ }
    }
}
