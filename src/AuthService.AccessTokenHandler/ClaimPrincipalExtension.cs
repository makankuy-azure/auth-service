using System.Security.Claims;

namespace AuthService.AccessTokenHandler
{
    public static class ClaimsPrincipalExtension
    {
        public static string GetUserId(this ClaimsPrincipal claims)
        {
            return claims?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
