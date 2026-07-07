namespace Goods.Services.Products.Repositories.Queries;

internal static class Sql
{
    internal static String GetById =>
        """
            SELECT * FROM settlementstypes
            WHERE id = @id;
        """;

    internal static String GetByName =>
        """
            SELECT * FROM @table 
            WHERE name = @name
            AND NOT isremoved;
        """;

    internal static String GetPage =>
        """
            SELECT 
                COUNT(*) OVER() as count, 
                *
            FROM @table
            WHERE NOT isremoved 
            ORDER BY createddatetimeutc DESC 
            OFFSET @offset 
            LIMIT @limit
        """;

    internal static String Remove =>
        """
        	DELETE ON CASCADE FROM @table WHERE id = @id
        """;
}