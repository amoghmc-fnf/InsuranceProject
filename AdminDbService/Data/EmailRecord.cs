using System;
using System.Collections.Generic;

namespace AdminDbService.Data;

public partial class EmailRecord
{
    public int EmailRecordId { get; set; }

    public string FromEmail { get; set; } = null!;

    public string ToEmail { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public string Content { get; set; } = null!;
}
