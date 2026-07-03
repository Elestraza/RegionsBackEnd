using Goods.Domain.Products;
using Goods.Services.Products.Repositories.Converters;
using Goods.Services.Products.Repositories.Models;
using Goods.Services.Products.Repositories.Queries;
using Goods.Tools.Utils;
using static Goods.Tools.Utils.NumberUtils;

namespace Goods.Services.Products.Repositories;

public class FederalRegionRepository : IFederaRegionRepository
{
	public Task SaveCarCodes(CarCodes carCodes)
    {
        CarCodesDb carCodesDb = carCodes.ToProductDb();

        return DatabaseUtils.ExecuteAsync(
            Sql.Products_Save,
            parameters =>
            {
                parameters.AddWithValue("@id", carCodesDb.Id);
                parameters.AddWithValue("@category", (Int32)carCodesDb.Category);
                parameters.AddWithValue("@name", carCodesDb.Name);
                parameters.AddWithValue("@description", (Object?)carCodesDb.Description ?? DBNull.Value);
                parameters.AddWithValue("@price", carCodesDb.Price);
                parameters.AddWithValue("@createdDateTimeUtc", carCodesDb.CreatedDateTimeUtc);
                parameters.AddWithValue("@modifiedDateTimeUtc", (Object?)carCodesDb.ModifiedDateTimeUtc ?? DBNull.Value);
                parameters.AddWithValue("@isRemoved", carCodesDb.IsRemoved);
            }
        );
    }

    public async Task<CarCodes?> GetCarCode(Guid id)
    {
        CarCodesDb? carCodesDb = await DatabaseUtils.GetAsync<CarCodesDb?>(
            Sql.Products_GetById,
            parameters =>
            {
                parameters.AddWithValue("@id", id);
            },
            reader => reader.ToCarCodesDb()
        );

        return carCodesDb?.ToCarCodes();
    }

    public async Task<CarCodes?> GetCarCode(String name)
    {
        CarCodesDb? carCodesDb = await DatabaseUtils.GetAsync<CarCodesDb?>(
            Sql.Products_GetByName,
            parameters =>
            {
                parameters.AddWithValue("@name", name);
            },
            reader => reader.ToProductDb()
        );

        return carCodesDb?.ToCarCodes();
    }

    public async Task<Page<CarCodes>> GetProducts(Int32 page, Int32 count)
    {
        (Int32 offset, Int32 limit) = NormalizeRange(page, count);

        Page<CarCodesDb> pageDb = await DatabaseUtils.GetPageAsync(
            Sql.Products_GetPage,
            parameters =>
            {
                parameters.AddWithValue("@offset", offset);
                parameters.AddWithValue("@limit", limit);
            },
            reader => reader.ToProductDb()
        );

        return pageDb.Convert(productDb => productDb.ToCarCodesDb());
    }
    
    public Task RemoveCarCode(Guid id)
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
