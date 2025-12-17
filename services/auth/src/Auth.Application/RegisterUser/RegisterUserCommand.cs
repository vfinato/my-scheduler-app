namespace Auth.Application.RegisterUser
{
    public sealed record RegisterUserCommand (string Email, string Password);
}