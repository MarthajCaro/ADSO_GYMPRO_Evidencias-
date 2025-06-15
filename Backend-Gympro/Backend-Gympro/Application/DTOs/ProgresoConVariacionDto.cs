namespace Backend_Gympro.Application.DTOs
{
    public class ProgresoConVariacionDto
    {
        public int Id { get; set; }
        public double Peso { get; set; }
        public double MedidaCintura { get; set; }
        public double MedidaPecho { get; set; }
        public string? Observaciones { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string? UrlFoto { get; set; }

        public double VariacionPeso { get; set; }
        public double VariacionCintura { get; set; }
        public double VariacionPecho { get; set; }
    }
}
