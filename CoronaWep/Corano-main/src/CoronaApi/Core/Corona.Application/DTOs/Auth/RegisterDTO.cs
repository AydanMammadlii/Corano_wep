namespace Corona.Application.DTOs.Auth;

public record RegisterDTO(string? Fullname, string Username, string Email, string password, DateTime? BirthDate);
