namespace Backend_Gympro.Application.DTOs
{
    public class ProgresoComparadoDto
    {
        public int UsuarioId { get; set; }
        public string? NombreUsuario { get; set; }
        public decimal PesoActual { get; set; }
        public decimal? PesoAnterior { get; set; }
        public decimal? VariacionPeso { get; set; }
        public decimal? VariacionMedidaCintura { get; set; }
        public decimal? VariacionMedidaPecho { get; set; }
        public DateTime UltimaFechaRegistro { get; set; }
        public string? Foto { get; set; }
    }
}
