using Goods.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Goods.Services.Products.Repositories.Models
{
    public class SettlementsTypesDb
    {

        public Int32 Id {  get; set; }
        public String Type { get; set; }
        public SettlementsTypesDb(
         Int32 id,
         String type
        )
        {
            Id = id;
            Type = type;
        }
    }
}
