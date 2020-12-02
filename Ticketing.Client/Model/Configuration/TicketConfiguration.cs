using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ticketing.Client.Model.Configuration
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            //var ticketModel = modelBuilder.Entity<Ticket>(); // non ci serve più perché adesso c'è builder che viene passato

            builder
                .HasKey(t => t.Id); // non è necessario se si rispettano le convenzioni

            builder
                .Property(t => t.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(t => t.Description)
                .HasMaxLength(500);

            builder
                .Property(t => t.Category)
                .IsRequired();

            builder
                .Property(t => t.Requestor)
                .IsRequired()
                .HasMaxLength(50);

            builder                 // qui viene gestita la relazione uno a molti partendo da Ticket (ma si può fare anche viceversa, però di solito si parte da quello che è 1 nella relazione 1:n)
                .HasMany(t => t.Notes)
                .WithOne(n => n.Ticket)
                .HasForeignKey(n => n.TicketId)
                .HasConstraintName("FK_Ticket_Notes")
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(t => t.RowVersion)
                .IsRowVersion();
        }
    }
}
