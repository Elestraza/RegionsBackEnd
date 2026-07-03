namespace Goods.Domain.Products;

public class Settlements
{
    public Int32 Id { get; set; }
    public ICollection<SettlementsTypes> Type { get; set; }
    public String Name { get; set; }
    public ICollection<Regions> Region { get; set; }
    public Int32 Age { get; set; }
    public Boolean IsHero { get; set; }
    public Int32 AverageHotelCost { get; set; }

    public Settlements
    (
        Int32 id,
        ICollection<SettlementsTypes> type,
        String name,
        ICollection<Regions> region,
        Int32 age,
        Boolean isHero,
        Int32 averageHotelCost
    )
    {
        Id = id;
        Type = type;
        Name = name;
        Region = region;
        Age = age;
        IsHero = isHero;
        AverageHotelCost = averageHotelCost;
    }
}