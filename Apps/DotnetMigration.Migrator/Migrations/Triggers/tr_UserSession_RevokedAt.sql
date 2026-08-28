CREATE OR ALTER TRIGGER [dbo].[tr_UserSession_RevokedAt]
ON [dbo].[UserSession]
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE s
    SET [RevokedAt] = COALESCE(s.[RevokedAt], SYSUTCDATETIME())
    FROM [dbo].[UserSession] s
    INNER JOIN inserted i
        ON i.[Id] = s.[Id]
    INNER JOIN deleted d
        ON d.[Id] = s.[Id]
    WHERE
        d.[RevokedAt] IS NULL
        AND i.[RevokedAt] IS NOT NULL;
END;