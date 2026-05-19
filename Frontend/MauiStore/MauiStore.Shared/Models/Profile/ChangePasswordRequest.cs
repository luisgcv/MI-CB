namespace MauiStore.Shared.Models.Profile
{
    /// <summary>
    /// Cuerpo del POST api/profile/change-password.
    /// </summary>
    public sealed class ChangePasswordRequest
    {
        /// <summary>Contraseña actual del usuario.</summary>
        public string CurrentPassword { get; set; } = string.Empty;

        /// <summary>Nueva contraseña deseada.</summary>
        public string NewPassword { get; set; } = string.Empty;
    }
}