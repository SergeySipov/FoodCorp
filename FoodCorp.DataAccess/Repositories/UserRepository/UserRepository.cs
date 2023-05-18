using FoodCorp.DataAccess.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace FoodCorp.DataAccess.Repositories.UserRepository;

public class UserRepository : IUserRepository
{
    private readonly FoodCorpDbContext _dbContext;

    public UserRepository(FoodCorpDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<int> GetUserPermissionsBitMaskAsync(int userId)
    {
        var userPermissionsBitMaskTask = _dbContext.UserPermissions
            .Where(u => u.UserId == userId)
            .Select(u => u.PermissionsBitMask)
            .FirstOrDefaultAsync();

        return userPermissionsBitMaskTask;
    }
}