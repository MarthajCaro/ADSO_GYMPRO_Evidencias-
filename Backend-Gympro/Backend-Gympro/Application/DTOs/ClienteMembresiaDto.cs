namespace Backend_Gympro.Application.DTOs
{
    public class ClienteMembresiaDto
    {
        public string? NombreCliente { get; set; }
        public string? Membresia { get; set; }
        public string? DescripcionMembresia { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string? Estado { get; set; } // Activo o Vencido
        public decimal Precio { get; set; } // Precio de la membresía
    }
}
