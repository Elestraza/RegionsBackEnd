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
        FederalRegionsDb federalRegionsDb = new FederalRegionsDb(
            reader.GetGuid(reader.GetOrdinal("id")),
            reader.GetString(reader.GetOrdinal("Name"))
        );
        RegionsDb regions = new RegionsDb(
            reader.GetGuid(reader.GetOrdinal("id")),
            reader.GetString(reader.GetOrdinal("Name")),
            federalRegionsDb
        );
        return new CarCodesDb(
            reader.GetGuid(reader.GetOrdinal("id")),
            reader.GetInt32(reader.GetOrdinal("Code")),
            regions
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