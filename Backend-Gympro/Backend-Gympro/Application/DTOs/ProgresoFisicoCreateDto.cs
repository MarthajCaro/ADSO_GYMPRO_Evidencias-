namespace Backend_Gympro.Application.DTOs
{
    public class ProgresoFisicoCreateDto
    {
        public int UsuarioId { get; set; }
        public decimal Peso { get; set; }
        public decimal? MedidaCintura { get; set; }
        public decimal? MedidaPecho { get; set; }
        public DateTime Fecha { get; set; }
        public string? Observaciones { get; set; }

        public IFormFile Foto { get; set; } // Aquí llega la imagen
    }
}
