using FoodCorp.BusinessLogic.Models;
using FoodCorp.DataAccess.DataModels;
using Mapster;

namespace FoodCorp.BusinessLogic.Mappers.ProductMapper;

[Mapper]
public interface IProductMapper
{
    ProductModel MapTo(ProductDataModel productDataModel);
    ProductModel MapTo(ProductDataModel product, ProductModel productModel);
    IEnumerable<ProductModel> MapTo(IEnumerable<ProductDataModel> productDataModels);
}