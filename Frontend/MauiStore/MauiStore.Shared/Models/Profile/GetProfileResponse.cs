namespace MauiStore.Shared.Models.Profile
{
    /// <summary>
    /// Respuesta del endpoint GET api/profile.
    /// Contiene la información del usuario autenticado
    /// combinada desde varias tablas de la BD.
    /// </summary>
    public sealed class GetProfileResponse
    {
        /// <summary>Cédula del usuario (solo lectura, viene del JWT).</summary>
        public string IdentificationNumber { get; set; } = string.Empty;

        /// <summary>Nombre completo del usuario (TBL_PROV_USUARIOS_INFORMACION.NOMBRE).</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Teléfono del usuario (TBL_PROV_USUARIOS_INFORMACION.TELEFONO).</summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>Correo de notificación (TBL_PROV_PROVEEDORES_DEPARTAMENTOS_USUARIO.CORREO_NOTIFICACION).</summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>Nombre de la empresa proveedora (TBL_PROV_PROVEEDORES.NOMBRE_PROVEEDOR).</summary>
        public string Company { get; set; } = string.Empty;

        /// <summary>Nombre del departamento (TBL_PROV_DEPARTAMENTOS.NOMBRE_DEPARTAMENTO).</summary>
        public string Department { get; set; } = string.Empty;

        /// <summary>Puesto o rol del usuario (TBL_PROV_PROVEEDORES_DEPARTAMENTOS_USUARIO.PUESTO).</summary>
        public string Position { get; set; } = string.Empty;
    }
}