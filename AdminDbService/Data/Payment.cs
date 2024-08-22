using System;
using System.Collections.Generic;

namespace AdminDbService.Data;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int InsuredPolicyId { get; set; }

    public int PolicyHolderId { get; set; }

    public DateOnly PaymentDate { get; set; }

    public decimal PaymentAmount { get; set; }
}
