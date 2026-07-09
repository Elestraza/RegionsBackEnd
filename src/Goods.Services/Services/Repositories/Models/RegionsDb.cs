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

        public Guid Id { get; set; }
        public String Name { get; set; }
        public FederalRegionsDb FederalRegion { get; set; }
        public RegionsDb
        (
            Guid id,
            String name,
            FederalRegionsDb federalRegion
        )
        {
            Id = id;
            Name = name;
            FederalRegion = federalRegion;
        }
    }
}
