CREATE TABLE nadzorovani
(
  ime character varying(50),
  priimek character varying(50),
  id serial NOT NULL
)
WITH (
  OIDS=FALSE
);
ALTER TABLE nadzorovani
  OWNER TO posty;