CREATE TABLE [dbo].[Products]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Title] NVARCHAR(255) NOT NULL, 
    [Description] NCHAR(1024) NOT NULL, 
    [Price] DECIMAL(10,2) NOT NULL
)
