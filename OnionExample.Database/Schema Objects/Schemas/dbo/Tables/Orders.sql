CREATE TABLE [dbo].[Orders]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [CreationDate] SMALLDATETIME NOT NULL, 
    [CustomerName] NCHAR(255) NOT NULL, 
    [CustomerPhone] NVARCHAR(255) NOT NULL, 
    [Status] SMALLINT NOT NULL
)
