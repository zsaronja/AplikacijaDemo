CREATE PROCEDURE [dbo].DohvatiPrimatelje
AS
	SELECT [dbo].Primatelji.ImePrezime, [dbo].Primatelji.BrojMobitela FROM [dbo].Primatelji
RETURN 0