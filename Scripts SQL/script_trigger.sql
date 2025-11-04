-- SELECT les données de la table et vérifie si les données sont entre la valeur max et min dans le wiki
-- sinon l'alerte de la plante s'active
USE Prog3A25_AntoineTommy;
IF OBJECT_ID('insert_donnee', 'TR') IS NOT NULL
	DROP TRIGGER insert_donnee;
GO   
CREATE TRIGGER insert_donnee
ON Donnee AFTER INSERT AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @noPlante INT;
	DECLARE c_noPlante CURSOR FOR
	SELECT noPlante
	FROM Plante
	-- curseur non testé (voir théorie)
	UPDATE P
    SET alerte = CASE 
                    WHEN I.humidite < W.minHumidite OR I.humidite > W.maxHumidite
                      OR I.temperature < W.minTemperature OR I.temperature > W.maxTemperature
                      OR I.rayonsUV < W.minRayonsUV OR I.rayonsUV > W.maxRayonsUV
                    THEN 1
                    ELSE 0
                 END
    FROM Plante P
    JOIN inserted I
        ON P.noPlante = I.noPlante
    JOIN Wiki W
        ON P.noWiki = W.noWiki
	WHERE P.noPlante = @noPlante;
END
-- température entre 15 et 35
-- humidité entre 20 et 50
-- rayon UV 60 et 100

-- tests
INSERT INTO Donnee (dateHeure, temperature, humidite, rayonsUV, noPlante) VALUES
('2025-09-19 08:45:00', 40, 30, 70, 5);

SELECT *
FROM Plante
WHERE noPlante = 5;