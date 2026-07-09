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
            regionsDb.FederalRegion.ToFederalRegions()
        );
    }

    internal static RegionsDb ToRegionsDb(this NpgsqlDataReader reader)
    {
        FederalRegionsDb federalRegions = new FederalRegionsDb(
            reader.GetGuid(reader.GetOrdinal("federalregion_id")),
            reader.GetString(reader.GetOrdinal("federalregion_name")),
            reader.GetInt32(reader.GetOrdinal("federalregion_historicalvalueage"))
        );
        return new RegionsDb(
            reader.GetGuid(reader.GetOrdinal("id")),
            reader.GetString(reader.GetOrdinal("name")),
            (FederalRegionsDb)federalRegions
        );
    }

    public static RegionsDb ToRegionsDb(this Regions regions)
    {
        return new RegionsDb(
            regions.Id, 
            regions.Name,
            regions.FederalRegion.ToFederalRegionsDb()
        );
    }
}