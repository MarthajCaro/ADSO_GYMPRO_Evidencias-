namespace Backend_Gympro.Domain.Entidades
{
    public class Usuarios
    {
        public int id { get; set; }
        public string? usuario { get; set; }
        public string? contrasena { get; set; }
        public int id_persona { get; set; }
        public int id_rol { get; set; }
    }
}
