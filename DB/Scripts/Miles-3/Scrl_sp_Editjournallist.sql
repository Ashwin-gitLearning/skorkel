IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Scrl_sp_Editjournallist')
DROP PROCEDURE Scrl_sp_Editjournallist
go
CREATE PROCEDURE Scrl_sp_Editjournallist @FlagNo    AS INT,   
                                          @JournalId AS NUMERIC(18, 0)= NULL,   
                                          @ArticleId AS NUMERIC(18, 0)= NULL,  
            @ActiveStatus AS bit= NULL  ,
			@Month As Int=null,
			@Year As INT=null,
			@intAddedBy As INT=null,
			@FilePath As Varchar(200) =null,
			@Title As Varchar(200) =null,
			@Comment As Varchar(500) =null,
			@CommentID As INT =null
AS   
  BEGIN   
      IF @FlagNo = 3   
        BEGIN   
          UPDATE journals   set status=@ActiveStatus where Id = @JournalId
         
        END   
		 IF @FlagNo = 4 
        BEGIN  
		declare @MaxJournalID as int
		 Select @MaxJournalID = ISNULL( max(id),0) +1  from Journals 
       insert into Journals(id,month, year,title,status,created_timestamp,published_timestamp)
	   values(@MaxJournalID, @Month,@Year,@Title, @ActiveStatus,GETDATE(),GETDATE())
         
        END   
		 IF @FlagNo = 8
        BEGIN  
		declare @MaxArticleID as int
		 Select @MaxArticleID = ISNULL( max(id),0) +1  from Articles 
       insert into Articles(id, title, userId, file_path, created_timestamp)
	   values(@MaxArticleID, @Title, @intAddedBy, @FilePath, GETDATE())
         
        END   
		 IF @FlagNo = 10
		 BEGIN
		 declare @MaxJourArticleID as int
		 Select @MaxJourArticleID = ISNULL( max(journal_article_id),0) +1  from JournalArticles 
		 insert into JournalArticles(journal_article_id, journal_id, article_id, published_title,
		 added_timestamp) values(@MaxJourArticleID, @JournalId, @ArticleId, @Title, GETDATE())
		 update Articles set Title = @Title where ID = @ArticleId
		 END
		 IF @FlagNo = 12
		 BEGIN
		 delete from ArticleLikes where journal_article_id = @ArticleId
		 delete from ArticleComments where journal_article_id = @ArticleId
		 delete from JournalArticles where journal_id=@JournalId and article_id=@ArticleId
		 END
		   IF @FlagNo = 14
		 BEGIN
		  declare @MaxID as int
		   Select @MaxID = ISNULL( max(ID),0) +1  from ArticleComments 
		 Insert into ArticleComments(ID, journal_article_id, comment_detail, user_id, created_timestamp) values(@MaxID, @ArticleId, @Comment, @intAddedBy, GETDATE())
		 END
		  IF @FlagNo = 15
		 BEGIN
		 update ArticleComments set comment_detail=@Comment where ID=@CommentID
		 END
		   IF @FlagNo = 16
		 BEGIN
		 delete from ArticleComments where ID=@CommentID
		 END
		 IF @FlagNo =17
		 Begin 
		 IF EXISTS (SELECT * FROM ArticleLikes WHERE user_id= @intAddedBy and journal_article_id=@ArticleId) 
			 BEGIN
				DELETE  FROM ArticleLikes where user_id= @intAddedBy and journal_article_id=@ArticleId
			 END
		 ELSE
			 BEGIN
				DECLARE @MaxLikeID AS INT
				SELECT @MaxLikeID = ISNULL( max(ID),0) +1  FROM ArticleComments 
				INSERT INTO ArticleLikes(id,journal_article_id, user_id, created_timestamp) VALUES(@MaxLikeID, @ArticleId, @intAddedBy, GETDATE())
				SELECT * FROM ArticleLikes
			 END
		 END
		 IF @FlagNo=19
		 BEGIN
			UPDATE journals   set title=@Title, year=@Year, month=@Month where Id = @JournalId
		 END
  END 