USE Prog3A25_AntoineTommy;

GO
CREATE VIEW infoUtilisateur AS
SELECT Utilisateur.noUtilisateur, Plante.noPlante, Utilisateur.nom AS utilisateurNom, Plante.nom AS planteNom, Utilisateur.administrateur, COUNT(Question.noQuestion) AS nbQuestions
FROM Utilisateur
JOIN Plante
    ON Utilisateur.noPlante = Plante.noPlante
JOIN Question
    ON Utilisateur.noUtilisateur = Question.noUtilisateur
GROUP BY
    Utilisateur.noUtilisateur, Plante.noPlante, Utilisateur.nom, Plante.nom, Utilisateur.administrateur;

GO
CREATE VIEW VueQuestionUtilisateur AS
SELECT Utilisateur.noUtilisateur, Utilisateur.nom, Utilisateur.email, Utilisateur.administrateur AS admin, Question.noQuestion, Question.titre, Question.question AS Question, COUNT(LikesQuestion.noUtilisateur) AS nbLikes
FROM Utilisateur
JOIN Question
    ON Utilisateur.noUtilisateur = Question.noUtilisateur
JOIN LikesQuestion
    ON Question.noQuestion = LikesQuestion.noQuestion
GROUP BY
    Utilisateur.noUtilisateur, Utilisateur.nom, Utilisateur.email, Utilisateur.administrateur, Question.noQuestion, Question.titre, Question.question;

GO
CREATE VIEW VueReponseUtilisateur AS
SELECT Utilisateur.noUtilisateur, Utilisateur.nom, Utilisateur.email, Utilisateur.administrateur AS admin, Reponse.noReponse, Reponse.reponse, COUNT(LikesReponse.noUtilisateur) AS nbLikes
FROM Utilisateur
JOIN Reponse
    ON Utilisateur.noUtilisateur = Reponse.noUtilisateur
JOIN LikesReponse
    ON Reponse.noReponse = LikesReponse.noReponse
GROUP BY Utilisateur.noUtilisateur, Utilisateur.nom, Utilisateur.email, Utilisateur.administrateur, Reponse.noReponse, Reponse.reponse;
