using Goods.Domain.Products;
using Goods.Services.Products.Repositories.Converters;
using Goods.Services.Products.Repositories.Models;
using Goods.Services.Products.Repositories.Queries;
using Goods.Tools.Utils;
using static Goods.Tools.Utils.NumberUtils;

namespace Goods.Services.Products.Repositories;

public class RegionsRepository : IRegionsRepository
{
	public Task SaveRegion(Regions regions)
    {
        RegionsDb regionsDb = regions.ToRegionsDb();

        return DatabaseUtils.ExecuteAsync(
            RegionsSql.Regions_Save,
            parameters =>
            {
                parameters.AddWithValue("@id", regionsDb.Id);
                parameters.AddWithValue("@name", regionsDb.Name);
                parameters.AddWithValue("@federalregion", (Object?)regionsDb.FederalRegion ?? DBNull.Value);
            }
        );
    }

    public async Task<Regions?> GetRegion(Guid id)
    {
        RegionsDb? regionsDb = await DatabaseUtils.GetAsync<RegionsDb?>(
            RegionsSql.GetById,
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
            RegionsSql.GetByName,
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
            RegionsSql.GetPage,
            parameters =>
            {
                parameters.AddWithValue("@offset", offset);
                parameters.AddWithValue("@limit", limit);
            },
            reader => reader.ToRegionsDb()
        );

        return pageDb.Convert(regionsDb => regionsDb.ToRegions());
    }
    
    public Task RemoveRegion(Guid id)
    {
        return DatabaseUtils.ExecuteAsync(
            RegionsSql.Remove,
            parameters =>
            {
                parameters.AddWithValue("@id", id);
            }
        );
    }
}
