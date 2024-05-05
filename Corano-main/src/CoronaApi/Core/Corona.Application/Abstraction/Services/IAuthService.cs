using Corona.Application.DTOs.Auth;
using Corona.Domain.Entities;
using Corona.Domain.Helpers;

namespace Corona.Application.Abstraction.Services;

public interface IAuthService
{
    Task<SignUpResponse> Register(RegisterDTO registerDTO);
    Task<TokenResponseDTO> Login(LoginDTO loginDTO);
    Task<TokenResponseDTO> LoginAdmin(LoginDTO loginDTO);
    Task<TokenResponseDTO> ValidRefleshToken(string refreshToken);
    Task AdminCreate(AdminCreate adminCreate);
    Task AdminDelete(AdminCreate adminCreate);
    Task<List<AppUser>> AllMemberUser();
    Task<List<AppUser>> AllAdminUser();

}
