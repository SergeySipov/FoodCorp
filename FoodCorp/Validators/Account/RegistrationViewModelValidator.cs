using FluentValidation;
using FoodCorp.API.ViewModels.Account;
using FoodCorp.Configuration.Model.AppSettings;
using Microsoft.Extensions.Options;

namespace FoodCorp.API.Validators.Account;

public class RegistrationViewModelValidator : AbstractValidator<RegistrationViewModel>
{
    public RegistrationViewModelValidator(IOptions<SecuritySettings> securitySettings)
    {
        RuleFor(r => r.Email)
            .EmailAddress();

        RuleFor(r => r.UserName)
            .MinimumLength(1);

        RuleFor(r => r.Name)
            .MinimumLength(1);

        RuleFor(l => l.Password)
            .MinimumLength(securitySettings.Value.RequiredLength)
            .WithMessage($"Password must contains minimum {securitySettings.Value.RequiredLength} symbols");

        RuleFor(r => r.PhoneNumber)
            .NotEmpty();

        RuleFor(r => r.Surname)
            .MinimumLength(1);
    }
}
