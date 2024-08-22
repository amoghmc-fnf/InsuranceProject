using System;
using System.Collections.Generic;

namespace InsuranceApi.Data;

public partial class InsuredIllness
{
    public int InsuredIllnessId { get; set; }

    public int InsuredId { get; set; }

    public int IllnessId { get; set; }

    public virtual Illness Illness { get; set; } = null!;

    public virtual Insured Insured { get; set; } = null!;
}
