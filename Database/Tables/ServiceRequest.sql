USE [CommunityRemedyDB]
GO

/****** Object:  Table [dbo].[ServiceRequest]    Script Date: 12/28/2019 11:20:34 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ServiceRequest](
	[RequestId] [bigint] IDENTITY(1,1) NOT NULL,
	[ServiceId] [bigint] NULL,
	[CreatedUserId] [nvarchar](128) NULL,
	[CommunityId] [bigint] NULL,
	[ProviderId] [bigint] NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdatedTime] [datetime] NULL,
	[Title] [varchar](100) NULL,
	[Description] [varchar](100) NULL,
	[AttachmentPath] [varchar](500) NULL,
	[Type] [varchar](50) NULL,
	[Status] [varchar](50) NULL,
	[LastUpdatedByUserId] [nvarchar](128) NULL,
	[ResidentId] [bigint] NULL,
 CONSTRAINT [PK_ServiceRequest] PRIMARY KEY CLUSTERED 
(
	[RequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ServiceRequest]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ServiceRequest_dbo.ApplicationUser_UserId] FOREIGN KEY([CreatedUserId])
REFERENCES [dbo].[ApplicationUser] ([Id])
GO

ALTER TABLE [dbo].[ServiceRequest] CHECK CONSTRAINT [FK_dbo.ServiceRequest_dbo.ApplicationUser_UserId]
GO

ALTER TABLE [dbo].[ServiceRequest]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ServiceRequest_dbo.Community_CommunityId] FOREIGN KEY([CommunityId])
REFERENCES [dbo].[Community] ([CommunityID])
GO

ALTER TABLE [dbo].[ServiceRequest] CHECK CONSTRAINT [FK_dbo.ServiceRequest_dbo.Community_CommunityId]
GO

ALTER TABLE [dbo].[ServiceRequest]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ServiceRequest_dbo.MaintenanceService_UserId] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[MaintenanceService] ([ServiceId])
GO

ALTER TABLE [dbo].[ServiceRequest] CHECK CONSTRAINT [FK_dbo.ServiceRequest_dbo.MaintenanceService_UserId]
GO

ALTER TABLE [dbo].[ServiceRequest]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ServiceRequest_dbo.Resident_ResidentId] FOREIGN KEY([ResidentId])
REFERENCES [dbo].[Resident] ([ResidentId])
GO

ALTER TABLE [dbo].[ServiceRequest] CHECK CONSTRAINT [FK_dbo.ServiceRequest_dbo.Resident_ResidentId]
GO

ALTER TABLE [dbo].[ServiceRequest]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ServiceRequest_dbo.ServiceProvider_ProviderId] FOREIGN KEY([ProviderId])
REFERENCES [dbo].[ServiceProvider] ([ProviderId])
GO

ALTER TABLE [dbo].[ServiceRequest] CHECK CONSTRAINT [FK_dbo.ServiceRequest_dbo.ServiceProvider_ProviderId]
GO

