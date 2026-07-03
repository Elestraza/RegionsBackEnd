using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Services.Products.Repositories.Models
{
    public class RegionsDb
    {
        /*
         CREATE TABLE regions (
	        id serial primary key NOT NULL,
	        name varchar NOT NULL,
	        federalregion int NOT NULL,
	        Foreign key(federalregion) REFERENCES federalregions(id) ON UPDATE CASCADE
        ;
         */

        public Int32 Id { get; set; }
        public String Type { get; set; }
        public ICollection<FederalRegionsDb> FederalRegion { get; set; }
        public RegionsDb
        (
            Int32 id,
            String type,
            ICollection<FederalRegionsDb> federalRegion
        )
        {
            Id = id;
            Type = type;
            FederalRegion = federalRegion;
        }
    }
}
