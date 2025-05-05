namespace Backend_Gympro.Application.DTOs
{
    public class PagoDto
    {
        public decimal Precio { get; set; }
        public DateTime FechaPago { get; set; }
        public DateTime FechaVigencia { get; set; }
        public int IdUsuario { get; set; }
        public int IdMetodoPago { get; set; }
        public int membresia_id { get; set; }
    }
}
