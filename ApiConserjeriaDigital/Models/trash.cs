using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiConserjeriaDigital.Models
{
    public class trash 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RUT { get; set; }
        public string ?pass {  get; set; }
    }
}
