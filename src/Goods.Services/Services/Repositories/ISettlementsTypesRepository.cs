using Goods.Domain.Products;

namespace Goods.Services.Products.Repositories;

public interface ISettlementsTypesRepository
{
    Task SaveSettlementsType(SettlementsTypes blank);
    Task<SettlementsTypes?> GetSettlementsType(Guid id);
    Task<SettlementsTypes?> GetSettlementsType(String type);
    Task<Page<SettlementsTypes>> GetSettlementsTypes(Int32 page, Int32 count);
    Task RemoveSettlementsType(Guid id);
}