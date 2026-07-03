namespace Goods.Domain.Products;

public class Regions
{
    public Int32 Id { get; set; }
    public String Type { get; set; }
    public ICollection<FederalRegions> FederalRegion { get; set; }
    public Regions
    (
        Int32 id,
        String type,
        ICollection<FederalRegions> federalRegion
    )
    {
        Id = id;
        Type = type;
        FederalRegion = federalRegion;
    }
}