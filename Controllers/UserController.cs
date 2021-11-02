using BookStoreBackEnd.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace BookStoreBackEnd.Controllers
{
    
    public class UserController : ApiController
    {
        [Route("api/User/Register")]
        [HttpPost]
        [AllowAnonymous]
        public IdentityResult Register(UserModel account)
        {
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var manager = new UserManager<ApplicationUser>(userStore);
            var user = new ApplicationUser() { UserName = account.UserName, Email = account.Email };
            IdentityResult result = manager.Create(user, account.Password);
            var currentUser = manager.FindByName(user.UserName);
            manager.AddToRoles(user.Id, account.Roles);
            return result;
        }

        [HttpGet]
        [Authorize]
        [Route("api/GetUserClaims")]
        public UserModel GetUserClaims()
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identityClaims.Claims;
            UserModel model = new UserModel()
            {
                UserName = identityClaims.FindFirst("Username").Value,
                Email = identityClaims.FindFirst("Email").Value,



                LoggedOn = identityClaims.FindFirst("LoggedOn").Value,
                Id = identityClaims.FindFirst("Id").Value
            };
            return model;
        }
    }
}
