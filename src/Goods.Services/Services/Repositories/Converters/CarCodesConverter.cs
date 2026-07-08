using Goods.Domain.Products;
using Goods.Services.Products.Repositories.Models;
using Npgsql;

namespace Goods.Services.Products.Repositories.Converters;

internal static class CarCodesConverter
{
    internal static CarCodes ToCarCodes(this CarCodesDb carCodesDb)
    {
        return new CarCodes(
            carCodesDb.Id,
            carCodesDb.Code,
            carCodesDb.Regions
        );
    }

    internal static CarCodesDb ToCarCodesDb(this NpgsqlDataReader reader)
    {
        return new CarCodesDb(
            reader.GetGuid(reader.GetOrdinal("id")),
            reader.GetString(reader.GetOrdinal("code")),
            reader.GetGuid(reader.GetOrdinal("region"))
        );
    }

    public static CarCodesDb ToCarCodesDb(this CarCodes carCodes)
    {
        return new CarCodesDb(
            carCodes.Id, 
            carCodes.Code,
            carCodes.Regions
        );
    }
}