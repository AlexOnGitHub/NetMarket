using Core.Entities.OrdenCompra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Data.Configuration
{
    public class OrdenCompraConfiguration : IEntityTypeConfiguration<OrdenCompras>
    {
        public void Configure(EntityTypeBuilder<OrdenCompras> builder)
        {
            builder.OwnsOne(o => o.DireccionEnvio, x =>
            {
                x.WithOwner();
            });

            builder.Property(s => s.Status)
                .HasConversion(
                    o => o.ToString(),
                    o => (OrdenStatus)Enum.Parse(typeof(OrdenStatus), o)
                );

            builder.HasMany(o => o.OrdenItems).WithOne().OnDelete(DeleteBehavior.Cascade); //Los items son dependientes de la orden de compra, aquí se configura de tal manera que cuando se elimine la orden de compra los items se eliminaran en cascada.

            builder.Property(o => o.Subtotal)
                .HasColumnType("decimal(18,2)");
        }
    }
}
