using Corona.Application.DTOs.Auth;
using Corona.Domain.Helpers;
using Corona.MVC.ExamProgramUIExceptions;
using Corona.MVC.Service.Interfaces;
using Corona.MVC.ViewModel.Auth;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using RestSharp;
using RestSharpMethod = RestSharp.Method;  // RestSharp.Method için bir alias oluşturuyoruz.

namespace Corona.MVC.Service.Implementations;

public class ApiService : IApiService
{
    protected readonly IConfiguration Configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    protected readonly RestClient _client;

    public ApiService(IConfiguration _configuration, IHttpContextAccessor httpContextAccessor)
    {
        Configuration = _configuration;
        _httpContextAccessor = httpContextAccessor;
        _client = new RestClient(_configuration["Api:Url"]);

        var token = _httpContextAccessor.HttpContext.Request.Cookies["token"];
        if (!string.IsNullOrEmpty(token))
            _client.AddDefaultHeader("Authorization", "Bearer " + token);
    }

    public async Task<TokenResponseDTO> Login(LoginViewModel model)
    {
        var request = new RestRequest("/Auth/Login", RestSharpMethod.Post);

        request.AddJsonBody(model);
        var response = await _client.ExecuteAsync<TokenResponseDTO>(request);

        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            throw new ApiException(response.StatusCode, response.Content);

        return response.Data;
    }

    public async Task<SignUpResponse> Register(RegisterViewModel model)
    {
        var request = new RestRequest("/Auth/Register", RestSharpMethod.Post);

        request.AddJsonBody(model);
        var response = await _client.ExecuteAsync<SignUpResponse>(request);

        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            throw new ApiException(response.StatusCode, response.Content);

        return response.Data;
    }
}
