using Goods.Domain.Products;
using Goods.Services.Products.Repositories.Converters;
using Goods.Services.Products.Repositories.Models;
using Goods.Services.Products.Repositories.Queries;
using Goods.Tools.Utils;
using static Goods.Tools.Utils.NumberUtils;

namespace Goods.Services.Products.Repositories;

public class SettlementsRepository : ISettlementsRepository
{
	public Task SaveSettlement(Settlements settlement)
    {
        SettlementsDb settlementDb = settlement.ToSettlementsDb();

        return DatabaseUtils.ExecuteAsync(
            SettlementsSql.Settlements_Save,
            parameters =>
            {
                parameters.AddWithValue("@id", settlementDb.Id);
                parameters.AddWithValue("@settlementtype", (Int32)settlementDb.Type);
                parameters.AddWithValue("@population", settlementDb.Population);
                parameters.AddWithValue("@name", settlementDb.Name);
                parameters.AddWithValue("@region", settlementDb.Region.Id);
                parameters.AddWithValue("@foundationyear", settlementDb.FoundationYear);
                parameters.AddWithValue("@ishero", (Boolean)settlementDb.IsHero);
                parameters.AddWithValue("@averagehotelcost", (Int32)settlementDb.AverageHotelCost);
            }
        );
    }

    public async Task<Settlements?> GetSettlement(Guid id)
    {
        SettlementsDb? settlementDb = await DatabaseUtils.GetAsync<SettlementsDb?>(
            SettlementsSql.GetById,
            parameters =>
            {
                parameters.AddWithValue("@id", id);
            },
            reader => reader.ToSettlementsDb()
        );

        return settlementDb?.ToSettlements();
    }

    public async Task<Settlements?> GetSettlement(String name)
    {
        SettlementsDb? settlementDb = await DatabaseUtils.GetAsync<SettlementsDb?>(
            SettlementsSql.GetByName,
            parameters =>
            {
                parameters.AddWithValue("@name", name);
            },
            reader => reader.ToSettlementsDb()
        );

        return settlementDb?.ToSettlements();
    }

    public async Task<Page<Settlements>> GetSettlements(Int32 page, Int32 count)
    {
        (Int32 offset, Int32 limit) = NormalizeRange(page, count);

        Page<SettlementsDb> pageDb = await DatabaseUtils.GetPageAsync(
            SettlementsSql.GetPage,
            parameters =>
            {
                parameters.AddWithValue("@offset", offset);
                parameters.AddWithValue("@limit", limit);
            },
            reader => reader.ToSettlementsDb()
        );

        return pageDb.Convert(settlementDb => settlementDb.ToSettlements());
    }

    public Task RemoveSettlement(Guid id)
    {
        return DatabaseUtils.ExecuteAsync(
            SettlementsSql.Remove,
            parameters =>
            {
                parameters.AddWithValue("@id", id);
            }
        );
    }

    
}
