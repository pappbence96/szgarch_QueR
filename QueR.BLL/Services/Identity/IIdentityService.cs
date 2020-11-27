using QueR.BLL.Services.Identity.DTOs;
using System.Threading.Tasks;

namespace QueR.BLL.Services.Identity
{
    public interface IIdentityService
    {
        Task<LoginResponse> CreateTokenForUser(LoginModel model);
        Task<RegisterResponse> RegisterSimpleUser(RegisterModel model);
        Task UpdateSimpleUser(UpdateUserModel model);
    }
}