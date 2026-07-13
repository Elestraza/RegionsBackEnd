CREATE TABLE settlements(
	id uuid primary key not null,
	settlementtype int NOT NULL, /*ENUM TYPE*/
	name varchar NOT NULL,
	population int NOT NULL,
	region uuid NOT NULL,
	foundationyear int NOT NULL,
	ishero bool NOT NULL DEFAULT False,
	averagehotelcost int NOT NULL,
	Foreign key(region) REFERENCES regions(id) ON UPDATE CASCADE
);