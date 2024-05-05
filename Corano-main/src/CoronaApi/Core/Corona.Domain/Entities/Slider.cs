using Corona.Domain.Entities.Common;

namespace Corona.Domain.Entities;

public class Slider : BaseEntity
{
    public string Image { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}