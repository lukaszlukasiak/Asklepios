using System.Security.Claims;

namespace Asklepios.Web.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static long? GetUserId(this ClaimsPrincipal user)
        {
            Claim claim=user.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
            {
                return null;
            }
            else
            {
                if (long.TryParse(claim.Value, out long id))
                {
                    return id;
                }
                else
                {
                    return null;
                }                              
            }
        }

    }
}
