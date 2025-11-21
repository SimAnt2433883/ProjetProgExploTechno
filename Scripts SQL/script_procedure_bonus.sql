USE Prog3A25_AntoineTommy;

IF OBJECT_ID('Verif_donnee', 'P') IS NOT NULL
	DROP PROCEDURE Verif_donnee;
IF OBJECT_ID('insert_wiki', 'TR') IS NOT NULL
	DROP TRIGGER insert_wiki;


GO
CREATE PROCEDURE Verif_donnee 
	@humidite DECIMAL(4, 1),
	@temperature DECIMAL(4, 1),
	@rayonsUV DECIMAL(4, 1),
	@minHumidite DECIMAL(4, 1),
	@minTemperature DECIMAL(4, 1),
	@minRayonsUV DECIMAL(4, 1),
	@maxHumidite DECIMAL(4, 1),
	@maxTemperature DECIMAL(4, 1),
	@maxRayonsUV DECIMAL(4, 1),
	@valide BIT OUTPUT
AS
BEGIN
	DECLARE @valideHumidite BIT = 0;
	DECLARE @valideTemperature BIT = 0;
	DECLARE @valideRayonsUV BIT = 0;

	IF (@minHumidite < @humidite AND @humidite < @maxHumidite)
		SET @valideHumidite = 1;
	IF (@minTemperature < @temperature AND @temperature < @maxTemperature)
		SET @valideTemperature = 1;
	IF (@minRayonsUV < @rayonsUV AND @rayonsUV < @maxRayonsUV)
		SET @valideRayonsUV = 1;

	IF (@valideHumidite = 1 AND @valideTemperature = 1 AND @valideRayonsUV = 1)
		SET @valide = 1;
	ELSE
		SET @valide = 0;
END;
GO

GO
CREATE TRIGGER insert_wiki ON Plante
AFTER INSERT AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @noWiki INT;
	DECLARE @noPlante INT;

	DECLARE cWiki CURSOR FOR
	SELECT noWiki, noPlante FROM inserted;

	OPEN cWiki;
	FETCH cWiki INTO @noWiki, @noPlante;
	WHILE (@@FETCH_STATUS = 0)
	BEGIN
		DECLARE @humidite DECIMAL(4, 1);
		DECLARE @temperature DECIMAL(4, 1);
		DECLARE @rayonsUV DECIMAL(4, 1);
		DECLARE @minHumidite DECIMAL(4, 1);
		DECLARE @minTemperature DECIMAL(4, 1);
		DECLARE @minRayonsUV DECIMAL(4, 1);
		DECLARE @maxHumidite DECIMAL(4, 1);
		DECLARE @maxTemperature DECIMAL(4, 1);
		DECLARE @maxRayonsUV DECIMAL(4, 1);
		SELECT
			noDonnee,
			humidite,
			temperature,
			rayonsUV,
			minHumidite,
			minTemperature,
			minRayonsUV,
			maxHumidite,
			maxTemperature,
			maxRayonsUV
		FROM Plante
		JOIN Wiki
			ON Plante.noWiki = Wiki.noWiki
		JOIN Donnee
			ON Plante.noPlante = Donnee.noPlante
		WHERE noDonnee =
		(
			SELECT MAX(noDonnee)
			FROM Donnee
			WHERE Donnee.noPlante = Plante.noPlante
		)
		AND @noPlante = Plante.noPlante;
		DECLARE @alerte BIT;
		EXEC Verif_donnee
			@humidite,
			@temperature,
			@rayonsUV,
			@minHumidite,
			@minTemperature,
			@minRayonsUV,
			@maxHumidite,
			@maxTemperature,
			@maxRayonsUV,
			@alerte OUTPUT;
		UPDATE Plante 
		SET alerte = @alerte
		WHERE noPlante = @noPlante;
		FETCH cWiki INTO @noWiki, @noPlante;
	END;
	CLOSE cWiki;
	DEALLOCATE cWiki;
	SET NOCOUNT OFF;
END;
GO
