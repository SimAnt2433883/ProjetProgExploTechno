USE Prog3A25_AntoineTommy;

IF OBJECT_ID('VueInteractionUtilisateur', 'V') IS NOT NULL
    DROP VIEW VueInteractionUtilisateur;
IF OBJECT_ID('VueReponseUtilisateur', 'V') IS NOT NULL
    DROP VIEW VueReponseUtilisateur;
IF OBJECT_ID('VueQuestionUtilisateur', 'V') IS NOT NULL
    DROP VIEW VueQuestionUtilisateur;
IF OBJECT_ID('VueInfoUtilisateur', 'V') IS NOT NULL
    DROP VIEW VueInfoUtilisateur;

GO
CREATE VIEW VueInfoUtilisateur AS
SELECT Utilisateur.noUtilisateur, 
	Plante.noPlante, 
	Utilisateur.nom AS utilisateurNom, 
	Plante.nom AS planteNom, 
	Utilisateur.administrateur, 
	COUNT(Question.noQuestion) AS nbQuestions
FROM Utilisateur
JOIN Plante
    ON Utilisateur.noPlante = Plante.noPlante
JOIN Question
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
	Question.question AS Question, 
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
FROM VueReponseUtilisateur
GO


/* REQUETE A FAIRE POUR AVOIR LES INFOS DANS LE BON ORDRE
SELECT * 
FROM VueInteractionUtilisateur
ORDER BY noQuestion, noInteraction, titre DESC;
*/