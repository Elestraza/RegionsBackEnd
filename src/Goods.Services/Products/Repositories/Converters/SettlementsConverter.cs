using Goods.Domain.Products;
using Goods.Services.Products.Repositories.Models;
using Npgsql;

namespace Goods.Services.Products.Repositories.Converters;

internal static class SettlementsConverter
{
    internal static Settlements ToProduct(this SettlementsDb settlementsDb)
    {
        return new Settlements(
            settlementsDb.Id,
            settlementsDb.Type, 
            settlementsDb.Name, 
            settlementsDb.Region, 
            settlementsDb.Age,
            settlementsDb.IsHero,
            settlementsDb.AverageHotelCost,
        );
    }

    internal static SettlementsDb ToProductDb(this NpgsqlDataReader reader)
    {
        return new SettlementsDb(
            reader.GetGuid(reader.GetOrdinal("id")),
            (ProductCategory)reader.GetInt32(reader.GetOrdinal("category")),
            reader.GetString(reader.GetOrdinal("name")),
            reader.IsDBNull(reader.GetOrdinal("description")) ? null : reader.GetString(reader.GetOrdinal("description")),
            reader.GetDouble(reader.GetOrdinal("price")),
            reader.GetDateTime(reader.GetOrdinal("createddatetimeutc")),
            reader.IsDBNull(reader.GetOrdinal("modifieddatetimeutc"))
                ? null
                : reader.GetDateTime(reader.GetOrdinal("modifieddatetimeutc")),
            reader.GetBoolean(reader.GetOrdinal("isremoved"))
        );
    }

    public static SettlementsDb ToProductDb(this Settlements settlements)
    {
        return new SettlementsDb(
            settlements.Id,
            settlements.Type, 
            settlements.Name, 
            settlements.Region, 
            settlements.Age,
            settlements.IsHero,
            settlements.AverageHotelCost,
            createdDateTimeUtc: DateTime.UtcNow,
            modifiedDateTimeUtc: DateTime.UtcNow,
            isRemoved: false
        );
    }
}