CREATE TABLE carcodes (
	id serial primary key not null,
	code int NOT NULL,
	region int NOT NULL,
	Foreign key(region) REFERENCES regions(id) ON UPDATE CASCADE
);