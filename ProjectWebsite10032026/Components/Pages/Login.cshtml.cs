using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class AccountLoginModel : PageModel
{
    private readonly SignInManager<IdentityUser> _signInManager;
    public AccountLoginModel(SignInManager<IdentityUser> s) => _signInManager = s;

    [BindProperty] public string Email { get; set; } = "";
    [BindProperty] public string Password { get; set; } = "";

    public async Task<IActionResult> OnPostAsync()
    {
        var result = await _signInManager.PasswordSignInAsync(
            Email, Password, isPersistent: false, lockoutOnFailure: false);

        return result.Succeeded
            ? Redirect("/")
            : Redirect("/login?Error=1");
    }
}