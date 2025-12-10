using BackEnd_G_P.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_G_P.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }
    }
}
