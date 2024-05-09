using Microsoft.EntityFrameworkCore;
using Pronia.Models;

namespace Pronia.DataAccesLayer
{

    public class ProniaContext : DbContext
    {
        public ProniaContext(DbContextOptions<ProniaContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=LAPTOP-ACG39MDK\\SQLEXPRESS;Database=Proniaa;Trusted_Connection=true;TrustServerCertificate=True;");
            base.OnConfiguring(options);
        }
    }
}
