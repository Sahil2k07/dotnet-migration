CREATE OR ALTER PROCEDURE [dbo].[sp_CreateUser]
    @Email NVARCHAR(320),
    @PasswordHash NVARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[User]
    (
        [Email],
        [PasswordHash]
    )
    VALUES
    (
        @Email,
        @PasswordHash
    );

    SELECT
        CAST(SCOPE_IDENTITY() AS BIGINT) AS [UserId];
END;