using FluentValidation;
using FoodCorp.API.ViewModels.Account;
using FoodCorp.Configuration.Model.AppSettings;
using Microsoft.Extensions.Options;

namespace FoodCorp.API.Validators.Account;

public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
{
    public LoginViewModelValidator(IOptions<SecuritySettings> securitySettings)
    {
        RuleFor(l => l.Email)
            .EmailAddress()
            .WithMessage("Invalid email");

        RuleFor(l => l.Password)
            .MinimumLength(securitySettings.Value.RequiredLength)
            .WithMessage($"Password must contains minimum {securitySettings.Value.RequiredLength} symbols");
    }
}
