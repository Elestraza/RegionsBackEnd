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
        Task<Result> SaveFederalRegion(FederalRegionsBlank blank);
        Task<FederalRegions> GetFederalRegion(Guid id);
        Task<Page<FederalRegions>> GetFederalRegions(Int32 page, Int32 count);
        Task<Result> RemoveFederalRegion(Guid id);
    }
}
