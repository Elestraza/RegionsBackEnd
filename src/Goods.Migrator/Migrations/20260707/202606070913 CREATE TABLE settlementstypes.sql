CREATE TABLE settlementstypes (
	id uuid primary key NOT NULL,
  	settlementstype varchar NOT NULL CHECK(settlementstype = 'город' OR settlementstype = 'деревня' OR settlementstype = 'село')
);