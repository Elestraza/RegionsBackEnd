using Goods.Domain.Products;
using Goods.Tools.Types.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Domain.Services
{
    public interface ICarCodesService
    {
        Task<Result> SaveCarCode(CarCodesBlank blank);
        Task<CarCodes> GetCarCode(Int32 id);
        Task<Page<CarCodes>> GetCarCodes(Int32 page, Int32 count);
        Task<Result> RemoveCarCode(Int32 id);
    }
}
