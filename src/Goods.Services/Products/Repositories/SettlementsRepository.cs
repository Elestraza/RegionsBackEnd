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
            Sql.Products_Save,
            parameters =>
            {
                parameters.AddWithValue("@id", settlementDb.Id);
                parameters.AddWithValue("@category", (Int32)settlementDb.Category);
                parameters.AddWithValue("@name", settlementDb.Name);
                parameters.AddWithValue("@description", (Object?)settlementDb.Description ?? DBNull.Value);
                parameters.AddWithValue("@price", settlementDb.Price);
                parameters.AddWithValue("@createdDateTimeUtc", settlementDb.CreatedDateTimeUtc);
                parameters.AddWithValue("@modifiedDateTimeUtc", (Object?)settlementDb.ModifiedDateTimeUtc ?? DBNull.Value);
                parameters.AddWithValue("@isRemoved", settlementDb.IsRemoved);
            }
        );
    }

    public async Task<Settlements?> GetSettlement(Guid id)
    {
        SettlementsDb? settlementDb = await DatabaseUtils.GetAsync<SettlementsDb?>(
            Sql.Products_GetById,
            parameters =>
            {
                parameters.AddWithValue("@id", id);
            },
            reader => reader.ToProductDb()
        );

        return settlementDb?.ToSettlements();
    }

    public async Task<Settlements?> GetSettlement(String name)
    {
        SettlementsDb? settlementDb = await DatabaseUtils.GetAsync<SettlementsDb?>(
            Sql.Products_GetByName,
            parameters =>
            {
                parameters.AddWithValue("@name", name);
            },
            reader => reader.ToProductDb()
        );

        return settlementDb?.ToSettlements();
    }

    public async Task<Page<Settlements>> GetSettlements(Int32 page, Int32 count)
    {
        (Int32 offset, Int32 limit) = NormalizeRange(page, count);

        Page<SettlementsDb> pageDb = await DatabaseUtils.GetPageAsync(
            Sql.Products_GetPage,
            parameters =>
            {
                parameters.AddWithValue("@offset", offset);
                parameters.AddWithValue("@limit", limit);
            },
            reader => reader.ToProductDb()
        );

        return pageDb.Convert(settlementDb => settlementDb.ToSettlements());
    }
    
    public Task RemoveSettlement(Guid id)
    {
        return DatabaseUtils.ExecuteAsync(
            Sql.Products_Remove,
            parameters =>
            {
                parameters.AddWithValue("@id", id);
                parameters.AddWithValue("@modifiedDateTimeUtc", DateTime.UtcNow);
            }
        );
    }
}
