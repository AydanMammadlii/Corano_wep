using Corona.Application.Abstraction.Repositories.IEntityRepository;
using Corona.Domain.Entities;
using Corona.Persistance.Context;

namespace Corona.Persistance.Implementations.Repositories.IEntityRepository;

public class TestimonialWriteRepository : WriteRepository<Testimonials>, ITestimonialWriteRepository
{
    public TestimonialWriteRepository(AppDbContext context) : base(context)
    {
    }
}