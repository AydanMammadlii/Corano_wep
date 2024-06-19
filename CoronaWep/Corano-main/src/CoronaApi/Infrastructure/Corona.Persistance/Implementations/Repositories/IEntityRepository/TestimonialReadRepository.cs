using Corona.Application.Abstraction.Repositories.IEntityRepository;
using Corona.Domain.Entities;
using Corona.Persistance.Context;

namespace Corona.Persistance.Implementations.Repositories.IEntityRepository;

public class TestimonialReadRepository : ReadRepository<Testimonials>, ITestimonialReadRepository
{
    public TestimonialReadRepository(AppDbContext context) : base(context)
    {
    }
}
