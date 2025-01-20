using Domain.Categorias;
using Domain.Categorias.ObjectValues;
using Domain.Usuarios;
using Domain.Usuarios.ObjectValues;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal class CategoriaConfiguracion : IEntityTypeConfiguration<Categoria>
{
    public void Configure(
     EntityTypeBuilder<Categoria> builder
     )
    {
        builder.ToTable("Categorias");
        builder.HasKey(categoria => categoria.Id);

        builder.Property(categoria => categoria.Id).HasConversion(builder => builder!.Value, value => new CategoriaId(value));

        builder.Property(categoria => categoria.Nombre)
            .HasMaxLength(200)
            .HasConversion(nombre => nombre!.Value, value => new NombreCategoria(value));

        builder.Property(categoria => categoria.UsuarioId)
            .HasMaxLength(200)
            .HasConversion(nombre => nombre!.Value, value => new UsuarioId(value));

        builder.HasOne<Usuario>()
          .WithMany()
          .HasForeignKey(alquiler => alquiler.UsuarioId);

    }
}
