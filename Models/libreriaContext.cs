using Microsoft.EntityFrameworkCore;

namespace L02P02_2022_SS_650_2021_OF_601.Models
{
    public class libreriaContext : DbContext
    {
        public libreriaContext(DbContextOptions options) : base(options)
        {

        }
    }
}
