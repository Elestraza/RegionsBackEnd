namespace Goods.Domain.Products;

public class Settlements
{

    /*
        id uuid primary key not null,
	    settlementtype uuid NOT NULL,
	    name varchar NOT NULL,
	    population int NOT NULL,
	    region uuid NOT NULL,
	    foundationyear varchar(4) NOT NULL,
	    ishero bool NOT NULL,
	    averagehotelcost int NOT NULL,
     */
    
    
    public Guid Id { get; set; }
    public SettlementsTypes Type { get; set; }
    public String Name { get; set; }
    public Int32 Population {  get; set; }
    public Regions Region { get; set; }
    public Int32 FoundationYear { get; set; }
    public Boolean IsHero { get; set; }
    public Int32 AverageHotelCost { get; set; }

    public Settlements
    (
        Guid id,
        SettlementsTypes type,
        String name,
        Int32 population,
        Regions region,
        Int32 foundationYear,
        Boolean isHero,
        Int32 averageHotelCost
    )
    {
        Id = id;
        Type = type;
        Name = name;
        Population = population;
        Region = region;
        FoundationYear = foundationYear;
        IsHero = isHero;
        AverageHotelCost = averageHotelCost;
    }
}