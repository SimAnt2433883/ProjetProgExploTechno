USE Prog3A25_AntoineTommy;

IF OBJECT_ID('Inscription', 'p') IS NOT NULL
	DROP PROCEDURE Inscription;
GO
CREATE PROCEDURE Inscription
	@nom VARCHAR(100),
	@email VARCHAR(100),
	@motPasse VARCHAR(100),
	@admin BIT = 0,
	@reponse VARCHAR(100) OUTPUT
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @sel UNIQUEIDENTIFIER=NEWID();
	BEGIN TRY
		INSERT INTO Utilisateur (nom, email, motPasseHash, administrateur, sel)
		VALUES (@nom, @email, HASHBYTES('SHA2_512', @motPasse + CAST(@sel AS NVARCHAR(36))), @admin, @sel);
		SET @reponse = SCOPE_IDENTITY();
	END TRY
	BEGIN CATCH
	-- Penser à changer @reponse par un INT qui retourne -1 et ensuite afficher le message d'erreur dans le code.
		SET @reponse = 'Une erreur est survenue, veuillez contacter votre administrateur.';
	END CATCH
END

GO
DECLARE @reponse NVARCHAR(50);
EXEC Inscription
	@nom = 'Tommy',
	@email = 'test2@test.ca',
	@motPasse = 'cegep',
	@admin = 0,
	@reponse = @reponse OUTPUT;
PRINT @reponse;