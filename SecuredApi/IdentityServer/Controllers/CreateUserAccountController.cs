using System;
using System.Web.Mvc;
using IdentityServer.Models;
using IdentityServer.UserStore;
using IdentityServer.UserStore.Model;
using IdentityServer3.Core;

namespace IdentityServer.Controllers
{
    public class CreateUserAccountController : Controller
    {
        [HttpGet]
        public ActionResult Index(string signin)
        {
            return View(new CreateUserAccountModel());
        }

        [HttpPost]
        public ActionResult Index(string signin, CreateUserAccountModel model)
        {
            CreateAccount(model);
            return Redirect($"~/core/{Constants.RoutePaths.Login}?signin={signin}");
        }

        private void CreateAccount(CreateUserAccountModel model)
        {
            if (!ModelState.IsValid) return;

            var newUser = new User
            {
                Subject = Guid.NewGuid().ToString(),
                IsActive = true,
                UserName = model.UserName,
                Password = model.Password
            };

            newUser.UserClaims.Add(new UserClaim()
            {
                Id = Guid.NewGuid().ToString(),
                Subject = newUser.Subject,
                ClaimType = Constants.ClaimTypes.GivenName,
                ClaimValue = model.FirstName
            });

            newUser.UserClaims.Add(new UserClaim()
            {
                Id = Guid.NewGuid().ToString(),
                Subject = newUser.Subject,
                ClaimType = Constants.ClaimTypes.FamilyName,
                ClaimValue = model.LastName
            });

            newUser.UserClaims.Add(new UserClaim()
            {
                Id = Guid.NewGuid().ToString(),
                Subject = newUser.Subject,
                ClaimType = Constants.ClaimTypes.Email,
                ClaimValue = model.Email
            });

            newUser.UserClaims.Add(new UserClaim()
            {
                Id = Guid.NewGuid().ToString(),
                Subject = newUser.Subject,
                ClaimType = "role",
                ClaimValue = model.Role
            });

            Save(newUser);
        }

        private void Save(User newUser) =>
            UserRepositoryFactory.Create().SaveUser(newUser);
    }
}