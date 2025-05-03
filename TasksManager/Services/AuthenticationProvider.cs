using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using TasksManager.Models;

namespace TasksManager.Services
{
    public class AuthenticationProvider : AuthenticationStateProvider
    {
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Check the user's authentication status (e.g., from local storage or a cookie)
            // For example, you might check if a JWT token exists in local storage
            var user = new ClaimsPrincipal(); // Default to an anonymous user

            // Logic to determine if the user is authenticated
            // If authenticated, create a ClaimsPrincipal with user info
            // For example:
            // var claims = new List<Claim> { new Claim(ClaimTypes.Name, "username") };
            // user = new ClaimsPrincipal(new ClaimsIdentity(claims, "custom"));


            return Task.FromResult(new AuthenticationState(user));
        }

        public void NotifyUserAuthentication(User model)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.Name, model.UserName!),
            new Claim(ClaimTypes.Email, model.Email!),
            new Claim("Tasks", model.UserTasks.ToString()!),
            new Claim("Documents", model.UserDocument.ToString()!)
        }, "custom"));

            var authenticationState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authenticationState);
        }

        public void NotifyUserLogout()
        {
            var anonymousUser = new ClaimsPrincipal();
            var authenticationState = Task.FromResult(new AuthenticationState(anonymousUser));
            NotifyAuthenticationStateChanged(authenticationState);
        }
    }
}
