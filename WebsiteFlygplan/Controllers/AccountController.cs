using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebsiteFlygplan.Models;
using WebsiteFlygplan.Service;

namespace WebsiteFlygplan.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;

        public AccountController()
        {

        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }

            set
            {
                _userManager = value;
            }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }

            set
            {
                _signInManager = value;
            }
        }

        public IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }


        // GET: Account
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginViewModel loginView)
        {
            if(ModelState.IsValid)
            {
                var result = await SignInManager.PasswordSignInAsync(loginView.Email, loginView.Password, shouldLockout: false, isPersistent: false);
                switch(result)
                {
                    case SignInStatus.Success:
                        return RedirectToAction("Index", "Home");
                    case SignInStatus.LockedOut:
                        return Content("You have been locked out");
                    case SignInStatus.Failure:
                        return Content("You have reached the maximum attempts to login");
                    case SignInStatus.RequiresVerification:
                    default:
                        return Content("This login requires verification");
                }
            }

            //Modelstate failed
            return View(loginView);
        }

        // GET : Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST : /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<ActionResult> Register(RegisterViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var user = new ApplicationUser { Email = viewModel.Email, UserName = viewModel.UserName };
                var result = await UserManager.CreateAsync(user, viewModel.Password);
                
                if(result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(viewModel);
        }
    }
}