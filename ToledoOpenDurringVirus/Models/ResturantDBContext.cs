using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ToledoOpenDurringVirus.Models
{
    public partial class ResturantDBContext : DbContext
    {
        public ResturantDBContext()
        {
        }

        public ResturantDBContext(DbContextOptions<ResturantDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AreaLookupTb> AreaLookupTb { get; set; }
        public virtual DbSet<LocationTb> LocationTb { get; set; }
        public virtual DbSet<OpenHoursTb> OpenHoursTb { get; set; }
        public virtual DbSet<TypeLookupTb> TypeLookupTb { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AreaLookupTb>(entity =>
            {
                entity.HasKey(e => e.AreaId);

                entity.ToTable("AreaLookupTB");

                entity.Property(e => e.AreaId).HasColumnName("AreaID");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<LocationTb>(entity =>
            {
                entity.HasKey(e => e.Lid);

                entity.ToTable("LocationTB");

                entity.Property(e => e.Lid)
                    .HasColumnName("LID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Address2).HasMaxLength(50);

                entity.Property(e => e.AreaId).HasColumnName("AreaID");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100);

                entity.Property(e => e.FaceBookId)
                    .HasColumnName("FaceBookID")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.TwitterId)
                    .HasColumnName("TwitterID")
                    .HasMaxLength(50);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.WebSiteUrl)
                    .HasColumnName("WebSiteURL")
                    .HasMaxLength(50);

                entity.Property(e => e.Zipcode).HasMaxLength(10);

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.LocationTb)
                    .HasForeignKey(d => d.AreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LocationTB_AreaLookupTB");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.LocationTb)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LocationTB_TypeLookupTB");
            });

            modelBuilder.Entity<OpenHoursTb>(entity =>
            {
                entity.HasKey(e => e.Hid);

                entity.ToTable("OpenHoursTB");

                entity.Property(e => e.Hid).HasColumnName("HID");

                entity.Property(e => e.HourClose)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.HourOpen)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.Lid).HasColumnName("LID");

                entity.HasOne(d => d.L)
                    .WithMany(p => p.OpenHoursTb)
                    .HasForeignKey(d => d.Lid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OpenHoursTB_LocationTB");
            });

            modelBuilder.Entity<TypeLookupTb>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.ToTable("TypeLookupTB");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
