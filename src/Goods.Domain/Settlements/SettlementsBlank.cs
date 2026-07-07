namespace Goods.Domain.Products;

public class SettlementsBlank
{
    public Guid Id { get; set; }
    public SettlementsTypes Type { get; set; }
    public String Name { get; set; }
    public Int32 Population { get; set; }
    public Regions Region { get; set; }
    public String FoundationYear { get; set; }
    public Boolean IsHero { get; set; }
    public Int32 AverageHotelCost { get; set; }
}
