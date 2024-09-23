using Domain.Usuarios;
using Domain.Usuarios.ObjectValues;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal class UsuarioConfiguracion : IEntityTypeConfiguration<Usuario>
{
    public void Configure(
       EntityTypeBuilder<Usuario> builder
       )
    {
        builder.ToTable("Usuarios");
        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id).HasConversion(builder => builder!.Value, value => new UsuarioId(value));

        builder.Property(user => user.Nombre)
            .HasMaxLength(200)
            .HasConversion(nombre => nombre!.Value, value => new Nombre(value));

        builder.Property(user => user.CorreoElectronico)
        .HasMaxLength(400)
        .HasConversion(email => email!.Value, value => new CorreoElectronico(value));

        builder.Property(user => user.Contrasena)
            .HasMaxLength(2000)
            .HasConversion(pass => pass!.Value, value => new Contrasena(value));

        builder.Property(user => user.IntentosLogin)
            .HasMaxLength(3)
            .HasConversion(pass => pass!.Value, value => new IntentosLogin(value));

        builder.Property(user => user.TokenCorreoConfirmar)
        .HasMaxLength(5)
        .HasConversion(pass => pass!.Value, value => new TokenCorreo(value));

        builder.HasIndex(user => user.CorreoElectronico).IsUnique();
    }
}
