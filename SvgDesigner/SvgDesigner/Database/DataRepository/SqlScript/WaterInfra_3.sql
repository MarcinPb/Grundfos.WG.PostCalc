/****** Object:  Database [WaterInfra_3]    Script Date: 05.03.2021 16:42:19 ******/
USE [WaterInfra_3]
GO

/****** Object:  Table [dbo].[tbInfraConnection]    Script Date: 05.03.2021 16:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbInfraConnection](
	[ParentNodeId] [int] NOT NULL,
	[ChildNodeId] [int] NOT NULL,
	[TypeId] [int] NULL,
 CONSTRAINT [PK_tbInfraConnection_1] PRIMARY KEY CLUSTERED 
(
	[ParentNodeId] ASC,
	[ChildNodeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbInfraNode]    Script Date: 05.03.2021 16:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbInfraNode](
	[NodeId] [int] NOT NULL,
	[NodeTypeId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](4000) NULL,
	[IsActive] [bit] NOT NULL,
	[Xx] [float] NOT NULL,
	[Yy] [float] NOT NULL,
 CONSTRAINT [PK_tbInfObject] PRIMARY KEY CLUSTERED 
(
	[NodeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbInfraNodeField]    Script Date: 05.03.2021 16:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbInfraNodeField](
	[NodeFieldId] [int] NOT NULL,
	[NodeTypeId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[DataTypeId] [int] NOT NULL,
	[Description] [nvarchar](4000) NULL,
 CONSTRAINT [PK_tbInfraNodeFieldType] PRIMARY KEY CLUSTERED 
(
	[NodeFieldId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbInfraNodeFielddValue]    Script Date: 05.03.2021 16:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbInfraNodeFielddValue](
	[NodeFieldId] [int] NOT NULL,
	[NodeId] [int] NOT NULL,
	[FloatValue] [float] NULL,
	[StringValue] [nvarchar](50) NULL,
 CONSTRAINT [PK_tbInfraNodeFielddValue] PRIMARY KEY CLUSTERED 
(
	[NodeFieldId] ASC,
	[NodeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbInfraNodeType]    Script Date: 05.03.2021 16:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbInfraNodeType](
	[NodeTypeId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tbInfraNodeType] PRIMARY KEY CLUSTERED 
(
	[NodeTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbInfraZone]    Script Date: 05.03.2021 16:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbInfraZone](
	[ZoneId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tbInfraZone] PRIMARY KEY CLUSTERED 
(
	[ZoneId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[tbInfraNodeField] ([NodeFieldId], [NodeTypeId], [Name], [DataTypeId], [Description]) VALUES (1, 73, N'Demand_AssociatedElement', 2, NULL)
GO
INSERT [dbo].[tbInfraNodeField] ([NodeFieldId], [NodeTypeId], [Name], [DataTypeId], [Description]) VALUES (2, 73, N'Demand_BaseFlow', 1, NULL)
GO
INSERT [dbo].[tbInfraNodeField] ([NodeFieldId], [NodeTypeId], [Name], [DataTypeId], [Description]) VALUES (3, 73, N'Demand_DemandPattern', 2, NULL)
GO
INSERT [dbo].[tbInfraNodeField] ([NodeFieldId], [NodeTypeId], [Name], [DataTypeId], [Description]) VALUES (4, 69, N'HMITopologyStartNodeLabel', 2, NULL)
GO
INSERT [dbo].[tbInfraNodeField] ([NodeFieldId], [NodeTypeId], [Name], [DataTypeId], [Description]) VALUES (5, 69, N'HMITopologyStopNodeLabel', 2, NULL)
GO
INSERT [dbo].[tbInfraNodeField] ([NodeFieldId], [NodeTypeId], [Name], [DataTypeId], [Description]) VALUES (6, 69, N'Physical_IsUserDefinedLength', 1, NULL)
GO
INSERT [dbo].[tbInfraNodeField] ([NodeFieldId], [NodeTypeId], [Name], [DataTypeId], [Description]) VALUES (7, 69, N'PipeStatus', 1, NULL)
GO
INSERT [dbo].[tbInfraNodeField] ([NodeFieldId], [NodeTypeId], [Name], [DataTypeId], [Description]) VALUES (8, 69, N'Physical_PipeMaterial', 2, NULL)
GO
INSERT [dbo].[tbInfraNodeField] ([NodeFieldId], [NodeTypeId], [Name], [DataTypeId], [Description]) VALUES (9, 69, N'Physical_InstallationYear', 1, NULL)
GO
INSERT [dbo].[tbInfraNodeField] ([NodeFieldId], [NodeTypeId], [Name], [DataTypeId], [Description]) VALUES (10, 69, N'HMIGeometryScaledLength', 1, NULL)
GO
INSERT [dbo].[tbInfraNodeField] ([NodeFieldId], [NodeTypeId], [Name], [DataTypeId], [Description]) VALUES (11, 69, N'Physical_PipeDiameter', 1, NULL)
GO
INSERT [dbo].[tbInfraNodeType] ([NodeTypeId], [Name]) VALUES (23, N'Scada Element')
GO
INSERT [dbo].[tbInfraNodeType] ([NodeTypeId], [Name]) VALUES (52, N'Tank')
GO
INSERT [dbo].[tbInfraNodeType] ([NodeTypeId], [Name]) VALUES (54, N'Hydrant')
GO
INSERT [dbo].[tbInfraNodeType] ([NodeTypeId], [Name]) VALUES (55, N'Junction')
GO
INSERT [dbo].[tbInfraNodeType] ([NodeTypeId], [Name]) VALUES (56, N'Reservoir')
GO
INSERT [dbo].[tbInfraNodeType] ([NodeTypeId], [Name]) VALUES (60, N'FCV')
GO
INSERT [dbo].[tbInfraNodeType] ([NodeTypeId], [Name]) VALUES (61, N'TCV')
GO
INSERT [dbo].[tbInfraNodeType] ([NodeTypeId], [Name]) VALUES (62, N'GPV')
GO
INSERT [dbo].[tbInfraNodeType] ([NodeTypeId], [Name]) VALUES (64, N'PRV')
GO
INSERT [dbo].[tbInfraNodeType] ([NodeTypeId], [Name]) VALUES (65, N'PSV')
GO
INSERT [dbo].[tbInfraNodeType] ([NodeTypeId], [Name]) VALUES (66, N'PBV')
GO
INSERT [dbo].[tbInfraNodeType] ([NodeTypeId], [Name]) VALUES (68, N'Pump')
GO
INSERT [dbo].[tbInfraNodeType] ([NodeTypeId], [Name]) VALUES (69, N'Pipe')
GO
INSERT [dbo].[tbInfraNodeType] ([NodeTypeId], [Name]) VALUES (71, N'Isolation Valve')
GO
INSERT [dbo].[tbInfraNodeType] ([NodeTypeId], [Name]) VALUES (72, N'Variable Speed Pump Battery')
GO
INSERT [dbo].[tbInfraNodeType] ([NodeTypeId], [Name]) VALUES (73, N'Customer Node')
GO
ALTER TABLE [dbo].[tbInfraConnection]  WITH CHECK ADD  CONSTRAINT [FK_tbInfraConnection_tbInfraNode] FOREIGN KEY([ChildNodeId])
REFERENCES [dbo].[tbInfraNode] ([NodeId])
GO
ALTER TABLE [dbo].[tbInfraConnection] CHECK CONSTRAINT [FK_tbInfraConnection_tbInfraNode]
GO
ALTER TABLE [dbo].[tbInfraConnection]  WITH CHECK ADD  CONSTRAINT [FK_tbInfraConnection_tbInfraNode1] FOREIGN KEY([ParentNodeId])
REFERENCES [dbo].[tbInfraNode] ([NodeId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbInfraConnection] CHECK CONSTRAINT [FK_tbInfraConnection_tbInfraNode1]
GO
ALTER TABLE [dbo].[tbInfraNode]  WITH CHECK ADD  CONSTRAINT [FK_tbInfraNode_tbInfraNodeType] FOREIGN KEY([NodeTypeId])
REFERENCES [dbo].[tbInfraNodeType] ([NodeTypeId])
GO
ALTER TABLE [dbo].[tbInfraNode] CHECK CONSTRAINT [FK_tbInfraNode_tbInfraNodeType]
GO
ALTER TABLE [dbo].[tbInfraNodeField]  WITH CHECK ADD  CONSTRAINT [FK_tbInfraNodeField_tbInfraNodeType] FOREIGN KEY([NodeTypeId])
REFERENCES [dbo].[tbInfraNodeType] ([NodeTypeId])
GO
ALTER TABLE [dbo].[tbInfraNodeField] CHECK CONSTRAINT [FK_tbInfraNodeField_tbInfraNodeType]
GO
ALTER TABLE [dbo].[tbInfraNodeFielddValue]  WITH CHECK ADD  CONSTRAINT [FK_tbInfraNodeFiledValue_tbInfraNode] FOREIGN KEY([NodeId])
REFERENCES [dbo].[tbInfraNode] ([NodeId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbInfraNodeFielddValue] CHECK CONSTRAINT [FK_tbInfraNodeFiledValue_tbInfraNode]
GO
ALTER TABLE [dbo].[tbInfraNodeFielddValue]  WITH CHECK ADD  CONSTRAINT [FK_tbInfraNodeFiledValue_tbInfraNodeField] FOREIGN KEY([NodeFieldId])
REFERENCES [dbo].[tbInfraNodeField] ([NodeFieldId])
GO
ALTER TABLE [dbo].[tbInfraNodeFielddValue] CHECK CONSTRAINT [FK_tbInfraNodeFiledValue_tbInfraNodeField]
GO
