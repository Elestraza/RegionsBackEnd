namespace Goods.Services.Products.Repositories.Queries;

internal static class FederalRegionsSql
{
    internal static String FederalRegions_Save =>
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