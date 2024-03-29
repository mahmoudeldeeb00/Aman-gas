USE [aman-gas]
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'0eb75c83-6fb9-442e-8bcc-1465ff92e37a', N'Admin', N'ADMIN', N'94d15959-3f61-4894-9a52-e03b55436b47')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'b8dfdbba-1461-4e04-8180-8d215e4a802c', N'User', N'USER', N'e4c08837-2834-4b49-8d05-0e04fcb3e8cb')
GO
SET IDENTITY_INSERT [dbo].[CarTypes] ON 

INSERT [dbo].[CarTypes] ([Id], [Name], [Description], [ARName], [Status]) VALUES (1, N'Special ', N'malaky', N'ملاكي', 1)
INSERT [dbo].[CarTypes] ([Id], [Name], [Description], [ARName], [Status]) VALUES (2, N'Transport', N'naql', N'نقل', 1)
SET IDENTITY_INSERT [dbo].[CarTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Cars] ON 

INSERT [dbo].[Cars] ([Id], [Name], [CarTypeId], [CarNumbers], [FirstChar], [SecondChar], [ThirdChar], [User]) VALUES (1, N'mahmoud  eldeeb  vecile ', 1, N'1234', N'ا', N'ب', N' ', N'mahmoudeldeeb')
INSERT [dbo].[Cars] ([Id], [Name], [CarTypeId], [CarNumbers], [FirstChar], [SecondChar], [ThirdChar], [User]) VALUES (2, N'Ahmed  Eldeeb  vecile ', 1, N'1234', N'ا', N'ب', N'ت', N'ahmedeldeeb')
SET IDENTITY_INSERT [dbo].[Cars] OFF
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Gmail], [CarId]) VALUES (N'39b3cfff-0feb-4d68-ad0a-0ea6840e0c01', N'Ahmed', N'Eldeeb', N'ahmedeldeeb', N'AHMEDELDEEB', N'ae244910@gmail.com', N'AE244910@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEMl7F0B6Nnpf6u0x083ZXkj+sWuxCsF1tWnoTJ4/7UnONjzXjh0JcNvl+bZ0km1sNw==', N'E7RJFWUHSZD42VKW5CBQIUWEXVJWWXNA', N'55c9a88a-59fb-4090-904e-d19a86dd6d96', NULL, 0, 0, NULL, 1, 0, N'ae244910@gmail.com', 1)
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Gmail], [CarId]) VALUES (N'933570ea-e726-43fd-bfb7-2039fece6205', N'mahmoud', N'eldeeb', N'mahmoudeldeeb', N'MAHMOUDELDEEB', N'', N'', 0, N'AQAAAAEAACcQAAAAEOfoLMQqqvgoRKTafNBREIB+nh841Preo7ThE3O7FXPcGpmlt2ZyjostpU+LijuCLg==', N'RQ3DXYJTRFKXZYOHYLVCWYS3F6U6AEIC', N'453fa011-7f25-41bb-b63b-14ab36f6c419', NULL, 0, 0, NULL, 1, 0, N'ae244910', NULL)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'933570ea-e726-43fd-bfb7-2039fece6205', N'0eb75c83-6fb9-442e-8bcc-1465ff92e37a')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'39b3cfff-0feb-4d68-ad0a-0ea6840e0c01', N'b8dfdbba-1461-4e04-8180-8d215e4a802c')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'933570ea-e726-43fd-bfb7-2039fece6205', N'b8dfdbba-1461-4e04-8180-8d215e4a802c')
GO
SET IDENTITY_INSERT [dbo].[UnitTypes] ON 

