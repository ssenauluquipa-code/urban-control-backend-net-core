namespace UrbanControl.Backend.Services
{
    public interface IAuthService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
        string GenerarTokenj(Models.User user);
    }
}
