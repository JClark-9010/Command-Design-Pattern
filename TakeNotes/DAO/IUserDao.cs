using TakeNotesServer.Models;

namespace TakeNotesServer.DAO
{
    public interface IUserDao
    {
        User GetUser(string username);
        User AddUser(string username, string password);
        List<User> GetUsers();
    }
}
