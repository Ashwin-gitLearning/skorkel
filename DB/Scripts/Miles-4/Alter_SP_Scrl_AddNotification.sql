       
CREATE  PROCEDURE [dbo].[Scrl_AddSANotification]        
@FlagNo int,        
@PageSize INT = NULL,        
@Currentpage INT = NULL,    
@Notification_ID  Int =null,   
@strNotificationDetail VARCHAR(Max)=NULL      
      
      
AS      
        
BEGIN        
 DECLARE @UpperBand INT, @LowerBand INT        
 SET @LowerBand  = (@CurrentPage - 1) * @PageSize        
 SET @UpperBand  = (@CurrentPage * @PageSize) + 1        
      IF @FlagNo=1        
    BEGIN        
     INSERT INTO SCL_SA_Notifications(Notification_Detail,dateAddedon ) VALUES(@strNotificationDetail,GETDATE())        
       END     
 Else IF @FlagNo=3        
    BEGIN        
     Delete from SCL_SA_Notifications where Notification_ID = @Notification_ID    
 END   
         
   Else       
   Begin       
    Select * from ( SELECT  ROW_NUMBER() OVER(ORDER BY Notification_ID Desc) AS rn, Notification_ID, COUNT(*) OVER()  AS Maxcount , Notification_Detail,CONVERT(VARCHAR(20), dateaddedon,106)dateaddedon FROM SCL_SA_Notifications ) as  T      
    WHERE T.rn > @LowerBand AND T.rn < @UpperBand order by dateAddedon desc      
   End       
 END        
        
         
    