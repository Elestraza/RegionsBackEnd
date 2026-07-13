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
        Task<Settlements> GetSettlement(Guid id);
        Task<String> GetSettlementHistoryValue(Guid id);
        Task<Page<Settlements>> GetSettlements(Int32 page, Int32 count);
        Task<Page<Settlements>> GetTopTenSettlements(Guid chosenRegion, DateOnly vacationDate);
        Task<Result> RemoveSettlement(Guid id);
    }
}
