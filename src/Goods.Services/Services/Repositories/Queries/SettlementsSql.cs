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
            SELECT 
                s.id,
                s.settlementtype,
                s.name,
                s.population,
                s.region,
                s.foundationyear,
                s.ishero,
                s.averagehotelcost,
                r.id as region_id,
                r.name as region_name,
                r.federalregion as region_federalregion
            FROM settlements s
            JOIN regions r ON r.id = s.region
            WHERE s.id = @id;
        """;

    internal static String GetByName =>
        """
            SELECT
                s.id,
                s.settlementtype,
                s.name,
                s.population,
                s.region,
                s.foundationyear,
                s.ishero,
                s.averagehotelcost,
                r.id as region_id,
                r.name as region_name,
                r.federalregion as region_federalregion
            FROM settlements s
            JOIN regions r ON r.id = s.region
            WHERE s.name = @name;
        """;

    internal static String GetPage =>
        """
            SELECT 
                COUNT(*) OVER() as count, 
                s.id,
                s.settlementtype,
                s.name,
                s.population,
                s.region,
                s.foundationyear,
                s.ishero,
                s.averagehotelcost,
                r.id as region_id,
                r.name as region_name,
                r.federalregion as region_federalregion
            FROM settlements s
            JOIN regions r ON r.id = s.region
            OFFSET @offset 
            LIMIT @limit
        """;

    internal static String Remove =>
        """
        	DELETE FROM settlements s WHERE s.id = @id
        """;
}