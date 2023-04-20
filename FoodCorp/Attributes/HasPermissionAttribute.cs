using FoodCorp.BusinessLogic.Constants;
using FoodCorp.DataAccess.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FoodCorp.API.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class HasPermissionAttribute : Attribute, IAuthorizationFilter
{
    private readonly int _permissionsBitMask;

    public HasPermissionAttribute(Permission permissions)
    {
        _permissionsBitMask = (int)permissions;
    }

    public HasPermissionAttribute(params Permission[] permissions)
    {
        if (permissions.Any())
        {
            _permissionsBitMask = (int)permissions.Aggregate((a, b) => a | b);
        }
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
        {
            return;
        }

        var httpContext = context.HttpContext;
        var identity = httpContext.User.Identity;
        if (identity == null || !identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var permissionsBitMaskClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimConstants.Permissions));
        if (permissionsBitMaskClaim == null)
        {
            context.Result = new ForbidResult();
            return;
        }

        if (int.TryParse(permissionsBitMaskClaim.Value, out var result))
        {
            if ((result & _permissionsBitMask) == _permissionsBitMask) //пермишены метода входят в множество пермишенов пользователя
            {
                return;
            }

            context.Result = new ForbidResult();
        }
    }
}
