using Goods.Domain.Products;
using Goods.Tools.Types.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Domain.Services
{
    public interface ISettlementsService
    {
        Task<Result> SaveSettlement(SettlementsBlank productBlank);
        Task<Settlements> GetSettlement(Int32 id);
        Task<Page<Settlements>> GetSettlements(Int32 page, Int32 count);
        Task<Result> RemoveSettlement(Int32 id);
    }
}
