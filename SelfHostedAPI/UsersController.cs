using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SelfHostedAPI
{
    public class UsersController:ApiController
    {
        static readonly IUserRepository _userRepository = new UserRepository();
        static readonly string filepath = ConfigurationManager.AppSettings["filepath"];  //The filepath has been configured in App.config

        // GET api/Users
        public IEnumerable<User> Get()
        {
            return _userRepository.GetAll(@filepath);
        }

        // GET api/Users/{id}
        public User Get(int id)
        {
            return _userRepository.GetUser(id, @filepath);
        }

        // POST api/Users
        public string Post([FromBody] User user)
        {
            return _userRepository.AddUser(user, @filepath);
        }

        // DELETE api/Users/{id}
        public string Delete(int id)
        {
            return _userRepository.DeleteUser(id, @filepath);
        }

        // PUT api/Users/{id}
        public string Put(int id, [FromBody] User user)
        {
            return _userRepository.UpdateUser(id, @filepath, user);
        }
    }
}
