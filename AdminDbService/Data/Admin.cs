using System;
using System.Collections.Generic;

namespace AdminDbService.Data;

public partial class Admin
{
    public int AdminId { get; set; }

    public string Name { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public virtual ICollection<Blacklist> Blacklists { get; set; } = new List<Blacklist>();
}
