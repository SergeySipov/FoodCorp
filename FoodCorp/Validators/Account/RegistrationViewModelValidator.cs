using System.Text.RegularExpressions;
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
            .NotEmpty()
            .NotNull().WithMessage("Phone Number is required.")
            .MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.")
            .MaximumLength(20).WithMessage("PhoneNumber must not exceed 50 characters.")
            .Matches(new Regex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}")).WithMessage("PhoneNumber not valid");

        RuleFor(r => r.Surname)
            .MinimumLength(1);
    }
}
