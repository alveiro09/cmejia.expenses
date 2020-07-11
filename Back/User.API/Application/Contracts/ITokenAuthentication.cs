using User.API.Application.Model.Response;

namespace User.API.Application.Contracts
{
    public interface ITokenAuthentication
    {        
        string BuildToken(TokenResponse tokenResponse);
    }
}
