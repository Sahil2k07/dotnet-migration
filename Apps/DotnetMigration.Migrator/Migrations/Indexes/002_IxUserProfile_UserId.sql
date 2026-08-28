IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_UserProfile_LastName'
        AND object_id = OBJECT_ID('[dbo].[UserProfile]')
)
BEGIN
    CREATE INDEX [IX_UserProfile_LastName]
    ON [dbo].[UserProfile] ([LastName]);
END;