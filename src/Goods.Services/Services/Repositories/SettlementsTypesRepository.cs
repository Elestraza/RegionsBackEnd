using Goods.Domain.Products;
using Goods.Services.Products.Repositories.Converters;
using Goods.Services.Products.Repositories.Models;
using Goods.Services.Products.Repositories.Queries;
using Goods.Tools.Utils;
using System.Xml.Linq;
using static Goods.Tools.Utils.NumberUtils;

namespace Goods.Services.Products.Repositories;

public class SettlementsTypesRepository : ISettlementsTypesRepository
{
	public Task SaveSettlementsType(SettlementsTypes settlementsType)
    {
        SettlementsTypesDb settlementsTypeDb = settlementsType.ToSettlementsTypesDb();

        return DatabaseUtils.ExecuteAsync(
            SettlementsTypesSql.SettlementsTypes_Save,
            parameters =>
            {
                parameters.AddWithValue("@id", settlementsTypeDb.Id);
                parameters.AddWithValue("@settlementstype", settlementsTypeDb.Type);
            }
        );
    }

    public async Task<SettlementsTypes?> GetSettlementsType(Guid id)
    {
        SettlementsTypesDb? settlementsTypeDb = await DatabaseUtils.GetAsync<SettlementsTypesDb?>(
            SettlementsTypesSql.GetById,
            parameters =>
            {
                parameters.AddWithValue("@id", id);
            },
            reader => reader.ToSettlementsTypesDb()
        );

        return settlementsTypeDb?.ToSettlementsTypes();
    }

    public async Task<SettlementsTypes?> GetSettlementsType(String name)
    {
        SettlementsTypesDb? settlementsTypeDb = await DatabaseUtils.GetAsync<SettlementsTypesDb?>(
            SettlementsTypesSql.GetByName,
            parameters =>
            {
                parameters.AddWithValue("@settlementstype", name);
            },
            reader => reader.ToSettlementsTypesDb()
        );

        return settlementsTypeDb?.ToSettlementsTypes();
    }

    public async Task<Page<SettlementsTypes>> GetSettlementsTypes(Int32 page, Int32 count)
    {
        (Int32 offset, Int32 limit) = NormalizeRange(page, count);

        Page<SettlementsTypesDb> pageDb = await DatabaseUtils.GetPageAsync(
            SettlementsTypesSql.GetPage,
            parameters =>
            {
                parameters.AddWithValue("@offset", offset);
                parameters.AddWithValue("@limit", limit);
            },
            reader => reader.ToSettlementsTypesDb()
        );

        return pageDb.Convert(settlementsTypeDb => settlementsTypeDb.ToSettlementsTypes());
    }
    
    public Task RemoveSettlementsType(Guid id)
    {
        return DatabaseUtils.ExecuteAsync(
            SettlementsTypesSql.Remove,
            parameters =>
            {
                parameters.AddWithValue("@id", id);
            }
        );
    }
}
