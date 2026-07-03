namespace Goods.Domain.Products;

public class RegionsBlank
{
    public Int32 Id { get; set; }
    public String Type { get; set; }
    public ICollection<FederalRegions> FederalRegion { get; set; }
}
