using Gestionale.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gestionale.Api.Data;

public class GestionaleDbContext(DbContextOptions<GestionaleDbContext> options) : DbContext(options)
{
    // Una DbSet per tabella: da qui partono le query (es. db.Articoli.Where(...))
    public DbSet<Categoria> Categorie => Set<Categoria>();
    public DbSet<Articolo> Articoli => Set<Articolo>();
    public DbSet<Ubicazione> Ubicazioni => Set<Ubicazione>();
    public DbSet<Giacenza> Giacenze => Set<Giacenza>();
    public DbSet<Movimento> Movimenti => Set<Movimento>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Applica tutte le classi IEntityTypeConfiguration della cartella Data/Configurations
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GestionaleDbContext).Assembly);
    }
}
