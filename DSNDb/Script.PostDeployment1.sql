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
DELETE FROM [dbo].[Graph];
DELETE FROM [dbo].[Individual] ;
DELETE FROM [dbo].[Organization];
DELETE FROM [dbo].[Donation];
DELETE FROM [dbo].[Need];
DELETE FROM [dbo].[User];

INSERT INTO [dbo].[User] 
VALUES (2, 'individual')
, (3, 'individual')
, (4, 'individual')
, (5, 'individual')
, (6, 'individual')
, (7, 'individual')
, (8, 'individual')
, (9, 'individual')
, (12, 'organization')
, (10, 'organization')
, (11, 'organization') 
;

INSERT INTO [Individual]  
VALUES 
 (2, 'Akash', 'Student', 'Kothari Primary School')
, (3, 'Prerana', 'Student', 'Jnanpith School')
, (4, 'Raghunath', 'Student', 'Golden Kids School')
, (5, 'Samrudhdhi', 'Student', 'Navodaya School')
, (6, 'Anup', 'Student', 'Vidya Mandir')
, (7, 'Tushar', 'Student', 'Shivaji High School')
, (8, 'Vishal', 'Student', 'Anjuman High school')
,(9, 'Aditya', 'Student', 'MTB School')
;

INSERT INTO [Organization]
VALUES (12, 'Kothari Prathamik Shala', 'Primary School')
, (10, 'Ugly Indians', 'Street Spot fixing NGO')
, (11, 'MTB School', 'School')
;

INSERT INTO [Graph]
VALUES (9, 2)
, (9, 3)
, (9, 4)
, (9, 5)
, (9, 6)
, (9, 7)
;

INSERT INTO [Need]
VALUES (1, 2, 'School Dress', 'School Dress', 500, 500, 11, 'P', '20160930 11:59:00 PM')
, (2, 2, 'Admission fees', 'Admission fees', 1000, 1000, 11, 'P', '20160930 11:59:00 PM')
, (3, 9, 'Admission fees', 'Admission fees', 1000, 1000, 11, 'P', '20160930 11:59:00 PM')
;

