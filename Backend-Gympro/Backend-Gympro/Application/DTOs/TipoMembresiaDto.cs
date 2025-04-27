namespace Backend_Gympro.Application.DTOs
{
    public class TipoMembresiaDto
    {
        public int Id { get; set; }
        public decimal Precio { get; set; }
        public int Duracion_Membresia_En_Meses { get; set; }
        public string Descripcion { get; set; }
        public int Id_Tipo_Membresia { get; set; }
    }
}
