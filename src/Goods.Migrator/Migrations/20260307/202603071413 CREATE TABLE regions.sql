CREATE TABLE regions (
	id serial primary key NOT NULL,
	name varchar NOT NULL,
	federalregion int NOT NULL,
	Foreign key(federalregion) REFERENCES federalregions(id) ON UPDATE CASCADE
);