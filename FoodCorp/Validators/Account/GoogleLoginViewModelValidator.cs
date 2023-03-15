using FluentValidation;
using FoodCorp.API.ViewModels.Account;

namespace FoodCorp.API.Validators.Account;

public class GoogleLoginViewModelValidator : AbstractValidator<GoogleLoginViewModel>
{
    public GoogleLoginViewModelValidator()
    {
        RuleFor(g => g.IdToken)
            .NotEmpty();

        RuleFor(g => g.IdentityProviderId)
            .NotEmpty();

        RuleFor(g => g.ProviderKey)
            .NotEmpty();
    }
}