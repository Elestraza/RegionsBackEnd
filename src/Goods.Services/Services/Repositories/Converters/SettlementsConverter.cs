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
            settlementsDb.Region,
            settlementsDb.FoundationYear,
            settlementsDb.IsHero,
            settlementsDb.AverageHotelCost
        );
    }

    internal static SettlementsDb ToSettlementsDb(this NpgsqlDataReader reader)
    {
        //FederalRegionsDb federalRegionsDb = new FederalRegionsDb(
        //    reader.GetGuid(reader.GetOrdinal("id")),
        //    reader.GetString(reader.GetOrdinal("Name"))
        //);
        //RegionsDb region = new RegionsDb(
        //    reader.GetGuid(reader.GetOrdinal("id")),
        //    reader.GetString(reader.GetOrdinal("Name")),
        //    federalRegionsDb
        //);
        return new SettlementsDb(
            reader.GetGuid(reader.GetOrdinal("id")),
            (SettlementsTypes)reader.GetInt32(reader.GetOrdinal("settlementstypee")),
            reader.GetString(reader.GetOrdinal("Name")),
            reader.GetInt32(reader.GetOrdinal("Population")),
            reader.GetGuid(reader.GetOrdinal("region")),
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
            settlements.Region,
            settlements.FoundationYear,
            settlements.IsHero,
            settlements.AverageHotelCost
        );
    }
}