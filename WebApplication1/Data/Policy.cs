using System;
using System.Collections.Generic;

namespace InsuranceApi.Data;

public partial class Policy
{
    public int PolicyId { get; set; }

    public string PolicyNumber { get; set; } = null!;

    public int InsuranceTypeId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public decimal PremiumAmount { get; set; }

    public virtual InsuranceType InsuranceType { get; set; } = null!;

    public virtual ICollection<InsuredPolicy> InsuredPolicies { get; set; } = new List<InsuredPolicy>();
}
