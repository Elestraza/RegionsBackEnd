namespace Goods.Services.Products.Repositories.Queries;

internal static class FederalRegionsSql
{
    internal static String FederalRegions_Save =>
        """
            INSERT INTO federalregions (
                id
                name
            )
            VALUES (
                @id,
                @name
            )
        	ON CONFLICT (id) DO UPDATE SET
        	    name = @name
        """;
    internal static String GetById =>
        """
            SELECT * FROM federalregions
            WHERE id = @id;
        """;

    internal static String GetByName =>
        """
            SELECT * FROM federalregions
            WHERE name = @name;
        """;

    internal static String GetPage =>
        """
            SELECT 
                COUNT(*) OVER() as count, 
                *
            FROM federalregions
            OFFSET @offset 
            LIMIT @limit
        """;

    internal static String Remove =>
        """
        	DELETE ON CASCADE FROM federalregions WHERE id = @id
        """;
}