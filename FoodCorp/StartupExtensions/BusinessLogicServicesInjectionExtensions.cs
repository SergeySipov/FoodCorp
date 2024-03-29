﻿using Mapster;
using MapsterMapper;
using System.Reflection;
using FoodCorp.BusinessLogic.Mappers.AccountMapper;
using FoodCorp.BusinessLogic.Mappers.ProductMapper;
using FoodCorp.BusinessLogic.Services.Account;
using FoodCorp.BusinessLogic.Services.JwtToken;
using FoodCorp.BusinessLogic.Services.Product;
using FoodCorp.BusinessLogic.Services.Email;

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
        services.AddScoped<IAccountService, AccountService>();
        services.AddSingleton<IJwtTokenGenerationService, JwtTokenGenerationService>();
        services.AddSingleton<IEmailSenderService, EmailSenderService>();

        services.AddSingleton<IProductMapper, ProductMapper>();
        services.AddSingleton<IAccountMapper, AccountMapper>();
    }
}
