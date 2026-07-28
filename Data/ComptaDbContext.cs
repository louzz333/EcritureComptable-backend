using EcritureComptable.Models;
using EcrituresApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace EcrituresApi.Data
{
    public class ComptaDbContext : DbContext
    {
        public ComptaDbContext(DbContextOptions<ComptaDbContext> options) : base(options)
        {
        }

        public DbSet<Ecriture> Ecritures { get; set; }

        public DbSet<AuditSuppression> AuditSuppressions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ecriture>(entity =>
            {
                entity.ToTable("mvt_Mouvements");

                entity.HasKey(e => e.CleMvt);

                entity.Property(e => e.CleMvt)
                      .HasColumnName("clemvt");

            }); //effectuer lecture avec cle primaire


        }
    }
}
