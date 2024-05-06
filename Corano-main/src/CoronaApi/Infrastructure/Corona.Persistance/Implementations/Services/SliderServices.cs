using AutoMapper;
using Corona.Application.Abstraction.Repositories.IEntityRepository;
using Corona.Application.Abstraction.Services;
using Corona.Application.DTOs.Slider;
using Corona.Domain.Entities;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Corona.Persistance.Implementations.Services;

public class SliderServices : ISliderServices
{
    private readonly ISliderReadRepository _SliderReadRepository;
    private readonly ISliderWriteRepository _SliderWriteRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    public SliderServices(ISliderReadRepository sliderReadRepository,
                          ISliderWriteRepository sliderWriteRepository,
                          IMapper mapper,
                          IConfiguration configuration)
    {
        _SliderReadRepository = sliderReadRepository;
        _SliderWriteRepository = sliderWriteRepository;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task CreateAsync(SliderCreateDTO sliderCreateDTO)
    {
        if (sliderCreateDTO.Image == null || sliderCreateDTO.Image.Length == 0)
            throw new Exception("Exception");

        string ApiKey = _configuration["GoogleCloud:ApiKey"];
        var credential = GoogleCredential.FromFile(ApiKey);

        var client = StorageClient.Create(credential);
        var imageUrl = string.Empty;

        using (var memoryStream = new MemoryStream())
        {
            await sliderCreateDTO.Image.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var objectName = $"Test/{Guid.NewGuid()}_{sliderCreateDTO.Image.FileName}";
            var bucketName = "testes22d";

            await client.UploadObjectAsync(bucketName, objectName, null, memoryStream);
            var url = $"https://storage.googleapis.com/{bucketName}/{objectName}";

            imageUrl = url;
        }
        var DtoToEntity = _mapper.Map<Slider>(sliderCreateDTO);
        DtoToEntity.Image = imageUrl;


        await _SliderWriteRepository.AddAsync(DtoToEntity);
        await _SliderWriteRepository.SaveChangeAsync();
    }

    public async Task<List<SliderGetDTO>> GetAllAsync()
    {
        var silder = await _SliderReadRepository.GetAll()
            .OrderByDescending(x => x.CreatedDate).ToListAsync();
        if (silder is null) throw new NullReferenceException();

        var entityToDto = _mapper.Map<List<SliderGetDTO>>(silder);

        return entityToDto;
    }

    public async Task<SliderGetDTO> GetByIdAsync(Guid Id)
    {
        var bySlider = await _SliderReadRepository.GetByIdAsync(Id);
        if (bySlider is null) throw new NullReferenceException();

        var toDto = _mapper.Map<SliderGetDTO>(bySlider);
        return toDto;
    }

    public async Task RemoveAsync(Guid Id)
    {
        var bySlider = await _SliderReadRepository.GetByIdAsync(Id);
        if (bySlider is null) throw new NullReferenceException();

        _SliderWriteRepository.Remove(bySlider);
        await _SliderWriteRepository.SaveChangeAsync();
    }

    public async Task UpdateAsync(SliderUpdateDTO sliderUpdateDTO)
    {
        var bySlider = await _SliderReadRepository.GetByIdAsync(sliderUpdateDTO.Id);
        if (bySlider is null) throw new NullReferenceException();

        if (sliderUpdateDTO.Image != null)
        {
            string ApiKey = _configuration["GoogleCloud:ApiKey"];
            var credential = GoogleCredential.FromFile(ApiKey);

            var client = StorageClient.Create(credential);
            var imageUrl = string.Empty;

            using (var memoryStream = new MemoryStream())
            {
                await sliderUpdateDTO.Image.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                var objectName = $"Test/{Guid.NewGuid()}_{sliderUpdateDTO.Image.FileName}";
                var bucketName = "testes22d";

                await client.UploadObjectAsync(bucketName, objectName, null, memoryStream);
                var url = $"https://storage.googleapis.com/{bucketName}/{objectName}";

                imageUrl = url;
            }
            bySlider.Image = imageUrl;
        }

        bySlider.Name = sliderUpdateDTO.Name ?? bySlider.Name;
        bySlider.Description = sliderUpdateDTO.Description ?? bySlider.Description;
        bySlider.Image = bySlider.Image;

        _SliderWriteRepository.Update(bySlider);
        await _SliderWriteRepository.SaveChangeAsync();
    }
}