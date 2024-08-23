using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AdminDbService.Data;

public partial class FnfProjectContext : DbContext
{
    public FnfProjectContext()
    {
    }

    public FnfProjectContext(DbContextOptions<FnfProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Blacklist> Blacklists { get; set; }

    public virtual DbSet<EmailRecord> EmailRecords { get; set; }

    public virtual DbSet<Hospital> Hospitals { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Admin__719FE4E8CF8C1A0A");

            entity.ToTable("Admin");

            entity.Property(e => e.AdminId).HasColumnName("AdminID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Blacklist>(entity =>
        {
            entity.HasKey(e => e.BlacklistId).HasName("PK__Blacklis__AFDBF438B10FFF10");

            entity.ToTable("Blacklist");

            entity.Property(e => e.BlacklistId).HasColumnName("BlacklistID");
            entity.Property(e => e.AdminId).HasColumnName("AdminID");
            entity.Property(e => e.PolicyHolderId).HasColumnName("PolicyHolderID");
            entity.Property(e => e.Reason).HasColumnType("text");

            entity.HasOne(d => d.Admin).WithMany(p => p.Blacklists)
                .HasForeignKey(d => d.AdminId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Blacklist__Admin__4AB81AF0");
        });

        modelBuilder.Entity<EmailRecord>(entity =>
        {
            entity.HasKey(e => e.EmailRecordId).HasName("PK__EmailRec__DB8BE325450EF8B6");

            entity.ToTable("EmailRecord");

            entity.Property(e => e.EmailRecordId).HasColumnName("EmailRecordID");
            entity.Property(e => e.FromEmail).HasMaxLength(255);
            entity.Property(e => e.Subject).HasMaxLength(512);
            entity.Property(e => e.ToEmail).HasMaxLength(255);
        });

        modelBuilder.Entity<Hospital>(entity =>
        {
            entity.HasKey(e => e.HospitalId).HasName("PK__Hospital__38C2E58FD26DE612");

            entity.ToTable("Hospital");

            entity.Property(e => e.HospitalId).HasColumnName("HospitalID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A388B4CFDA5");

            entity.ToTable("Payment");

            entity.Property(e => e.InsuredPolicyId).HasColumnName("InsuredPolicyID");
            entity.Property(e => e.PaymentAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PolicyHolderId).HasColumnName("PolicyHolderID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
