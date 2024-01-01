using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ProjeOdevi.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AdminAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Check if the user is authenticated and has the "Type" claim with a value of "Admin"
            if (!context.HttpContext.User.Identity.IsAuthenticated ||
                !context.HttpContext.User.HasClaim(c => c.Type == "Type" && c.Value == "Admin"))
            {
                // Redirect to an unauthorized page or return a 403 Forbidden result
                context.Result = new ForbidResult();
            }
        }
    }
}
