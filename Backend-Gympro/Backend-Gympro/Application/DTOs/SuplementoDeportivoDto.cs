namespace Backend_Gympro.Application.DTOs
{
    public class SuplementoDeportivoDto
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? tipo { get; set; }
        public string? descripcion { get; set; }
        public decimal precio { get; set; }
        public int id_usuario { get; set; }
        public bool estado { get; set; } = true;
        public int stock { get; set; }
        public IFormFile Foto { get; set; }
    }
}
