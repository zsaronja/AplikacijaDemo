CREATE TABLE [dbo].Logovi
(
	Id			BIGINT      IDENTITY (1, 1) NOT NULL, 
    Vrijeme		DATETIME2	(2)		NOT NULL,
	Akcija		INT					NOT NULL,
	Korisnik	VARCHAR		(32)	NOT NULL,
	Podaci		VARCHAR		(200)
	PRIMARY KEY (Id ASC)
)
