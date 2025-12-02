USE Prog3A25_AntoineTommy;
GO

IF OBJECT_ID('Verif_donnee', 'P') IS NOT NULL
    DROP PROCEDURE Verif_donnee;
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
    @alerte BIT OUTPUT
AS
BEGIN
    DECLARE @valideHumidite BIT = 1;
    DECLARE @valideTemperature BIT = 1;
    DECLARE @valideRayonsUV BIT = 1;

	-- On check chaque valeur dans chaque barème
    IF (@humidite < @minHumidite OR @humidite > @maxHumidite)
        SET @valideHumidite = 0;
    IF (@temperature < @minTemperature OR @temperature > @maxTemperature)
        SET @valideTemperature = 0;
    IF (@rayonsUV < @minRayonsUV OR @rayonsUV > @maxRayonsUV)
        SET @valideRayonsUV = 0;

	-- On check les valeurs ensemble
    IF (@valideHumidite = 1 AND @valideTemperature = 1 AND @valideRayonsUV = 1)
        SET @alerte = 0; -- Tout est valide, pas d'alerte
    ELSE
        SET @alerte = 1; -- Donnée invalide, alerte
END;
GO


IF OBJECT_ID('insert_wiki', 'TR') IS NOT NULL
    DROP TRIGGER insert_wiki;
GO

CREATE TRIGGER insert_wiki 
ON Plante
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @noWiki INT;
    DECLARE @noPlante INT;

    DECLARE cWiki CURSOR FOR
    SELECT noWiki, noPlante FROM inserted;

    OPEN cWiki;
    FETCH cWiki INTO @noWiki, @noPlante;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        DECLARE 
            @humidite DECIMAL(4,1),
            @temperature DECIMAL(4,1),
            @rayonsUV DECIMAL(4,1),
            @minHumidite DECIMAL(4,1),
            @minTemperature DECIMAL(4,1),
            @minRayonsUV DECIMAL(4,1),
            @maxHumidite DECIMAL(4,1),
            @maxTemperature DECIMAL(4,1),
            @maxRayonsUV DECIMAL(4,1),
            @alerte BIT;

        -- On récupère les données de la plante et les barèmes du wiki
        SELECT TOP 1 -- Top 1 s'assure qu'il n'y a pas de doublons
            @humidite = Donnee.humidite,
            @temperature = Donnee.temperature,
            @rayonsUV = Donnee.rayonsUV,
            @minHumidite = Wiki.minHumidite,
            @minTemperature = Wiki.minTemperature,
            @minRayonsUV = Wiki.minRayonsUV,
            @maxHumidite = Wiki.maxHumidite,
            @maxTemperature = Wiki.maxTemperature,
            @maxRayonsUV = Wiki.maxRayonsUV
        FROM Plante
        JOIN Wiki ON Plante.noWiki = Wiki.noWiki
        JOIN Donnee ON Plante.noPlante = Donnee.noPlante
        WHERE Plante.noPlante = @noPlante
        ORDER BY Donnee.dateHeure DESC; -- La donnée la plus récente en premier

		-- On vérifie les données
        EXEC Verif_donnee 
            @humidite, @temperature, @rayonsUV,
            @minHumidite, @minTemperature, @minRayonsUV,
            @maxHumidite, @maxTemperature, @maxRayonsUV,
            @alerte OUTPUT;

		-- On met à jour l'alerte de la plante
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

-- 30 = invalide
-- 31 = valide
UPDATE Plante SET noWiki = 30 WHERE noPlante = 2;
SELECT * FROM Plante;