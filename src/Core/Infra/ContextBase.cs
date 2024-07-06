using Core.Data;
using Core.DomainObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Infra;

public abstract class ContextBase(DbContextOptions options) : DbContext(options), IUnitOfWork
{
    private static readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(
        builder =>
        {
            builder
                .AddFilter((category, level) =>
                    category == DbLoggerCategory.Database.Command.Name
                    && level == LogLevel.Information);
            builder.AddConsole();
        });

    public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
    {
        this.EnsureAutoHistory(() => new EFAutoHistory());

        return await SaveChangesAsync(cancellationToken) > 0;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseLoggerFactory(_loggerFactory);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.EnableAutoHistory<EFAutoHistory>(o => { });

        base.OnModelCreating(modelBuilder);
    }
}
