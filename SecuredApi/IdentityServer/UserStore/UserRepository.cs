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
            var fileSystem = new PhysicalFileSystem("");

            IFileInfo fileInfo;
            if (!fileSystem.TryGetFileInfo(_jsonFileLocation, out fileInfo))
            {
                throw new InvalidOperationException($"Cannot find user store file: {_jsonFileLocation}");
            }

            var json = File.ReadAllText(fileInfo.PhysicalPath);
            var result = JsonConvert.DeserializeObject<List<User>>(json);

            _users = result.ToList();
        }

        public Task<User> GetUserAsync(string userName, string password)
        {
            var user = _users.FirstOrDefault(u => u.UserName == userName && u.Password == password);
            return Task.FromResult(user);
        }
    }
}