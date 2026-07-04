using Goods.Domain.Products;

namespace Goods.Services.Products.Repositories;

public interface ISettlementsRepository
{
    Task SaveSettlement(Settlements blank);
    Task<Settlements?> GetSettlement(Int32 id);
    Task<Settlements?> GetSettlement(String name);
    Task<Page<Settlements>> GetSettlements(Int32 page, Int32 count);
    Task RemoveSettlement(Int32 id);
}