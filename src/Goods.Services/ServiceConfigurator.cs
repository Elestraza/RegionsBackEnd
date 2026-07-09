using Goods.Domain.Services;
using Goods.Services.Products;
using Goods.Services.Products.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Goods.Services;

public static class ServiceConfigurator
{
    public static IServiceCollection AddServices(this IServiceCollection collection)
    {
        collection.AddSingleton<IFederalRegionsService, FederalRegionsService>();
        collection.AddSingleton<IFederalRegionsRepository, FederalRegionsRepository>();


        collection.AddSingleton<ICarCodesService, CarCodesService>();
        collection.AddSingleton<ICarCodesRepository, CarCodesRepository>();

        collection.AddSingleton<IRegionsService, RegionsService>();
        collection.AddSingleton<IRegionsRepository, RegionsRepository>();

        collection.AddSingleton<ISettlementsService, SettlementsService>();
        collection.AddSingleton<ISettlementsRepository, SettlementsRepository>();

        return collection;
    }
}
