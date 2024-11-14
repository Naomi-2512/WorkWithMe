using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Approval> Approvals { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Position> Positions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("Users")
                .HasKey(u => u.UserId);

            modelBuilder.Entity<User>()
                .Property(u => u.UserId)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Fullname)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Mobile)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Country)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.City)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.ProfilePicture)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasMaxLength(255)
                .HasDefaultValue("assistant");

            modelBuilder.Entity<User>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Job>()
                .ToTable("Jobs")
                .HasKey(j => j.JobId);

            modelBuilder.Entity<Job>()
                .Property(j => j.JobId)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Job>()
                .Property(j => j.UserId)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Job>()
                .Property(j => j.Title)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Job>()
                .Property(j => j.CompanyName)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Job>()
                .Property(j => j.JobPositions)
                .IsRequired();

            modelBuilder.Entity<Job>()
                .Property(j => j.Category)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Job>()
                .Property(j => j.Country)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Job>()
                .Property(j => j.City)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Job>()
                .Property(j => j.Description)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Job>()
                .Property(j => j.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Job>()
                .Property(j => j.IsActivated)
                .HasDefaultValue(false);

            modelBuilder.Entity<Job>()
                .HasOne(j => j.User)
                .WithMany(u => u.Jobs)
                .HasForeignKey(j => j.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payment>()
                .ToTable("Payments")
                .HasKey(p => p.PaymentId);

            modelBuilder.Entity<Payment>()
                .Property(p => p.PaymentId)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Payment>()
                .Property(p => p.JobId)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Payment>()
                .Property(p => p.UserId)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Payment>()
                .Property(p => p.IsVerified)
                .HasDefaultValue(true);

            modelBuilder.Entity<Payment>()
                .Property(p => p.Price)
                .IsRequired();

            modelBuilder.Entity<Payment>()
                .Property(p => p.AdditionalPrice)
                .HasDefaultValue(0);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.User)
                .WithMany(u => u.Payments)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Job)
                .WithMany(j => j.Payments)
                .HasForeignKey(p => p.JobId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Position>()
                .ToTable("JobPositions")
                .HasKey(p => p.PositionId);

            modelBuilder.Entity<Position>()
                .Property(p => p.PositionId)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Position>()
                .Property(p => p.JobId)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Position>()
                .Property(p => p.Price)
                .IsRequired();

            modelBuilder.Entity<Position>()
                .Property(p => p.Title)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Position>()
                .Property(p => p.RequiredUsers)
                .IsRequired();

            modelBuilder.Entity<Position>()
                .Property(p => p.TimePeriod)
                .HasMaxLength(255)
                .HasDefaultValue("indefinitely");

            modelBuilder.Entity<Position>()
                .HasOne(p => p.Job)
                .WithMany(j => j.Positions)
                .HasForeignKey(p => p.JobId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Approval>()
                .ToTable("Approvals")
                .HasKey(a => a.ApprovalId);

            modelBuilder.Entity<Approval>()
                .Property(a => a.ApprovalId)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Approval>()
                .Property(a => a.JobId)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Approval>()
                .Property(a => a.OwnerId)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Approval>()
                .Property(a => a.ApplierId)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Approval>()
                .Property(a => a.IsApproved)
                .HasDefaultValue(false);

            modelBuilder.Entity<Approval>()
                .Property(a => a.IsRejected)
                .HasDefaultValue(false);

            modelBuilder.Entity<Approval>()
                .Property(a => a.PositionApplied)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Approval>()
                .HasOne(a => a.Job)
                .WithMany(j => j.Approvals)
                .HasForeignKey(a => a.JobId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Approval>()
                .HasOne(a => a.Owner)
                .WithMany(u => u.Approvals)
                .HasForeignKey(a => a.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Approval>()
                .HasOne(a => a.Applier)
                .WithMany()
                .HasForeignKey(a => a.ApplierId)
                .OnDelete(DeleteBehavior.NoAction);
        }


    }
}
