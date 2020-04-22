
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Jobs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](100) NULL,
	[Created_timestamp] [datetime] NULL,
	[Description] [varchar](max) NULL,
	[Location] [varchar](200) NULL,
	[JobType] [varchar](100) NULL,
	[StartingSalary] [numeric](18, 0) NULL,
	[EndingSalary] [numeric](18, 0) NULL,
	[StartDuration] [varchar](100) NULL,
	[EndDuration] [varchar](100) NULL,
	[Status] [varchar](2) NULL,
 CONSTRAINT [PK_Jobs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Jobs]  WITH CHECK ADD  CONSTRAINT [FK_Jobs_JobCandidate] FOREIGN KEY([ID])
REFERENCES [dbo].[Jobs] ([ID])
GO

ALTER TABLE [dbo].[Jobs] CHECK CONSTRAINT [FK_Jobs_JobCandidate]
GO


