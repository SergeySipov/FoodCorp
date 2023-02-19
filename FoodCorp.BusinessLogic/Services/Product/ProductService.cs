using FoodCorp.BusinessLogic.Models;
using FoodCorp.DataAccess.Repositories.ProductRepository;
using FoodCorp.DataAccess.UnitOfWork;
using Mapster;
using MapsterMapper;

namespace FoodCorp.BusinessLogic.Services.Product;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public ProductService(IUnitOfWork unitOfWork, 
        IMapper mapper, 
        IProductRepository productRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<List<ProductModel>> GetAllProductsAsync()
    {
        var productDataModels = _productRepository.GetAllProductsAsync();

        var productModels = new List<ProductModel>();
        await foreach (var productDataModel in productDataModels)
        {
            var productModel = productDataModel.Adapt<ProductModel>();
            productModels.Add(productModel);
        }//попробовать вместо этой хуйни tolist и List<UserDto> destinationList = sourceList.Adapt<List<UserDto>>();

        return productModels;
    }

    public async Task<ProductModel> GetProductByIdAsync(int productId)
    {
        var productDataModel = await _productRepository.GetFullProductInfoByIdAsync(productId);
        var productModel = productDataModel.Adapt<ProductModel>();
        
        return productModel;
    }
}