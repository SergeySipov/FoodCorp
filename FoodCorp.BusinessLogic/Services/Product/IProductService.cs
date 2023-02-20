using FoodCorp.BusinessLogic.Models;

namespace FoodCorp.BusinessLogic.Services.Product;

public interface IProductService
{
    Task<List<ProductModel>> GetAllProductsAsync();
    Task<ProductModel> GetProductByIdAsync(int productId);
}