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
        Task<Result> SaveCarCode(SettlementsBlank productBlank);
        Task<Product> GetCarCode(Guid id);
        Task<Page<Product>> GetCarCodes(Int32 page, Int32 count);
        Task<Result> RemoveCarCode(Guid id);
    }
}
