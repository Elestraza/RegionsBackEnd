namespace Goods.Services.Products.Repositories.Queries;

internal static class FederalRegionsSql
{
    internal static String FederalRegions_Save =>
        """
            INSERT INTO federalregions (
                id,
                name,
                historicalvalueage
            )
            VALUES (
                @id,
                @name,
                @historicalvalueage
            )
        	ON CONFLICT (id) DO UPDATE SET
                name = @name,
        	    historicalvalueage = @historicalvalueage;
        """;
    internal static String GetById =>
        """
            SELECT  
                id,
                name,
                historicalvalueage,
            FROM federalregions
            WHERE id = @id;
        """;

    internal static String GetByName =>
        """
            SELECT 
                id,
                name,
                historicalvalueage,
            FROM federalregions
            WHERE name = @name;
        """;

    internal static String GetPage =>
        """
            SELECT 
                COUNT(*) OVER() as count, 
                id,
                name,
                historicalvalueage
            FROM federalregions
            OFFSET @offset 
            LIMIT @limit;
        """;

    internal static String Remove =>
        """
        	DELETE FROM federalregions WHERE id = @id;
        """;
}