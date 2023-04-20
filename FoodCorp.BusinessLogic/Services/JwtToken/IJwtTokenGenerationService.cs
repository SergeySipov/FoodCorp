namespace FoodCorp.BusinessLogic.Services.JwtToken;

public interface IJwtTokenGenerationService
{
    string GenerateJwt(int userId, string userEmail, int userPermissionsBitMask);
}