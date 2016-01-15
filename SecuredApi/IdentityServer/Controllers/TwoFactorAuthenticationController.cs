using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using IdentityServer.Models;
using IdentityServer.Services;
using IdentityServer3.Core.Extensions;

namespace IdentityServer.Controllers
{
    public class TwoFactorAuthenticationController : PartialLoginControllerBase
    {
        public async Task<ActionResult> Index()
        {
            await EnsurePartialSignedUserFound();

            return View(new TwoFactorAuthenticationModel());
        }

        [HttpPost]
        public async Task<ActionResult> Index(TwoFactorAuthenticationModel model)
        {
            var partialSignInUser = await EnsurePartialSignedUserFound();

            if (!ModelState.IsValid) return View();

            return await ValidateCode(model, partialSignInUser);
        }

        private async Task<ActionResult> ValidateCode(TwoFactorAuthenticationModel model,
            ClaimsIdentity partialSignInUser)
        {
            var twoFactorTokenService = new TwoFactorTokenService();

            var codeValid = twoFactorTokenService.VerifyTwoFactorCodeFor(
                partialSignInUser.GetSubjectId(), model.Code);
            if (codeValid)
            {
                return Redirect(await GetOwinContext().Environment.GetPartialLoginResumeUrlAsync());
            }

            return View("This code is invalid.");
        }
    }
}