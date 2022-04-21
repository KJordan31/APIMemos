namespace Aplicacion.Usuarios
{
    public class UsuarioDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellidos { get; set; }

        public string Correo { get; set; }

        public string Contraseña { get; set; }

        public string Tipo { get; set; }

        public bool Super_Usuario { get; set; }
    }
}
