using System;
using System.Collections.Generic;

namespace InsuranceApi.Data;

public partial class Hospital
{
    public int HospitalId { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public virtual ICollection<Claim> Claims { get; set; } = new List<Claim>();
}
