using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.UserStore.Model;
using Microsoft.Owin.FileSystems;
using Newtonsoft.Json;

namespace IdentityServer.UserStore
{
    public class UserRepository : IUserRepository
    {
        private readonly string _jsonFileLocation;
        private List<User> _users; 

        public UserRepository(string jsonFileLocation)
        {
            _jsonFileLocation = jsonFileLocation;

            ReadUserStoreFile();
        }

        private void ReadUserStoreFile()
        {
            var jsonFile = GetJsonFile();
            if (jsonFile == null)
            {
                _users = new List<User>();
                return;
            }

            var json = File.ReadAllText(jsonFile.PhysicalPath);
            var result = JsonConvert.DeserializeObject<List<User>>(json);

            _users = result.ToList();
        }

        public Task<User> GetUserAsync(string subject)
        {
            var user = _users.FirstOrDefault(u =>
                string.Equals(u.Subject, subject, StringComparison.InvariantCultureIgnoreCase));

            return Task.FromResult(user);
        }

        public Task<User> GetUserAsync(string userName, string password)
        {
            var user = _users.FirstOrDefault(u => u.UserName == userName && u.Password == password);
            return Task.FromResult(user);
        }

        public void SaveUser(User newUser)
        {
            var jsonFile = GetJsonFile();
            if (jsonFile == null) return;

            _users.Add(newUser);
            var json = JsonConvert.SerializeObject(_users);
            File.WriteAllText(jsonFile.PhysicalPath, json);
        }

        public Task<User> GetUserForExternalProviderAsync(string loginProvider, string providerKey)
        {
            var user = _users
                .FirstOrDefault(u => u.UserLogins.Any(ul =>
                    string.Equals(ul.LoginProvider, loginProvider, StringComparison.InvariantCultureIgnoreCase) &&
                    string.Equals(ul.ProviderKey, providerKey, StringComparison.InvariantCultureIgnoreCase)));

            return Task.FromResult(user);
        }

        private IFileInfo GetJsonFile()
        {
            var fileSystem = new PhysicalFileSystem("");

            IFileInfo fileInfo;
            fileSystem.TryGetFileInfo(_jsonFileLocation, out fileInfo);

            return fileInfo;
        }
    }
}