namespace FoodCorp.DataAccess.UnitOfWork;

public interface IUnitOfWork
{
    public Task<int> CommitAsync();
    public int Commit();
}