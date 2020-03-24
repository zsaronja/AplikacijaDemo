CREATE PROCEDURE dbo.DodajPrimatelja
	@ImePrezime varchar(50),
	@BrojMobitela varchar(12)
AS
	INSERT INTO dbo.Primatelji (ImePrezime, BrojMobitela) VALUES (@ImePrezime, @BrojMobitela)
RETURN 0