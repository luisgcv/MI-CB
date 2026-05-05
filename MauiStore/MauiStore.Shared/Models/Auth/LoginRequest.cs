namespace MauiStore.Shared.Models.Auth
{
    /// <summary>
    /// Cuerpo del POST de autenticación.
    /// Ajusta los nombres si tu API usa otros ("email", etc.).
    /// </summary>
    public sealed class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}