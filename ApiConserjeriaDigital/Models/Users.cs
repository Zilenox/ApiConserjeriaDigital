using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiConserjeriaDigital.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int RUT { get; set; }
        public string Nombre { get; set; }
    }

    public class Residente : User
    {
        public int NumeroDepto { get; set; }
        public int Casilla { get; set; }
    }

    public class Conserje : User
    {

    }

    public class Administrador : User
    {

    }
}
