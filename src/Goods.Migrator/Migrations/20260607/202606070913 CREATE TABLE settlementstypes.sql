CREATE TYPE valid_settlementstypes AS ENUM ('город', 'деревня', 'село');

CREATE TABLE settlementstypes (
	id uuid primary key NOT NULL,
    settlementstype valid_settlementstypes NOT NULL,
);