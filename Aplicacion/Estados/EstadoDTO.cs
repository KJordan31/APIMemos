namespace  Aplicacion.Estados
{

    public class EstadoDTO
    {
        public int Id_Estado {get; set;}

        public string Descripcion { get; set; }

        public bool EnUso { get; set; }
        
        public string Usuario { get; set; }
    }
    
}