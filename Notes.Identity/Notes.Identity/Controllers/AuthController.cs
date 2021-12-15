using System;
using System.Threading.Tasks;

using IdentityServer4.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Notes.Identity.Models;
using Notes.Identity.ViewModels;

namespace Notes.Identity.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IIdentityServerInteractionService _interactionService;

        public AuthController(
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IIdentityServerInteractionService interactionService)
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _interactionService = interactionService ?? throw new ArgumentNullException(nameof(interactionService));
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var loginVM = new LoginViewModel()
            {
                ReturnUrl = returnUrl
            };
            return View(loginVM);
        }

        [HttpPost]
        public async ValueTask<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            //var user = await _userManager.FindByNameAsync(viewModel.Username);
            //if (user == null)
            //{
            //    ModelState.AddModelError(string.Empty, "User not found.");
            //    return View(viewModel);
            //}

            var result = await _signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, false, false);
            if (result.Succeeded)
            {
                return Redirect(viewModel.ReturnUrl);
            }

            ModelState.AddModelError(string.Empty, "Incorrect login or password.");
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            var registerVM = new RegisterViewModel()
            {
                ReturnUrl = returnUrl
            };
            return View(registerVM);
        }

        [HttpPost]
        public async ValueTask<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = new AppUser()
            {
                UserName = viewModel.Username
            };
            var result = await _userManager.CreateAsync(user: user, password: viewModel.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Redirect(viewModel.ReturnUrl);
            }

            ModelState.AddModelError(string.Empty, "An error occurred during regastration.");
            return View(viewModel);
        }

        [HttpGet]
        public async ValueTask<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);
            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }
    }
}
