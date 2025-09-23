USE Prog3A25_AntoineTommy;

INSERT INTO Wiki (info, minHumidite, maxHumidite, minTemperature, maxTemperature, minRayonsUV, maxRayonsUV) VALUES
('Ficus elastica (caoutchouc) : plante d�int�rieur r�sistante.', 40, 70, 15, 30, 20, 60),
('Cactus : plante succulente adapt�e aux environnements secs et ensoleill�s.', 10, 40, 10, 40, 60, 100),
('Orchid�e Phalaenopsis : aime l�humidit� et la chaleur mod�r�e.', 50, 80, 18, 28, 10, 40),
('Basilic : herbe aromatique qui pousse bien � chaleur et lumi�re mod�r�e.', 50, 70, 18, 30, 30, 70),
('Lavande : plante m�diterran�enne tol�rante � la s�cheresse et au plein soleil.', 20, 50, 15, 35, 60, 100);

INSERT INTO Plante (nom, noWiki, alerte) VALUES
('Ficus elastica', 1, 0),
('Cactus', 2, 0),
('Orchid�e Phalaenopsis', 3, 1),
('Basilic', 4, 0),
('Lavande', 5, 0);

INSERT INTO Donnee (dateHeure, temperature, humidite, rayonsUV, noPlante) VALUES
('2025-09-19 08:00:00', 22, 55, 40, 1),
('2025-09-19 08:05:00', 28, 25, 80, 2),
('2025-09-19 08:10:00', 24, 70, 20, 3),
('2025-09-19 08:15:00', 26, 60, 50, 4),
('2025-09-19 08:20:00', 27, 35, 75, 5),
('2025-09-19 08:25:00', 20, 65, 35, 1),
('2025-09-19 08:30:00', 32, 15, 90, 2),
('2025-09-19 08:35:00', 23, 75, 15, 3),
('2025-09-19 08:40:00', 25, 55, 45, 4),
('2025-09-19 08:45:00', 29, 40, 70, 5);

INSERT INTO Adresse (nomRue, noCivique, codePostal, ville) VALUES
('Rue Sainte-Catherine', 1250, 'H3B1E5', 'Montr�al'),
('Boulevard Laurier', 230, 'G1V2L1', 'Qu�bec'),
('Avenue du Parc', 789, 'H2V4H9', 'Montr�al'),
('Rue des Forges', 45, 'G9A4X6', 'Trois-Rivi�res'),
('Chemin Sainte-Foy', 3100, 'G1X1R6', 'Qu�bec'),
('Rue King Ouest', 980, 'J1H1S4', 'Sherbrooke'),
('Boulevard Charest Est', 560, 'G1K3J3', 'Qu�bec'),
('Rue Saint-Joseph', 212, 'G1K3A9', 'Qu�bec'),
('Boulevard Taschereau', 4125, 'J4V2H9', 'Longueuil'),
('Avenue Cartier', 150, 'G1R2S8', 'Qu�bec');

INSERT INTO Utilisateur (motPasse, nom, email, noAdresse, noPlante, administrateur, sel) VALUES
('mdpFicus123', 'Alice Tremblay', 'alice.tremblay@example.com', 1, 1, 0, NEWID()),
('mdpCactus456', 'Marc Dubois', 'marc.dubois@example.com', 2, 2, 0, NEWID()),
('orchidee789', 'Sophie Gagnon', 'sophie.gagnon@example.com', 3, 3, 0, NEWID()),
('basilic321', 'Jean Fortin', 'jean.fortin@example.com', 4, 4, 0, NEWID()),
('lavande654', '�milie Roy', 'emilie.roy@example.com', 5, 5, 0, NEWID()),
('admin123', 'Admin Principal', 'admin@example.com', 6, NULL, 1, NEWID());

INSERT INTO Question (titre, question, noUtilisateur) VALUES
('Arrosage du ficus', 'Mon ficus perd ses feuilles, est-ce que je l�arrose trop souvent?', 1),
('Cactus au soleil', 'Est-ce que je peux laisser mon cactus dehors tout l��t� au soleil?', 2),
('Orchid�e en fleurs', 'Mon orchid�e ne refleurit pas, avez-vous des astuces?', 3),
('Basilic qui jaunit', 'Mon basilic jaunit rapidement, est-ce d� � un manque de lumi�re?', 4),
('Lavande en pot', 'Peut-on cultiver la lavande en pot sur un balcon?', 5);

INSERT INTO Reponse (reponse, noUtilisateur, noQuestion) VALUES
('Ton ficus n�aime pas trop d�eau, laisse s�cher la terre avant d�arroser.', 2, 1),
('Oui, le cactus adore le plein soleil, mais surveille les arrosages.', 1, 2),
('Il faut couper la hampe florale et mettre l�orchid�e pr�s d�une fen�tre lumineuse.', 5, 3),
('Ton basilic a s�rement trop d�eau, il pr�f�re un arrosage l�ger mais r�gulier.', 3, 4),
('Oui, mais choisis un pot profond et un terreau bien drain�.', 4, 5);

INSERT INTO LikesQuestion (noUtilisateur, noQuestion) VALUES
(2, 1),
(3, 1),
(1, 2),
(4, 3),
(5, 4);

INSERT INTO LikesReponse (noUtilisateur, noReponse) VALUES
(1, 1),
(3, 2),
(2, 3),
(5, 4),
(4, 5);