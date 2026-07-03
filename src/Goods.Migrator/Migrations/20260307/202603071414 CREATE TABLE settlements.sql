CREATE TABLE settlements(
	id serial primary key not null,
	type int NOT NULL,
	name varchar NOT NULL,
	region int NOT NULL,
	age int NOT NULL,
	Foreign key(type) REFERENCES settlementstypes(id) ON UPDATE CASCADE,
	Foreign key(region) REFERENCES regions(id) ON UPDATE CASCADE
);