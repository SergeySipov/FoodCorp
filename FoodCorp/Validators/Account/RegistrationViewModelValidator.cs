using FluentValidation;
using FoodCorp.API.Constants.Resources;
using FoodCorp.API.ViewModels.Account;
using FoodCorp.Configuration.Model.AppSettings;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace FoodCorp.API.Validators.Account;

public class RegistrationViewModelValidator : AbstractValidator<RegistrationViewModel>
{
    public RegistrationViewModelValidator(IOptions<IdentitySecuritySettings> securitySettings,
        IStringLocalizer stringLocalizer)
    {
        RuleFor(r => r.Email)
            .EmailAddress();

        RuleFor(r => r.UserName)
            .MinimumLength(1);

        RuleFor(r => r.Name)
            .MinimumLength(1);

        RuleFor(r => r.Password)
            .MinimumLength(securitySettings.Value.RequiredLength)
            .WithMessage(string.Format(stringLocalizer[ValidatorsResourcesKeyConstants.PasswordLengthError], securitySettings.Value.RequiredLength));

        RuleFor(r => r.PhoneNumber)
            .Matches(@"((?:[0-9]\-?){6,14}[0-9]$)|((?:[0-9]\x20?){6,14}[0-9]$)")
            .NotEmpty()
            .WithMessage(stringLocalizer[ValidatorsResourcesKeyConstants.PhoneNumberError]);

        RuleFor(r => r.Surname)
            .MinimumLength(1);
    }
}
