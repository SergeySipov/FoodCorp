using System.Security.Authentication;
using Microsoft.AspNetCore.Identity;

namespace FoodCorp.BusinessLogic.Extensions
{
    public static class IdentityExtensions
    {
        private const string SignInFailureMessage = "Wrong login or password";

        public static void ThrowExceptionOnFailure(this IdentityResult result)
        {
            if (!result.Succeeded)
            {
                var errorsDescription = result.Errors.Select(_ => _.Description);
                var errors = string.Join(Environment.NewLine, errorsDescription);
                throw new AuthenticationException(errors);
            }
        }

        public static void ThrowExceptionOnFailure(this SignInResult result)
        {
            if (!result.Succeeded)
            {
                throw new AuthenticationException(SignInFailureMessage);
            }
        }
    }
}
