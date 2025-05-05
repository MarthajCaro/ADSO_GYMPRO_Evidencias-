using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Gympro.Domain.Entidades
{
    public class Pago
    {
        public int id { get; set; }
        public decimal precio { get; set; }
        public DateTime fecha_pago { get; set; }
        public DateTime fecha_vigencia { get; set; }
        public int id_usuario { get; set; }
        public int id_metodo_pago { get; set; }

        [Column("membresia_id")]
        public int MembresiaId { get; set; }
        public Membresia Membresia { get; set; }
    }
}
