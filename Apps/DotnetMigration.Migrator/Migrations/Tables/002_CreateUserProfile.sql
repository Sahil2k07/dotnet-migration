IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    INNER JOIN sys.schemas s
        ON s.schema_id = t.schema_id
    WHERE s.name = 'dbo'
        AND t.name = 'UserProfile'
)
BEGIN
    CREATE TABLE [dbo].[UserProfile]
    (
        [Id]          BIGINT IDENTITY(1, 1) NOT NULL,
        [UserId]      BIGINT NOT NULL,
        [FirstName]   NVARCHAR(100) NOT NULL,
        [LastName]    NVARCHAR(100) NOT NULL,
        [DisplayName] NVARCHAR(200) NULL,
        [DateOfBirth] DATE NULL,
        [CreatedAt]   DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
        [UpdatedAt]   DATETIME2 NULL,

        CONSTRAINT [PK_UserProfile] PRIMARY KEY ([Id]),
        CONSTRAINT [UQ_UserProfile_UserId] UNIQUE ([UserId]),
        CONSTRAINT [FK_UserProfile_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
    );
END;