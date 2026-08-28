IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_User_IsActive'
        AND object_id = OBJECT_ID('[dbo].[User]')
)
BEGIN
    CREATE INDEX [IX_User_IsActive]
    ON [dbo].[User] ([IsActive]);
END;