using System.ComponentModel.DataAnnotations;

namespace L02P02_2022_SS_650_2021_OF_601.Models
{
    public class pedido_encabezado
    {
        [Key]
        public int id { get; set; }
        public int id_cliente { get; set; }
        public int cantidad_libros { get; set; }
        public double total { get; set; }
        public char estado {  get; set; }
    }
}
