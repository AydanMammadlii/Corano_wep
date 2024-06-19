using Corona.Application.Abstraction.Repositories.IEntityRepository;
using Corona.Domain.Entities;
using Corona.Persistance.Context;

namespace Corona.Persistance.Implementations.Repositories.IEntityRepository;

public class BlogReadRepository : ReadRepository<Blog>, IBlogReadRepository
{
    public BlogReadRepository(AppDbContext context) : base(context)
    {
    }
}
