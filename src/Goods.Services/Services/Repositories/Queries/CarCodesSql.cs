namespace Goods.Services.Products.Repositories.Queries;

internal static class CarCodesSql
{
    internal static String CarCodes_Save =>
        """
            INSERT INTO carcodes (
                id,
                code,
                region
            )
            VALUES (
                @id,
                @code,
                @region
            )
        	ON CONFLICT (id) DO UPDATE SET
                code = @code,
        	    region = @region;
        """;
    internal static String GetById =>
        """
            SELECT  
                c.id,
                c.code,
                c.region,
                r.id as region_id,
                r.name as region_name,
                r.federalregion as region_federalregion,
                fr.id as federalregion_id,
                fr.name as federalregion_name,
                fr.historicalvalueage as federalregion_historicalvalueage
            FROM carcodes c
            JOIN regions r ON r.id = c.region
            JOIN federalregions fr ON fr.id = r.federalregion
            WHERE c.id = @id;
        """;

    internal static String GetByName =>
        """
            SELECT 
                c.id,
                c.code,
                c.region,
                r.id as region_id,
                r.name as region_name,
                r.federalregion as region_federalregion,
                fr.id as federalregion_id,
                fr.name as federalregion_name,
                fr.historicalvalueage as federalregion_historicalvalueage
            FROM carcodes c
            JOIN regions r ON r.id = c.region
            JOIN federalregions fr ON fr.id = r.federalregion
            WHERE c.code = @code;
        """;

    internal static String GetPage =>
        """
            SELECT 
                COUNT(*) OVER() as count, 
                c.id,
                c.code,
                c.region,
                r.id as region_id,
                r.name as region_name,
                r.federalregion as region_federalregion,
                fr.id as federalregion_id,
                fr.name as federalregion_name,
                fr.historicalvalueage as federalregion_historicalvalueage
            FROM carcodes c
            JOIN regions r ON r.id = c.region
            JOIN federalregions fr ON fr.id = r.federalregion
            OFFSET @offset 
            LIMIT @limit;
        """;

    internal static String Remove =>
        """
        	DELETE FROM carcodes WHERE id = @id;
        """;
}