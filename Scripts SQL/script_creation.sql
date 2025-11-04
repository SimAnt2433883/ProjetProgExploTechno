USE master;

IF EXISTS (SELECT name FROM sys.databases WHERE name = N'Prog3A25_AntoineTommy')
BEGIN
    ALTER DATABASE Prog3A25_AntoineTommy SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE Prog3A25_AntoineTommy;
END

GO
CREATE DATABASE Prog3A25_AntoineTommy;
GO

USE Prog3A25_AntoineTommy;

CREATE TABLE Question (
	noQuestion		INT					NOT NULL		PRIMARY KEY		IDENTITY(1, 1),
	titre			VARCHAR(50)			NOT NULL,
	question		VARCHAR(1000)		NOT NULL,
	noUtilisateur	INT					NULL
);

CREATE TABLE Reponse (
	noReponse		INT					NOT NULL		PRIMARY KEY		IDENTITY(1, 1),
	reponse			VARCHAR(1000)		NOT NULL,
	noUtilisateur	INT					NULL,
	noQuestion		INT					NOT NULL
);

CREATE TABLE LikesQuestion (
	noUtilisateur	INT					NOT NULL,
	noQuestion		INT					NOT NULL,
	PRIMARY KEY (noUtilisateur, noQuestion)
);

CREATE TABLE LikesReponse (
	noUtilisateur	INT					NOT NULL,
	noReponse		INT					NOT NULL,
	PRIMARY KEY (noUtilisateur, noReponse)
);

CREATE TABLE Adresse (
	noAdresse		INT					NOT NULL		PRIMARY KEY		IDENTITY(1, 1),
	nomRue			VARCHAR(100)		NOT NULL,
	noCivique		SMALLINT			NOT NULL,
	codePostal		VARCHAR(6)			NULL,
	ville			VARCHAR(100)		NOT NULL
);

CREATE TABLE Wiki (
	noWiki			INT					NOT NULL		PRIMARY KEY		IDENTITY(1, 1),
	imagePlante		VARCHAR(100)		NULL,
	info			VARCHAR(1000)		NOT NULL,
	minHumidite		DECIMAL(4, 1)		NOT NULL,
	maxHumidite		DECIMAL(4, 1)		NOT NULL,
	minTemperature	DECIMAL(4, 1)		NOT NULL,
	maxTemperature	DECIMAL(4, 1)		NOT NULL,
	minRayonsUV		DECIMAL(4, 1)		NOT NULL,
	maxRayonsUV		DECIMAL(4, 1)		NOT NULL
);

CREATE TABLE Plante (
	noPlante		INT					NOT NULL		PRIMARY KEY		IDENTITY(1, 1),
	nom				VARCHAR(100)		NOT NULL,
	noWiki			INT					NULL,
	alerte			BIT					NOT NULL
);

CREATE TABLE Donnee (
	noDonnee		INT					NOT NULL		PRIMARY KEY		IDENTITY(1, 1),
	dateHeure		DATETIME			NOT NULL,
	temperature		DECIMAL(4, 1)		NOT NULL,
	humidite		DECIMAL(4, 1)		NOT NULL,
	rayonsUV		DECIMAL(4, 1)		NOT NULL,
	noPlante		INT					NOT NULL
);

CREATE TABLE Utilisateur (
	noUtilisateur	INT					NOT NULL		PRIMARY KEY		IDENTITY(1, 1),
	motPasse		VARCHAR(100)		NOT NULL,
	nom				VARCHAR(100)		NOT NULL,
	email			VARCHAR(100)		NOT NULL,
	noAdresse		INT					NULL,
	noPlante		INT					NULL,
	administrateur	BIT					NOT NULL,
	sel				UNIQUEIDENTIFIER	NULL
);
