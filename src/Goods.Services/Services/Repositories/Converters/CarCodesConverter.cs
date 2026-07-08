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
            carCodesDb.Regions.ToRegions()
        );
    }

    internal static CarCodesDb ToCarCodesDb(this NpgsqlDataReader reader)
    {
        RegionsDb region = new RegionsDb(
            reader.GetGuid(reader.GetOrdinal("region_id")),
            reader.GetString(reader.GetOrdinal("region_name")),
            (FederalRegions)reader.GetInt32(reader.GetOrdinal("region_federalregion"))
        );
        return new CarCodesDb(
            reader.GetGuid(reader.GetOrdinal("id")),
            reader.GetString(reader.GetOrdinal("code")),
            region
        );
    }

    public static CarCodesDb ToCarCodesDb(this CarCodes carCodes)
    {
        return new CarCodesDb(
            carCodes.Id, 
            carCodes.Code,
            carCodes.Regions.ToRegionsDb()
        );
    }
}