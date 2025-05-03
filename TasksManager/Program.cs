using TasksManager.Components;
using TasksManager.SharedDataServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using TasksManager.Data;
using TasksManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Security.AccessControl;
using TasksManager.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddQuickGridEntityFrameworkAdapter();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme; // For cookie authentication
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme; // For external sign-in
})
.AddCookie(IdentityConstants.ApplicationScheme)
.AddCookie(IdentityConstants.ExternalScheme)
// .AddCookie( options => 
// {
//     options.Cookie.Name = "auth_cookie";
//     options.LoginPath = "/login";
//     // options.Cookie.MaxAge = TimeSpan.FromDays(30);
// })
.AddGoogle(options => 
{
    options.ClientId =  builder.Configuration["Authentication:Google:ClientId"]!;
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
    options.Scope.Add("email");
    options.Scope.Add("profile");
});

var connectionString = builder.Configuration.GetConnectionString("default") 
    ?? throw new NullReferenceException("Connection string 'default' not found in configuration");

builder.Services.AddDbContext<TasksManagerDbContext>(options =>
{
    options.UseSqlServer("Data Source=DESKTOP-DRLUK05\\SQLEXPRESS;Initial Catalog=TaskManagerDb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<User>(options => 
{
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
})
    .AddEntityFrameworkStores<TasksManagerDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

// builder.Services.AddDefaultIdentity<User>(options => 
// {
//     options.SignIn.RequireConfirmedAccount = false; // Disable email confirmation for sign-in
//     options.SignIn.RequireConfirmedEmail = false;
//     options.Password.RequireDigit = false; // Set to true if you want to require digits
//     options.Password.RequireLowercase = false; // Set to true if you want to require lowercase letters
//     options.Password.RequireUppercase = false; // Set to true if you want to require uppercase letters
//     options.Password.RequireNonAlphanumeric = false; // Set to true if you want to require non-alphanumeric characters
//     options.Password.RequiredLength = 0; // Set the minimum length as needed
//     options.Password.RequiredUniqueChars = 0;
// } )
//     .AddEntityFrameworkStores<TasksManagerDbContext>()
//     .AddSignInManager();


builder.Services.AddSingleton<SharedDataService>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProvider>();


    
builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(options => options.DetailedErrors = true);




// Register HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Register SignInManager
// builder.Services.AddScoped<SignInManager<User>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
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


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(TasksManager.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
// app.MapAdditionalIdentityEndpoints();

app.Run();
