namespace Goods.Domain.Products;

public class FederalRegions
{
    public Guid Id { get; set; }
    public String Name { get; set; }
    public Int32 HistoricalValueAge { get; set; }

    public FederalRegions
    (
        Guid id,
        String name,
        Int32 historicalValueAge
    )
    {
        Id = id;
        Name = name;
        HistoricalValueAge = historicalValueAge;
    }
}