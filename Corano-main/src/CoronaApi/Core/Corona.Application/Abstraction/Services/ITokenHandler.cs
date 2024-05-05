using Corona.Application.DTOs.Auth;
using Corona.Domain.Entities;

namespace Corona.Application.Abstraction.Services;

public interface ITokenHandler
{
    public Task<TokenResponseDTO> CreateAccessToken(int minutes, int refreshTokenMinutes, AppUser appUser, Guid basketId);
}
