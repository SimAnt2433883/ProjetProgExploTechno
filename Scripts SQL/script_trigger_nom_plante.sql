USE Prog3A25_AntoineTommy;

IF OBJECT_ID('afficher_nom_plante', 'TR') IS NOT NULL
	DROP TRIGGER afficher_nom_plante;

GO
CREATE TRIGGER afficher_nom_plante
ON Plante
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE 
        @nomPlante VARCHAR(100);

    DECLARE c_Plante CURSOR FOR
        SELECT nom
        FROM inserted;

    OPEN c_Plante;

    FETCH c_Plante INTO @nomPlante;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        PRINT 'Nouvelle plante insérée : ' + @nomPlante;

        FETCH c_Plante INTO @nomPlante;
    END

    CLOSE c_Plante;
    DEALLOCATE c_Plante;
	SET NOCOUNT OFF;
END;
GO