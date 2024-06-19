using Corona.Application.Abstraction.Repositories.IEntityRepository;
using Corona.Domain.Entities;
using Corona.Persistance.Context;

namespace Corona.Persistance.Implementations.Repositories.IEntityRepository;

public class CategoryWriteRepository : WriteRepository<Category>, ICategoryWriteRepository
{
    public CategoryWriteRepository(AppDbContext context) : base(context)
    {
    }
}
