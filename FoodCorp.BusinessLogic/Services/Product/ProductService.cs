using FoodCorp.BusinessLogic.Mappers.ProductMapper;
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
    private readonly IProductMapper _productMapper;

    public ProductService(IUnitOfWork unitOfWork, 
        IMapper mapper, 
        IProductRepository productRepository, 
        IProductMapper productMapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _productRepository = productRepository;
        _productMapper = productMapper;
    }

    public async Task<List<ProductModel>> GetAllProductsAsync()
    {
        var productDataModels = await _productRepository.GetAllProductsAsync().ToListAsync();
        var productModels = _productMapper.MapTo(productDataModels);
        
        return productModels.ToList();
    }

    public async Task<ProductModel> GetProductByIdAsync(int productId)
    {
        var productDataModel = await _productRepository.GetFullProductInfoByIdAsync(productId);
        var productModel = productDataModel.Adapt<ProductModel>();
        
        return productModel;
    }
}