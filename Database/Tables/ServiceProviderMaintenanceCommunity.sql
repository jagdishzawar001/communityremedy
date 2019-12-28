USE [CommunityRemedyDB]
GO

/****** Object:  Table [dbo].[ServiceProviderMaintenanceCommunity]    Script Date: 12/28/2019 11:20:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ServiceProviderMaintenanceCommunity](
	[ProviderId] [bigint] NOT NULL,
	[ServiceId] [bigint] NOT NULL,
	[CommunityId] [bigint] NOT NULL,
	[RelationId] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_ServiceProviderMaintenanceCommunity] PRIMARY KEY CLUSTERED 
(
	[RelationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ServiceProviderMaintenanceCommunity]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ServiceProviderMaintenanceCommunity_dbo.Community_CommunityId] FOREIGN KEY([CommunityId])
REFERENCES [dbo].[Community] ([CommunityID])
GO

ALTER TABLE [dbo].[ServiceProviderMaintenanceCommunity] CHECK CONSTRAINT [FK_dbo.ServiceProviderMaintenanceCommunity_dbo.Community_CommunityId]
GO

ALTER TABLE [dbo].[ServiceProviderMaintenanceCommunity]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ServiceProviderMaintenanceCommunity_dbo.MaintenanceService_ServiceId] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[MaintenanceService] ([ServiceId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ServiceProviderMaintenanceCommunity] CHECK CONSTRAINT [FK_dbo.ServiceProviderMaintenanceCommunity_dbo.MaintenanceService_ServiceId]
GO

ALTER TABLE [dbo].[ServiceProviderMaintenanceCommunity]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ServiceProviderMaintenanceCommunity_dbo.ServiceProvider_ProviderId] FOREIGN KEY([ProviderId])
REFERENCES [dbo].[ServiceProvider] ([ProviderId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ServiceProviderMaintenanceCommunity] CHECK CONSTRAINT [FK_dbo.ServiceProviderMaintenanceCommunity_dbo.ServiceProvider_ProviderId]
GO

