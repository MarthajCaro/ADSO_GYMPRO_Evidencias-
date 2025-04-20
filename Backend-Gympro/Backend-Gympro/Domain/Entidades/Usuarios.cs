namespace Backend_Gympro.Domain.Entidades
{
    public class Usuarios
    {
        public int id { get; set; }
        public string? usuario { get; set; }
        public string? contrasena { get; set; }
        public int PersonaId { get; set; }
        public int RolId { get; set; }
        public Persona Persona { get; set; }
    }
}
