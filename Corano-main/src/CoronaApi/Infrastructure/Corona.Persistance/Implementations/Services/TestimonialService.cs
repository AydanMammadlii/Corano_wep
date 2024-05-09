using AutoMapper;
using Corona.Application.Abstraction.Repositories.IEntityRepository;
using Corona.Application.Abstraction.Services;
using Corona.Application.DTOs.Testimonial;
using Corona.Domain.Entities;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Corona.Persistance.Implementations.Services;

public class TestimonialService : ITestimonialService
{
    private readonly IMapper _mapper;
    private readonly ITestimonialReadRepository _testimonialReadRepository;
    private readonly ITestimonialWriteRepository _testimonialWriteRepository;
    private readonly IConfiguration _configuration;


    public TestimonialService(IMapper mapper,
                              ITestimonialReadRepository testimonialReadRepository,
                              ITestimonialWriteRepository testimonialWriteRepository,
                              IConfiguration configuration)
    {
        _mapper = mapper;
        _testimonialReadRepository = testimonialReadRepository;
        _testimonialWriteRepository = testimonialWriteRepository;
        _configuration = configuration;
    }

    public async Task CreateAsync(TestimonialsCreateDto testimonialsCreateDto)
    {
        if (testimonialsCreateDto.ImageUrl == null || testimonialsCreateDto.ImageUrl.Length == 0)
            throw new Exception("Exception");

        string ApiKey = _configuration["GoogleCloud:ApiKey"];
        var credential = GoogleCredential.FromFile(ApiKey);

        var client = StorageClient.Create(credential);
        var imageUrl = string.Empty;

        using (var memoryStream = new MemoryStream())
        {
            await testimonialsCreateDto.ImageUrl.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var objectName = $"Test/{Guid.NewGuid()}_{testimonialsCreateDto.ImageUrl.FileName}";
            var bucketName = "testes22d";

            await client.UploadObjectAsync(bucketName, objectName, null, memoryStream);
            var url = $"https://storage.googleapis.com/{bucketName}/{objectName}";

            imageUrl = url;
        }

        var DtoToEntity = _mapper.Map<Testimonials>(testimonialsCreateDto);
        DtoToEntity.ImageUrl = imageUrl;


        await _testimonialWriteRepository.AddAsync(DtoToEntity);
        await _testimonialWriteRepository.SaveChangeAsync();
    }

    public async Task<List<TestimonialsGetDto>> GetAllAsync()
    {
        var testimonials = await _testimonialReadRepository.GetAll().OrderByDescending(x => x.CreatedDate).ToListAsync();
        if (testimonials is null) throw new NullReferenceException();
        var entityToDto = _mapper.Map<List<TestimonialsGetDto>>(testimonials);

        return entityToDto;
    }

    public async Task<TestimonialsGetDto> GetByIdAsync(Guid Id)
    {
        var byStestimonial = await _testimonialReadRepository.GetByIdAsync(Id);
        if (byStestimonial is null) throw new NullReferenceException();

        var toDto = _mapper.Map<TestimonialsGetDto>(byStestimonial);
        //toDto.ImageUrl = Convert.ToBase64String(byStestimonial.ImageUrl);
        return toDto;
    }

    public async Task RemoveAsync(Guid Id)
    {
        var byStestimonial = await _testimonialReadRepository.GetByIdAsync(Id);
        if (byStestimonial is null) throw new NullReferenceException();

        _testimonialWriteRepository.Remove(byStestimonial);
        await _testimonialWriteRepository.SaveChangeAsync();
    }

    public async Task UpdateAsync(TestimonialsUpdateDto testimonialsUpdateDto)
    {
        var byStestimonial = await _testimonialReadRepository.GetByIdAsync(testimonialsUpdateDto.Id);
        if (byStestimonial is null) throw new NullReferenceException();

        if (testimonialsUpdateDto.ImageUrl != null)
        {
            string ApiKey = _configuration["GoogleCloud:ApiKey"];
            var credential = GoogleCredential.FromFile(ApiKey);

            var client = StorageClient.Create(credential);
            var imageUrl = string.Empty;

            using (var memoryStream = new MemoryStream())
            {
                await testimonialsUpdateDto.ImageUrl.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                var objectName = $"Test/{Guid.NewGuid()}_{testimonialsUpdateDto.ImageUrl.FileName}";
                var bucketName = "testes22d";

                await client.UploadObjectAsync(bucketName, objectName, null, memoryStream);
                var url = $"https://storage.googleapis.com/{bucketName}/{objectName}";

                imageUrl = url;
            }
            byStestimonial.ImageUrl = imageUrl;
        }

        byStestimonial.Name = testimonialsUpdateDto.Name;
        byStestimonial.Description = testimonialsUpdateDto.Description;
        byStestimonial.ImageUrl = byStestimonial.ImageUrl;

        _testimonialWriteRepository.Update(byStestimonial);
        await _testimonialWriteRepository.SaveChangeAsync();
    }
}