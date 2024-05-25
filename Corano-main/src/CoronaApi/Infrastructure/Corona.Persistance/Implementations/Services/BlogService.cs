using AutoMapper;
using Corona.Application.Abstraction.Repositories.IEntityRepository;
using Corona.Application.Abstraction.Services;
using Corona.Application.DTOs.BlogImages;
using Corona.Application.DTOs.Blogs;
using Corona.Application.DTOs.Slider;
using Corona.Domain.Entities;
using Corona.Persistance.Context;
using Corona.Persistance.Exceptions;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Corona.Persistance.Implementations.Services;

public class BlogService : IBlogService
{
    private readonly IMapper _mapper;
    private readonly IBlogReadRepository _blogReadRepository;
    private readonly IBlogWriteRepository _blogWriteRepository;
    private readonly AppDbContext _appDbContext;
    private readonly IConfiguration _configuration;

    public BlogService(IMapper mapper,
                       IBlogReadRepository blogReadRepository,
                       IBlogWriteRepository blogWriteRepository,
                       AppDbContext appDbContext,
                       IConfiguration configuration)
    {
        _mapper = mapper;
        _blogReadRepository = blogReadRepository;
        _blogWriteRepository = blogWriteRepository;
        _appDbContext = appDbContext;
        _configuration = configuration;
    }

    public async Task CreateAsync(CreateBlogDto createBlogDto)
    {
        var newBlog = new Blog()
        {
            Title = createBlogDto.Title,
            Description = createBlogDto.Description
        };

        await _blogWriteRepository.AddAsync(newBlog);
        await _blogWriteRepository.SaveChangeAsync();

        var newBlogImage = new List<CreateBlogImageDto>();

        if (createBlogDto.CreateBlogImagesDto is not null)
        {

            foreach (var item in createBlogDto.CreateBlogImagesDto)
            {
                string ApiKey = _configuration["GoogleCloud:ApiKey"];
                var credential = GoogleCredential.FromFile(ApiKey);

                var client = StorageClient.Create(credential);
                var imageUrl = string.Empty;

                using (var memoryStream = new MemoryStream())
                {
                    await item.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;

                    var objectName = $"Test/{Guid.NewGuid()}_{item.FileName}";
                    var bucketName = "testes22d";

                    await client.UploadObjectAsync(bucketName, objectName, null, memoryStream);
                    var url = $"https://storage.googleapis.com/{bucketName}/{objectName}";

                    imageUrl = url;
                }

                var blogImage = new CreateBlogImageDto()
                {
                    BlogId = newBlog.Id,
                    ImageUrl = imageUrl
                };
                newBlogImage.Add(blogImage);
            }
        }

        var blogImages = _mapper.Map<List<BlogImage>>(newBlogImage);
        await _appDbContext.BlogImages.AddRangeAsync(blogImages);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<List<GetBlogDto>> GetAllAsync()
    {
        var allblog = await _blogReadRepository.GetAll()
                            .Include(x => x.BlogImages).ToListAsync();

        var toDto = _mapper.Map<List<GetBlogDto>>(allblog);
        return toDto;
    }

    public async Task<GetBlogDto> GetByIdAsync(Guid Id)
    {
        var blog = await _appDbContext.Blogs.Include(x => x.BlogImages).FirstOrDefaultAsync(x => x.Id == Id);
        if (blog is null) throw new NotFoundException("Blog not found");

        var toDto = _mapper.Map<GetBlogDto>(blog);
        return toDto;
    }

    public async Task RemoveAsync(Guid Id)
    {
        var blog = await _blogReadRepository.GetByIdAsync(Id);
        if (blog is null) throw new NotFoundException("Blog not found");

        _blogWriteRepository.Remove(blog);
        await _blogWriteRepository.SaveChangeAsync();
    }

    public async Task UpdateAsync(UpdateBlogDto updateBlogDto)
    {
        var blog = await _appDbContext.Blogs.Include(x => x.BlogImages)
                        .FirstOrDefaultAsync(x => x.Id == updateBlogDto.Id);
        if (blog is null) throw new NotFoundException("Blog not found");


        blog.Title = updateBlogDto.Title;
        blog.Description = updateBlogDto.Description;

        if (updateBlogDto.UpdateBlogImagesDto is not null)
        {
            _appDbContext.BlogImages.RemoveRange(blog.BlogImages);
            await _appDbContext.SaveChangesAsync();
        }

        var newBlogImage = new List<CreateBlogImageDto>();

        if (updateBlogDto.UpdateBlogImagesDto is not null)
        {

            foreach (var item in updateBlogDto.UpdateBlogImagesDto)
            {
                string ApiKey = _configuration["GoogleCloud:ApiKey"];
                var credential = GoogleCredential.FromFile(ApiKey);

                var client = StorageClient.Create(credential);
                var imageUrl = string.Empty;

                using (var memoryStream = new MemoryStream())
                {
                    await item.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;

                    var objectName = $"Test/{Guid.NewGuid()}_{item.FileName}";
                    var bucketName = "testes22d";

                    await client.UploadObjectAsync(bucketName, objectName, null, memoryStream);
                    var url = $"https://storage.googleapis.com/{bucketName}/{objectName}";

                    imageUrl = url;
                }
                var blogImage = new CreateBlogImageDto()
                {
                    BlogId = blog.Id,
                    ImageUrl = imageUrl
                };
                newBlogImage.Add(blogImage);
            }
        }

        var blogImages = _mapper.Map<List<BlogImage>>(newBlogImage);
        await _appDbContext.BlogImages.AddRangeAsync(blogImages);
        await _appDbContext.SaveChangesAsync();

        _blogWriteRepository.Update(blog);
        await _blogWriteRepository.SaveChangeAsync();
    }
}