using System;
using System.Collections.Generic;

namespace InsuranceApi.Data;

public partial class InsuranceType
{
    public int InsuranceId { get; set; }

    public string InsuranceType1 { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Policy> Policies { get; set; } = new List<Policy>();
}
