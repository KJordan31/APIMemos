namespace Aplicacion.Destinatarios
{

    public class TipoDestinatarioDTO
    {
        public int Id_Tipo_Destinatario { get; set; }

        public string Descripcion { get; set; }

        public bool EnUso { get; set; }
        
        public string Usuario { get; set; }
    }
}