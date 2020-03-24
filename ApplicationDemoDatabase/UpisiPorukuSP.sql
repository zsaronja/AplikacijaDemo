CREATE PROCEDURE dbo.UpisiPoruku
 @Datum         DATETIME2 (2),
 @Primatelj     VARCHAR  (50),
 @BrojMobitela  VARCHAR  (12),
 @NazivDatoteke VARCHAR  (20) 
AS
	INSERT INTO dbo.Poruke (Datum, Primatelj, BrojMobitela, NazivDatoteke) VALUES (@Datum, @Primatelj, @BrojMobitela, @NazivDatoteke)
RETURN 0