using Corona.Application.Abstraction.Repositories.IEntityRepository;
using Corona.Domain.Entities;
using Corona.Persistance.Context;

namespace Corona.Persistance.Implementations.Repositories.IEntityRepository;

public class CommentWriteRepository : WriteRepository<Comment>, ICommentWriteRepository
{
    public CommentWriteRepository(AppDbContext context) : base(context)
    {
    }
}
