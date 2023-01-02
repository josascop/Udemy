using Microsoft.EntityFrameworkCore;
using Udemy.Models;

namespace Udemy.Data {
    public class UdemyContext : DbContext {
        public UdemyContext(DbContextOptions<UdemyContext> options)
            : base(options) {
        }

        public DbSet<Departamento> Departamento { get; set; } = default!;
        public DbSet<Vendedor> Vendedor { get; set; } = default!;
        public DbSet<RegistroDeVenda> RegistroDeVenda { get; set; } = default!;
    }
}
