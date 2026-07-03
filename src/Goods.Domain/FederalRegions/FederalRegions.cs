namespace Goods.Domain.Products;

public class FederalRegions
{
    public Int32 Id { get; set; }
    public String Type { get; set; }
    public FederalRegions(
        Int32 id,
        String type
    )
    {
        Id = id;
        Type = type;
    }
}