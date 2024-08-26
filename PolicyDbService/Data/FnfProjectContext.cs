using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PolicyDbService.Data;

public partial class FnfProjectContext : DbContext
{
    public FnfProjectContext()
    {
    }

    public FnfProjectContext(DbContextOptions<FnfProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Claim> Claims { get; set; }

    public virtual DbSet<InsuranceType> InsuranceTypes { get; set; }

    public virtual DbSet<InsuredPolicy> InsuredPolicies { get; set; }

    public virtual DbSet<Policy> Policies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Claim>(entity =>
        {
            entity.HasKey(e => e.ClaimId).HasName("PK__Claim__EF2E13BB762876EB");

            entity.ToTable("Claim");

            entity.Property(e => e.ClaimId).HasColumnName("ClaimID");
            entity.Property(e => e.ClaimAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ClaimStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DispenseAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.DocumentPath).HasColumnType("text");
            entity.Property(e => e.DocumentType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HospitalId).HasColumnName("HospitalID");
            entity.Property(e => e.InsuredPolicyId).HasColumnName("InsuredPolicyID");
            entity.Property(e => e.PolicyHolderId).HasColumnName("PolicyHolderID");

            entity.HasOne(d => d.InsuredPolicy).WithMany(p => p.Claims)
                .HasForeignKey(d => d.InsuredPolicyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Claim__InsuredPo__4D94879B");
        });

        modelBuilder.Entity<InsuranceType>(entity =>
        {
            entity.HasKey(e => e.InsuranceId).HasName("PK__Insuranc__74231BC4FDC83509");

            entity.ToTable("InsuranceType");

            entity.Property(e => e.InsuranceId).HasColumnName("InsuranceID");
            entity.Property(e => e.BaseRate).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<InsuredPolicy>(entity =>
        {
            entity.HasKey(e => e.InsuredPolicyId).HasName("PK__InsuredP__72634910997B9CEC");

            entity.ToTable("InsuredPolicy");

            entity.Property(e => e.InsuredPolicyId).HasColumnName("InsuredPolicyID");
            entity.Property(e => e.ApprovalStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.InsuredId).HasColumnName("InsuredID");
            entity.Property(e => e.RenewalStatus)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Policy).WithMany(p => p.InsuredPolicies)
                .HasForeignKey(d => d.PolicyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InsuredPo__Polic__160F4887");
        });

        modelBuilder.Entity<Policy>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PK__Policy__2E13394418BF7D3F");

            entity.ToTable("Policy");

            entity.HasIndex(e => e.PolicyNumber, "UQ__Policy__46DA01573F7658B0").IsUnique();

            entity.Property(e => e.PolicyId).HasColumnName("PolicyID");
            entity.Property(e => e.InsuranceTypeId).HasColumnName("InsuranceTypeID");
            entity.Property(e => e.PolicyNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PremiumAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.InsuranceType).WithMany(p => p.Policies)
                .HasForeignKey(d => d.InsuranceTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Policy__Insuranc__0E6E26BF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
