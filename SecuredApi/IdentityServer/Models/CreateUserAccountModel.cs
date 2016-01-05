using System.Collections.Generic;
using System.Web.Mvc;

namespace IdentityServer.Models
{
    public class CreateUserAccountModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public IEnumerable<SelectListItem> Roles { get; } = new List<SelectListItem>()
        {
            new SelectListItem { Text = "Book access", Value = "Books"},
            new SelectListItem { Text = "Movie access", Value = "Movies"}
        };
    }
}