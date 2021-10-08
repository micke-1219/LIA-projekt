using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebAPI.Entities
{
    public partial class LIADbContext : DbContext
    {
        public LIADbContext()
        {
        }

        public LIADbContext(DbContextOptions<LIADbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApartmentContract> ApartmentContracts { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<BulletinMessage> BulletinMessages { get; set; }
        public virtual DbSet<ErrorReport> ErrorReports { get; set; }
        public virtual DbSet<LaundryBooking> LaundryBookings { get; set; }
        public virtual DbSet<Maintenance> Maintenances { get; set; }
        public virtual DbSet<ParkingContract> ParkingContracts { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Micke\\Documents\\LIA-db.mdf;Integrated Security=True;Connect Timeout=30");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ApartmentContract>(entity =>
            {
                entity.Property(e => e.ApartmentNumber)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.ZipCode)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ApartmentContracts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Apartment__UserI__267ABA7A");
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.Property(e => e.Month)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Year)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Bills__UserId__2A4B4B5E");
            });

            modelBuilder.Entity<BulletinMessage>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.BulletinMessages)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BulletinM__UserI__2D27B809");
            });

            modelBuilder.Entity<ErrorReport>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.ErrorReports)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ErrorRepo__UserI__300424B4");
            });

            modelBuilder.Entity<LaundryBooking>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.LaundryBookings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LaundryBo__UserI__33D4B598");
            });

            modelBuilder.Entity<Maintenance>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Maintenances)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Maintenan__UserI__36B12243");
            });

            modelBuilder.Entity<ParkingContract>(entity =>
            {
                entity.Property(e => e.LotNumber)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.ZipCode)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ParkingContracts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ParkingCo__UserI__3A81B327");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.PersonalIdentityNumber).IsUnicode(false);

                entity.Property(e => e.PhoneNumber).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
