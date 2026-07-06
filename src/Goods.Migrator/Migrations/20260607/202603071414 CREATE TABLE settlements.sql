CREATE TABLE settlements(
	id uuid primary key not null,
	settlementtype uuid NOT NULL,
	name varchar NOT NULL,
	population int NOT NULL,
	region uuid NOT NULL,
	foundationyear varchar(4) NOT NULL,
	ishero bool NOT NULL,
	averagehotelcost int NOT NULL,
	Foreign key(settlementtype) REFERENCES settlementstypes(id) ON UPDATE CASCADE,
	Foreign key(region) REFERENCES regions(id) ON UPDATE CASCADE
);