

/****** Object:  Table [dbo].[NewsArticles]    Script Date: 03-10-2018 18:32:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[NewsArticles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](100) NULL,
	[Type] [varchar](50) NULL,
	[Source_link] [varchar](100) NULL,
	[Photo_path] [varchar](50) NULL,
	[Content] [varchar](max) NULL,
	[Created_timestamp] [datetime] NULL,
	[Status] [varchar](2) NULL,
	[Published_Timestamp] [datetime] NULL,
 CONSTRAINT [PK_NewsArticles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


