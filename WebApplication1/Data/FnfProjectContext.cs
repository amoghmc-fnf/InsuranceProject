using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApi.Data;

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

    public virtual DbSet<Claim> Claims { get; set; }

    public virtual DbSet<Hospital> Hospitals { get; set; }

    public virtual DbSet<Illness> Illnesses { get; set; }

    public virtual DbSet<InsuranceType> InsuranceTypes { get; set; }

    public virtual DbSet<Insured> Insureds { get; set; }

    public virtual DbSet<InsuredIllness> InsuredIllnesses { get; set; }

    public virtual DbSet<InsuredPolicy> InsuredPolicies { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Policy> Policies { get; set; }

    public virtual DbSet<PolicyHolder> PolicyHolders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=FNFIDVPRE20529\\SQLSERVER; Database=FnfProject; Trusted_Connection=True; TrustServerCertificate=True");

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

            entity.HasOne(d => d.PolicyHolder).WithMany(p => p.Blacklists)
                .HasForeignKey(d => d.PolicyHolderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Blacklist__Polic__4BAC3F29");
        });

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

            entity.HasOne(d => d.Hospital).WithMany(p => p.Claims)
                .HasForeignKey(d => d.HospitalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Claim__HospitalI__4CA06362");

            entity.HasOne(d => d.InsuredPolicy).WithMany(p => p.Claims)
                .HasForeignKey(d => d.InsuredPolicyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Claim__InsuredPo__4D94879B");

            entity.HasOne(d => d.PolicyHolder).WithMany(p => p.Claims)
                .HasForeignKey(d => d.PolicyHolderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Claim__PolicyHol__4E88ABD4");
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

        modelBuilder.Entity<Illness>(entity =>
        {
            entity.HasKey(e => e.IllnessId).HasName("PK__Illness__2BA575BB35CB09EA");

            entity.ToTable("Illness");

            entity.Property(e => e.Name).HasMaxLength(50);
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
                .HasConstraintName("FK__InsuredIl__Illne__5FB337D6");

            entity.HasOne(d => d.Insured).WithMany(p => p.InsuredIllnesses)
                .HasForeignKey(d => d.InsuredId)
                .HasConstraintName("FK__InsuredIl__Insur__5EBF139D");
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

            entity.HasOne(d => d.Admin).WithMany(p => p.InsuredPolicies)
                .HasForeignKey(d => d.AdminId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InsuredPo__Admin__5070F446");

            entity.HasOne(d => d.Insured).WithMany(p => p.InsuredPolicies)
                .HasForeignKey(d => d.InsuredId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InsuredPo__Insur__5165187F");

            entity.HasOne(d => d.Policy).WithMany(p => p.InsuredPolicies)
                .HasForeignKey(d => d.PolicyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InsuredPo__Polic__52593CB8");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A388B4CFDA5");

            entity.ToTable("Payment");

            entity.Property(e => e.InsuredPolicyId).HasColumnName("InsuredPolicyID");
            entity.Property(e => e.PaymentAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PolicyHolderId).HasColumnName("PolicyHolderID");

            entity.HasOne(d => d.InsuredPolicy).WithMany(p => p.Payments)
                .HasForeignKey(d => d.InsuredPolicyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payment__Insured__534D60F1");

            entity.HasOne(d => d.PolicyHolder).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PolicyHolderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payment__PolicyH__5441852A");
        });

        modelBuilder.Entity<Policy>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PK__Policy__2E133944FDFB5CF9");

            entity.ToTable("Policy");

            entity.HasIndex(e => e.PolicyNumber, "UQ__Policy__46DA015755325948").IsUnique();

            entity.Property(e => e.PolicyId)
                .ValueGeneratedNever()
                .HasColumnName("PolicyID");
            entity.Property(e => e.InsuranceTypeId).HasColumnName("InsuranceTypeID");
            entity.Property(e => e.PolicyNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PremiumAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.InsuranceType).WithMany(p => p.Policies)
                .HasForeignKey(d => d.InsuranceTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Policy__Insuranc__5535A963");
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
