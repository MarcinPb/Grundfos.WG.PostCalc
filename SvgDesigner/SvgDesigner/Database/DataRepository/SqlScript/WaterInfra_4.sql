USE [WaterInfra_4]
GO
/****** Object:  Table [dbo].[tbInfraConn]    Script Date: 05.03.2021 19:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbInfraConn](
	[ParentObjId] [int] NOT NULL,
	[ChildObjId] [int] NOT NULL,
	[ConnTypeId] [int] NULL,
 CONSTRAINT [PK_tbInfraConn_1] PRIMARY KEY CLUSTERED 
(
	[ParentObjId] ASC,
	[ChildObjId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbInfraField]    Script Date: 05.03.2021 19:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbInfraField](
	[FieldId] [int] NOT NULL,
	[ObjTypeId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[DataTypeId] [int] NOT NULL,
	[Description] [nvarchar](4000) NULL,
 CONSTRAINT [PK_tbInfraObjFieldType] PRIMARY KEY CLUSTERED 
(
	[FieldId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbInfraObj]    Script Date: 05.03.2021 19:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbInfraObj](
	[ObjId] [int] NOT NULL,
	[ObjTypeId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](4000) NULL,
	[IsActive] [bit] NOT NULL,
	[Xx] [float] NOT NULL,
	[Yy] [float] NOT NULL,
 CONSTRAINT [PK_tbInfObject] PRIMARY KEY CLUSTERED 
(
	[ObjId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbInfraObjType]    Script Date: 05.03.2021 19:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbInfraObjType](
	[ObjTypeId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tbInfraObjType] PRIMARY KEY CLUSTERED 
(
	[ObjTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbInfraValue]    Script Date: 05.03.2021 19:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbInfraValue](
	[FieldId] [int] NOT NULL,
	[ObjId] [int] NOT NULL,
	[FloatValue] [float] NULL,
	[StringValue] [nvarchar](50) NULL,
 CONSTRAINT [PK_tbInfraObjFielddValue] PRIMARY KEY CLUSTERED 
(
	[FieldId] ASC,
	[ObjId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[tbInfraField] ([FieldId], [ObjTypeId], [Name], [DataTypeId], [Description]) VALUES (1, 73, N'Demand_AssociatedElement', 2, NULL)
GO
INSERT [dbo].[tbInfraField] ([FieldId], [ObjTypeId], [Name], [DataTypeId], [Description]) VALUES (2, 73, N'Demand_BaseFlow', 1, NULL)
GO
INSERT [dbo].[tbInfraField] ([FieldId], [ObjTypeId], [Name], [DataTypeId], [Description]) VALUES (3, 73, N'Demand_DemandPattern', 2, NULL)
GO
INSERT [dbo].[tbInfraField] ([FieldId], [ObjTypeId], [Name], [DataTypeId], [Description]) VALUES (4, 69, N'HMITopologyStartObjLabel', 2, NULL)
GO
INSERT [dbo].[tbInfraField] ([FieldId], [ObjTypeId], [Name], [DataTypeId], [Description]) VALUES (5, 69, N'HMITopologyStopObjLabel', 2, NULL)
GO
INSERT [dbo].[tbInfraField] ([FieldId], [ObjTypeId], [Name], [DataTypeId], [Description]) VALUES (6, 69, N'Physical_IsUserDefinedLength', 1, NULL)
GO
INSERT [dbo].[tbInfraField] ([FieldId], [ObjTypeId], [Name], [DataTypeId], [Description]) VALUES (7, 69, N'PipeStatus', 1, NULL)
GO
INSERT [dbo].[tbInfraField] ([FieldId], [ObjTypeId], [Name], [DataTypeId], [Description]) VALUES (8, 69, N'Physical_PipeMaterial', 2, NULL)
GO
INSERT [dbo].[tbInfraField] ([FieldId], [ObjTypeId], [Name], [DataTypeId], [Description]) VALUES (9, 69, N'Physical_InstallationYear', 1, NULL)
GO
INSERT [dbo].[tbInfraField] ([FieldId], [ObjTypeId], [Name], [DataTypeId], [Description]) VALUES (10, 69, N'HMIGeometryScaledLength', 1, NULL)
GO
INSERT [dbo].[tbInfraField] ([FieldId], [ObjTypeId], [Name], [DataTypeId], [Description]) VALUES (11, 69, N'Physical_PipeDiameter', 1, NULL)
GO
INSERT [dbo].[tbInfraObjType] ([ObjTypeId], [Name]) VALUES (23, N'Scada Element')
GO
INSERT [dbo].[tbInfraObjType] ([ObjTypeId], [Name]) VALUES (52, N'Tank')
GO
INSERT [dbo].[tbInfraObjType] ([ObjTypeId], [Name]) VALUES (54, N'Hydrant')
GO
INSERT [dbo].[tbInfraObjType] ([ObjTypeId], [Name]) VALUES (55, N'Junction')
GO
INSERT [dbo].[tbInfraObjType] ([ObjTypeId], [Name]) VALUES (56, N'Reservoir')
GO
INSERT [dbo].[tbInfraObjType] ([ObjTypeId], [Name]) VALUES (60, N'FCV')
GO
INSERT [dbo].[tbInfraObjType] ([ObjTypeId], [Name]) VALUES (61, N'TCV')
GO
INSERT [dbo].[tbInfraObjType] ([ObjTypeId], [Name]) VALUES (62, N'GPV')
GO
INSERT [dbo].[tbInfraObjType] ([ObjTypeId], [Name]) VALUES (64, N'PRV')
GO
INSERT [dbo].[tbInfraObjType] ([ObjTypeId], [Name]) VALUES (65, N'PSV')
GO
INSERT [dbo].[tbInfraObjType] ([ObjTypeId], [Name]) VALUES (66, N'PBV')
GO
INSERT [dbo].[tbInfraObjType] ([ObjTypeId], [Name]) VALUES (68, N'Pump')
GO
INSERT [dbo].[tbInfraObjType] ([ObjTypeId], [Name]) VALUES (69, N'Pipe')
GO
INSERT [dbo].[tbInfraObjType] ([ObjTypeId], [Name]) VALUES (71, N'Isolation Valve')
GO
INSERT [dbo].[tbInfraObjType] ([ObjTypeId], [Name]) VALUES (72, N'Variable Speed Pump Battery')
GO
INSERT [dbo].[tbInfraObjType] ([ObjTypeId], [Name]) VALUES (73, N'Customer Obj')
GO
ALTER TABLE [dbo].[tbInfraConn]  WITH CHECK ADD  CONSTRAINT [FK_tbInfraConn_tbInfraObj] FOREIGN KEY([ChildObjId])
REFERENCES [dbo].[tbInfraObj] ([ObjId])
GO
ALTER TABLE [dbo].[tbInfraConn] CHECK CONSTRAINT [FK_tbInfraConn_tbInfraObj]
GO
ALTER TABLE [dbo].[tbInfraConn]  WITH CHECK ADD  CONSTRAINT [FK_tbInfraConn_tbInfraObj1] FOREIGN KEY([ParentObjId])
REFERENCES [dbo].[tbInfraObj] ([ObjId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbInfraConn] CHECK CONSTRAINT [FK_tbInfraConn_tbInfraObj1]
GO
ALTER TABLE [dbo].[tbInfraField]  WITH CHECK ADD  CONSTRAINT [FK_tbInfraObjField_tbInfraObjType] FOREIGN KEY([ObjTypeId])
REFERENCES [dbo].[tbInfraObjType] ([ObjTypeId])
GO
ALTER TABLE [dbo].[tbInfraField] CHECK CONSTRAINT [FK_tbInfraObjField_tbInfraObjType]
GO
ALTER TABLE [dbo].[tbInfraObj]  WITH CHECK ADD  CONSTRAINT [FK_tbInfraObj_tbInfraObjType] FOREIGN KEY([ObjTypeId])
REFERENCES [dbo].[tbInfraObjType] ([ObjTypeId])
GO
ALTER TABLE [dbo].[tbInfraObj] CHECK CONSTRAINT [FK_tbInfraObj_tbInfraObjType]
GO
ALTER TABLE [dbo].[tbInfraValue]  WITH CHECK ADD  CONSTRAINT [FK_tbInfraObjFiledValue_tbInfraObj] FOREIGN KEY([ObjId])
REFERENCES [dbo].[tbInfraObj] ([ObjId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbInfraValue] CHECK CONSTRAINT [FK_tbInfraObjFiledValue_tbInfraObj]
GO
ALTER TABLE [dbo].[tbInfraValue]  WITH CHECK ADD  CONSTRAINT [FK_tbInfraObjFiledValue_tbInfraObjField] FOREIGN KEY([FieldId])
REFERENCES [dbo].[tbInfraField] ([FieldId])
GO
ALTER TABLE [dbo].[tbInfraValue] CHECK CONSTRAINT [FK_tbInfraObjFiledValue_tbInfraObjField]
GO
