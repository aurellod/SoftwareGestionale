using Gestionale.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestionale.Api.Data.Configurations;

public class GiacenzaConfiguration : IEntityTypeConfiguration<Giacenza>
{
    public void Configure(EntityTypeBuilder<Giacenza> builder)
    {
        // I vincoli CHECK usano i nomi delle colonne nel database (snake_case)
        builder.ToTable("giacenza", t =>
        {
            t.HasCheckConstraint("ck_giacenza_quantita", "quantita >= 0");
            t.HasCheckConstraint("ck_giacenza_soglia_minima", "soglia_minima >= 0");
        });

        // Chiave composta: un articolo compare al massimo una volta per ubicazione
        builder.HasKey(g => new { g.IdArticolo, g.IdUbicazione });

        builder.Property(g => g.Quantita).HasPrecision(10, 2);
        builder.Property(g => g.SogliaMinima).HasPrecision(10, 2);
        builder.Property(g => g.StatoRiordino).HasConversion<string>().HasMaxLength(20);
        builder.Property(g => g.DataAggiornamento).HasDefaultValueSql("now()");

        builder.HasOne(g => g.Articolo)
            .WithMany(a => a.GiacenzeArticolo)
            .HasForeignKey(g => g.IdArticolo)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(g => g.Ubicazione)
            .WithMany(u => u.GiacenzeUbicazione)
            .HasForeignKey(g => g.IdUbicazione)
            .OnDelete(DeleteBehavior.Restrict);
    }
}