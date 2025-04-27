namespace Backend_Gympro.Domain.Entidades
{
    public class Membresia
    {
        public int id { get; set; }
        public decimal Precio { get; set; }
        public int duracion_membresia_en_meses { get; set; }
        public string? descripcion { get; set; }
        public int id_tipo_membresia { get; set; }
        public TipoMembresia TipoMembresia { get; set; }
    }
}
