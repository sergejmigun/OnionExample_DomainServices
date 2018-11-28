CREATE TABLE [dbo].[OrderItems]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [OrderId] INT NOT NULL,
    [ProductId] INT NULL,
    [ProductTitle] NVARCHAR(255) NOT NULL, 
    [Price] DECIMAL(10,2) NOT NULL, 
    [Quantity] INT NOT NULL,
    CONSTRAINT [FK_OrderItems_ToProducts] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products]([Id]) ON DELETE SET NULL,
    CONSTRAINT [FK_OrderItems_ToOrders] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders]([Id]) ON DELETE CASCADE
)
