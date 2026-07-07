namespace Goods.Services.Products.Repositories.Queries;

internal static class SettlementsSql
{
    internal static String Settlements_Save =>
        """
            INSERT INTO settlements (
                id,
                settlementtype,
                name,
                population,
                region,
                foundationyear,
                ishero,
                averagehotelcost
            )
            VALUES (
                @id,
                @settlementtype,
                @name,
                @population,
                @region,
                @foundationyear,
                @ishero,
                @averagehotelcost
            )
        	ON CONFLICT (id) DO UPDATE SET
                settlementtype = @settlementtype,
                name = @name,
                population = @population,
                region = @region,
                foundationyear = @foundationyear,
                ishero = @ishero,
                averagehotelcost = @averagehotelcost
        """;
    internal static String GetById =>
        """
            SELECT * FROM settlements
            WHERE id = @id;
        """;

    internal static String GetByName =>
        """
            SELECT * FROM settlements
            WHERE name = @name;
        """;

    internal static String GetPage =>
        """
            SELECT 
                COUNT(*) OVER() as count, 
                *
            FROM settlements
            OFFSET @offset 
            LIMIT @limit
        """;

    internal static String Remove =>
        """
        	DELETE ON CASCADE FROM settlements WHERE id = @id
        """;
}