using Goods.Domain.Products;
using Goods.Services.Products.Repositories.Models;
using Npgsql;

namespace Goods.Services.Products.Repositories.Converters;

internal static class FederalRegionsConverter
{
    internal static FederalRegions ToFederalRegions(this FederalRegionsDb federalRegionsDb)
    {
        return new FederalRegions(
            federalRegionsDb.Id,
            federalRegionsDb.Type
        );
    }

    internal static FederalRegionsDb ToFederalRegionsDb(this NpgsqlDataReader reader)
    {
        return new FederalRegionsDb(
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

    public static FederalRegionsDb ToFederalRegionsDb(this FederalRegions federalRegions)
    {
        return new FederalRegionsDb(
            federalRegions.Id, 
            federalRegions.Type,
            createdDateTimeUtc: DateTime.UtcNow,
            modifiedDateTimeUtc: DateTime.UtcNow,
            isRemoved: false
        );
    }
}