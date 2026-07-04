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
            regionsDb.Type,
            regionsDb.FederalRegion
        );
    }

    internal static RegionsDb ToRegionsDb(this NpgsqlDataReader reader)
    {
        return new RegionsDb(
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

    public static RegionsDb ToProductDb(this Regions regions)
    {
        return new RegionsDb(
            regions.Id, 
            regions.Type,
            regions.FederalRegion,
            createdDateTimeUtc: DateTime.UtcNow,
            modifiedDateTimeUtc: DateTime.UtcNow,
            isRemoved: false
        );
    }
}