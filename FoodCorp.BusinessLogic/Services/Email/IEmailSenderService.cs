namespace FoodCorp.BusinessLogic.Services.Email;

public interface IEmailSenderService
{
    Task SendEmailAsync(string email, string subject, string message);
}