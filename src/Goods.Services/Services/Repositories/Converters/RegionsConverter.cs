using Goods.Domain.Products;
using Goods.Services.Products.Repositories.Models;
using Npgsql;

namespace Goods.Services.Products.Repositories.Converters;

internal static class RegionsConverter
{
    internal static Regions ToRegions(this RegionsDb regionsDb)
    {
        return new Regions(
            regionsDb.Id,
            regionsDb.Name,
            regionsDb.FederalRegion
        );
    }

    internal static RegionsDb ToRegionsDb(this NpgsqlDataReader reader)
    {
        return new RegionsDb(
            reader.GetGuid(reader.GetOrdinal("id")),
            reader.GetString(reader.GetOrdinal("name")),
            (FederalRegions)reader.GetInt32(reader.GetOrdinal("federalregion"))
        );
    }

    public static RegionsDb ToRegionsDb(this Regions regions)
    {
        return new RegionsDb(
            regions.Id, 
            regions.Name,
            regions.FederalRegion
        );
    }
}