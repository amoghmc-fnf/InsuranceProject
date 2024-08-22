using System;
using System.Collections.Generic;

namespace InsuranceApi.Data;

public partial class InsuranceType
{
    public int InsuranceId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal BaseRate { get; set; }

    public int CoverageSize { get; set; }

    public virtual ICollection<Policy> Policies { get; set; } = new List<Policy>();
}
