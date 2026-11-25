using Gestionale.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestionale.Api.Data.Configurations;

public class ArticoloConfiguration : IEntityTypeConfiguration<Articolo>
{
    public void Configure(EntityTypeBuilder<Articolo> builder)
    {
        builder.ToTable("articolo");
        builder.HasKey(a => a.IdArticolo);

        builder.Property(a => a.NomeArticolo).HasMaxLength(150);
        builder.Property(a => a.UnitaMisura).HasMaxLength(10).HasDefaultValue("pz");
        builder.Property(a => a.CodiceArticolo).HasMaxLength(50);
        builder.Property(a => a.DataCreazione).HasDefaultValueSql("now()");
        // Attivo: nessun default nel database. Con un default "true" EF scambierebbe
        // un false voluto per "valore non impostato" e salverebbe true.

        // Articolo -> Categoria: una categoria con articoli non si puo' eliminare
        builder.HasOne(a => a.Categoria)
            .WithMany(c => c.ArticoliCategoria)
            .HasForeignKey(a => a.IdCategoria)
            .OnDelete(DeleteBehavior.Restrict);
    }
}