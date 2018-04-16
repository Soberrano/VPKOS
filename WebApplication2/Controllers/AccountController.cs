using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Threading.Tasks;

using DarkSide;
using System.Net.Mail;
using System.Text;
using System.Web;
using WebApplication2.Models;
using WebApplication2.Controllers.Abstract;

namespace WebApplication2.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : BaseApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<IHttpActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return WrapError("Логин / пароль не введены.");
                //return BadRequest(ModelState);
            }

            // если логин содержит собаку, то проверяем, как email
            if (model.Login.Contains("@"))
            {
                User emailUser = await UserManager.FindByEmailAsync(model.Login);

                if (emailUser != null)
                {
                    model.Login = emailUser.UserName;
                }
            }

            User user = await UserManager.FindAsync(model.Login, model.Password);

            if (user == null)
            {
                return WrapError("Логин / пароль введены не верно.");
            }

            ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            AuthManager.SignOut();
            AuthManager.SignIn(new AuthenticationProperties { IsPersistent = false }, ident);

            return WrapSuccess(await CreateUserProfile(user));
        }

        [HttpPost]
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            AuthManager.SignOut();
            return WrapSuccess(null);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("Registration")]
        public async Task<IHttpActionResult> Registration(RegistrationModel model)
        {
            User user = new Models.User() { UserName = model.Login };
            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await SignInAsync(user, false);
            }
            return WrapSuccess(await CreateUserProfile(user));
        }

        public async Task SignInAsync(User user, bool isPersistent)
        {
            AuthManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetCurrentUser")]
        public async Task<IHttpActionResult> GetCurrentUser()
        {
            //if (!AuthManager.User.Identity.IsAuthenticated) SetResponseMessage(ApiResponseWrap.MessageType.success, "Пользователь не авторизован");
            return WrapSuccess(await CreateUserProfile(await UserManager.FindByNameAsync(AuthManager.User.Identity.Name)));
        }

        private async Task<UserProfile> CreateUserProfile(User user)
        {
            if (user != null)
            {
                UserProfile profile = new UserProfile(user);
                return profile;
            }

            return null;
        }
    }
}