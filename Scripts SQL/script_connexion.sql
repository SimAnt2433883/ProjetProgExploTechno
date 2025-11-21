USE Prog3A25_AntoineTommy;

/*
ALTER TABLE Utilisateur
ADD motPasseHash BINARY(64);
GO
DECLARE @bound INT;

SELECT @bound = COUNT(noUtilisateur)
FROM Utilisateur;

DECLARE @i INT = 0;
WHILE @i <= @bound
BEGIN
	DECLARE @mdp VARCHAR(100);
	
	SELECT @mdp = motPasse
	FROM Utilisateur
	WHERE noUtilisateur = @i

	UPDATE Utilisateur
	SET motPasseHash = HASHBYTES('SHA2_512', motPasse + CAST(sel AS NVARCHAR(36)))
	WHERE noUtilisateur = @i;

	SET @i = @i + 1;
END

ALTER TABLE Utilisateur
DROP COLUMN motPasse;
*/

IF OBJECT_ID('Connexion', 'P') IS NOT NULL
	DROP PROCEDURE Connexion;
GO

-- donne le noUser d'un mdp et d'un email, sinon -1
CREATE PROCEDURE Connexion 
	@email VARCHAR(100), 
	@mdp VARCHAR(100),
	@reponse INT OUTPUT
AS 
BEGIN
	SET NOCOUNT ON;
	-- check si bon email
	IF EXISTS (SELECT noUtilisateur FROM Utilisateur WHERE @email = email)
	BEGIN
		DECLARE @noUser INT;

		-- verifie le mdp et prend le noUser correspondant
		SET @noUser = (SELECT noUtilisateur FROM Utilisateur WHERE @email = email AND motPasseHash = HASHBYTES('SHA2_512', @mdp + CAST(sel AS NVARCHAR(36))));
		
		-- verifie si un noUser a ete pris et renvoie le numero correspondant
		SET @reponse = ISNULL(@noUser, -1);
	END
	ELSE
		-- mauvais email
		SET @reponse = -1;
END;
GO

DELETE FROM Donnee
WHERE noDonnee > 12;

DECLARE @reponse INT;
EXEC Connexion 
	@email = 'admin@example.com', 
	@mdp = 'admin123', 
	@reponse = @reponse OUTPUT;
PRINT @reponse;

SELECT * FROM Donnee
