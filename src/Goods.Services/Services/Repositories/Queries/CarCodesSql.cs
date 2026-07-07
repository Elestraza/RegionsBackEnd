namespace Goods.Services.Products.Repositories.Queries;

internal static class CarCodesSql
{
    internal static String CarCodes_Save =>
        """
            INSERT INTO regions (
                id
                name
                federalregion
            )
            VALUES (
                @id,
                @name,
                @federalregion
            )
        	ON CONFLICT (id) DO UPDATE SET
        	    federalregion = @federalregion,
        	    name = @name
        """;
    internal static String GetById =>
        """
            SELECT * FROM regions
            WHERE id = @id;
        """;

    internal static String GetByName =>
        """
            SELECT * FROM regions
            WHERE name = @name;
        """;

    internal static String GetPage =>
        """
            SELECT 
                COUNT(*) OVER() as count, 
                *
            FROM regions
            OFFSET @offset 
            LIMIT @limit
        """;

    internal static String Remove =>
        """
        	DELETE ON CASCADE FROM regions WHERE id = @id
        """;
}