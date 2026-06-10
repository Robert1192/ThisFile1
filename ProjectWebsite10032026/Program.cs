using Microsoft.EntityFrameworkCore;
using ProjectWebsite10032026.Components;
using ProjectWebsite10032026.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register DbContext factory for Blazor server components (prevents concurrent DbContext use)
builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Ensure Downloads table has FileName and FilePath columns (add if missing)
using (var scope = app.Services.CreateScope())
{
    var factory = scope.ServiceProvider.GetRequiredService<Microsoft.EntityFrameworkCore.IDbContextFactory<ProjectWebsite10032026.Data.AppDbContext>>();
    using var db = factory.CreateDbContext();
    var conn = db.Database.GetDbConnection();
    try
    {
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Downloads')
BEGIN
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Downloads' AND COLUMN_NAME = 'FileName')
        ALTER TABLE Downloads ADD FileName nvarchar(max) NULL;
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Downloads' AND COLUMN_NAME = 'FilePath')
        ALTER TABLE Downloads ADD FilePath nvarchar(max) NULL;
END";
        cmd.ExecuteNonQuery();
    }
    finally
    {
        conn.Close();
    }
}

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();