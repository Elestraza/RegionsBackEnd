namespace Goods.Domain.Products;

public class FederalRegions
{
    public Guid Id { get; set; }
    public String Name { get; set; }
    public FederalRegions(
        Guid id,
        String name
    )
    {
        Id = id;
        Name = name;
    }
}