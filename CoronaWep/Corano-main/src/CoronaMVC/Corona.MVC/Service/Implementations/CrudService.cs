using Corona.MVC.ExamProgramUIExceptions;
using Corona.MVC.Service.Interfaces;
using RestSharp;

namespace Corona.MVC.Service.Implementations;

public class CrudService : ICrudService
{
    private readonly RestClient _client;

    public CrudService(IHttpContextAccessor _httpContextAccessor)
    {
        _client = new RestClient("https://localhost:7295/api");
        var token = _httpContextAccessor.HttpContext.Request.Cookies["token"];
        _client.AddDefaultHeader("Authorization", "Bearer " + token);
    }

    public async Task CreateAsync<T>(string endpoint, T model) where T : class
    {
        var request = new RestRequest(endpoint, Method.Post);
        request.AddJsonBody(model);
        RestResponse response = await _client.ExecuteAsync(request);

        if (!response.IsSuccessful)
        {
            throw new ApiException(response.StatusCode, response.Content);
        }
    }

    public async Task DeleteAsync(string endpoint, Guid id)
    {
        var request = new RestRequest(endpoint, Method.Delete);
        var response = await _client.ExecuteAsync(request);

        if (!response.IsSuccessful)
        {
            throw new ApiException(response.StatusCode, response.Content);
        }
    }

    public async Task<T> GetAllAsync<T>(string endpoint)
    {
        var request = new RestRequest(endpoint, Method.Get);
        var response = await _client.ExecuteAsync<T>(request);

        if (!response.IsSuccessful)
        {
            throw new HttpRequestException(response.ErrorMessage);
        }

        return response.Data;
    }

    public async Task<T> GetByIdAsync<T>(string endpoint, int id)
    {
        var request = new RestRequest(endpoint, Method.Get);
        var response = await _client.ExecuteAsync<T>(request);

        if (!response.IsSuccessful)
        {
            throw new ApiException(response.StatusCode, response.Content);
        }

        return response.Data;
    }

    public async Task<T> GetByIdAsyncGuid<T>(string endpoint, string id)
    {
        var request = new RestRequest(endpoint, Method.Get);
        var response = await _client.ExecuteAsync<T>(request);

        if (!response.IsSuccessful)
        {
            throw new ApiException(response.StatusCode, response.Content);
        }

        return response.Data;
    }

    public async Task UpdateAsync<T>(string endpoint, T model) where T : class
    {
        var request = new RestRequest(endpoint, Method.Put);
        request.AddJsonBody(model);
        var response = await _client.ExecuteAsync(request);
        if (!response.IsSuccessful)
        {
            throw new ApiException(response.StatusCode, response.Content);
        }
    }
}
