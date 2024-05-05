namespace Corona.MVC.Service.Interfaces;

public interface ICrudService
{
    Task<T> GetByIdAsync<T>(string endpoint, int id);
    Task<T> GetByIdAsyncGuid<T>(string endpoint, string id);
    Task<T> GetAllAsync<T>(string endpoint);
    Task CreateAsync<T>(string endpoint, T model) where T : class;
    Task UpdateAsync<T>(string endpoint, T model) where T : class;
    Task DeleteAsync(string endpoint, Guid id);
}
