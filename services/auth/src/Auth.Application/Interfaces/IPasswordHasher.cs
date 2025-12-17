namespace Auth.Application.Interfaces
{
    public interface IPasswordHasher
    {
        string Hash(string Password);
    }
}