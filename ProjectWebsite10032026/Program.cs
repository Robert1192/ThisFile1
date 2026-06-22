using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectWebsite10032026.Data;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // =========================
        // BLazor
        // =========================
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();

        builder.Services.AddHttpContextAccessor();

        // =========================
        // HttpClient (FIXED LOCATION)
        // =========================
        builder.Services.AddScoped(sp =>
        {
            var nav = sp.GetRequiredService<NavigationManager>();
            return new HttpClient
            {
                BaseAddress = new Uri(nav.BaseUri)
            };
        });

        // =========================
        // DB
        // =========================
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("Missing DefaultConnection in appsettings.json");

        builder.Services.AddDbContextFactory<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        // =========================
        // Identity
        // =========================
        builder.Services.AddDefaultIdentity<IdentityUser>(options =>
        {
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireDigit = false;
            options.SignIn.RequireConfirmedAccount = false;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<AppDbContext>();

        builder.Services.AddAuthorization();
        builder.Services.AddControllers();

        var app = builder.Build();

        // =========================
        // SEED USERS
        // =========================
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            await SeedUsersAsync(userManager, roleManager);
        }

        // =========================
        // PIPELINE
        // =========================
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        await app.RunAsync();
    }

    // =========================
    // SEED METHOD (FIXED)
    // =========================
    private static async Task SeedUsersAsync(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        // =========================
        // ROLE
        // =========================
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        // =========================
        // ADMIN
        // =========================
        var adminEmail = "admin@example.com";
        var adminPassword = "Admin@Secure#2026!X9";

        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            await userManager.CreateAsync(adminUser, adminPassword);
        }
        else
        {
            var token = await userManager.GeneratePasswordResetTokenAsync(adminUser);
            await userManager.ResetPasswordAsync(adminUser, token, adminPassword);
        }

        if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }

        Console.WriteLine($"Admin: {adminEmail} / {adminPassword}");

        // =========================
        // NORMAL USER
        // =========================
        var userEmail = "user@test.com";
        var userPassword = "User@Strong#2026!Q7";

        var normalUser = await userManager.FindByEmailAsync(userEmail);

        if (normalUser == null)
        {
            normalUser = new IdentityUser
            {
                UserName = userEmail,
                Email = userEmail,
                EmailConfirmed = true
            };

            await userManager.CreateAsync(normalUser, userPassword);
        }
        else
        {
            var token = await userManager.GeneratePasswordResetTokenAsync(normalUser);
            await userManager.ResetPasswordAsync(normalUser, token, userPassword);
        }

        Console.WriteLine($"User: {userEmail} / {userPassword}");
    }
}