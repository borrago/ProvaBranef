using Core.Data;
using Core.DomainObjects;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.Infra;

public abstract class GenericRepository<TEntity>(ContextBase context) : IGenericRepository<TEntity>, IRepository where TEntity : Entity
{
    private readonly ContextBase _context = context ?? throw new ArgumentNullException("context");

    public IUnitOfWork UnitOfWork => _context;

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        var query = GetQuery(predicate);

        return await query.SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity?> GetAsNoTrackingAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        var query = GetQuery(predicate).AsNoTracking();

        return await query.SingleOrDefaultAsync(cancellationToken);
    }

    public void Add(TEntity entity)
        => _context.AddAsync(entity);

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        => await _context.AddAsync(entity, cancellationToken);

    public void Update(TEntity entity)
        => _context.Entry(entity).State = EntityState.Modified;

    public void Remove(Guid id)
        => _context.Remove(id);

    public void Remove(TEntity entity)
        => _context.Remove(entity);

    private IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>>? predicate)
    {
        var query = _context
                    .Set<TEntity>()
                    .AsQueryable();

        if (predicate != null)
            query = query.Where(predicate);

        return query;
    }
}