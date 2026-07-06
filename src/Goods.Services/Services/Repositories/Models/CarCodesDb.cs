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
        public Guid Id { get; set; }
        public Int32 Code { get; set; }
        public RegionsDb Regions { get; set; }

        public CarCodesDb
        (
            Guid id,
            Int32 code,
            RegionsDb regions
        )
        {
            Id = id;
            Code = code;
            Regions = regions;
        }
    }
}
