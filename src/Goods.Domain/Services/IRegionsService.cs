using Goods.Domain.Products;
using Goods.Tools.Types.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Domain.Services
{
    public interface IRegionsService
    {
        Task<Result> SaveRegion(SettlementsBlank productBlank);
        Task<Regions> GetRegion(Guid id);
        Task<Page<Regions>> GetRegions(Int32 page, Int32 count);
        Task<Result> RemoveRegion(Guid id);
    }
}
