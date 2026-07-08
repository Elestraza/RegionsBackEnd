namespace Goods.Domain.Products;

public class CarCodes
{
    public Guid Id { get; set; }
    public String Code { get; set; }
    public Regions Regions { get; set; }

    public CarCodes
    (
        Guid id,
        String code,
        Regions regions
    )
    {
        Id = id;
        Code = code;
        Regions = regions;
    }
}