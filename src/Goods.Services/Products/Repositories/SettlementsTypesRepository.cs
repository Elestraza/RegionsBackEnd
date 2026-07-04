using Goods.Domain.Products;
using Goods.Services.Products.Repositories.Converters;
using Goods.Services.Products.Repositories.Models;
using Goods.Services.Products.Repositories.Queries;
using Goods.Tools.Utils;
using static Goods.Tools.Utils.NumberUtils;

namespace Goods.Services.Products.Repositories;

public class SettlementsTypesRepository : ISettlementsTypesRepository
{
	public Task SaveSettlementsTypes(SettlementsTypes settlementsType)
    {
        SettlementsTypesDb settlementsTypeDb = settlementsType.ToSettlementsTypesDb();

        return DatabaseUtils.ExecuteAsync(
            Sql.Products_Save,
            parameters =>
            {
                parameters.AddWithValue("@id", settlementsTypeDb.Id);
                parameters.AddWithValue("@category", (Int32)settlementsTypeDb.Category);
                parameters.AddWithValue("@name", settlementsTypeDb.Name);
                parameters.AddWithValue("@description", (Object?)settlementsTypeDb.Description ?? DBNull.Value);
                parameters.AddWithValue("@price", settlementsTypeDb.Price);
                parameters.AddWithValue("@createdDateTimeUtc", settlementsTypeDb.CreatedDateTimeUtc);
                parameters.AddWithValue("@modifiedDateTimeUtc", (Object?)settlementsTypeDb.ModifiedDateTimeUtc ?? DBNull.Value);
                parameters.AddWithValue("@isRemoved", settlementsTypeDb.IsRemoved);
            }
        );
    }

    public async Task<SettlementsTypes?> GetSettlementsType(Guid id)
    {
        SettlementsTypes? settlementsTypeDb = await DatabaseUtils.GetAsync<SettlementsTypesDb?>(
            Sql.Products_GetById,
            parameters =>
            {
                parameters.AddWithValue("@id", id);
            },
            reader => reader.ToProductDb()
        );

        return settlementsTypeDb?.ToSettlementsTypes();
    }

    public async Task<SettlementsTypes?> GetSettlementsType(String name)
    {
        SettlementsTypesDb? settlementsTypeDb = await DatabaseUtils.GetAsync<SettlementsTypesDb?>(
            Sql.Products_GetByName,
            parameters =>
            {
                parameters.AddWithValue("@name", name);
            },
            reader => reader.ToProductDb()
        );

        return settlementsTypeDb?.ToSettlementsTypes();
    }

    public async Task<Page<SettlementsTypes>> GetSettlementsTypes(Int32 page, Int32 count)
    {
        (Int32 offset, Int32 limit) = NormalizeRange(page, count);

        Page<SettlementsTypesDb> pageDb = await DatabaseUtils.GetPageAsync(
            Sql.Products_GetPage,
            parameters =>
            {
                parameters.AddWithValue("@offset", offset);
                parameters.AddWithValue("@limit", limit);
            },
            reader => reader.ToProductDb()
        );

        return pageDb.Convert(settlementsTypeDb => settlementsTypeDb.ToSettlementsTypes());
    }
    
    public Task RemoveSettlementsTypes(Guid id)
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
