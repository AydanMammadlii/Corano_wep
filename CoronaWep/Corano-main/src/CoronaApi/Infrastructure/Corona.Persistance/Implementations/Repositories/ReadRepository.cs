using Microsoft.EntityFrameworkCore;
using Corona.Application.Abstraction.Repositories;
using Corona.Domain.Entities.Common;
using Corona.Persistance.Context;
using System.Linq.Expressions;

namespace Corona.Persistance.Implementations.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity, new()
{
    private readonly AppDbContext _context;

    public ReadRepository(AppDbContext context) => _context = context;

    public DbSet<T> Table => _context.Set<T>();

    public IQueryable<T> GetAll(bool isTracking = true, params string[] inculdes)
    {
        var query = Table.AsQueryable();
        foreach (var incul in inculdes)
        {
            query = query.Include(incul);
        }
        return isTracking ? query : query.AsNoTracking();
    }

    public IQueryable<T> GetAllExpression(Expression<Func<T, bool>> expression, int Skip, int Take, bool isTracking = true, params string[] inculdes)
    {
        var query = Table.Where(expression).Skip(Skip).Take(Take).AsQueryable();
        foreach(var incul in inculdes)
        {
            query = query.Include(incul);   
        }
        return isTracking ? query : query.AsNoTracking();
    }

    public IQueryable<T> GetAllExpressionOrderBy(Expression<Func<T, bool>> expression, int Skip, int Take, Expression<Func<T, object>> expressionOrder, bool IsOrder = true, bool IsTracking = true, params string[] inculdes)
    {
        var query = Table.Where(expression).AsQueryable();
        query = IsOrder ? query.OrderBy(expressionOrder) : query.OrderByDescending(expression);
        foreach (var inculude in inculdes)
        {
            query = query.Include(inculude);
        }
        return IsTracking ? query : query.AsNoTracking();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        var query = await Table.FindAsync(id);
        return query;
    }

    public async Task<T> GetByIdAsyncExpression(Expression<Func<T, bool>> expression, bool isTracking = true)
    {
        var query = isTracking ? Table.AsQueryable() : Table.AsNoTracking();
        return await query.FirstOrDefaultAsync(expression);
    }
}
