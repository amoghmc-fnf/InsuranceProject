using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UserDbService.Data;

public partial class FnfProjectContext : DbContext
{
    public FnfProjectContext()
    {
    }

    public FnfProjectContext(DbContextOptions<FnfProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Illness> Illnesses { get; set; }

    public virtual DbSet<Insured> Insureds { get; set; }

    public virtual DbSet<InsuredIllness> InsuredIllnesses { get; set; }

    public virtual DbSet<PolicyHolder> PolicyHolders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Illness>(entity =>
        {
            entity.HasKey(e => e.IllnessId).HasName("PK__Illness__2BA575BB35CB09EA");

            entity.ToTable("Illness");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Insured>(entity =>
        {
            entity.HasKey(e => e.InsuredId).HasName("PK__Insured__03C4A29B8A870382");

            entity.ToTable("Insured");

            entity.Property(e => e.InsuredId).HasColumnName("InsuredID");
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PolicyHolderId).HasColumnName("PolicyHolderID");

            entity.HasOne(d => d.PolicyHolder).WithMany(p => p.Insureds)
                .HasForeignKey(d => d.PolicyHolderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Insured__PolicyH__4F7CD00D");
        });

        modelBuilder.Entity<InsuredIllness>(entity =>
        {
            entity.HasKey(e => e.InsuredIllnessId).HasName("PK__InsuredI__67B6BD8B1E1A6D91");

            entity.ToTable("InsuredIllness");

            entity.Property(e => e.InsuredIllnessId).HasColumnName("InsuredIllnessID");
            entity.Property(e => e.IllnessId).HasColumnName("IllnessID");
            entity.Property(e => e.InsuredId).HasColumnName("InsuredID");

            entity.HasOne(d => d.Illness).WithMany(p => p.InsuredIllnesses)
                .HasForeignKey(d => d.IllnessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InsuredIl__Illne__5FB337D6");

            entity.HasOne(d => d.Insured).WithMany(p => p.InsuredIllnesses)
                .HasForeignKey(d => d.InsuredId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InsuredIl__Insur__5EBF139D");
        });

        modelBuilder.Entity<PolicyHolder>(entity =>
        {
            entity.HasKey(e => e.PolicyHolderId).HasName("PK__PolicyHo__0549D1CBEF14F08A");

            entity.ToTable("PolicyHolder");

            entity.Property(e => e.PolicyHolderId).HasColumnName("PolicyHolderID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PasswordHash).HasMaxLength(50);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Status).HasDefaultValue(1);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
