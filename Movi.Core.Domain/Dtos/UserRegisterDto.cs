namespace Movi.Core.Domain.Dtos;

public class UserRegisterDto
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
