using EcritureComptable.Models;
using EcrituresApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcrituresApi.Data
{
    public class ComptaDbContext : DbContext
    {
        public ComptaDbContext(DbContextOptions<ComptaDbContext> options) : base(options)
        {
        }

        // mvt_Mouvements est une table existante, créée par un système externe à ce projet.
        // Seules certaines colonnes sont mappées ci-dessous (les autres ne nous concernent pas).
        // Ne jamais renommer un HasColumnName(...) sans vérifier la colonne SQL réelle correspondante.
        public DbSet<Ecriture> Ecritures { get; set; }

        // Table entièrement gérée par ce projet, mais dont le schéma est défini par
        // Scripts/01_Creation_Table_AuditSuppressions.sql (pas par des migrations EF Core).
        // Les colonnes suivent donc les conventions EF par défaut (nom de propriété = nom de colonne).
        public DbSet<AuditSuppression> AuditSuppressions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ecriture>(entity =>
            {
                entity.ToTable("mvt_Mouvements");
                entity.HasKey(e => e.CleMvt);
                entity.Property(e => e.CleMvt).HasColumnName("clemvt");
                entity.Property(e => e.EtatComptabilisation).HasColumnName("etat_comptabilisation");
            });
        }
    }
}