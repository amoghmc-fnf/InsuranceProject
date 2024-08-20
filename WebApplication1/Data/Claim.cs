using System;
using System.Collections.Generic;

namespace InsuranceApi.Data;

public partial class Claim
{
    public int ClaimId { get; set; }

    public int InsuredPolicyId { get; set; }

    public int PolicyHolderId { get; set; }

    public DateOnly ClaimDate { get; set; }

    public decimal ClaimAmount { get; set; }

    public string ClaimStatus { get; set; } = null!;

    public decimal? DispenseAmount { get; set; }

    public string DocumentType { get; set; } = null!;

    public string DocumentPath { get; set; } = null!;

    public int HospitalId { get; set; }

    public virtual Hospital Hospital { get; set; } = null!;

    public virtual InsuredPolicy InsuredPolicy { get; set; } = null!;

    public virtual PolicyHolder PolicyHolder { get; set; } = null!;
}
