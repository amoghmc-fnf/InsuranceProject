using System;
using System.Collections.Generic;

namespace InsuranceApi.Data;

public partial class Illness
{
    public int IllnessId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<InsuredIllness> InsuredIllnesses { get; set; } = new List<InsuredIllness>();
}
