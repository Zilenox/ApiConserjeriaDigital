using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiConserjeriaDigital.Models
{
    public class Correspondencia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string tipo { get; set; } = "Carta";
        public required int Destinatario { get; set; }

    }
}
