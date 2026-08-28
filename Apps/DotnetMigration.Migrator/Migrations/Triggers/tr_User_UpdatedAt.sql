CREATE OR ALTER TRIGGER [dbo].[tr_User_UpdatedAt]
ON [dbo].[User]
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE u
    SET [UpdatedAt] = SYSUTCDATETIME()
    FROM [dbo].[User] u
    INNER JOIN inserted i
        ON i.[Id] = u.[Id];
END;