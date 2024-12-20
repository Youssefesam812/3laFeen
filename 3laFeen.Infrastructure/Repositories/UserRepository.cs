using _3laFeen.Domain.Entities;
using _3laFeen.Domain.IRepositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace _3laFeen.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserRepository(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager; // Inject SignInManager
        }

        public async Task<string> RegisterAsync(string email, string password)
        {
            try
            {
                var user = new IdentityUser { UserName = email, Email = email };
                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    return "User registered successfully";
                }

                // Return a message if registration failed
                return $"Registration failed: {string.Join(", ", result.Errors.Select(e => e.Description))}";
            }
            catch (Exception ex)
            {
                return $"Error occurred: {ex.Message}";
            }
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            try
            {
                // Use SignInManager for login
                var result = await _signInManager.PasswordSignInAsync(email, password, false, false);

                if (result.Succeeded)
                {
                    return "User logged in successfully";
                }

                // Return a message if login failed
                return "Invalid login attempt";
            }
            catch (Exception ex)
            {
                return $"Error occurred: {ex.Message}";
            }
        }

    }
}
