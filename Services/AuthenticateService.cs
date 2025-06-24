
using Microsoft.AspNetCore.Identity;

namespace ApiDeProdutos.Services
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthenticateService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            var resultado = await _signInManager.PasswordSignInAsync(email, password,
                false, lockoutOnFailure: false);

            return resultado.Succeeded;

        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> RegisterUser(string email, string password)
        {
            var appUsuario = new IdentityUser
            {
                UserName = email,
                Email = email,

            };


            var resultado = await _userManager.CreateAsync(appUsuario, password);

            if (resultado.Succeeded)
            {
                await _signInManager.SignInAsync(appUsuario, isPersistent: false);
            }
            return resultado.Succeeded;
        }
    }
}