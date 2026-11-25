using Gestionale.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestionale.Api.Data.Configurations;

public class UbicazioneConfiguration : IEntityTypeConfiguration<Ubicazione>
{
    public void Configure(EntityTypeBuilder<Ubicazione> builder)
    {
        builder.ToTable("ubicazione");
        builder.HasKey(u => u.IdUbicazione);

        builder.Property(u => u.NomeUbicazione).HasMaxLength(50);
        builder.HasIndex(u => u.NomeUbicazione).IsUnique();

        // L'enum si salva come testo ("Furgone", "Magazzino"), non come numero
        builder.Property(u => u.TipoUbicazione).HasConversion<string>().HasMaxLength(20);

        builder.Property(u => u.Targa).HasMaxLength(10);
    }
}