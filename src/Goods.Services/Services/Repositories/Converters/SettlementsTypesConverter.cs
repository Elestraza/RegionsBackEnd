using Goods.Domain.Products;
using Goods.Services.Products.Repositories.Models;
using Npgsql;

namespace Goods.Services.Products.Repositories.Converters;

internal static class SettlementsTypesConverter
{
    internal static SettlementsTypes ToSettlementsTypes(this SettlementsTypesDb settlementsTypesDb)
    {
        return new SettlementsTypes(
            settlementsTypesDb.Id,
            settlementsTypesDb.Type
        );
    }

    internal static SettlementsTypesDb ToSettlementsTypesDb(this NpgsqlDataReader reader)
    {
        return new SettlementsTypesDb(
            reader.GetGuid(reader.GetOrdinal("id")),
            reader.GetString(reader.GetOrdinal("type"))
        );
    }

    public static SettlementsTypesDb ToSettlementsTypesDb(this SettlementsTypes product)
    {
        return new SettlementsTypesDb(
            product.Id, 
            product.Type
        );
    }
}