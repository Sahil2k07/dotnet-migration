IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_UserSession_UserId'
        AND object_id = OBJECT_ID('[dbo].[UserSession]')
)
BEGIN
    CREATE INDEX [IX_UserSession_UserId]
    ON [dbo].[UserSession] ([UserId]);
END;