namespace Backend_Gympro.Domain.Entidades
{
    public class Persona
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? apellido { get; set; }
        public char genero { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public string? telefono { get; set; }
        public string? correo { get; set; }
        public int id_municipio { get; set; }
        public string? direccion { get; set; }
        public string? zip { get; set; }
    }
}
