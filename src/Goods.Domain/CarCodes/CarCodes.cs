namespace Goods.Domain.Products;

public class CarCodes
{
    public Guid Id { get; set; }
    public Int32 Code { get; set; }
    public Regions Regions { get; set; }

    public CarCodes
    (
        Guid id,
        Int32 code,
        Regions regions
    )
    {
        Id = id;
        Code = code;
        Regions = regions;
    }
}