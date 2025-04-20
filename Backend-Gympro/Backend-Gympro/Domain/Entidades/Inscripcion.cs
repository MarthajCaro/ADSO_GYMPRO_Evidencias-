namespace Backend_Gympro.Domain.Entidades
{
    public class Inscripcion
    {
        public int Id { get; set; }
        public DateTime fecha_inscripcion { get; set; }
        public string? estado { get; set; }
        public int id_clase { get; set; }
        public int id_usuario { get; set; }
    }
}
