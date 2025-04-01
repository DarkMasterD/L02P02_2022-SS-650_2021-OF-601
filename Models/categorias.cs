using System.ComponentModel.DataAnnotations;

namespace L02P02_2022_SS_650_2021_OF_601.Models
{
    public class categorias
    {
        [Key]
        public int id { get; set; }
        public string categoria { get; set; }
    }
}
