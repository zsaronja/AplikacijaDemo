CREATE PROCEDURE dbo.DodajLogZapis
@Vrijeme	DATETIME2(2),
@Akcija		INT			,
@Korisnik	VARCHAR	(32),
@Podaci		VARCHAR	(200)
AS
	INSERT INTO dbo.Logovi (Vrijeme, Akcija, Korisnik, Podaci) VALUES (@Vrijeme, @Akcija, @Korisnik, @Podaci)
RETURN 0