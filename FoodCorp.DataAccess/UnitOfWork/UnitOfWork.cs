using FoodCorp.DataAccess.DatabaseContext;

namespace FoodCorp.DataAccess.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly FoodCorpDbContext _dbContext;

    public UnitOfWork(FoodCorpDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<int> CommitAsync()
    {
        return _dbContext.SaveChangesAsync();
    }

    public int Commit()
    {
        return _dbContext.SaveChanges();
    }
}
