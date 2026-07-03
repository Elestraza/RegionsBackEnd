namespace Goods.Domain.Products;

public class CarCodesBlank
{
    public Int32 Id { get; set; }
    public Int32 Code { get; set; }
    public ICollection<Regions> Regions { get; set; }
}
