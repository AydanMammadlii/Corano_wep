using Corona.Application.Abstraction.Repositories.IEntityRepository;
using Corona.Domain.Entities;
using Corona.Persistance.Context;

namespace Corona.Persistance.Implementations.Repositories.IEntityRepository;

public class SliderReadRepository : ReadRepository<Slider>, ISliderReadRepository
{
    public SliderReadRepository(AppDbContext context) : base(context)
    {
    }
}
