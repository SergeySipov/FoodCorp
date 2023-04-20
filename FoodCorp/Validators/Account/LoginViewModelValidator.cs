using FluentValidation;
using FoodCorp.API.Constants.Resources;
using FoodCorp.API.ViewModels.Account;
using FoodCorp.Configuration.Model.AppSettings;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace FoodCorp.API.Validators.Account;

public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
{
    public LoginViewModelValidator(IOptions<IdentitySecuritySettings> securitySettings,
        IStringLocalizer<LoginViewModelValidator> stringLocalizer)
    {
        RuleFor(l => l.Email)
            .EmailAddress()
            .WithMessage(stringLocalizer[ValidatorsResourcesKeyConstants.EmailError]);

        RuleFor(l => l.Password)
            .MinimumLength(securitySettings.Value.RequiredLength)
            .WithMessage(string.Format(stringLocalizer[ValidatorsResourcesKeyConstants.PasswordLengthError], securitySettings.Value.RequiredLength));
    }
}
