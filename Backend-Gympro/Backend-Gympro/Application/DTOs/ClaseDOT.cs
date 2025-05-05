namespace Backend_Gympro.Application.DTOs
{
    public class ClaseDOT
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int duracion_en_minutos { get; set; }
        public string? descripcion { get; set; }
        public int id_usuario { get; set; }
        public string? Dia { get; set; } // Día de la clase (Lunes, Martes, etc.)
        public string? Hora { get; set; } // Hora de inicio de la clase
        public bool estado { get; set; }
    }
}
