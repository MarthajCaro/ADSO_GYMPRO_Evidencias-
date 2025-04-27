namespace Backend_Gympro.Application.DTOs
{
    public class MembresiaDto
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int DuracionMeses { get; set; }
        public string? TipoMembresiaNombre { get; set; }
        public string? Estado { get; set; }
    }
}
