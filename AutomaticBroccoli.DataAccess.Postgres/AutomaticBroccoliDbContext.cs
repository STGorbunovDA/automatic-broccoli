using Microsoft.EntityFrameworkCore;
using static AutomaticBroccoli.DataAccess.Postgres.Entities.AutomaticBroccoliDbContext;

namespace AutomaticBroccoli.DataAccess.Postgres
{
    public partial class AutomaticBroccoliDbContext : DbContext
    {
        public AutomaticBroccoliDbContext(DbContextOptions<AutomaticBroccoliDbContext> options) 
            : base(options)
        {
        }

        public DbSet<OpenLoop> OpenLoops { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AutomaticBroccoliDbContext).Assembly);
            //modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
