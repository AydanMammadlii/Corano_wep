using Corona.Application.Abstraction.Repositories.IEntityRepository;
using Corona.Domain.Entities;
using Corona.Persistance.Context;

namespace Corona.Persistance.Implementations.Repositories.IEntityRepository;

public class BrandWriteRepository : WriteRepository<Brand>, IBrandWriteRepository
{
    public BrandWriteRepository(AppDbContext context) : base(context)
    {
    }
}
