using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Rent.Models;

namespace Rent.Models;

public partial class RentalDb3Context : DbContext
{
    public RentalDb3Context()
    {
    }

    public RentalDb3Context(DbContextOptions<RentalDb3Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Apartment> Apartments { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Property> Properties { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    //public virtual DbSet<CustomProperty> CustomProperties { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Server=PREET-PC\\SQLEXPRESS2022;Initial Catalog=RentalDB3;User=sa;Password=lasalle;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Apartment>(entity =>
        {
            entity.Property(e => e.ApartmentId).ValueGeneratedNever();
            entity.Property(e => e.RentPrice).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Property).WithMany(p => p.Apartments)
                .HasForeignKey(d => d.PropertyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Apartments_Properties");
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.Property(e => e.AppointmentId).ValueGeneratedNever();
            entity.Property(e => e.AppointmentDate).HasColumnType("datetime");

            entity.HasOne(d => d.Apartment).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ApartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Appointments_Apartments");

            entity.HasOne(d => d.Manager).WithMany(p => p.AppointmentManagers)
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Appointments_Manager");

            entity.HasOne(d => d.Tenant).WithMany(p => p.AppointmentTenants)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Appointments_Tenant");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.Property(e => e.MessageId).ValueGeneratedNever();
            entity.Property(e => e.Content).HasMaxLength(100);
            entity.Property(e => e.SendDate).HasColumnType("datetime");

            entity.HasOne(d => d.Appointment).WithMany(p => p.Messages)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Messages_Appointments");

            entity.HasOne(d => d.Receiver).WithMany(p => p.MessageReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Messages_Receiver");

            entity.HasOne(d => d.Sender).WithMany(p => p.MessageSenders)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Messages_Sender");
        });

        modelBuilder.Entity<Property>(entity =>
        {
            entity.Property(e => e.PropertyId).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.PostalCode).HasMaxLength(50);

            entity.HasOne(d => d.Manager).WithMany(p => p.PropertyManagers)
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Properties_Users1");

            entity.HasOne(d => d.Owner).WithMany(p => p.PropertyOwners)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Properties_Users");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Status");

            entity.Property(e => e.StatusType).HasMaxLength(50);

            entity.HasOne(d => d.Apartment).WithMany()
                .HasForeignKey(d => d.ApartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Status_Apartments");

            entity.HasOne(d => d.Manager).WithMany()
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Status_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);
        });


      
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

public DbSet<Rent.Models.StatusModel> StatusModel { get; set; } = default!;
}
