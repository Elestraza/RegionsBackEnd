using Goods.Domain.Products;

namespace Goods.Services.Products.Repositories;

public interface ISettlementsRepository
{
    Task SaveSettlement(Settlements blank);
    Task<Settlements?> GetSettlement(Guid id);
    Task<Settlements?> GetSettlements(String name);
    Task<Page<Settlements>> GetSettlements(Int32 page, Int32 count);
    Task RemoveSettlement(Guid id);
}