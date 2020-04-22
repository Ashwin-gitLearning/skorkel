
/****** Object:  Table [dbo].[SubDocs]    Script Date: 01-04-2019 17:00:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SubDocs](
	[id] [int] IDENTITY(1,1) PRIMARY KEY,
	[userId] [int] NOT NULL,
	[file_path] [varchar](500) NOT NULL,
	[file_name] [varchar](500) NOT NULL,
	[created_timestamp] [datetime] NOT NULL
) ON [PRIMARY]
GO


