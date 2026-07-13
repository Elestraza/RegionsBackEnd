CREATE DATABASE regions;

USE regions;

CREATE TABLE federalregions (
	id uuid primary key not null,
	name varchar NOT NULL,
	historicalvalueage int NOT NULL
);

CREATE TABLE regions (
	id uuid primary key NOT NULL,
	name varchar NOT NULL,
	federalregion uuid NOT NULL,
	Foreign key(federalregion) REFERENCES federalregions(id) ON UPDATE CASCADE
);

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

CREATE TABLE carcodes (
	id uuid primary key not null,
	code varchar(3) NOT NULL,
	region uuid NOT NULL,
	Foreign key(region) REFERENCES regions(id) ON UPDATE CASCADE
);


-- TEST INSERTS --


INSERT INTO federalregions (id, name, historicalvalueage) VALUES (uuid_generate_v4(), 'NAME', 500);
INSERT INTO federalregions (id, name, historicalvalueage) VALUES (uuid_generate_v4(), 'NAME2', 400);
INSERT INTO federalregions (id, name, historicalvalueage) VALUES (uuid_generate_v4(), 'NAME3', 200);
SELECT * FROM federalregions;

INSERT INTO regions (id, name, federalregion) VALUES (
	uuid_generate_v4(), 
	'R', 
	'[dederaldegion.uuid]'
);
INSERT INTO regions (id, name, federalregion) VALUES (
	uuid_generate_v4(), 
	'R2', 
	'[dederaldegion.uuid]'
);
INSERT INTO regions (id, name, federalregion) VALUES (
	uuid_generate_v4(), 
	'R3', 
	'[dederaldegion.uuid]'
);
SELECT * FROM regions;

INSERT INTO settlements(
	id, 
	settlementtype, 
	name, 
	population, 
	region, 
	foundationyear, 
	ishero, 
	averagehotelcost
) VALUES(
	uuid_generate_v4(),
	1,
	'Settlement',
	48000,
	'[region.uuid]',
	'1373',
	False,
	1500
);
INSERT INTO settlements(
	id, 
	settlementtype, 
	name, 
	population, 
	region, 
	foundationyear, 
	ishero, 
	averagehotelcost
) VALUES(
	uuid_generate_v4(),
	3,
	'Settlement2',
	480000,
	'[region.uuid]',
	1103,
	False,
	4500
);
INSERT INTO settlements(
	id, 
	settlementtype, 
	name, 
	population, 
	region, 
	foundationyear, 
	ishero, 
	averagehotelcost
) VALUES(
	uuid_generate_v4(),
	1,
	'Settlement3',
	48000,
	'[region.uuid]',
	1572,
	True,
	9600
);

SELECT * FROM settlements;


INSERT INTO carcodes (id, code, region) VALUES (
	uuid_generate_v4(),
	'71',
	'[region.uuid]'
);
INSERT INTO carcodes (id, code, region) VALUES (
	uuid_generate_v4(),
	'70',
	'[region.uuid]'
);
INSERT INTO carcodes (id, code, region) VALUES (
	uuid_generate_v4(),
	'777',
	'[region.uuid]'
);
INSERT INTO carcodes (id, code, region) VALUES (
	uuid_generate_v4(),
	'750',
	'[region.uuid]'
);
INSERT INTO carcodes (id, code, region) VALUES (
	uuid_generate_v4(),
	'170',
	'[region.uuid]'
);
INSERT INTO carcodes (id, code, region) VALUES (
	uuid_generate_v4(),
	'05',
	'[region.uuid]'
);
INSERT INTO carcodes (id, code, region) VALUES (
	uuid_generate_v4(),
	'15',
	'[region.uuid]'
);
INSERT INTO carcodes (id, code, region) VALUES (
	uuid_generate_v4(),
	'55',
	'[region.uuid]'
);
SELECT * FROM carcodes;

DELETE FROM regions WHERE id = '[region.uuid]';


SELECT 
    COUNT(*) OVER() as count, 
    r.id,
    r.name,
    r.federalregion,
    fr.id as federalregion_id,
    fr.name as federalregion_name,
    fr.historicalvalueage as federalregion_historicalvalueage
FROM regions r
JOIN federalregions fr ON fr.id = r.federalregion
OFFSET 1
LIMIT 999

