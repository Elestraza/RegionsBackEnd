namespace Goods.Domain.Products;

public class SettlementsTypes
{
    public Int32 Id { get; set; }
    public String Type { get; set; }
    public SettlementsTypes(
     Int32 id,
     String type
    )
    {
        Id = id;
        Type = type;
    }
}