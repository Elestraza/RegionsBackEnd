using Goods.Domain.Products;

namespace Goods.Services.Products.Repositories;

public interface ISettlementsRepository
{
    Task SaveSettlement(Settlements blank);
    Task<Settlements?> GetSettlement(Guid id);
    Task<Settlements?> GetSettlement(String name);
    Task<Page<Settlements>> GetSettlementsByRegion(Guid regionId);
    Task<Page<Settlements>> GetSettlements(Int32 page, Int32 count);
    Task RemoveSettlement(Guid id);
}