using Goods.Domain.Products;
using Goods.Services.Products.Repositories.Converters;
using Goods.Services.Products.Repositories.Models;
using Goods.Services.Products.Repositories.Queries;
using Goods.Tools.Utils;
using static Goods.Tools.Utils.NumberUtils;

namespace Goods.Services.Products.Repositories;

public class CarCodesRepository : ICarCodesRepository
{
    public Task SaveCarCode(CarCodes carCodes)
    {
        CarCodesDb carCodesDb = carCodes.ToCarCodesDb();

        return DatabaseUtils.ExecuteAsync(
            CarCodesSql.CarCodes_Save,
            parameters =>
            {
                parameters.AddWithValue("@id", carCodesDb.Id);
                parameters.AddWithValue("@category", (Int32)carCodesDb.Code);
                parameters.AddWithValue("@description", (Object?)carCodesDb.Regions ?? DBNull.Value);
            }
        );
    }

    public async Task<CarCodes?> GetCarCode(Guid id)
    {
        CarCodesDb? carCodesDb = await DatabaseUtils.GetAsync<CarCodesDb?>(
            Sql.GetById,
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
            Sql.GetByName,
            parameters =>
            {
                parameters.AddWithValue("@name", name);
            },
            reader => reader.ToCarCodesDb()
        );

        return carCodesDb?.ToCarCodes();
    }

    public async Task<Page<CarCodes>> GetCarCodes(Int32 page, Int32 count)
    {
        (Int32 offset, Int32 limit) = NormalizeRange(page, count);

        Page<CarCodesDb> pageDb = await DatabaseUtils.GetPageAsync(
            Sql.GetPage,
            parameters =>
            {
                parameters.AddWithValue("@offset", offset);
                parameters.AddWithValue("@limit", limit);
            },
            reader => reader.ToCarCodesDb()
        );

        return pageDb.Convert(carcodeDb => carcodeDb.ToCarCodes());
    }
    
    public Task RemoveCarCode(Guid id)
    {
        return DatabaseUtils.ExecuteAsync(
            Sql.Remove,
            parameters =>
            {
                parameters.AddWithValue("@id", id);
            }
        );
    }

    
}
