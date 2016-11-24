CREATE TABLE [dbo].[Employees]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Firstname] NVARCHAR(50) NOT NULL, 
    [Lastname] NVARCHAR(50) NOT NULL, 
    [JobTitleId] INT NOT NULL, 
    [SkillLevel] INT NOT NULL, 
    [Salary] MONEY NOT NULL, 
    [GenderId] INT NOT NULL, 
    CONSTRAINT [FK_Employees_JobTitles] FOREIGN KEY ([JobTitleId]) REFERENCES [JobTitles]([Id]), 
    CONSTRAINT [FK_Employees_Genders] FOREIGN KEY ([GenderId]) REFERENCES [Genders]([Id])
)
