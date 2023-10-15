using MovieRatingApp.Common;
using MovieRatingApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRatingApp.Services
{
    public class UserService
    {     
        public async Task<List<User>> GetUsers()
        {
            string appDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string filePath = Path.Combine(appDataDirectory, "users.json");

            if (!File.Exists(filePath))
            {
                return new List<User>();              
            }

            string contents = File.ReadAllText(filePath);

            List<User> users = JsonConvert.DeserializeObject<List<User>>(contents);

            if (users == null)
            {
                return new List<User>();
            }

            return users;
        }

        public async void AddUser(string username, string password)
        {
            List<User> userList = new();

            userList = await GetUsers();

            userList.Add(new User()
            {
                Id = userList.Count > 0 ? userList.Max(u => u.Id) + 1 : 1,
                UserName = username,
                Password = password
            });

            string appDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            string filePath = Path.Combine(appDataDirectory, "users.json");

            File.WriteAllText(filePath, JsonConvert.SerializeObject(userList));
        }

    }
}
