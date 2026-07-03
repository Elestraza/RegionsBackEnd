using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Goods.Services.Products.Repositories.Models
{
    public class SettlementsDb
    {
        /*
        CREATE TABLE settlements(
            id serial primary key not null,
            type int NOT NULL,
            name varchar NOT NULL,
            region int NOT NULL,
            age int NOT NULL,
            ishero bool NOT NULL,
	        averagehotelcost int NOT NULL,
            Foreign key(type) REFERENCES settlementstypes(id) ON UPDATE CASCADE,
            Foreign key(region) REFERENCES regions(id) ON UPDATE CASCADE
        );
        */
        public Int32 Id { get; set; }
        public ICollection<SettlementsTypesDb> Type { get; set; }
        public String Name { get; set; }
        public ICollection<RegionsDb> Region { get; set; }
        public Int32 Age { get; set; }
        public Boolean IsHero { get; set; }
        public Int32 AverageHotelCost { get; set; }

        public SettlementsDb
        (
            Int32 id,
            ICollection<SettlementsTypesDb> type,
            String name,
            ICollection<RegionsDb> region,
            Int32 age,
            Boolean isHero,
            Int32 averageHotelCost
        ) 
        {
            Id = id;
            Type = type; 
            Name= name; 
            Region= region; 
            Age = age;
            IsHero = isHero;
            AverageHotelCost = averageHotelCost;
        }
    }
}
