-- wersja z dnia 06.05 11.05
DROP PROCEDURE sp_categoryAdd
DROP PROCEDURE sp_categoryDelete
DROP PROCEDURE sp_categoryEdit
DROP PROCEDURE sp_categoryGet
DROP PROCEDURE sp_categoryGetProduct

DROP PROCEDURE sp_producerAdd
DROP PROCEDURE sp_producerDelete
DROP PROCEDURE sp_producerEdit
DROP PROCEDURE sp_producerGet
DROP PROCEDURE sp_producerGetProduct

DROP PROCEDURE sp_userAdd
DROP PROCEDURE sp_userGet
DROP PROCEDURE sp_userGetUser

-- do Category--------------------------------------------------------------------------------
GO
CREATE PROCEDURE [dbo].[sp_categoryAdd]
@Id INT,
@ShortName VARCHAR(20),
@LongName VARCHAR(50) OUTPUT
AS
INSERT INTO Category (ShortName,LongName ) VALUES (@ShortName, @LongName)
SET @Id = @@IDENTITY


GO
CREATE PROCEDURE [dbo].[sp_categoryDelete]
@Id int OUTPUT
AS
DELETE FROM Category 
WHERE @Id = Id


GO
CREATE PROCEDURE [dbo].[sp_categoryEdit]
@Id INT,
@ShortName VARCHAR (20),
@LongName VARCHAR(50) OUTPUT
AS
UPDATE Category SET ShortName = @ShortName, LongName= @LongName
WHERE @Id= Id

--pobieranie calej tabeli create
GO
CREATE PROCEDURE [dbo].[sp_categoryGet]
AS
SELECT * FROM Category

--pobieranie konkretnego jednego produktu 
GO
CREATE PROCEDURE [dbo].[sp_categoryGetProduct]
@Id int OUTPUT
AS
SELECT * FROM Category
WHERE @Id= Id
-- do Producer ---------------------------------------------------------
GO
CREATE PROCEDURE [dbo].[sp_producerAdd]
@Id INT,
@Name VARCHAR(50),
@Country VARCHAR(50) OUTPUT
AS
INSERT INTO Producer_1(Name,Country ) VALUES (@Name, @Country)
SET @Id = @@IDENTITY


GO
CREATE PROCEDURE [dbo].[sp_producerDelete]
@Id int OUTPUT
AS
DELETE FROM Producer_1 
WHERE @Id = Id


GO
CREATE PROCEDURE [dbo].[sp_producerEdit]
@Id INT,
@Name VARCHAR(50),
@Country VARCHAR(50) OUTPUT
AS
UPDATE Producer_1 SET Name = @Name, Country= @Country
WHERE @Id= Id

GO
CREATE PROCEDURE [dbo].[sp_producerGet]
AS
SELECT * FROM Producer_1

GO
CREATE PROCEDURE [dbo].[sp_producerGetProduct]
@Id int OUTPUT
AS
SELECT * FROM Producer_1
WHERE @Id= Id


-- do Logowania --------------------------------------------------------
GO
CREATE PROCEDURE [dbo].[sp_userAdd]
@userName VARCHAR(50),
@password TEXT,
@role VARCHAR(50) OUTPUT
AS
INSERT INTO LoginData  (userName, password, role ) VALUES (@userName,@password, @role)

GO
CREATE PROCEDURE [dbo].[sp_userGet]
AS
SELECT * FROM LoginData

GO
CREATE PROCEDURE [dbo].[sp_userGetUser]
@userName VARCHAR(50) OUTPUT
AS
SELECT * FROM LoginData
WHERE @userName= userName


