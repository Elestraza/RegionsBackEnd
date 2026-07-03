namespace Goods.Domain.Products;

public class SettlementsBlank
{
    public Int32 Id { get; set; }
    public ICollection<SettlementsTypes> Type { get; set; }
    public String Name { get; set; }
    public ICollection<Regions> Region { get; set; }
    public Int32 Age { get; set; }
    public Boolean IsHero { get; set; }
    public Int32 AverageHotelCost { get; set; }
}
