using Corona.Domain.Entities.Common;

namespace Corona.Domain.Entities;

public class Testimonials : BaseEntity
{
    public string ImageUrl { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}