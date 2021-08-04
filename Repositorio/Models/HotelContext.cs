using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Repositorio.Models
{
    public partial class HotelContext : DbContext
    {
        public HotelContext()
        {
        }

        public HotelContext(DbContextOptions<HotelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CalificacionCliente> CalificacionCliente { get; set; }
        public virtual DbSet<Foto> Foto { get; set; }
        public virtual DbSet<Hotel> Hotel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Hotel;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CalificacionCliente>(entity =>
            {
                entity.HasKey(e => e.CalificacionID)
                    .HasName("PK__Califica__4CF54ABE82BBF244");

                entity.Property(e => e.Comentario).IsUnicode(false);

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.CalificacionCliente)
                    .HasForeignKey(d => d.HotelID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HotelID");
            });

            modelBuilder.Entity<Foto>(entity =>
            {
                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.Foto)
                    .HasForeignKey(d => d.HotelID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKHotelID");
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.Property(e => e.Eliminado).HasDefaultValueSql("((0))");

                entity.Property(e => e.Nombre).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
