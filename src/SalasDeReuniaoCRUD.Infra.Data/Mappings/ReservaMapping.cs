using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalasDeReuniaoCRUD.Domain.Reservas;
using SalasDeReuniaoCRUD.Infra.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalasDeReuniaoCRUD.Infra.Data.Mappings
{
    public class ReservaMapping : EntityTypeConfiguration<Reserva>
    {
        public override void Configure(EntityTypeBuilder<Reserva> builder)
        {
            builder.ToTable("Reservas");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Titulo)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(r => r.Responsavel)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.DataInicio)
                .IsRequired();

            builder.Property(r => r.DataFim)
                .IsRequired();

            builder.Property(r => r.ParticipantesPrevistos)
                .IsRequired();

            builder.Property(r => r.ValorHora)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(r => r.Desconto)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(r => r.ValorTotal)
                .HasColumnType("decimal(18,2)")
                .IsRequired();



        }
    }
}
