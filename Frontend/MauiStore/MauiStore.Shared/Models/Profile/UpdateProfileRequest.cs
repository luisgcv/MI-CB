namespace MauiStore.Shared.Models.Profile
{
    /// <summary>
    /// Cuerpo del POST api/profile para actualizar datos editables del usuario.
    /// </summary>
    public sealed class UpdateProfileRequest
    {
        /// <summary>Nombre completo del usuario.</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Teléfono del usuario.</summary>
        public string Phone { get; set; } = string.Empty;
    }
}