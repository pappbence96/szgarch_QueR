using System.Threading.Tasks;

namespace QueR.BLL.Services.Identity
{
    public interface IIdentityService
    {
        Task<LoginResponse> CreateTokenForUser(LoginModel model);
    }
}