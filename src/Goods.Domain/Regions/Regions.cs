namespace Goods.Domain.Products;

public class Regions
{
    public Guid Id { get; set; }
    public String Name { get; set; }
    public FederalRegions FederalRegion { get; set; }
    public Regions
    (
        Guid id,
        String name,
        FederalRegions federalRegion
    )
    {
        Id = id;
        Name = name;
        FederalRegion = federalRegion;
    }
}