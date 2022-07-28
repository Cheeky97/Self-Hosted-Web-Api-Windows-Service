using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHostedAPI
{
    class UserRepository : IUserRepository
    {
        static readonly string tempFile = ConfigurationManager.AppSettings["temp"];
        static readonly int position = Int32.Parse(ConfigurationManager.AppSettings["position"]);

        public IEnumerable<User> GetAll(string filepath)
        {
            User user = new User();
            List<User> users = new List<User>();
            try
            {
                // Checking if the file exist or not. If file doesn't exist then create a file
                Logger.WriteLog($"This is the method for getting all the user details from the file.Checking the file exist or not in the path : {filepath}");
                if (!File.Exists(@filepath)) 
                {
                    using (StreamWriter stream = File.CreateText(@filepath))
                    {
                        stream.Write("");
                    }
                    Logger.WriteLog($"A new File has been created in the path : {filepath}");
                }

                string[] lines = File.ReadAllLines(@filepath);
                foreach(var line in lines)
                {
                    string[] fields = line.Split(',');
                    
                    users.Add(new User() { id = Int32.Parse(fields[0]), firstName = fields[1], lastName = fields[2], emailAddress = fields[3], notes = fields[4], creationTime = fields[5] });
                }
                Logger.WriteLog($"Data has been retrieved from the path : {filepath}");
                return users;
            }
            catch(Exception ex)
            {
                Logger.WriteLog($"There is an Exception occured : {ex}");
                throw new ApplicationException("There is an exception :", ex);
            }
            
        }

        public User GetUser(int id, string filepath)
        {
            User user = new User();
            try
            {
                // Checking if the file exist or not. If file doesn't exist then create a file
                Logger.WriteLog($"This is the method for getting particular user detail by user id from the file.Checking the file exist or not in the path : {filepath}");
                if (!File.Exists(@filepath))
                {
                    using(StreamWriter stream = File.CreateText(@filepath))
                    {
                        stream.Write("");
                    }
                    Logger.WriteLog($"A new File has been created in the path : {filepath}");
                }
                string[] lines = File.ReadAllLines(@filepath);
                foreach (var line in lines)
                {
                    string[] fields = line.Split(',');
                    user.id = Int32.Parse(fields[0]);
                    user.firstName = fields[1];
                    user.lastName = fields[2];
                    user.emailAddress = fields[3];
                    user.notes = fields[4];
                    user.creationTime = fields[5];

                    // Get the first occurance of the user with the user id
                    if (RecordMatches(id, fields, position))
                    {
                        Logger.WriteLog($"Data has been retrieved from the path : {filepath}");
                        return user;
                    }
                }
                
                return null;
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"There is an Exception occured : {ex}");
                throw new ApplicationException("There is an exception :", ex);
            }

        }

        private bool RecordMatches(int id, string[] fields, int position)
        {
            if (fields[position].Equals(id.ToString()))
            {
                return true;
            }
            return false;
        }
        private bool CheckUserIdExist(int id, string filepath)
        {
            string[] lines = File.ReadAllLines(@filepath);
            if (!(lines.Length == 0))
            {
                foreach (var line in lines)
                {
                    string[] fields = line.Split(',');
                    if (RecordMatches(id, fields, position))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public string AddUser(User user, string filepath)
        {
            try
            {
                // Creating creationTime for a new user
                if (user.creationTime == null)
                {
                    user.creationTime = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
                }

                // Checking if the userid is already exist
                if(File.Exists(filepath) && CheckUserIdExist(user.id, @filepath))
                {
                    Logger.WriteLog("Error: User Id is already taken, please use unique id.");
                    return "Error: User Id is already taken, please use unique id.";
                }
                else
                {
                    using (StreamWriter file = new StreamWriter(@filepath, true))
                    {
                        file.WriteLine(user.id + ", " + user.firstName + ", " + user.lastName + ", " + user.emailAddress + ", " + user.notes + ", " + user.creationTime);
                    }
                }

                Logger.WriteLog($"User details has been added to the file path : {filepath}");
                return "User added successfully";
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"There is an Exception occured : {ex}");
                throw new ApplicationException("There is an exception :", ex);
            }
        }

        public string DeleteUser(int id, string filepath)
        {
            User user = new User();
            string userString = ConfigurationManager.AppSettings["userString"];
            bool deleted = false;
            try
            {
                Logger.WriteLog($"This is the method to Delete a particular user from the file path: {filepath}");
                string[] lines = File.ReadAllLines(@filepath);
                foreach (var line in lines)
                {
                    string[] fields = line.Split(',');
                    user.id = Int32.Parse(fields[0]);
                    user.firstName = fields[1];
                    user.lastName = fields[2];
                    user.emailAddress = fields[3];
                    user.notes = fields[4];
                    user.creationTime = fields[5];

                    // If the user found then the user is not added into the temp file, hence deleting the user done.
                    if (!(RecordMatches(id, fields, position)) || deleted)
                    {
                        AddUser(user, @tempFile);
                    }
                    else
                    {
                        deleted = true;
                        Logger.WriteLog($"The user has been deleted successfully in the file path : {filepath}");
                        userString = "User deleted";
                    }
                }

                File.Delete(@filepath);
                File.Move(@tempFile, @filepath);
                if (!deleted)
                {
                    Logger.WriteLog($"User not found in the file path : {filepath}");
                }
                return userString;
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"There is an Exception occured : {ex}");
                throw new ApplicationException("There is an exception :", ex);
            }
        }

        public string UpdateUser(int id, string filepath, User modifiedUser)
        {
            User user = new User();
            bool edited = false;
            string userString = ConfigurationManager.AppSettings["userString"];

            try
            {
                Logger.WriteLog($"This is the method to Update a particular user in the file path: {filepath}");
                string[] lines = File.ReadAllLines(@filepath);
                foreach (var line in lines)
                {
                    string[] fields = line.Split(',');
                    user.id = Int32.Parse(fields[0]);
                    user.firstName = fields[1];
                    user.lastName = fields[2];
                    user.emailAddress = fields[3];
                    user.notes = fields[4];
                    user.creationTime = fields[5];

                    // If the user found then the user is not added into the temp file, hence editing of the user done.
                    if (!(RecordMatches(id, fields, position)))
                    {
                        AddUser(user, @tempFile);
                    }
                    else
                    {
                        if (!edited)
                        {
                            AddUser(modifiedUser, @tempFile);
                            userString = "Edited..!";
                            edited = true;
                            Logger.WriteLog($"The user has been edited successfully in the file path : {filepath}");
                        }

                    }
                }
                File.Delete(@filepath);
                File.Move(@tempFile, @filepath);
                if (!edited)
                {
                    Logger.WriteLog($"User not found in the file path : {filepath}");
                }
                return userString;
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"There is an Exception occured : {ex}");
                throw new ApplicationException("There is an exception :", ex);
            }
        }
    }
}
