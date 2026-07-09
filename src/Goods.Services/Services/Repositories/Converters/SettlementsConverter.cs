using Goods.Domain.Products;
using Goods.Services.Products.Repositories.Models;
using Npgsql;

namespace Goods.Services.Products.Repositories.Converters;

internal static class SettlementsConverter
{
    internal static Settlements ToSettlements(this SettlementsDb settlementsDb)
    {
        return new Settlements(
            settlementsDb.Id,
            settlementsDb.Type,
            settlementsDb.Name,
            settlementsDb.Population,
            settlementsDb.Region.ToRegions(),
            settlementsDb.FoundationYear,
            settlementsDb.IsHero,
            settlementsDb.AverageHotelCost
        );
    }

    internal static SettlementsDb ToSettlementsDb(this NpgsqlDataReader reader)
    {
        FederalRegionsDb federalRegions = new FederalRegionsDb(
            reader.GetGuid(reader.GetOrdinal("federalregion_id")),
            reader.GetString(reader.GetOrdinal("federalregion_name")),
            reader.GetInt32(reader.GetOrdinal("federalregion_historicalvalueage"))
        );
        RegionsDb region = new RegionsDb(
            reader.GetGuid(reader.GetOrdinal("region_id")),
            reader.GetString(reader.GetOrdinal("region_name")),
            (FederalRegionsDb)federalRegions
        );
        return new SettlementsDb(
            reader.GetGuid(reader.GetOrdinal("id")),
            (SettlementsTypes)reader.GetInt32(reader.GetOrdinal("settlementtype")),
            reader.GetString(reader.GetOrdinal("Name")),
            reader.GetInt32(reader.GetOrdinal("Population")),
            region,
            reader.GetInt32(reader.GetOrdinal("FoundationYear")),
            reader.GetBoolean(reader.GetOrdinal("IsHero")),
            reader.GetInt32(reader.GetOrdinal("AverageHotelCost"))
        );
    }

    public static SettlementsDb ToSettlementsDb(this Settlements settlements)
    {
        return new SettlementsDb(
            settlements.Id,
            settlements.Type,
            settlements.Name, 
            settlements.Population,
            settlements.Region.ToRegionsDb(),
            settlements.FoundationYear,
            settlements.IsHero,
            settlements.AverageHotelCost
        );
    }
}