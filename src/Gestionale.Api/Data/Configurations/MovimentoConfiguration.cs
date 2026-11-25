using Gestionale.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestionale.Api.Data.Configurations;

public class MovimentoConfiguration : IEntityTypeConfiguration<Movimento>
{
    public void Configure(EntityTypeBuilder<Movimento> builder)
    {
        builder.ToTable("movimento", t =>
        {
            t.HasCheckConstraint("ck_movimento_quantita", "quantita > 0");
            t.HasCheckConstraint("ck_movimento_origine_destinazione",
                "id_ubicazione_origine <> id_ubicazione_destinazione");

            // R5: quali ubicazioni servono per ogni tipo di movimento
            t.HasCheckConstraint("ck_movimento_direzione",
                "(tipo_movimento = 'Carico' AND id_ubicazione_origine IS NULL AND id_ubicazione_destinazione IS NOT NULL) OR " +
                "(tipo_movimento = 'Uso' AND id_ubicazione_origine IS NOT NULL AND id_ubicazione_destinazione IS NULL) OR " +
                "(tipo_movimento = 'Trasferimento' AND id_ubicazione_origine IS NOT NULL AND id_ubicazione_destinazione IS NOT NULL) OR " +
                "(tipo_movimento = 'Correzione' AND (id_ubicazione_origine IS NULL) <> (id_ubicazione_destinazione IS NULL))");
        });

        builder.HasKey(m => m.IdMovimento);

        builder.Property(m => m.TipoMovimento).HasConversion<string>().HasMaxLength(20);
        builder.Property(m => m.Quantita).HasPrecision(10, 2);
        builder.Property(m => m.DataMovimento).HasDefaultValueSql("now()");
        builder.Property(m => m.Note).HasMaxLength(500);

        builder.HasOne(m => m.Articolo)
            .WithMany(a => a.MovimentiArticolo)
            .HasForeignKey(m => m.IdArticolo)
            .OnDelete(DeleteBehavior.Restrict);

        // Due relazioni verso Ubicazione: senza lista dall'altra parte, quindi WithMany() vuoto
        builder.HasOne(m => m.UbicazioneOrigine)
            .WithMany()
            .HasForeignKey(m => m.IdUbicazioneOrigine)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.UbicazioneDestinazione)
            .WithMany()
            .HasForeignKey(m => m.IdUbicazioneDestinazione)
            .OnDelete(DeleteBehavior.Restrict);
    }
}