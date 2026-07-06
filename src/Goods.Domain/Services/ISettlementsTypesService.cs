using Goods.Domain.Products;
using Goods.Tools.Types.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Domain.Services
{
    public interface ISettlementsTypesService
    {
        Task<Result> SaveSettlementsType(SettlementsTypesBlank blank);
        Task<SettlementsTypes?> GetSettlementsType(Guid id);
        Task<Page<SettlementsTypes>> GetSettlementsTypes(Int32 page, Int32 count);
        Task<Result> RemoveSettlementsType(Guid id);
    }
}
