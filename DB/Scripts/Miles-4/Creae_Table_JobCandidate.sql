
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[JobCandidate](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Job_ID] [int] NULL,
	[User_ID] [int] NULL,
	[Submitted_timestamp] [datetime] NULL,
	[Resume_path] [varchar](200) NULL,
	[Resume_file_title] [varchar](200) NULL,
 CONSTRAINT [PK_JobCandidate] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[JobCandidate]  WITH CHECK ADD  CONSTRAINT [FK_JobCandidate_Jobs] FOREIGN KEY([Job_ID])
REFERENCES [dbo].[Jobs] ([ID])
GO

ALTER TABLE [dbo].[JobCandidate] CHECK CONSTRAINT [FK_JobCandidate_Jobs]
GO


