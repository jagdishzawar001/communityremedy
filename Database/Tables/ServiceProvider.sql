USE [CommunityRemedyDB]
GO

/****** Object:  Table [dbo].[ServiceProvider]    Script Date: 12/28/2019 11:19:54 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ServiceProvider](
	[ProviderId] [bigint] IDENTITY(1,1) NOT NULL,
	[ProviderName] [varchar](100) NULL,
	[Address] [varchar](200) NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](50) NULL,
	[Country] [varchar](50) NULL,
	[Zip] [varchar](50) NULL,
	[ContactNumber] [varchar](50) NULL,
	[GSTN] [varchar](50) NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[Website] [varchar](100) NULL,
 CONSTRAINT [PK_ServiceProvider] PRIMARY KEY CLUSTERED 
(
	[ProviderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ServiceProvider]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ServiceProvider_dbo.ApplicationUser_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[ApplicationUser] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ServiceProvider] CHECK CONSTRAINT [FK_dbo.ServiceProvider_dbo.ApplicationUser_UserId]
GO

