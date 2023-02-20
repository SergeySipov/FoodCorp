using FoodCorp.BusinessLogic.Models;
using FoodCorp.DataAccess.DataModels;
using Mapster;

namespace FoodCorp.BusinessLogic.Mappers;

public class ProductRegisterMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ProductDataModel, ProductModel>()
            .RequireDestinationMemberSource(true);
    }
}
