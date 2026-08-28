CREATE OR ALTER FUNCTION [dbo].[fn_GetUserFullName]
(
    @UserId BIGINT
)
RETURNS NVARCHAR(201)
AS
BEGIN
    DECLARE @FullName NVARCHAR(201);

    SELECT
        @FullName =
            LTRIM(RTRIM(
                COALESCE([FirstName], '') + ' ' +
                COALESCE([LastName], '')
            ))
    FROM [dbo].[UserProfile]
    WHERE [UserId] = @UserId;

    RETURN @FullName;
END;