namespace Auth.Application.RegisterUser
{
    public sealed record RegisterUserResult (bool Success, string? ErrorMessage = null, Guid? UserId = null)
    {
        public static RegisterUserResult Ok(Guid userId) => new(true, null, UserId: userId);
        public static RegisterUserResult Fail(string errorMessage) => new(false, errorMessage);
    }
}