using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Route("Account")]
public class AuthController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthController(SignInManager<IdentityUser> signInManager)
        => _signInManager = signInManager;

    [HttpPost("Login")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Login(string email, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(
            email, password, isPersistent: false, lockoutOnFailure: false);

        return result.Succeeded
            ? Redirect("/")
            : Redirect("/login?Error=1");
    }

    [HttpPost("Logout")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Redirect("/login");
    }
}