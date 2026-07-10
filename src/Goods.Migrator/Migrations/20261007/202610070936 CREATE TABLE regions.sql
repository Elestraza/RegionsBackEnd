CREATE TABLE regions (
	id uuid primary key NOT NULL,
	name varchar NOT NULL,
	federalregion uuid NOT NULL,
	Foreign key(federalregion) REFERENCES federalregions(id) ON UPDATE CASCADE
);
