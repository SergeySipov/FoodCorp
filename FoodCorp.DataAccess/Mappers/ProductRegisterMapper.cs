using FoodCorp.DataAccess.DataModels;
using FoodCorp.DataAccess.Entities;
using Mapster;

namespace FoodCorp.DataAccess.Mappers;

public class ProductRegisterMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Product, ProductDataModel>()
            .RequireDestinationMemberSource(true);
    }
}
