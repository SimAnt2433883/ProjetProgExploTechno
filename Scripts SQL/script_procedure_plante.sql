USE Prog3A25_AntoineTommy;

IF OBJECT_ID('CreerPlante', 'p') IS NOT NULL
	DROP PROCEDURE CreerPlante;
GO
CREATE PROCEDURE CreerPlante
	@nom        VARCHAR(100),
    @noWiki     INT = NULL,
    @alerte     BIT,
    @newId      INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        INSERT INTO Plante (nom, noWiki, alerte)
        VALUES (@nom, @noWiki, @alerte);

        SET @newId = SCOPE_IDENTITY();
    END TRY
    BEGIN CATCH
        SET @newId = -1;
    END CATCH
END;
GO
DECLARE @newId INT;
EXEC CreerPlante
	@nom = 'Test',
	@alerte = 0,
	@newId = @newId OUTPUT;
PRINT @newId;

SELECT * FROM Plante;