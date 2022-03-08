using System.ComponentModel.DataAnnotations;

namespace Dominio
{
    public class LoginVM
    {
      
        public string Correo { get; set; }

      
        public string Contraseña { get; set; }

        public string Nombre { get; set; }

        public string Apellidos { get; set; }
    }
}
