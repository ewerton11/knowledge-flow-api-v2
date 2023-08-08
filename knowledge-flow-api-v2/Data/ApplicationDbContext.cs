using knowledge_flow_api_v2.Models;
using Microsoft.EntityFrameworkCore;

namespace knowledge_flow_api_v2.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Topics> Topics { get; set; }
}