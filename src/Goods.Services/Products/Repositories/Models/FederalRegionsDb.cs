using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Services.Products.Repositories.Models
{
    public class FederalRegionsDb
    {
        public Int32 Id { get; set; }
        public String Type { get; set; }
        public FederalRegionsDb(
         Int32 id,
         String type
        )
        {
            Id = id;
            Type = type;
        }
    }
}
