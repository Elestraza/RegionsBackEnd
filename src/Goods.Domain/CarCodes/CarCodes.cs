namespace Goods.Domain.Products;

public class CarCodes
{
    public Int32 Id { get; set; }
    public Int32 Code { get; set; }
    public ICollection<Regions> Regions { get; set; }

    public CarCodes
    (
        Int32 id,
        Int32 code,
        ICollection<Regions> regions
    )
    {
        Id = id;
        Code = code;
        Regions = regions;
    }
}