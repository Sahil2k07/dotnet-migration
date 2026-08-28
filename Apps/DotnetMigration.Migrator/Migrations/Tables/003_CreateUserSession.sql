IF NOT EXISTS (
    SELECT 1
    FROM sys.tables t
    INNER JOIN sys.schemas s
        ON s.schema_id = t.schema_id
    WHERE s.name = 'dbo'
        AND t.name = 'UserSession'
)
BEGIN
    CREATE TABLE [dbo].[UserSession]
    (
        [Id]               BIGINT IDENTITY(1, 1) NOT NULL,
        [UserId]           BIGINT NOT NULL,
        [SessionId]        UNIQUEIDENTIFIER NOT NULL,
        [RefreshTokenHash] NVARCHAR(500) NOT NULL,
        [ExpiresAt]        DATETIME2 NOT NULL,
        [CreatedAt]        DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
        [RevokedAt]        DATETIME2 NULL,

        CONSTRAINT [PK_UserSession] PRIMARY KEY ([Id]),
        CONSTRAINT [UQ_UserSession_SessionId] UNIQUE ([SessionId]),
        CONSTRAINT [FK_UserSession_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
    );
END;