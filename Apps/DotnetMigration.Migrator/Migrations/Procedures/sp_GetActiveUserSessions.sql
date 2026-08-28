CREATE OR ALTER PROCEDURE [dbo].[sp_GetActiveUserSessions]
    @UserId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        [Id],
        [SessionId],
        [ExpiresAt],
        [CreatedAt]
    FROM [dbo].[UserSession]
    WHERE
        [UserId] = @UserId
        AND [RevokedAt] IS NULL
        AND [ExpiresAt] > SYSUTCDATETIME()
    ORDER BY [CreatedAt] DESC;
    
END;