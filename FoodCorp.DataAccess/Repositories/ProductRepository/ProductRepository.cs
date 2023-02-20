using FoodCorp.DataAccess.DatabaseContext;
using FoodCorp.DataAccess.DataModels;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace FoodCorp.DataAccess.Repositories.ProductRepository;

public class ProductRepository : IProductRepository
{
    private readonly FoodCorpDbContext _dbContext;
    private readonly IMapper _mapper;

    public ProductRepository(FoodCorpDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public IAsyncEnumerable<ProductDataModel> GetAllProductsAsync()
    {
        var taskWithProductDataModel = _dbContext.Products
            .ProjectToType<ProductDataModel>()
            .AsAsyncEnumerable();

        return taskWithProductDataModel;
    }

    public Task<ProductDataModel> GetFullProductInfoByIdAsync(int productId)
    {
        var taskWithProductDataModel = _dbContext.Products
            .ProjectToType<ProductDataModel>()
            .FirstOrDefaultAsync();

        return taskWithProductDataModel;
    }
}
