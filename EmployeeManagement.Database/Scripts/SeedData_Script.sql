/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

INSERT INTO JobTitles (Id, [Description])
VALUES
(3010, 'Programmer I'),
(3020, 'Programmer II'),
(3050, 'Software Developer'),
(4110, 'Junior CPA'),
(4120, 'Senior CPA'),
(4010, 'Tax Accountant');

INSERT INTO Genders (Id, [Description])
values
(0, 'Undisclosed'),
(1, 'Male'),
(2, 'Female');