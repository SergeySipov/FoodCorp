using System.Collections.Generic;
using System.Linq;
using FoodCorp.BusinessLogic.Mappers.ProductMapper;
using FoodCorp.BusinessLogic.Models;
using FoodCorp.DataAccess.DataModels;

namespace FoodCorp.BusinessLogic.Mappers.ProductMapper
{
    public partial class ProductMapper : IProductMapper
    {
        public ProductModel MapTo(ProductDataModel p1)
        {
            return p1 == null ? null : new ProductModel(p1.Name, p1.Category, p1.Price, p1.Description);
        }
        public ProductModel MapTo(ProductDataModel p2, ProductModel p3)
        {
            return p2 == null ? null : new ProductModel(p2.Name, p2.Category, p2.Price, p2.Description);
        }
        public IEnumerable<ProductModel> MapTo(IEnumerable<ProductDataModel> p4)
        {
            return p4 == null ? null : p4.Select<ProductDataModel, ProductModel>(funcMain1);
        }
        
        private ProductModel funcMain1(ProductDataModel p5)
        {
            return p5 == null ? null : new ProductModel(p5.Name, p5.Category, p5.Price, p5.Description);
        }
    }
}