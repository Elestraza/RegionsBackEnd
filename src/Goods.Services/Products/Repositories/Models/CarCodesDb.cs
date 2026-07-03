using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Services.Products.Repositories.Models
{
    public class CarCodesDb
    {
        /*
         CREATE TABLE carcodes (
	        id serial primary key not null,
	        code int NOT NULL,
	        region int NOT NULL,
	        Foreign key(region) REFERENCES regions(id) ON UPDATE CASCADE
        );*/
        public Int32 Id { get; set; }
        public Int32 Code { get; set; }
        public ICollection<RegionsDb> Regions { get; set; }

        public CarCodesDb
        (
            Int32 id,
            Int32 code,
            ICollection<RegionsDb> regions
        )
        {
            Id = id;
            Code = code;
            Regions = regions;
        }
    }
}
