USE master;

IF EXISTS (SELECT name FROM sys.databases WHERE name = N'Prog3A25_AntoineTommy')
BEGIN
    ALTER DATABASE Prog3A25_AntoineTommy SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE Prog3A25_AntoineTommy;
END

CREATE DATABASE Prog3A25_AntoineTommy;

USE Prog3A25_AntoineTommy;

CREATE TABLE Question (
	noQuestion		INT				PRIMARY KEY		IDENTITY(1, 1),
	question		VARCHAR(1000),
	noUtilisateur	INT
);

CREATE TABLE Reponse (
	noReponse		INT				PRIMARY KEY		IDENTITY(1, 1),
	reponse			VARCHAR(1000),
	noUtilisateur	INT,
	noQuestion		INT
);

CREATE TABLE LikesQuestion (
	noUtilisateur	INT,
	noQuestion		INT,
	PRIMARY KEY (noUtilisateur, noQuestion)
);

CREATE TABLE LikesReponse (
	noUtilisateur	INT,
	noReponse		INT,
	PRIMARY KEY (noUtilisateur, noReponse)
);

CREATE TABLE Adresse (
	noAdresse		INT				PRIMARY KEY		IDENTITY(1, 1),
	nomRue			VARCHAR(100),
	noCivique		SMALLINT,
	codePostal		VARCHAR(6),
	Ville			VARCHAR(100)
);

CREATE TABLE Wiki (
	noWiki			INT				PRIMARY KEY		IDENTITY(1, 1),
	imagePlante		VARCHAR(50),
	info			VARCHAR	(1000),
	minHumidite		DECIMAL(3, 1),
	maxHumidite		DECIMAL(3, 1),
	minTemperature	DECIMAL(3, 1),
	maxTemperature	DECIMAL(3, 1),
	minRayonsUV		DECIMAL(3, 1),
	maxRayonsUV		DECIMAL(3, 1)
);

CREATE TABLE Plante (
	noPlante		INT				PRIMARY KEY		IDENTITY(1, 1),
	nom				VARCHAR(100),
	noWiki			INT,
	alerte			BIT
);

CREATE TABLE Donnee (
	noDonnee		INT				PRIMARY KEY		IDENTITY(1, 1),
	dateHeure		DATETIME,
	temperature		DECIMAL(3, 1),
	humidite		DECIMAL(3, 1),
	rayonsUV		DECIMAL(3, 1),
	noPlante		INT
);

CREATE TABLE Utilisateur (
	noUtilisateur	INT				PRIMARY KEY		IDENTITY(1, 1),
	motPasse		VARCHAR(100),
	nom				VARCHAR(100),
	email			VARCHAR(100),
	noAdresse		INT,
	noPlante		INT,
	administrateur	BIT
);


