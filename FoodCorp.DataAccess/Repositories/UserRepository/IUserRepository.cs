namespace FoodCorp.DataAccess.Repositories.UserRepository;

public interface IUserRepository
{
    Task<int> GetUserPermissionsBitMaskAsync(int userId);
}