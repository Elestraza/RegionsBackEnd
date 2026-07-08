namespace Goods.Services.Products.Repositories.Queries;

internal static class CarCodesSql
{
    internal static String CarCodes_Save =>
        """
            INSERT INTO carcodes (
                id
                code
                region
            )
            VALUES (
                @id,
                @code,
                @federalregion
            )
        	ON CONFLICT (id) DO UPDATE SET
        	    region = @region,
        	    code = @code
        """;
    internal static String GetById =>
        """
            SELECT  
                c.id,
                c.code,
                c.region,
                r.id as region_id,
                r.name as region_name,
                r.federalregion as region_federalregion
            FROM carcodes c
            JOIN regions r ON r.id = c.region
            WHERE id = @id;
        """;

    internal static String GetByName =>
        """
            SELECT 
                c.id,
                c.code,
                c.region,
                r.id as region_id,
                r.name as region_name,
                r.federalregion as region_federalregion
            FROM carcodes c
            JOIN regions r ON r.id = c.region
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
                r.federalregion as region_federalregion
            FROM carcodes c
            JOIN regions r ON r.id = c.region
            OFFSET @offset 
            LIMIT @limit
        """;

    internal static String Remove =>
        """
        	DELETE ON CASCADE FROM carcodes WHERE id = @id
        """;
}