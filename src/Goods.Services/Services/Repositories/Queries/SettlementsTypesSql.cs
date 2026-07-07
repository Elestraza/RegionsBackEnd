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
    internal static String GetById =>
        """
            SELECT * FROM settlements
            WHERE id = @id;
        """;

    internal static String GetByName =>
        """
            SELECT * FROM settlements
            WHERE name = @name;
        """;

    internal static String GetPage =>
        """
            SELECT 
                COUNT(*) OVER() as count, 
                *
            FROM settlements
            OFFSET @offset 
            LIMIT @limit
        """;

    internal static String Remove =>
        """
        	DELETE ON CASCADE FROM settlements WHERE id = @id
        """;
}