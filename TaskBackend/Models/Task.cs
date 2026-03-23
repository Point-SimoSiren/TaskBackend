using System;
using System.Collections.Generic;

namespace TaskBackend.Models;

public partial class Task
{
    public int TaskId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int Status { get; set; }

    public DateTime? StatusChanged { get; set; }

    public int Priority { get; set; }
}
