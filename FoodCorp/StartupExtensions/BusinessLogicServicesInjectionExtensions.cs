using Mapster;
using MapsterMapper;
using System.Reflection;
using FoodCorp.BusinessLogic.Mappers.ProductMapper;
using FoodCorp.BusinessLogic.Services.Product;

namespace FoodCorp.API.StartupExtensions;

public static class BusinessLogicServicesInjectionExtensions
{
    public static void AddMapster(this IServiceCollection services)
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;

        //typeAdapterConfig.Compiler = exp => exp.CompileWithDebugInfo();
        typeAdapterConfig.Default.PreserveReference(true);
        typeAdapterConfig.RequireDestinationMemberSource = true;

        // scans the assembly and gets the IRegister, adding the registration to the TypeAdapterConfig
        typeAdapterConfig.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(typeAdapterConfig);
        services.AddScoped<IMapper, ServiceMapper>();
    }

    public static void AddBusinessLogicServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();

        services.AddSingleton<IProductMapper, ProductMapper>();
    }
}
