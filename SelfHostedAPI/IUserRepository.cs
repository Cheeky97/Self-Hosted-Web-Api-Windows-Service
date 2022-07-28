using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHostedAPI
{
    interface IUserRepository
    {
        IEnumerable<User> GetAll(string filepath);
        User GetUser(int id, string filepath);
        string AddUser(User user, string filepath);    
        string DeleteUser(int id, string filepath);
        string UpdateUser(int id, string filepath, User user);

    }
}
