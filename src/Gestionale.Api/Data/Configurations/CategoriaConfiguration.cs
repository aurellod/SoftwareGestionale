using Gestionale.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestionale.Api.Data.Configurations;

public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("categoria");

        builder.HasKey(c => c.IdCategoria);

        //definizione di vincoli lunghezza e unicità
        builder.Property(c => c.NomeCategoria).HasMaxLength(100);
        builder.HasIndex(c => c.NomeCategoria).IsUnique();
    }
}