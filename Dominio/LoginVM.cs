using System.ComponentModel.DataAnnotations;

namespace Dominio
{
    public class LoginVM
    {
        [Required(ErrorMessage = "El usuario es obligatorio.")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La clave es obligatoria.")]
        public string Contraseña { get; set; }

        public string Nombre { get; set; }

        public string Apellidos { get; set; }
    }
}
