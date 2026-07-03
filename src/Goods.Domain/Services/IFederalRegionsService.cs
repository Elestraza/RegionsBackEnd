using Goods.Domain.Products;
using Goods.Tools.Types.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Domain.Services
{
    public interface IFederalRegionsService
    {
        Task<Result> SaveFederalRegion(SettlementsBlank productBlank);
        Task<Product> GetFederalRegion(Guid id);
        Task<Page<Product>> GetFederalRegions(Int32 page, Int32 count);
        Task<Result> RemoveFederalRegion(Guid id);
    }
}
