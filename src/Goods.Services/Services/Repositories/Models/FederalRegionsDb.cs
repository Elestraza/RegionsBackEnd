using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Services.Products.Repositories.Models
{
    public class FederalRegionsDb
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public FederalRegionsDb(
         Guid id,
         String name
        )
        {
            Id = id;
            Name = name;
        }
    }
}
