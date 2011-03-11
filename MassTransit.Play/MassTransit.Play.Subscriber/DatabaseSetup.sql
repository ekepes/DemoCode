USE [MassTransitPlay]
GO

/****** Object:  Table [dbo].[AuditLog]    Script Date: 03/11/2011 13:58:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AuditLog](
	[Id] [bigint] NOT NULL,
	[Description] [varchar](1000) NOT NULL,
	[EventDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_AuditLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

USE [MassTransitPlay]
GO

/****** Object:  Table [dbo].[IdentifierSeeds]    Script Date: 03/11/2011 13:58:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[IdentifierSeeds](
	[TableName] [nvarchar](200) NOT NULL,
	[NextKey] [bigint] NOT NULL
) ON [PRIMARY]

GO

INSERT INTO [MassTransitPlay].[dbo].[IdentifierSeeds]
           ([TableName]
           ,[NextKey])
     VALUES
           ('AuditLog'
           ,1)
GO

