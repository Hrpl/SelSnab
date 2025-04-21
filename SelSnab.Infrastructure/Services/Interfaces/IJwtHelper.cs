using SelSnab.Domain.Commons.Response;
namespace SelSnab.Infrastructure.Services.Interfaces;

public interface IJwtHelper
{
    public JwtResponse CreateJwtAsync(int userId);
    public Task<int> DecodJwt(string accessToken);
}
