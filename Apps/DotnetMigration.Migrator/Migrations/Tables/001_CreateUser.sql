IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    INNER JOIN sys.schemas s
        ON s.schema_id = t.schema_id
    WHERE s.name = 'dbo'
        AND t.name = 'User'
)
BEGIN
    CREATE TABLE [dbo].[User]
    (
        [Id]           BIGINT IDENTITY(1, 1) NOT NULL,
        [Email]        NVARCHAR(320) NOT NULL,
        [PasswordHash] NVARCHAR(500) NOT NULL,
        [IsActive]     BIT NOT NULL DEFAULT 1,
        [CreatedAt]    DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
        [UpdatedAt]    DATETIME2 NULL,

        CONSTRAINT [PK_User] PRIMARY KEY ([Id]),
        CONSTRAINT [UQ_User_Email] UNIQUE ([Email])
    );
END;