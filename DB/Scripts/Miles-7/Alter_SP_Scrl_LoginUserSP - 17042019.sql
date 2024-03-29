
/****** Object:  StoredProcedure [dbo].[scrl_LoginUserSP]    Script Date: 17-04-2019 12:11:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Monali Verulkar
-- Create date: 31/01/2012
-- Description:	For retrieving login user login data from Registration table
-- =============================================
-- [dbo].[scrl_LoginUserSP] 1,null,'iimlaw@gmail.com',1234,null,null,null
ALTER PROCEDURE [dbo].[scrl_LoginUserSP]
@FlagNo INT,
@RegistrationId INT =NULL ,
@UserName varchar(50)=NULL,
@Password varchar(50)=NULL,
@intUserId int =NULL,
@InstituteName varchar(50) =NULL,
@ParentID INT=NULL


AS
BEGIN
	IF @FlagNo=1 
		--BEGIN
		--	Select Row_Number() OVER (ORDER BY intRegistrationId) AS sr,(vchrFirstName +' '+ ISNULL(vchrLastName,''))LoginName, intRegistrationId,vchrUserName,vchrPassword,intUserTypeID
		--	from  scrl_RegistrationTbl where vchrUserName=@UserName AND vchrPassword=@Password AND vchrActive ='Y'
		--END
		BEGIN
			Select Row_Number() OVER (ORDER BY intRegistrationId) as sr,(vchrFirstName +' '+ ISNULL(vchrLastName,''))LoginName,intRegistrationId,vchrUserName,vchrPassword,intUserTypeID,vchrRegistartionType as RegistartionType
			from scrl_RegistrationTbl where vchrUserName=@UserName AND vchrPassword=@Password AND vchrActive ='Y'
		END
		ELSE IF @FLagNo=2
		BEGIN
			Update scrl_RegistrationTbl set vchrPassword=@Password where intRegistrationId=@RegistrationId 			
		End
		
		ELSE IF @FLagNo=3
		BEGIN
			Select Row_Number() over (order By intUserId) as sr,intUserId,strEmailID,strPassword ,
			strFirstName , strMiddleName , strLastName
			from Scrl_InternalUserTbl where strEmailId=@UserName and strPassword=@Password			
		End
	
		ELSE IF @FLagNo=4
		BEGIN
			SELECT (vchrFirstName +' '+ ISNULL(vchrLastName,''))LoginName, intRegistrationId,vchrUserName,vchrPassword,intUserTypeID,vchrFirstName,vchrLastName
			FROM scrl_RegistrationTbl 
			WHERE intRegistrationId=@RegistrationId
		END	
		
		ELSE  IF @FlagNo=5
		BEGIN
			Select Row_Number() OVER (ORDER BY intRegistrationId) AS sr,vchrFirstName as InstituteName ,intParentId,(vchrFirstName +' '+ ISNULL(vchrLastName,''))LoginName, intRegistrationId,vchrUserName,vchrPassword,intUserTypeID
			from  scrl_RegistrationTbl where vchrUserName=@UserName and vchrPassword=@Password  
		END
		ELSE IF @FlagNo=6
		BEGIN 
			SELECT ROW_NUMBER() OVER (ORDER BY intRegistrationId) AS sr,strSrcklID,vchrFirstName AS InstituteName ,
			intParentId,(vchrFirstName +' '+ ISNULL(vchrLastName,''))LoginName, intRegistrationId,vchrUserName,vchrPassword,intUserTypeID
			FROM  scrl_RegistrationTbl WHERE vchrUserName=@UserName
		END
		
		ELSE IF @FlagNo=7 
		BEGIN
			Select Row_Number() OVER (ORDER BY intRegistrationId) AS sr,(vchrFirstName +' '+ ISNULL(vchrLastName,''))LoginName, intRegistrationId,vchrUserName,vchrPassword,intUserTypeID
			from  scrl_RegistrationTbl where vchrUserName=@UserName 
		END
		ELSE IF @FlagNo=8
		BEGIN
			Select Row_Number() OVER (ORDER BY intRegistrationId) AS sr,(vchrFirstName +' '+ ISNULL(vchrLastName,''))LoginName, intRegistrationId,vchrUserName,vchrPassword,intUserTypeID
			from  scrl_RegistrationTbl where vchrUserName=@UserName AND vchrPassword=@Password AND vchrActive ='N'
		END
		ELSE IF @FlagNo=9
		BEGIN
			IF NOT EXISTS(SELECT * FROM Scrl_UserLoginLogoutDetails WHERE intRegistrationId=@RegistrationId )
		BEGIN
			Insert into Scrl_UserLoginLogoutDetails(intRegistrationId,Logindt)values(@RegistrationId,getdate())
		END
		ELSE
		BEGIN		
			UPDATE Scrl_UserLoginLogoutDetails
			SET Logindt=getdate()
			where intRegistrationId=@RegistrationId		
			END	
			UPDATE Scrl_RegistrationTbl
			SET logintimestamp=getdate()
			where intRegistrationId=@RegistrationId	
		END
		ELSE IF @FlagNo=10
		BEGIN
			UPDATE Scrl_UserLoginLogoutDetails
			SET Logoutdt=getdate()
			where intRegistrationId=@RegistrationId		
		END
		ELSE IF @FlagNo=11
		BEGIN
			Select LTRIM(RIGHT(CONVERT(VARCHAR(20), Logindt, 100), 7)) LoginTime,
			CONVERT(varchar, Logindt, 106) LoginDate, 
			LTRIM(RIGHT(CONVERT(VARCHAR(20), Logoutdt, 100), 7)) LogOutTime,
			CONVERT(varchar, Logoutdt, 106) LogOutDate,
			LTRIM(RIGHT(CONVERT(VARCHAR(20), Passwordchangedt, 100), 7)) PasswordTime,
			CONVERT(varchar, Passwordchangedt, 106) PasswordDate		
			from Scrl_UserLoginLogoutDetails
			where intRegistrationId=@RegistrationId		
		END
		ELSE IF @FlagNo=12
		BEGIN
			UPDATE Scrl_UserLoginLogoutDetails
			SET Passwordchangedt=getdate()
			where intRegistrationId=@RegistrationId		
		END
		IF @FlagNo=13 
		BEGIN
			--Select Row_Number() OVER (ORDER BY intRegistrationId) AS sr,(vchrFirstName +' '+ ISNULL(vchrLastName,''))LoginName, intRegistrationId,vchrUserName,vchrPassword,intUserTypeID
			--from  scrl_RegistrationTbl where vchrUserName=@UserName AND vchrActive ='Y'
			Select Row_Number() OVER (ORDER BY intRegistrationId) AS sr,(vchrFirstName +' '+ ISNULL(vchrLastName,''))LoginName,intRegistrationId,
				vchrUserName,vchrPassword,intUserTypeID,vchrRegistartionType as RegistartionType, logintimestamp
				from scrl_RegistrationTbl where vchrUserName=@UserName AND vchrActive ='Y'
		END
		ELSE IF @FlagNo=14
		BEGIN
			Select UPPER(SUBSTRING(sys.fn_sqlvarbasetostr(HASHBYTES('MD5',CONVERT(VARCHAR(200),@Password))),3,32))strPasswordMD5
		END
		ELSE IF @FlagNo=15 
		BEGIN
			Select * FROM scrl_RegistrationTbl
				where vchrUserName=@UserName AND vchrActive ='Y'
		END
		
END





		


