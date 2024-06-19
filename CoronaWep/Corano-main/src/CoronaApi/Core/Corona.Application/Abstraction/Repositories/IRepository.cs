using Microsoft.EntityFrameworkCore;
using Corona.Domain.Entities.Common;

namespace Corona.Application.Abstraction.Repositories;

public interface IRepository<T> where T : BaseEntity, new()
{
    public DbSet<T> Table { get; }
}
