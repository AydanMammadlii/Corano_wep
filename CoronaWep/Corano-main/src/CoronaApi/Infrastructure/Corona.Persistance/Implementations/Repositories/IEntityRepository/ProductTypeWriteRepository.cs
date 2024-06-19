using Corona.Application.Abstraction.Repositories.IEntityRepository;
using Corona.Domain.Entities;
using Corona.Persistance.Context;

namespace Corona.Persistance.Implementations.Repositories.IEntityRepository;

public class ProductTypeWriteRepository : WriteRepository<ProductType>, IProductTypeWriteRepository
{
    public ProductTypeWriteRepository(AppDbContext context) : base(context)
    {
    }
}
