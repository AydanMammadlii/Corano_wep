using Microsoft.AspNetCore.Http;
using Corona.Application.Abstraction.Services.Stroge.Local;

namespace Corona.Infrastructure.Services.Stroge.Local;

public class LocalStorage : ILocalStorage
{
    public string ConvertFileToBase64(string pathOrContainerName, IFormFile file)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteFileAsync(string pathOrContainerName, string fileName)
    {
        throw new NotImplementedException();
    }

    public Task<byte[]> DownlandFile(string file)
    {
        throw new NotImplementedException();
    }

    public Task<List<string>> GetFilesAsync(string pathOrContainerName)
    {
        throw new NotImplementedException();
    }

    public Task<bool> HasFile(string pathOrContainerName, string fileName)
    {
        throw new NotImplementedException();
    }

    public Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
    {
        throw new NotImplementedException();
    }

    public Task<string> UploadFileAsync(string pathOrContainerName, byte[] fileData, string fileName)
    {
        throw new NotImplementedException();
    }

    public Task<string> WriteFile(string pathOrContainerName, IFormFile file)
    {
        throw new NotImplementedException();
    }
}
