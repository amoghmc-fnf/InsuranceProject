using System;
using System.Collections.Generic;

namespace InsuranceApi.Data;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int InsuredPolicyId { get; set; }

    public int PolicyHolderId { get; set; }

    public DateOnly PaymentDate { get; set; }

    public decimal PaymentAmount { get; set; }

    public virtual InsuredPolicy InsuredPolicy { get; set; } = null!;

    public virtual PolicyHolder PolicyHolder { get; set; } = null!;
}
