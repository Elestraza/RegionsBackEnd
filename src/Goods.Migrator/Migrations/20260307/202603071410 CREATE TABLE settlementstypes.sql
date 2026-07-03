CREATE TABLE settlementstypes(
	id serial primary key NOT NULL,
	type varchar NOT NULL CHECK(type = 'город' OR type = 'деревня' OR type = 'село')
);