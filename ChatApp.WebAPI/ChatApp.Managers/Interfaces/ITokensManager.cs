using ChatApp.Models;

namespace ChatApp.Managers.Interfaces
{
    public interface ITokensManager
    {
        RefreshTokenModel AddToken(string userEmail, string ipAddress, string userAgent);
        RefreshTokenModel GetRefreshToken(string refreshToken, string userAgent);
        RefreshTokenModel GetRefreshTokenByEmail(string email, string userAgent);
        RefreshTokenModel UpdateToken(string refreshToekn, string userAgent);
    }
}