INSERT [dbo].[UnitTypes] ([Id], [Name], [ARName], [Description], [Status]) VALUES (4, N'L', N'لتر', NULL, 1)
INSERT [dbo].[UnitTypes] ([Id], [Name], [ARName], [Description], [Status]) VALUES (5, N'M3', N'متر مكعب ', NULL, 1)
SET IDENTITY_INSERT [dbo].[UnitTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[FuelTypes] ON 

INSERT [dbo].[FuelTypes] ([Id], [Name], [ARName], [UnitTypeId], [Price], [Description], [Status], [Date]) VALUES (2, N'petrol 92', N'بنزين 92 ', 4, CAST(18.0000 AS Decimal(10, 4)), N'بنزين', 1, CAST(N'2023-08-20T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[FuelTypes] ([Id], [Name], [ARName], [UnitTypeId], [Price], [Description], [Status], [Date]) VALUES (3, N'Petrol 95', N'بنزين 95', 4, CAST(22.0000 AS Decimal(10, 4)), N'95', 1, CAST(N'2023-08-20T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[FuelTypes] ([Id], [Name], [ARName], [UnitTypeId], [Price], [Description], [Status], [Date]) VALUES (4, N'Gas', N'غاز طبيعي', 5, CAST(25.0000 AS Decimal(10, 4)), N'غاز طبيعي', 1, CAST(N'2023-08-20T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[FuelTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Ranges] ON 

INSERT [dbo].[Ranges] ([Id], [Name], [ARName], [Description], [Status]) VALUES (1, N'Cairo', N'القاهرة', NULL, 1)
INSERT [dbo].[Ranges] ([Id], [Name], [ARName], [Description], [Status]) VALUES (2, N'Giza', N'الجيزة', NULL, 1)
INSERT [dbo].[Ranges] ([Id], [Name], [ARName], [Description], [Status]) VALUES (3, N'Menofia', N'المنوفية', NULL, 1)
INSERT [dbo].[Ranges] ([Id], [Name], [ARName], [Description], [Status]) VALUES (4, N'Qaluobia', N'القليوبية', NULL, 1)
SET IDENTITY_INSERT [dbo].[Ranges] OFF
GO
SET IDENTITY_INSERT [dbo].[Regions] ON 

INSERT [dbo].[Regions] ([Id], [Name], [ARName], [Description], [Status], [RangeId]) VALUES (2, N'Maadi', N'المعادي', NULL, 1, 1)
INSERT [dbo].[Regions] ([Id], [Name], [ARName], [Description], [Status], [RangeId]) VALUES (4, N'EL-Tagamo3', N'التجمع', NULL, 1, 1)
INSERT [dbo].[Regions] ([Id], [Name], [ARName], [Description], [Status], [RangeId]) VALUES (5, N'October', N'اكتوبر', NULL, 1, 2)
INSERT [dbo].[Regions] ([Id], [Name], [ARName], [Description], [Status], [RangeId]) VALUES (7, N'El-Sheikh Zayed', N'الشيخ زايد ', NULL, 1, 2)
INSERT [dbo].[Regions] ([Id], [Name], [ARName], [Description], [Status], [RangeId]) VALUES (8, N'Haram', N'هرم ', NULL, 1, 2)
INSERT [dbo].[Regions] ([Id], [Name], [ARName], [Description], [Status], [RangeId]) VALUES (10, N'Shebin Alkom', N'شبين الكوم', NULL, 1, 3)
INSERT [dbo].[Regions] ([Id], [Name], [ARName], [Description], [Status], [RangeId]) VALUES (11, N'AlShohadaa', N'الشهداء', NULL, 1, 3)
INSERT [dbo].[Regions] ([Id], [Name], [ARName], [Description], [Status], [RangeId]) VALUES (12, N'Tala', N'تلا', NULL, 1, 3)
INSERT [dbo].[Regions] ([Id], [Name], [ARName], [Description], [Status], [RangeId]) VALUES (13, N'Bagur', N'الباجور', NULL, 1, 3)
INSERT [dbo].[Regions] ([Id], [Name], [ARName], [Description], [Status], [RangeId]) VALUES (14, N'Banha', N'بنها', NULL, 1, 4)
INSERT [dbo].[Regions] ([Id], [Name], [ARName], [Description], [Status], [RangeId]) VALUES (16, N'Shubra', N'شبرا', NULL, 1, 4)
SET IDENTITY_INSERT [dbo].[Regions] OFF
GO
SET IDENTITY_INSERT [dbo].[Stations] ON 

INSERT [dbo].[Stations] ([Id], [Name], [ARName], [Address], [Longtude], [Latitude], [RegionId], [Phone], [DateCreated]) VALUES (1, N'Aman Gas 1', N'امان غاز 1 ', N'المعادي', CAST(1.11100000 AS Decimal(11, 8)), CAST(2.22200000 AS Decimal(11, 8)), 2, N'01026708089', CAST(N'2023-08-18T18:28:39.3147112' AS DateTime2))
INSERT [dbo].[Stations] ([Id], [Name], [ARName], [Address], [Longtude], [Latitude], [RegionId], [Phone], [DateCreated]) VALUES (3, N'AmanGas2', N'امان غاز 2', N'غير محدد', CAST(30.20000000 AS Decimal(11, 8)), CAST(30.30000000 AS Decimal(11, 8)), 11, N'01026708089', CAST(N'2023-08-25T19:39:26.8157489' AS DateTime2))
INSERT [dbo].[Stations] ([Id], [Name], [ARName], [Address], [Longtude], [Latitude], [RegionId], [Phone], [DateCreated]) VALUES (4, N'AmanGas 3', N'امان غاز 3', N'غير محدد', CAST(30.20000000 AS Decimal(11, 8)), CAST(30.30000000 AS Decimal(11, 8)), 12, N'01026708089', CAST(N'2023-08-25T19:41:44.2366548' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Stations] OFF
GO
SET IDENTITY_INSERT [dbo].[SalesMen] ON 

INSERT [dbo].[SalesMen] ([Id], [Password], [Name], [DateOfBirth], [Status], [JoinDate], [FinishDate], [StationId], [StationId1], [NationalId], [PhoneNumber]) VALUES (6, N'MTIzNA==', N'ahmed', NULL, 1, CAST(N'2023-08-18T21:47:55.1818497' AS DateTime2), NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[SalesMen] ([Id], [Password], [Name], [DateOfBirth], [Status], [JoinDate], [FinishDate], [StationId], [StationId1], [NationalId], [PhoneNumber]) VALUES (7, N'MTIzNA==', N'ahmedali', CAST(N'2000-05-31T00:00:00.0000000' AS DateTime2), 1, CAST(N'2023-08-18T21:49:22.6886996' AS DateTime2), NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[SalesMen] ([Id], [Password], [Name], [DateOfBirth], [Status], [JoinDate], [FinishDate], [StationId], [StationId1], [NationalId], [PhoneNumber]) VALUES (8, N'MTIzNA==', N'ahmed3', CAST(N'2000-05-31T00:00:00.0000000' AS DateTime2), 1, CAST(N'2023-08-25T19:55:28.5876344' AS DateTime2), NULL, 3, NULL, NULL, NULL)
INSERT [dbo].[SalesMen] ([Id], [Password], [Name], [DateOfBirth], [Status], [JoinDate], [FinishDate], [StationId], [StationId1], [NationalId], [PhoneNumber]) VALUES (9, N'MTIzNA==', N'mohamedgamal', CAST(N'2023-05-31T10:10:30.2160000' AS DateTime2), 1, CAST(N'2023-08-26T13:25:04.5627261' AS DateTime2), NULL, 4, NULL, N'1234567890', N'01026708089')
SET IDENTITY_INSERT [dbo].[SalesMen] OFF
GO
SET IDENTITY_INSERT [dbo].[Fuelings] ON 

INSERT [dbo].[Fuelings] ([Id], [UserId], [Date], [FuelTypeId], [FuelSize], [StationId], [Status], [SalesManId], [VoidingDescription]) VALUES (1, N'39b3cfff-0feb-4d68-ad0a-0ea6840e0c01', CAST(N'2023-08-19T18:26:45.8389099' AS DateTime2), 2, CAST(15.0000 AS Decimal(12, 4)), 1, 0, 6, NULL)
INSERT [dbo].[Fuelings] ([Id], [UserId], [Date], [FuelTypeId], [FuelSize], [StationId], [Status], [SalesManId], [VoidingDescription]) VALUES (2, N'39b3cfff-0feb-4d68-ad0a-0ea6840e0c01', CAST(N'2023-08-19T18:31:25.2173796' AS DateTime2), 2, CAST(15.0000 AS Decimal(12, 4)), 1, 0, 6, NULL)
INSERT [dbo].[Fuelings] ([Id], [UserId], [Date], [FuelTypeId], [FuelSize], [StationId], [Status], [SalesManId], [VoidingDescription]) VALUES (3, N'39b3cfff-0feb-4d68-ad0a-0ea6840e0c01', CAST(N'2023-08-19T18:38:38.2631214' AS DateTime2), 2, CAST(15.0000 AS Decimal(12, 4)), 1, 0, 6, NULL)
INSERT [dbo].[Fuelings] ([Id], [UserId], [Date], [FuelTypeId], [FuelSize], [StationId], [Status], [SalesManId], [VoidingDescription]) VALUES (4, N'39b3cfff-0feb-4d68-ad0a-0ea6840e0c01', CAST(N'2023-08-19T18:43:20.4798975' AS DateTime2), 2, CAST(15.0000 AS Decimal(12, 4)), 1, 1, 6, NULL)
INSERT [dbo].[Fuelings] ([Id], [UserId], [Date], [FuelTypeId], [FuelSize], [StationId], [Status], [SalesManId], [VoidingDescription]) VALUES (5, N'39b3cfff-0feb-4d68-ad0a-0ea6840e0c01', CAST(N'2023-08-19T18:44:47.2435172' AS DateTime2), 2, CAST(15.0000 AS Decimal(12, 4)), 1, 1, 6, NULL)
INSERT [dbo].[Fuelings] ([Id], [UserId], [Date], [FuelTypeId], [FuelSize], [StationId], [Status], [SalesManId], [VoidingDescription]) VALUES (6, N'39b3cfff-0feb-4d68-ad0a-0ea6840e0c01', CAST(N'2023-08-19T18:45:40.7912126' AS DateTime2), 2, CAST(15.0000 AS Decimal(12, 4)), 1, 1, 6, NULL)
INSERT [dbo].[Fuelings] ([Id], [UserId], [Date], [FuelTypeId], [FuelSize], [StationId], [Status], [SalesManId], [VoidingDescription]) VALUES (7, N'39b3cfff-0feb-4d68-ad0a-0ea6840e0c01', CAST(N'2023-08-19T18:46:51.8710033' AS DateTime2), 2, CAST(20.0000 AS Decimal(12, 4)), 1, 1, 7, NULL)
INSERT [dbo].[Fuelings] ([Id], [UserId], [Date], [FuelTypeId], [FuelSize], [StationId], [Status], [SalesManId], [VoidingDescription]) VALUES (8, N'933570ea-e726-43fd-bfb7-2039fece6205', CAST(N'2023-08-19T19:23:46.1263501' AS DateTime2), 2, CAST(30.0000 AS Decimal(12, 4)), 1, 1, 7, NULL)
INSERT [dbo].[Fuelings] ([Id], [UserId], [Date], [FuelTypeId], [FuelSize], [StationId], [Status], [SalesManId], [VoidingDescription]) VALUES (9, N'933570ea-e726-43fd-bfb7-2039fece6205', CAST(N'2023-08-20T21:02:47.2791673' AS DateTime2), 2, CAST(30.0000 AS Decimal(12, 4)), 1, 1, 6, NULL)
INSERT [dbo].[Fuelings] ([Id], [UserId], [Date], [FuelTypeId], [FuelSize], [StationId], [Status], [SalesManId], [VoidingDescription]) VALUES (10, N'933570ea-e726-43fd-bfb7-2039fece6205', CAST(N'2023-08-21T22:20:23.3092213' AS DateTime2), 2, CAST(30.0000 AS Decimal(12, 4)), 1, 1, 6, NULL)
INSERT [dbo].[Fuelings] ([Id], [UserId], [Date], [FuelTypeId], [FuelSize], [StationId], [Status], [SalesManId], [VoidingDescription]) VALUES (12, N'933570ea-e726-43fd-bfb7-2039fece6205', CAST(N'2023-08-21T22:42:06.9465892' AS DateTime2), 2, CAST(35.0000 AS Decimal(12, 4)), 1, 0, 6, N'User Point Balance is Less Than Fueling Balance')
INSERT [dbo].[Fuelings] ([Id], [UserId], [Date], [FuelTypeId], [FuelSize], [StationId], [Status], [SalesManId], [VoidingDescription]) VALUES (13, N'933570ea-e726-43fd-bfb7-2039fece6205', CAST(N'2023-08-21T22:43:58.8158261' AS DateTime2), 2, CAST(30.0000 AS Decimal(12, 4)), 1, 2, 6, NULL)
INSERT [dbo].[Fuelings] ([Id], [UserId], [Date], [FuelTypeId], [FuelSize], [StationId], [Status], [SalesManId], [VoidingDescription]) VALUES (14, N'933570ea-e726-43fd-bfb7-2039fece6205', CAST(N'2023-08-25T19:58:00.8275479' AS DateTime2), 2, CAST(40.0000 AS Decimal(12, 4)), 3, 1, 8, NULL)
INSERT [dbo].[Fuelings] ([Id], [UserId], [Date], [FuelTypeId], [FuelSize], [StationId], [Status], [SalesManId], [VoidingDescription]) VALUES (15, N'933570ea-e726-43fd-bfb7-2039fece6205', CAST(N'2023-08-25T19:58:37.1570959' AS DateTime2), 3, CAST(40.0000 AS Decimal(12, 4)), 3, 1, 8, NULL)
INSERT [dbo].[Fuelings] ([Id], [UserId], [Date], [FuelTypeId], [FuelSize], [StationId], [Status], [SalesManId], [VoidingDescription]) VALUES (16, N'933570ea-e726-43fd-bfb7-2039fece6205', CAST(N'2023-08-25T20:06:34.6686941' AS DateTime2), 2, CAST(10.0000 AS Decimal(12, 4)), 3, 2, 8, NULL)
INSERT [dbo].[Fuelings] ([Id], [UserId], [Date], [FuelTypeId], [FuelSize], [StationId], [Status], [SalesManId], [VoidingDescription]) VALUES (17, N'933570ea-e726-43fd-bfb7-2039fece6205', CAST(N'2023-08-26T13:42:53.1898713' AS DateTime2), 4, CAST(40.5000 AS Decimal(12, 4)), 4, 1, 9, NULL)
INSERT [dbo].[Fuelings] ([Id], [UserId], [Date], [FuelTypeId], [FuelSize], [StationId], [Status], [SalesManId], [VoidingDescription]) VALUES (18, N'933570ea-e726-43fd-bfb7-2039fece6205', CAST(N'2023-08-26T13:45:45.7739169' AS DateTime2), 4, CAST(10.0000 AS Decimal(12, 4)), 4, 2, 9, NULL)
SET IDENTITY_INSERT [dbo].[Fuelings] OFF
GO
SET IDENTITY_INSERT [dbo].[AssignPointss] ON 

INSERT [dbo].[AssignPointss] ([Id], [UserId], [FuelingId], [Date], [Status], [Count]) VALUES (1, N'39b3cfff-0feb-4d68-ad0a-0ea6840e0c01', 4, CAST(N'2023-08-19T18:43:20.4798975' AS DateTime2), 1, CAST(1.5000 AS Decimal(10, 4)))
INSERT [dbo].[AssignPointss] ([Id], [UserId], [FuelingId], [Date], [Status], [Count]) VALUES (2, N'39b3cfff-0feb-4d68-ad0a-0ea6840e0c01', 5, CAST(N'2023-08-19T18:44:47.2435172' AS DateTime2), 1, CAST(1.5000 AS Decimal(10, 4)))
INSERT [dbo].[AssignPointss] ([Id], [UserId], [FuelingId], [Date], [Status], [Count]) VALUES (3, N'39b3cfff-0feb-4d68-ad0a-0ea6840e0c01', 6, CAST(N'2023-08-19T18:45:40.7912126' AS DateTime2), 1, CAST(1.5000 AS Decimal(10, 4)))
INSERT [dbo].[AssignPointss] ([Id], [UserId], [FuelingId], [Date], [Status], [Count]) VALUES (4, N'39b3cfff-0feb-4d68-ad0a-0ea6840e0c01', 7, CAST(N'2023-08-19T18:46:51.8710033' AS DateTime2), 1, CAST(2.0000 AS Decimal(10, 4)))
INSERT [dbo].[AssignPointss] ([Id], [UserId], [FuelingId], [Date], [Status], [Count]) VALUES (5, N'933570ea-e726-43fd-bfb7-2039fece6205', 8, CAST(N'2023-08-19T19:23:46.1263501' AS DateTime2), 1, CAST(3.0000 AS Decimal(10, 4)))
INSERT [dbo].[AssignPointss] ([Id], [UserId], [FuelingId], [Date], [Status], [Count]) VALUES (6, N'933570ea-e726-43fd-bfb7-2039fece6205', 9, CAST(N'2023-08-20T21:02:47.2791673' AS DateTime2), 1, CAST(3.0000 AS Decimal(10, 4)))
INSERT [dbo].[AssignPointss] ([Id], [UserId], [FuelingId], [Date], [Status], [Count]) VALUES (7, N'933570ea-e726-43fd-bfb7-2039fece6205', 10, CAST(N'2023-08-21T22:20:23.3092213' AS DateTime2), 1, CAST(54.0000 AS Decimal(10, 4)))
INSERT [dbo].[AssignPointss] ([Id], [UserId], [FuelingId], [Date], [Status], [Count]) VALUES (9, N'933570ea-e726-43fd-bfb7-2039fece6205', 13, CAST(N'2023-08-21T22:43:58.8158261' AS DateTime2), 2, CAST(54.0000 AS Decimal(10, 4)))
INSERT [dbo].[AssignPointss] ([Id], [UserId], [FuelingId], [Date], [Status], [Count]) VALUES (10, N'933570ea-e726-43fd-bfb7-2039fece6205', 14, CAST(N'2023-08-25T19:58:00.8275479' AS DateTime2), 1, CAST(72.0000 AS Decimal(10, 4)))
INSERT [dbo].[AssignPointss] ([Id], [UserId], [FuelingId], [Date], [Status], [Count]) VALUES (11, N'933570ea-e726-43fd-bfb7-2039fece6205', 15, CAST(N'2023-08-25T19:58:37.1570959' AS DateTime2), 1, CAST(88.0000 AS Decimal(10, 4)))
INSERT [dbo].[AssignPointss] ([Id], [UserId], [FuelingId], [Date], [Status], [Count]) VALUES (12, N'933570ea-e726-43fd-bfb7-2039fece6205', 16, CAST(N'2023-08-25T20:06:34.6686941' AS DateTime2), 2, CAST(18.0000 AS Decimal(10, 4)))
INSERT [dbo].[AssignPointss] ([Id], [UserId], [FuelingId], [Date], [Status], [Count]) VALUES (13, N'933570ea-e726-43fd-bfb7-2039fece6205', 17, CAST(N'2023-08-26T13:42:53.1898713' AS DateTime2), 1, CAST(101.2500 AS Decimal(10, 4)))
INSERT [dbo].[AssignPointss] ([Id], [UserId], [FuelingId], [Date], [Status], [Count]) VALUES (14, N'933570ea-e726-43fd-bfb7-2039fece6205', 18, CAST(N'2023-08-26T13:45:45.7739169' AS DateTime2), 2, CAST(25.0000 AS Decimal(10, 4)))
SET IDENTITY_INSERT [dbo].[AssignPointss] OFF
GO
SET IDENTITY_INSERT [dbo].[PointsRatios] ON 

INSERT [dbo].[PointsRatios] ([Id], [FueltTypeId], [FuelTypeId], [Ratio], [MoneyRatio]) VALUES (1, 2, 2, CAST(0.1000 AS Decimal(10, 4)), CAST(0.10 AS Decimal(18, 2)))
INSERT [dbo].[PointsRatios] ([Id], [FueltTypeId], [FuelTypeId], [Ratio], [MoneyRatio]) VALUES (2, 3, 3, CAST(0.2000 AS Decimal(10, 4)), CAST(0.10 AS Decimal(18, 2)))
INSERT [dbo].[PointsRatios] ([Id], [FueltTypeId], [FuelTypeId], [Ratio], [MoneyRatio]) VALUES (3, 4, 4, CAST(0.3333 AS Decimal(10, 4)), CAST(0.10 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[PointsRatios] OFF
GO
SET IDENTITY_INSERT [dbo].[SalesRequest] ON 

INSERT [dbo].[SalesRequest] ([Id], [Name], [Password], [RequestDate], [Status], [Comment], [MangerApproved], [StationId], [DateOfBirth], [NationalId], [PhoneNumber]) VALUES (1, N'ahmedali', N'MTIzNA==', CAST(N'2023-08-18T21:46:43.8409000' AS DateTime2), 1, N'Iam Ahmed Ali Friend of mahmoud eldeeb', N'mahmoudeldeeb', 1, CAST(N'2000-05-31T00:00:00.0000000' AS DateTime2), NULL, NULL)
INSERT [dbo].[SalesRequest] ([Id], [Name], [Password], [RequestDate], [Status], [Comment], [MangerApproved], [StationId], [DateOfBirth], [NationalId], [PhoneNumber]) VALUES (2, N'ahmed', N'MTIzNA==', CAST(N'2023-08-18T21:46:51.1430187' AS DateTime2), 1, N'Iam Ahmed Ali Friend of mahmoud eldeeb', N'mahmoudeldeeb', 1, CAST(N'2000-05-31T00:00:00.0000000' AS DateTime2), NULL, NULL)
INSERT [dbo].[SalesRequest] ([Id], [Name], [Password], [RequestDate], [Status], [Comment], [MangerApproved], [StationId], [DateOfBirth], [NationalId], [PhoneNumber]) VALUES (1007, N'ahmed3', N'MTIzNA==', CAST(N'2023-08-25T19:52:47.7733472' AS DateTime2), 1, N'comment3', N'mahmoudeldeeb', 3, CAST(N'2000-05-31T00:00:00.0000000' AS DateTime2), NULL, NULL)
INSERT [dbo].[SalesRequest] ([Id], [Name], [Password], [RequestDate], [Status], [Comment], [MangerApproved], [StationId], [DateOfBirth], [NationalId], [PhoneNumber]) VALUES (1008, N'mohamedgamal', N'MTIzNA==', CAST(N'2023-08-26T13:11:57.1938561' AS DateTime2), 1, N'mahmoud eldeeb KNefuew', N'mahmoudeldeeb', 1, CAST(N'2023-05-31T10:10:30.2160000' AS DateTime2), N'1234567890', N'01026708089')
SET IDENTITY_INSERT [dbo].[SalesRequest] OFF
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230604201501_initial', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230605164910_addgmail', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230607205527_addsometables', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230610160004_addsalesman', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230610161339_adits', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230610180748_additions', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230610181249_addition', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230613182300_addview', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230701084359_addsalesrequesttable', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230701091254_ss', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230701093116_asss', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230701093339_assss', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230818155019_changeuseertypetostring', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230818182306_repair', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230819132051_update', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230819132245_update2', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230819143726_repair2', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230821175524_update3', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230821182830_update12', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230821191048_createviews', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230821193305_addvoidingdescription', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230825151125_adddailyreports', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230825161523_adddailyreportsdayinstring', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230825162907_changetodateonly', N'6.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230826092735_addnathionalId', N'6.0.16')
GO
INSERT [HangFire].[Schema] ([Version]) VALUES (9)
GO
INSERT [HangFire].[Server] ([Id], [Data], [LastHeartbeat]) VALUES (N'desktop-j5pce81:23512:e40f0c72-f7fd-46ac-9d95-ad4cb8e6ce9e', N'{"WorkerCount":20,"Queues":["default"],"StartedAt":"2023-08-18T14:26:05.3419616Z"}', CAST(N'2023-08-18T14:34:27.803' AS DateTime))
GO
