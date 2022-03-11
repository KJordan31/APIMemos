using Aplicacion.Contenidos;
namespace Aplicacion.Memos
{
    public class MemorandumDTO
    {
        public int Id { get; set; }

        public string Usuario { get; set; }

        public string Asunto { get; set; }

        public string Codigo { get; set; }

        public string Fecha { get; set; }

        public string DestinatarioUsu { get; set; }

        public ContenidoDTO Contenido { get; set; }
    }
}
