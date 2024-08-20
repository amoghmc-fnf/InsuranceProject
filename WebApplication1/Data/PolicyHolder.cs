using System;
using System.Collections.Generic;

namespace InsuranceApi.Data;

public partial class PolicyHolder
{
    public int PolicyHolderId { get; set; }

    public string Name { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Status { get; set; }

    public virtual ICollection<Blacklist> Blacklists { get; set; } = new List<Blacklist>();

    public virtual ICollection<Claim> Claims { get; set; } = new List<Claim>();

    public virtual ICollection<Insured> Insureds { get; set; } = new List<Insured>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
