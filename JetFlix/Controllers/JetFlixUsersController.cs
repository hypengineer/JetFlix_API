using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JetFlix_API.Data;
using JetFlix_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace JetFlix_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JetFlixUsersController : ControllerBase
    {

        public struct LoginModel
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        private readonly SignInManager<JetFlixUser> _signInManager;

        public JetFlixUsersController(SignInManager<JetFlixUser> signInManager)
        {
            _signInManager = signInManager;
        }

        // GET: api/JetFlixUsers
        [HttpGet]
        [Authorize("Administrator")]
        public ActionResult<List<JetFlixUser>> GetUsers(bool includePassive = true)
        {
            IQueryable<JetFlixUser> users = _signInManager.UserManager.Users;

            if (includePassive == false)
            {
                users = users.Where(u => u.Passive == false);
            }

            return users.AsNoTracking().ToList();
        }

        // GET: api/JetFlixUsers/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<JetFlixUser> GetJetFlixUser(long id)
        {
            JetFlixUser? jetFlixUser = null;

            if (User.IsInRole("Administrator") == false)
            {

                if (User.FindFirstValue(ClaimTypes.NameIdentifier) != id.ToString())  // Eğer id giriş yapan kullanıcı Admin değilse kendi bilgilerini çekebiliriz
                {
                    return Unauthorized();
                }

            }
            jetFlixUser = _signInManager.UserManager.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefault();



            if (jetFlixUser == null)
            {
                return NotFound();
            }
            return jetFlixUser;






        }

        // PUT: api/JetFlixUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Authorize]
        public ActionResult PutJetFlixUser(JetFlixUser jetFlixUser)
        {
            JetFlixUser? user = null;

            if (User.IsInRole("CustomerRepresentative") == false)
            {

                if (User.FindFirstValue(ClaimTypes.NameIdentifier) != jetFlixUser.Id.ToString())  // Eğer id giriş yapan kullanıcı Admin değilse kendi bilgilerini çekebiliriz
                {
                    return Unauthorized();
                }

            }
            user = _signInManager.UserManager.Users.Where(u => u.Id == jetFlixUser.Id).FirstOrDefault(); // Burada AsNoTracking yapamayız. Çünkü oku ve unut diyemeyiz, unutmaması gerekiyor aşağıda değiştirmek için

            if (user == null)
            {
                return NotFound();
            }
            user.Name = jetFlixUser.Name;
            user.PhoneNumber = jetFlixUser.PhoneNumber;
            user.UserName = jetFlixUser.UserName;
            user.BirthDate = jetFlixUser.BirthDate;
            user.Email = jetFlixUser.Email;

            _signInManager.UserManager.UpdateAsync(user).Wait();
            return Ok();



        }

        // POST: api/JetFlixUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<string> PostJetFlixUser(JetFlixUser jetFlixUser, string Password)
        {
            if (User.Identity!.IsAuthenticated == true)
            {
                return BadRequest();
            }
            //_signInManager.UserManager.CreateAsync(jetFlixUser).Wait();
            IdentityResult identityResult = _signInManager.UserManager.CreateAsync(jetFlixUser, Password).Result;

            if (identityResult != IdentityResult.Success)
            {
                return identityResult.Errors.FirstOrDefault()!.Description;
            }



            return Ok();
        }

        // DELETE: api/JetFlixUsers/5
        [HttpDelete("{id}")]
        public ActionResult DeleteJetFlixUser(long id)
        {
            JetFlixUser? user = null;

            if (User.IsInRole("CustomerRepresentative") == false)
            {

                if (User.FindFirstValue(ClaimTypes.NameIdentifier) != id.ToString())  // Eğer id giriş yapan kullanıcı Admin değilse kendi bilgilerini çekebiliriz
                {
                    return Unauthorized();
                }


            }
            user = _signInManager.UserManager.Users.Where(u => u.Id == id).FirstOrDefault(); // Burada AsNoTracking yapamayız. Çünkü oku ve unut diyemeyiz, unutmaması gerekiyor aşağıda değiştirmek için

            if (user == null)
            {
                return NotFound();
            }
            user.Passive = true;

            _signInManager.UserManager.UpdateAsync(user).Wait();
            return Ok();


        }
        [HttpPost("Login")]
        public bool Login(LoginModel loginModel)
        {
            Microsoft.AspNetCore.Identity.SignInResult signInResult;
            JetFlixUser jetFlixUser = _signInManager.UserManager.FindByNameAsync(loginModel.UserName).Result;


            if (jetFlixUser == null)
            {
                return false;
            }

            if (jetFlixUser.Passive == true)
            {
                return false;
            }
            signInResult = _signInManager.PasswordSignInAsync(jetFlixUser, loginModel.Password, false, false).Result;

            return signInResult.Succeeded;
        }

        
    }
}
