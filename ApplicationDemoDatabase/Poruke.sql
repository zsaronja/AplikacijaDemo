CREATE TABLE [dbo].Poruke (
    Id            BIGINT        IDENTITY (1, 1) NOT NULL,
    Datum         DATETIME2 (2) NULL,
    Primatelj     VARCHAR (50)  NULL,
    BrojMobitela  VARCHAR (12)  NULL,
    NazivDatoteke VARCHAR (21)  NULL,
    PRIMARY KEY CLUSTERED (Id ASC),
    FOREIGN KEY (BrojMobitela) REFERENCES Primatelji(BrojMobitela)
);
