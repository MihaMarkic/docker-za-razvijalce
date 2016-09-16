#!/bin/bash
set -e

psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" <<-EOSQL
    CREATE DATABASE sledilec;
    GRANT ALL PRIVILEGES ON DATABASE sledilec TO postgres;
EOSQL
psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname sledilec  <<-EOSQL
	CREATE TABLE nadzorovani
	(
	  ime character varying(50),
	  priimek character varying(50),
	  id serial NOT NULL
	)
	WITH (
	  OIDS=FALSE
	);
EOSQL