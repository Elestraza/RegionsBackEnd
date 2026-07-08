namespace Goods.Domain.Products;

public class CarCodes
{
    public Guid Id { get; set; }
    public String Code { get; set; }
    public Guid Regions { get; set; }

    public CarCodes
    (
        Guid id,
        String code,
        Guid regions
    )
    {
        Id = id;
        Code = code;
        Regions = regions;
    }
}