
INSERT INTO [Core].[User] (Id,Name,Login,Email,Password,IsGroup,IsActive) VALUES 
('0aea0363-fe1e-4c52-87a3-b484c6f35587','Admin',NULL,NULL,NULL,1,1)
GO

INSERT INTO [Core].[User] (Id,Name,Login,Email,Password,IsGroup,IsActive) VALUES 
('ABF560FB-FB94-4CC0-A5CF-2FED13F28F96','User',NULL,NULL,NULL,1,1)
GO


INSERT INTO [Core].[Member] (GroupId, UserId) VALUES
('0aea0363-fe1e-4c52-87a3-b484c6f35587','70A2DC51-457B-466E-AAB9-EC229F03362B')
GO