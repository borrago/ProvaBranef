using Core.Infra;
using Domain.ClientAggregate;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infra;

public class Context(DbContextOptions<Context> options) : ContextBase(options)
{
    public DbSet<Client> Clients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}