using Goods.Domain.Products;

namespace Goods.Services.Products.Repositories;

public interface ICarCodesRepository
{
    Task SaveCarCode(CarCodes blank);
    Task<CarCodes?> GetCarCode(Guid id);
    Task<CarCodes?> GetCarCode(String name);
    Task<Page<CarCodes>> GetCarCodes(Int32 page, Int32 count);
    Task RemoveCarCode(Guid id);
}