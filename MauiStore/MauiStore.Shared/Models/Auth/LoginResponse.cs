using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiStore.Shared.Models.Auth
{
    /// <summary>
    /// Respuesta del endpoint de login que devuelve un JWT.
    /// </summary>
    public sealed class LoginResponse
    {
        /// <summary>JWT emitido por el API.</summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>Nombre de usuario (opcional según tu API).</summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>Roles incluidos (opcional).</summary>
        public string[] Roles { get; set; } = System.Array.Empty<string>();
    }
}