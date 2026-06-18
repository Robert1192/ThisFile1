using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;

namespace ProjectWebsite10032026.Data
{
    public class IdentityAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private ClaimsPrincipal _currentUser = new ClaimsPrincipal(new ClaimsIdentity());

        public IdentityAuthenticationStateProvider(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();

            try
            {
                using var scope = _scopeFactory.CreateScope();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var signInManager = scope.ServiceProvider.GetRequiredService<SignInManager<IdentityUser>>();

                // Get the current user from the HttpContext
                var context = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext;
                if (context?.User?.Identity?.IsAuthenticated == true)
                {
                    var user = await userManager.GetUserAsync(context.User);
                    if (user != null)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id),
                            new Claim(ClaimTypes.Name, user.UserName ?? ""),
                            new Claim(ClaimTypes.Email, user.Email ?? "")
                        };

                        // Add roles
                        var roles = await userManager.GetRolesAsync(user);
                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }

                        identity = new ClaimsIdentity(claims, "Identity");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error if you have logging
                Console.WriteLine($"Error getting authentication state: {ex.Message}");
            }

            _currentUser = new ClaimsPrincipal(identity);
            return new AuthenticationState(_currentUser);
        }

        public void NotifyUserAuthentication(string userId)
        {
            // This is called after successful login
            var authenticatedUser = GetAuthenticationStateAsync().GetAwaiter().GetResult();
            NotifyAuthenticationStateChanged(Task.FromResult(authenticatedUser));
        }

        public void NotifyUserLogout()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));
        }
    }
}