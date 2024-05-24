using Corona.Application.Abstraction.Repositories.IEntityRepository;
using Corona.Domain.Entities;
using Corona.Persistance.Context;

namespace Corona.Persistance.Implementations.Repositories.IEntityRepository;

public class BlogWriteRepository : WriteRepository<Blog>, IBlogWriteRepository
{
    public BlogWriteRepository(AppDbContext context) : base(context)
    {
    }
}