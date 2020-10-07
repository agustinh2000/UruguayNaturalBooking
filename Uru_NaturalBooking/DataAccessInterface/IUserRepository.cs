using Domain;

namespace DataAccessInterface
{
    public interface IUserRepository: IRepository<User>
    {
        User GetUserByEmailAndPassword(string email, string password);
    }
}
