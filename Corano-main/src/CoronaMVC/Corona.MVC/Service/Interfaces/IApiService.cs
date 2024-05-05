using Corona.Application.DTOs.Auth;
using Corona.Domain.Helpers;
using Corona.MVC.ViewModel.Auth;

namespace Corona.MVC.Service.Interfaces;

public interface IApiService
{
    Task<TokenResponseDTO> Login(LoginViewModel model);
    Task<SignUpResponse> Register(RegisterViewModel model);
}
