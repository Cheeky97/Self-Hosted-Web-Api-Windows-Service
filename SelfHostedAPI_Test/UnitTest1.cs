using Microsoft.VisualStudio.TestTools.UnitTesting;
using SelfHostedAPI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SelfHostedAPI_Test
{
    [TestClass]
    public class UnitTest1
    {
        //Adding new User
        [TestMethod]
        public void Test_AddUser()
        {
            User user = new User(15, "Ross", "Geller", "Ross.Gellar@gmail.com", "Front-end Developer");
            UsersController usersController = new UsersController();
            string result = usersController.Post(user);
            Assert.AreEqual("User added successfully", result);
        }
        //Adding new User with existing user id
        [TestMethod]
        public void Test_AddUser_WorstCase()
        {
            User user = new User(1, "Ross", "Geller", "Ross.Gellar@gmail.com", "Front-end Developer");
            UsersController usersController = new UsersController();
            string result = usersController.Post(user);
            Assert.AreEqual("Error: User Id is already taken, please use unique id.", result);
        }

        //Get All Users
        [TestMethod]
        public void Test_GetAll()
        {
            User user = new User(1, "Srikanth", "Jambulingam", "srikanth.jambulingam@gmail.com", "Senior Software Engineer");
            UsersController usersController = new UsersController();
            var result = usersController.Get();
            Assert.AreEqual(user.id, result.ToList()[0].id);
        }

        //Get User by ID
        [TestMethod]
        public void Test_GetUser()
        {
            int id = 1;
            UsersController usersController = new UsersController();
            var result = usersController.Get(id);
            Assert.AreEqual(id, result.id);
        }
        //Get User by ID if there is no such user exist
        [TestMethod]
        public void Test_GetUser_WorstCase()
        {
            int id = 65;
            User user = new User();
            UsersController usersController = new UsersController();
            var result = usersController.Get(id);
            Assert.IsNull(result);
        }

        //Deleting a user by ID
        [TestMethod]
        public void Test_DeleteUser()
        {
            int id = 7;
            UsersController usersController = new UsersController();
            var result = usersController.Delete(id);
            Assert.AreEqual("User deleted", result);
        }

        //Deleting a user by ID if there is no such user exist
        [TestMethod]
        public void Test_DeleteUser_WorstCase()
        {
            int id = 29;
            UsersController usersController = new UsersController();
            var result = usersController.Delete(id);
            Assert.AreEqual("No user found", result);
        }

        //Updating user details by user ID
        [TestMethod]
        public void Test_UpdateUser()
        {
            int id = 1;
            User user = new User(1, "Chandler", "Geller", "Chandler.Gellar@gmail.com", "Software Developer");
            UsersController usersController = new UsersController();
            var result = usersController.Put(id, user);
            Assert.AreEqual("Edited..!", result);
        }

        //Updating user details by user ID if there is no such user exist
        [TestMethod]
        public void Test_UpdateUser_WorstCase()
        {
            int id = 3;
            User user = new User(3, "Chandler", "Geller", "Chandler.Gellar@gmail.com", "Software Developer");
            UsersController usersController = new UsersController();
            var result = usersController.Put(id, user);
            Assert.AreEqual("No user found", result);
        }
    }
}
