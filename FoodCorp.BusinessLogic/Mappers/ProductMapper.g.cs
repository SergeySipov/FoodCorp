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
            return p1 == null ? null : new ProductModel()
            {
                Name = p1.Name,
                Category = p1.Category,
                Price = p1.Price,
                Description = p1.Description
            };
        }
        public ProductModel MapTo(ProductDataModel p2, ProductModel p3)
        {
            if (p2 == null)
            {
                return null;
            }
            ProductModel result = p3 ?? new ProductModel();
            
            result.Name = p2.Name;
            result.Category = p2.Category;
            result.Price = p2.Price;
            result.Description = p2.Description;
            return result;
            
        }
        public IEnumerable<ProductModel> MapTo(IEnumerable<ProductDataModel> p4)
        {
            return p4 == null ? null : p4.Select<ProductDataModel, ProductModel>(funcMain1);
        }
        
        private ProductModel funcMain1(ProductDataModel p5)
        {
            return p5 == null ? null : new ProductModel()
            {
                Name = p5.Name,
                Category = p5.Category,
                Price = p5.Price,
                Description = p5.Description
            };
        }
    }
}