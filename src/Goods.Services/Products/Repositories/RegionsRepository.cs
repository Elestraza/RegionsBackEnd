using Goods.Domain.Products;
using Goods.Services.Products.Repositories.Converters;
using Goods.Services.Products.Repositories.Models;
using Goods.Services.Products.Repositories.Queries;
using Goods.Tools.Utils;
using static Goods.Tools.Utils.NumberUtils;

namespace Goods.Services.Products.Repositories;

public class RegionsRepository : IRegionsRepository
{
	public Task SaveRegions(Regions regions)
    {
        RegionsDb regionsDb = regions.ToProductDb();

        return DatabaseUtils.ExecuteAsync(
            Sql.Products_Save,
            parameters =>
            {
                parameters.AddWithValue("@id", regionsDb.Id);
                parameters.AddWithValue("@category", (Int32)regionsDb.Category);
                parameters.AddWithValue("@name", regionsDb.Name);
                parameters.AddWithValue("@description", (Object?)regionsDb.Description ?? DBNull.Value);
                parameters.AddWithValue("@price", regionsDb.Price);
                parameters.AddWithValue("@createdDateTimeUtc", regionsDb.CreatedDateTimeUtc);
                parameters.AddWithValue("@modifiedDateTimeUtc", (Object?)regionsDb.ModifiedDateTimeUtc ?? DBNull.Value);
                parameters.AddWithValue("@isRemoved", regionsDb.IsRemoved);
            }
        );
    }

    public async Task<Regions?> GetRegion(Guid id)
    {
        RegionsDb? regionsDb = await DatabaseUtils.GetAsync<RegionsDb?>(
            Sql.Products_GetById,
            parameters =>
            {
                parameters.AddWithValue("@id", id);
            },
            reader => reader.ToRegionsDb()
        );

        return regionsDb?.ToRegions();
    }

    public async Task<Regions?> GetRegion(String name)
    {
        RegionsDb? regionsDb = await DatabaseUtils.GetAsync<RegionsDb?>(
            Sql.Products_GetByName,
            parameters =>
            {
                parameters.AddWithValue("@name", name);
            },
            reader => reader.ToRegionsDb()
        );

        return regionsDb?.ToRegions();
    }

    public async Task<Page<Regions>> GetRegions(Int32 page, Int32 count)
    {
        (Int32 offset, Int32 limit) = NormalizeRange(page, count);

        Page<RegionsDb> pageDb = await DatabaseUtils.GetPageAsync(
            Sql.Products_GetPage,
            parameters =>
            {
                parameters.AddWithValue("@offset", offset);
                parameters.AddWithValue("@limit", limit);
            },
            reader => reader.ToRegionsDb()
        );

        return pageDb.Convert(regionsDb => regionsDb.ToCarCodesDb());
    }
    
    public Task RemoveRegion(Guid id)
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
