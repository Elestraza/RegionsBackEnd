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
            SELECT * FROM carcodes
            WHERE id = @id;
        """;

    internal static String GetByName =>
        """
            SELECT * FROM carcodes
            WHERE name = @name;
        """;

    internal static String GetPage =>
        """
            SELECT 
                COUNT(*) OVER() as count, 
                *
            FROM carcodes
            OFFSET @offset 
            LIMIT @limit
        """;

    internal static String Remove =>
        """
        	DELETE ON CASCADE FROM carcodes WHERE id = @id
        """;
}