namespace Goods.Services.Products.Repositories.Queries;

internal static class RegionsSql
{
    internal static String Regions_Save =>
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

}