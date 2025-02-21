using JoseApiRest.Domain.Entitys;
using Microsoft.EntityFrameworkCore;

namespace JoseApiRest.Infrastructure.Services.EntityFramework;

public class DataContext : DbContext
{
    public DataContext() { }
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<TaskItem> TaskItems { get; set; }
}
