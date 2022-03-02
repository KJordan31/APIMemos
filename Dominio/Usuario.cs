using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio
{
    public class Usuario
    {
        public int Id_Usuario { get; set; }

        public string Nombre { get; set; }

        public string Apellidos { get; set; }

        public bool Super_Usuario { get; set; }

        [Required(ErrorMessage = "El correo no puede estar vacio")]
        public string Correo { get; set; }

        public DateTime SistemaFecha { get; set; }

        [Required(ErrorMessage = "La contraseña no puede estar vacia")]
        public string Contraseña { get; set; }

        [Compare("Contraseña", ErrorMessage = "Las contraseñas no coinciden")]
        [NotMapped]
        public string ConfirmarContraseña { get; set; }

        public string Token { get; set; }
        public bool Succeeded { get; set; }
    }
}
