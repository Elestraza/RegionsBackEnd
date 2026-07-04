using Goods.Domain.Products;
using Goods.Services.Products.Repositories.Converters;
using Goods.Services.Products.Repositories.Models;
using Goods.Services.Products.Repositories.Queries;
using Goods.Tools.Utils;
using static Goods.Tools.Utils.NumberUtils;

namespace Goods.Services.Products.Repositories;

public class FederalRegionsRepository : IFederalRegionsRepository
{
	public Task SaveFederalRegions(FederalRegions federalRegions)
    {
        FederalRegionsDb federalRegionsDb = federalRegions.ToFederalRegionsDb();

        return DatabaseUtils.ExecuteAsync(
            Sql.Products_Save,
            parameters =>
            {
                parameters.AddWithValue("@id", federalRegionsDb.Id);
                parameters.AddWithValue("@category", (Int32)federalRegionsDb.Category);
                parameters.AddWithValue("@name", federalRegionsDb.Name);
                parameters.AddWithValue("@description", (Object?)federalRegionsDb.Description ?? DBNull.Value);
                parameters.AddWithValue("@price", federalRegionsDb.Price);
                parameters.AddWithValue("@createdDateTimeUtc", federalRegionsDb.CreatedDateTimeUtc);
                parameters.AddWithValue("@modifiedDateTimeUtc", (Object?)federalRegionsDb.ModifiedDateTimeUtc ?? DBNull.Value);
                parameters.AddWithValue("@isRemoved", federalRegionsDb.IsRemoved);
            }
        );
    }

    public async Task<FederalRegions?> GetFederalRegion(Guid id)
    {
        FederalRegionsDb? federalRegionsDb = await DatabaseUtils.GetAsync<FederalRegionsDb?>(
            Sql.Products_GetById,
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
            Sql.Products_GetByName,
            parameters =>
            {
                parameters.AddWithValue("@name", name);
            },
            reader => reader.ToFederalRegionsDb()
        );

        return FederalRegionsDb?.ToFederalRegions();
    }

    public async Task<Page<FederalRegions>> GetFederalRegions(Int32 page, Int32 count)
    {
        (Int32 offset, Int32 limit) = NormalizeRange(page, count);

        Page<FederalRegionsDb> pageDb = await DatabaseUtils.GetPageAsync(
            Sql.Products_GetPage,
            parameters =>
            {
                parameters.AddWithValue("@offset", offset);
                parameters.AddWithValue("@limit", limit);
            },
            reader => reader.ToProductDb()
        );

        return pageDb.Convert(productDb => productDb.ToFederalRegionsDb());
    }
    
    public Task RemoveFederalRegion(Guid id)
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
