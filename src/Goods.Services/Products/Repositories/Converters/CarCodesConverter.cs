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
            carCodesDb.Regions
        );
    }

    internal static CarCodesDb ToCarCodesDb(this NpgsqlDataReader reader)
    {
        return new CarCodesDb(
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

    public static CarCodesDb ToProductDb(this CarCodes carCodes)
    {
        return new CarCodesDb(
            carCodes.Id, 
            carCodes.Code,
            carCodes.Regions,
            createdDateTimeUtc: DateTime.UtcNow,
            modifiedDateTimeUtc: DateTime.UtcNow,
            isRemoved: false
        );
    }
}