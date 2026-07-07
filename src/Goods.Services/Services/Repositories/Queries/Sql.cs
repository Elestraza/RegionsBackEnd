namespace Goods.Services.Products.Repositories.Queries;

internal static class Sql
{
    internal static String GetById =>
        """
            SELECT * FROM @table
            WHERE id = @id;
        """;

    internal static String GetByName =>
        """
            SELECT * FROM @table 
            WHERE name = @name;
        """;

    internal static String GetPage =>
        """
            SELECT 
                COUNT(*) OVER() as count, 
                *
            FROM @table
            OFFSET @offset 
            LIMIT @limit
        """;

    internal static String Remove =>
        """
        	DELETE ON CASCADE FROM @table WHERE id = @id
        """;
}