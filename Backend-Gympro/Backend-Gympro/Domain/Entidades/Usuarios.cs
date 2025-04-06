namespace Backend_Gympro.Domain.Entidades
{
    public class Usuarios
    {
        public int id { get; set; }
        public string? usuario { get; set; }
        public string? correo { get; set; }
        public string? contrasena { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public string? edad { get; set; }
        public int id_persona { get; set; }
        public int id_rol { get; set; }
    }
}
