USE [JwtSampleAppDb]
GO

INSERT INTO [dbo].[ApplicationUsers]
           ([Username]
           ,[Password]
           ,[FirstName]
           ,[LastName]
           ,[EmailId]
           ,[PhoneNumber])
     VALUES
           ('test',
           'a',
           'Elizabeth',
           null,
           'test@gmail.com',
           '8547208190')
GO


