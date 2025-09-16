USE Prog3A25_AntoineTommy;

-- Foreign Keys
ALTER TABLE Question
ADD CONSTRAINT FK_Question_Utilisateur
FOREIGN KEY (noUtilisateur) REFERENCES Utilisateur(noUtilisateur);

ALTER TABLE Reponse
ADD CONSTRAINT FK_Reponse_Utilisateur
FOREIGN KEY (noUtilisateur) REFERENCES Utilisateur(noUtilisateur);

ALTER TABLE Reponse
ADD CONSTRAINT FK_Reponse_Question
FOREIGN KEY (noQuestion) REFERENCES Question(noQuestion);

ALTER TABLE LikesQuestion
ADD CONSTRAINT FK_LikesQuestion_Utilisateur
FOREIGN KEY (noUtilisateur) REFERENCES Utilisateur(noUtilisateur);

ALTER TABLE LikesQuestion
ADD CONSTRAINT FK_LikesQuestion_Question
FOREIGN KEY (noQuestion) REFERENCES Question(noQuestion);

ALTER TABLE LikesReponse
ADD CONSTRAINT FK_LikesReponse_Utilisateur
FOREIGN KEY (noUtilisateur) REFERENCES Utilisateur(noUtilisateur);

ALTER TABLE LikesReponse
ADD CONSTRAINT FK_LikesReponse_Reponse
FOREIGN KEY (noReponse) REFERENCES Reponse(noReponse);

ALTER TABLE Plante
ADD CONSTRAINT FK_Plante_Wiki
FOREIGN KEY (noWiki) REFERENCES Wiki(noWiki);

ALTER TABLE Donnee
ADD CONSTRAINT FK_Donnee_Plante
FOREIGN KEY (noPlante) REFERENCES Plante(noPlante);

ALTER TABLE Utilisateur
ADD CONSTRAINT FK_Utilisateur_Adresse
FOREIGN KEY (noAdresse) REFERENCES Adresse(noAdresse);

ALTER TABLE Utilisateur
ADD CONSTRAINT FK_Utilisateur_Plante
FOREIGN KEY (noPlante) REFERENCES Plante(noPlante);

-- Uniques
ALTER TABLE Plante
ADD CONSTRAINT UQ_Plante_Nom UNIQUE (nom);

ALTER TABLE Utilisateur
ADD CONSTRAINT UQ_Utilisateur_Email UNIQUE (email);

-- Checks
ALTER TABLE Donnee
ADD CONSTRAINT CHK_Donnee_Temperature CHECK (temperature >= -50 AND temperature <= 80);

ALTER TABLE Donnee
ADD CONSTRAINT CHK_Donnee_Humidite CHECK (humidite BETWEEN 0 AND 100);

ALTER TABLE Donnee
ADD CONSTRAINT CHK_Donnee_RayonsUV CHECK (rayonsUV >= 0);

-- Defaults
ALTER TABLE Utilisateur
ADD CONSTRAINT DF_Utilisateur_Administrateur DEFAULT 0 FOR administrateur;

ALTER TABLE Donnee
ADD CONSTRAINT DF_Donnee_DateHeure DEFAULT GETDATE() FOR dateHeure;
