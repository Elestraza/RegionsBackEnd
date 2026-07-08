CREATE TABLE carcodes (
	id uuid primary key not null,
	code varchar(3) NOT NULL,
	region uuid NOT NULL,
	Foreign key(region) REFERENCES regions(id) ON UPDATE CASCADE
);