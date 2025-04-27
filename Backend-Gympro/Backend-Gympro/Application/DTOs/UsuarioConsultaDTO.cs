namespace Backend_Gympro.Application.DTOs
{
    public class UsuarioConsultaDTO
    {
        public int Id { get; set; }
        public string? NombreCompleto { get; set; }
        public int Edad { get; set; }
        public string? Correo { get; set; }
        public string? Usuario { get; set; }
        public string? Rol { get; set; }
        public bool Estado { get; set; }
    }
}
