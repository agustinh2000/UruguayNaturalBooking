using Domain;

namespace DataAccessInterface
{
    public interface IUserRepository: IRepository<User>
    {
        User GetUserByNicknameAndPassword(string nickname, string password);
    }
}
