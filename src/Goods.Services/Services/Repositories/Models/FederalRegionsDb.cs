using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Services.Products.Repositories.Models
{
    public class FederalRegionsDb
    {
        /*
         CREATE TABLE carcodes (
	        id serial primary key not null,
	        code int NOT NULL,
	        region int NOT NULL,
	        Foreign key(region) REFERENCES regions(id) ON UPDATE CASCADE
        );*/
        public Guid Id { get; set; }
        public String Name { get; set; }
        public Int32 HistoricalValueAge { get; set; }

        public FederalRegionsDb
        (
            Guid id,
            String name,
            Int32 historicalValueAge
        )
        {
            Id = id;
            Name = name;
            HistoricalValueAge = historicalValueAge;
        }
    }
}
