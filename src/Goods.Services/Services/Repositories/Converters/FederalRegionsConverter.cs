using Goods.Services.Products.Repositories.Models;
using Goods.Domain.Products;
using Npgsql;

namespace Goods.Services.Products.Repositories.Converters;

internal static class FederalRegionsConverter
{
    internal static FederalRegions ToFederalRegions(this FederalRegionsDb federalRegionsDb)
    {
        return new FederalRegions(
            federalRegionsDb.Id,
            federalRegionsDb.Name,
            federalRegionsDb.HistoricalValueAge
        );
    }

    internal static FederalRegionsDb ToFederalRegionsDb(this NpgsqlDataReader reader)
    {
        return new FederalRegionsDb(
            reader.GetGuid(reader.GetOrdinal("id")),
            reader.GetString(reader.GetOrdinal("name")),
            reader.GetInt32(reader.GetOrdinal("historicalvalueage"))
        );
    }

    public static FederalRegionsDb ToFederalRegionsDb(this FederalRegions federalRegions)
    {
        return new FederalRegionsDb(
            federalRegions.Id,
            federalRegions.Name,
            federalRegions.HistoricalValueAge
        );
    }
}