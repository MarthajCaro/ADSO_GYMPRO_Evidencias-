namespace Backend_Gympro.Domain.Entidades
{
    public class ProgresoFisico
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public decimal Peso { get; set; }           // Peso en kilogramos
        public decimal? MedidaCintura { get; set; } // Opcional
        public decimal? MedidaPecho { get; set; }   // Opcional
        public string? FotoProgresoUrl { get; set; } // Ruta de la imagen
        public DateTime FechaRegistro { get; set; }         // Fecha del progreso
        public string? Observaciones { get; set; }
        // Propiedad de navegación
        public virtual Usuarios Usuario { get; set; }
    }
}
