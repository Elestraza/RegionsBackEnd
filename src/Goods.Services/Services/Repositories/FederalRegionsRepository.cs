using Goods.Domain.Products;
using Goods.Services.Products.Repositories.Converters;
using Goods.Services.Products.Repositories.Models;
using Goods.Services.Products.Repositories.Queries;
using Goods.Tools.Utils;
using static Goods.Tools.Utils.NumberUtils;

namespace Goods.Services.Products.Repositories;

public class FederalRegionsRepository : IFederalRegionsRepository
{
    public Task SaveFederalRegion(FederalRegions federalRegion)
    {
        FederalRegionsDb federalRegionsDb = federalRegion.ToFederalRegionsDb();

        return DatabaseUtils.ExecuteAsync(
            FederalRegionsSql.FederalRegions_Save,
            parameters =>
            {
                parameters.AddWithValue("@id", federalRegionsDb.Id);
                parameters.AddWithValue("@name", (String)federalRegionsDb.Name);
                parameters.AddWithValue("@historicalvalueage", (Int32)federalRegion.HistoricalValueAge);
            }
        );
    }

    public async Task<FederalRegions?> GetFederalRegion(Guid id)
    {
        FederalRegionsDb? federalRegionsDb = await DatabaseUtils.GetAsync<FederalRegionsDb?>(
            FederalRegionsSql.GetById,
            parameters =>
            {
                parameters.AddWithValue("@id", id);
            },
            reader => reader.ToFederalRegionsDb()
        );

        return federalRegionsDb?.ToFederalRegions();
    }

    public async Task<FederalRegions?> GetFederalRegion(String name)
    {
        FederalRegionsDb? federalRegionsDb = await DatabaseUtils.GetAsync<FederalRegionsDb?>(
            FederalRegionsSql.GetByName,
            parameters =>
            {
                parameters.AddWithValue("@name", name);
            },
            reader => reader.ToFederalRegionsDb()
        );

        return federalRegionsDb?.ToFederalRegions();
    }

    public async Task<Page<FederalRegions>> GetFederalRegions(Int32 page, Int32 count)
    {
        (Int32 offset, Int32 limit) = NormalizeRange(page, count);

        Page<FederalRegionsDb> pageDb = await DatabaseUtils.GetPageAsync(
            FederalRegionsSql.GetPage,
            parameters =>
            {
                parameters.AddWithValue("@offset", offset);
                parameters.AddWithValue("@limit", limit);
            },
            reader => reader.ToFederalRegionsDb()
        );

        return pageDb.Convert(federalRegionsDb => federalRegionsDb.ToFederalRegions());
    }
    
    public Task RemoveFederalRegion(Guid id)
    {
        return DatabaseUtils.ExecuteAsync(
            FederalRegionsSql.Remove,
            parameters =>
            {
                parameters.AddWithValue("@id", id);
            }
        );
    }

    
}
