using System;
using System.Collections.Generic;

namespace PolicyDbService.Data;

public partial class InsuredPolicy
{
    public int InsuredPolicyId { get; set; }

    public int InsuredId { get; set; }

    public int PolicyId { get; set; }

    public string ApprovalStatus { get; set; } = null!;

    public string RenewalStatus { get; set; } = null!;

    public int AdminId { get; set; }

    public DateOnly? ApprovalDate { get; set; }

    public virtual ICollection<Claim> Claims { get; set; } = new List<Claim>();

    public virtual Policy Policy { get; set; } = null!;
}
