using Goods.Domain.Products;

namespace Goods.Services.Products.Repositories;

public interface IRegionsRepository
{
    Task SaveRegion(Regions blank);
    Task<Regions?> GetRegion(Int32 id);
    Task<Regions?> GetRegion(String name);
    Task<Page<Regions>> GetRegions(Int32 page, Int32 count);
    Task RemoveRegion(Int32 id);
}