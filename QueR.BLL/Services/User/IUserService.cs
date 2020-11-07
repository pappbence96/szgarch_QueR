using System.Threading.Tasks;

namespace QueR.BLL.Services.User
{
    public interface IUserService
    {
        Task<int> CreateAdminUser(UserModel model);
    }
}