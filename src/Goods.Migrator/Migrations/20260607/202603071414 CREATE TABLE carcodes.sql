CREATE TABLE carcodes (
	id uuid primary key not null,
	code int NOT NULL,
	region uuid NOT NULL,
	Foreign key(region) REFERENCES regions(id) ON UPDATE CASCADE
);