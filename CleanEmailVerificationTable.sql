
CREATE PROCEDURE cleanEmailVerificationTable
AS
BEGIN
DELETE FROM [dbo].[EmailVerification]
WHERE	ExpirationTime < GETDATE()
END
