CREATE TABLE [dbo].[Libri]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Titolo] NCHAR(50) NULL, 
    [DataPub] DATE NULL, 
    [IdAutore] INT NULL, 
    [IdEditore] INT NULL, 
    [Prezzo] DECIMAL(18, 2) NULL, 
    [Quantita] INT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Prezzo di vendita del libro',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Libri',
    @level2type = N'COLUMN',
    @level2name = N'Prezzo'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Data di pubblicazione',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Libri',
    @level2type = N'COLUMN',
    @level2name = N'DataPub'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Titolo del Libro',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Libri',
    @level2type = N'COLUMN',
    @level2name = N'Titolo'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Disponibilità in magazzino',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Libri',
    @level2type = N'COLUMN',
    @level2name = N'Quantita'