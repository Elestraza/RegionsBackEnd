namespace Goods.Domain.Products;

public class SettlementsTypes
{
    public Guid Id { get; set; }
    public String Type { get; set; }
    public SettlementsTypes(
        Guid id,
        String type
    )
    {
        Id = id;
        Type = type;
    }
}