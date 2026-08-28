CREATE OR ALTER VIEW [dbo].[vw_UserDetails]
AS
SELECT
    u.[Id] AS [UserId],
    u.[Email],
    u.[IsActive],
    u.[CreatedAt] AS [UserCreatedAt],

    p.[FirstName],
    p.[LastName],
    p.[DisplayName],
    p.[DateOfBirth]
FROM [dbo].[User] u
LEFT JOIN [dbo].[UserProfile] p
    ON p.[UserId] = u.[Id];