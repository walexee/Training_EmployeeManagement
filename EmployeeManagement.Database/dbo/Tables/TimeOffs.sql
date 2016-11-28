CREATE TABLE [dbo].[TimeOffs]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[EmployeeId] UNIQUEIDENTIFIER NOT NULL, 
    [Date] DATE NOT NULL, 
    [HoursTaken] INT NOT NULL , 
    CONSTRAINT [FK_TimeOffs_Employees] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees]([Id])
)
