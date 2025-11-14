USE Prog3A25_AntoineTommy;

IF OBJECT_ID('VueInteractionUtilisateur', 'V') IS NOT NULL
    DROP VIEW VueInteractionUtilisateur;
IF OBJECT_ID('VueReponseUtilisateur', 'V') IS NOT NULL
    DROP VIEW VueReponseUtilisateur;
IF OBJECT_ID('VueQuestionUtilisateur', 'V') IS NOT NULL
    DROP VIEW VueQuestionUtilisateur;
IF OBJECT_ID('VueInfoUtilisateur', 'V') IS NOT NULL
    DROP VIEW VueInfoUtilisateur;
IF OBJECT_ID('insert_question', 'TR') IS NOT NULL
	DROP TRIGGER insert_question;
IF OBJECT_ID('insert_reponse', 'TR') IS NOT NULL
	DROP TRIGGER insert_reponse;

GO
CREATE VIEW VueInfoUtilisateur AS
SELECT Utilisateur.noUtilisateur, 
	Plante.noPlante, 
	Utilisateur.nom AS utilisateurNom, 
	Plante.nom AS planteNom, 
	Utilisateur.administrateur, 
	COUNT(Question.noQuestion) AS nbQuestions
FROM Utilisateur
LEFT JOIN Plante
    ON Utilisateur.noPlante = Plante.noPlante
LEFT JOIN Question
    ON Utilisateur.noUtilisateur = Question.noUtilisateur
GROUP BY
    Utilisateur.noUtilisateur, 
	Plante.noPlante, 
	Utilisateur.nom, 
	Plante.nom, 
	Utilisateur.administrateur;

GO
CREATE VIEW VueQuestionUtilisateur AS
SELECT Utilisateur.noUtilisateur, 
	Utilisateur.nom, 
	Utilisateur.email, 
	Utilisateur.administrateur, 
	Question.noQuestion, 
	Question.titre, 
	Question.question AS question, 
	COUNT(LikesQuestion.noUtilisateur) AS nbLikes
FROM Utilisateur
JOIN Question
    ON Utilisateur.noUtilisateur = Question.noUtilisateur
LEFT JOIN LikesQuestion
    ON Question.noQuestion = LikesQuestion.noQuestion
GROUP BY
    Utilisateur.noUtilisateur, 
	Utilisateur.nom, 
	Utilisateur.email, 
	Utilisateur.administrateur, 
	Question.noQuestion, 
	Question.titre, 
	Question.question;
GO

CREATE VIEW VueReponseUtilisateur AS
SELECT Utilisateur.noUtilisateur,
	Utilisateur.nom, 
	Utilisateur.email, 
	Utilisateur.administrateur, 
	Reponse.noReponse,
	Reponse.reponse,
	COUNT(LikesReponse.noUtilisateur) AS nbLikes,
	Reponse.noQuestion
FROM Utilisateur
JOIN Reponse
    ON Utilisateur.noUtilisateur = Reponse.noUtilisateur
LEFT JOIN LikesReponse
    ON Reponse.noReponse = LikesReponse.noReponse
GROUP BY Utilisateur.noUtilisateur, 
	Utilisateur.nom, 
	Utilisateur.email, 
	Utilisateur.administrateur, 
	Reponse.noReponse, 
	Reponse.reponse,
	Reponse.noQuestion;
GO

CREATE VIEW VueInteractionUtilisateur AS
SELECT noUtilisateur, 
	nom, 
	email, 
	administrateur, 
	noQuestion AS noInteraction, 
	titre, 
	question AS texte, 
	nbLikes,
	noQuestion
FROM VueQuestionUtilisateur
UNION
SELECT noUtilisateur, 
	nom, 
	email, 
	administrateur, 
	noReponse AS noInteraction, 
	NULL AS titre,
	reponse AS texte, 
	nbLikes,
	noQuestion
FROM VueReponseUtilisateur;

GO
CREATE TRIGGER insert_question ON VueQuestionUtilisateur
INSTEAD OF INSERT AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @noUtilisateur INT;
	DECLARE @titre VARCHAR(100);
	DECLARE @question VARCHAR(1000);

	DECLARE cQuestion CURSOR FOR 
	SELECT noUtilisateur, titre, question
	FROM inserted;

	OPEN cQuestion;
	FETCH cQuestion INTO @noUtilisateur, @titre, @question;
	WHILE (@@FETCH_STATUS = 0)
	BEGIN
		INSERT INTO Question (noUtilisateur, titre, question) VALUES
		(@noUtilisateur, @titre, @question);
		FETCH cQuestion INTO @noUtilisateur, @titre, @question;
	END;

	CLOSE cQuestion;
	DEALLOCATE cQuestion;
	SET NOCOUNT OFF;
END;
GO

CREATE TRIGGER insert_reponse ON VueReponseUtilisateur
INSTEAD OF INSERT AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @noUtilisateur INT;
	DECLARE @noQuestion INT;
	DECLARE @reponse VARCHAR(1000);

	DECLARE cReponse CURSOR FOR 
	SELECT noUtilisateur, noQuestion, reponse
	FROM inserted;

	OPEN cReponse;
	FETCH cReponse INTO @noUtilisateur, @noQuestion, @reponse;
	WHILE (@@FETCH_STATUS = 0)
	BEGIN
		INSERT INTO Reponse (noUtilisateur, noQuestion, reponse) VALUES
		(@noUtilisateur, @noQuestion, @reponse);
		FETCH cReponse INTO @noUtilisateur, @noQuestion, @reponse;
	END;

	CLOSE cReponse;
	DEALLOCATE cReponse;
	SET NOCOUNT OFF;
END;
GO

INSERT INTO VueQuestionUtilisateur (noUtilisateur, titre, question) VALUES
(5, 'Titre test question', 'Texte test question'),
(1, 'Titre test question', 'Texte test question'),
(4, 'Titre test question', 'Texte test question');

INSERT INTO VueReponseUtilisateur (noUtilisateur, noQuestion, reponse) VALUES
(6, 1, 'Reponse test question'),
(6, 2, 'Reponse test question'),
(6, 3, 'Reponse test question');

-- Requete a faire pour avoir les interactions dans le bon ordre
SELECT * 
FROM VueInteractionUtilisateur
ORDER BY noQuestion, titre DESC;

SELECT * FROM Donnee