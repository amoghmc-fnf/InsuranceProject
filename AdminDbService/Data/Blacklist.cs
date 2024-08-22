using System;
using System.Collections.Generic;

namespace AdminDbService.Data;

public partial class Blacklist
{
    public int BlacklistId { get; set; }

    public int PolicyHolderId { get; set; }

    public int AdminId { get; set; }

    public DateOnly BlacklistDate { get; set; }

    public string Reason { get; set; } = null!;

    public virtual Admin Admin { get; set; } = null!;
}
