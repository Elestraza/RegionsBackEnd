using Goods.Domain.Products;

namespace Goods.Services.Products.Repositories;

public interface IFederalRegionsRepository
{
    Task SaveFederalRegion(FederalRegions blank);
    Task<FederalRegions?> GetFederalRegion(Guid id);
    Task<FederalRegions?> GetFederalRegion(String name);
    Task<Page<FederalRegions>> GetFederalRegions(Int32 page, Int32 count);
    Task RemoveFederalRegion(Guid id);
}