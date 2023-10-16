using Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Persistence;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<ProductOriginal> ProductOriginals { get; set; }
    


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        

        base.OnModelCreating(modelBuilder);
    }

}