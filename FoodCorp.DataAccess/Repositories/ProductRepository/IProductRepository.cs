using FoodCorp.DataAccess.DataModels;

namespace FoodCorp.DataAccess.Repositories.ProductRepository;

public interface IProductRepository
{
    IAsyncEnumerable<ProductDataModel> GetAllProductsAsync();
    Task<ProductDataModel> GetFullProductInfoByIdAsync(int productId);
}