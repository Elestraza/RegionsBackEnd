namespace Goods.Services.Products.Repositories.Queries;

internal static class RegionsSql
{
    internal static String Regions_Save =>
        """
            INSERT INTO regions (
                id,
                name,
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
            SELECT
                r.id,
                r.name,
                r.federalregion,
                fr.id as federalregion_id,
                fr.name as federalregion_name,
                fr.historicalvalueage as federalregion_historicalvalueage
            FROM regions r
            JOIN federalregions fr ON fr.id = r.federalregion
            WHERE r.id = @id;
        """;

    internal static String GetByName =>
        """
            SELECT
                r.id,
                r.name,
                r.federalregion,
                fr.id as federalregion_id,
                fr.name as federalregion_name,
                fr.historicalvalueage as federalregion_historicalvalueage
            FROM regions r
            JOIN federalregions fr ON fr.id = r.federalregion
            WHERE r.name = @name;
        """;

    internal static String GetPage =>
        """
            SELECT 
                COUNT(*) OVER() as count, 
                r.id,
                r.name,
                r.federalregion,
                fr.id as federalregion_id,
                fr.name as federalregion_name,
                fr.historicalvalueage as federalregion_historicalvalueage
            FROM regions r
            JOIN federalregions fr ON fr.id = r.federalregion
            OFFSET @offset 
            LIMIT @limit
        """;

    internal static String Remove =>
        """
        	DELETE FROM regions WHERE id = @id
        """;
}