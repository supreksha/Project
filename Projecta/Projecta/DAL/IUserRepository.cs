using UserData.Models;

namespace UserData.DAL
{
    public interface IUserRepository
    {
        UserEntity GetUserById(uint id);
        int UpdateUser(UserEntity user);
    }
}