using TasksManager.Components;
using TasksManager.SharedDataServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using TasksManager.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("default") 
    ?? throw new NullReferenceException("Connection string 'default' not found in configuration");

builder.Services.AddDbContext<TasksManagerDbContext>(options =>
{
    options.UseSqlServer("Data Source=DESKTOP-DRLUK05\\SQLEXPRESS;Initial Catalog=TaskHubDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
});

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<TasksManagerDbContext>();

// builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//     .AddEntityFrameworkStores<TasksManagerDbContext>()
//     .AddDefaultTokenProviders();


builder.Services.AddSingleton<SharedDataService>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
    
builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(options => options.DetailedErrors = true);

// Add Authentication services
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie( options => 
{
    // options.LoginPath = "/account/login";
    // options.AccessDeniedPath = "/Account/AccessDenied";
    // options.Cookie.Name = "auth_cookie";
    // options.LoginPath = "/login";
    // options.Cookie.MaxAge = TimeSpan.FromDays(30);
    // options.AccessDeniedPath = "/accessdenied";
})
.AddGoogle(options => 
{
    options.ClientId =  builder.Configuration["Authentication:Google:ClientId"]!;
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
    options.Scope.Add("email");
    options.Scope.Add("profile");
});

// builder.Services.AddCascadingAuthenticationState();
    
    
builder.Services.AddQuickGridEntityFrameworkAdapter();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    // Allow access to the login page without authentication
    if (!context.User.Identity.IsAuthenticated && 
        (context.Request.Path.StartsWithSegments("/login") || 
         context.Request.Path.StartsWithSegments("/signup")))
    {
        await next(); // Proceed to the next middleware (the login page)
        return;
    }

    // For all other paths, check if the user is authenticated
    if (!context.User.Identity.IsAuthenticated)
    {
        context.Response.Redirect("/login"); // Redirect to login
        return; // Stop further processing
    }

    await next(); // Proceed to the next middleware
});

// app.Use(async (context, next) =>
// {

//     if (!context.User.Identity.IsAuthenticated)
//     {
//         context.Response.Redirect("/login");
//         return;
//     }
//     await next();
    
// });

// app.MapFallbackToPage("/Account/Register", "/Account/Register");

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(TasksManager.Client._Imports).Assembly);

app.Run();
