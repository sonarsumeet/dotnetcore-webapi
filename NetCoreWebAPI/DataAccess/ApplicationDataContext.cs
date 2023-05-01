using Microsoft.EntityFrameworkCore;
using NetCoreWebAPI.Models;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebAPI.DataAccess
{
    public class ApplicationDataContext: DbContext
    {
        public ApplicationDataContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Products> Products { get; set; }
    }
}
