namespace Goods.Services.Products.Repositories.Queries;

internal static class SettlementsTypesSql
{
    internal static String SettlementsTypes_Save =>
        """
            INSERT INTO settlementstypes (
                id
                settlementstype
            )
            VALUES (
                @id,
                @name
            )
        	ON CONFLICT (id) DO UPDATE SET
        	    settlementstype = @settlementstype
        """;

}