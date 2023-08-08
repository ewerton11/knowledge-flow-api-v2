using System;

namespace knowledge_flow_api_v2.Models;

public class Topics
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int? ParentId { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsChecked { get; set; }

    public Topics()
    {
        Title = string.Empty;
        CreatedAt = DateTime.Now;
        IsChecked = false;
    }
}