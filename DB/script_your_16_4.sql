USE [OnlineShop]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 16/04/2016 2:32:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[PreCateID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Images]    Script Date: 16/04/2016 2:32:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Images](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NameCode] [int] NULL,
	[Path] [nvarchar](1000) NULL,
	[Utility] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Product]    Script Date: 16/04/2016 2:32:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Price] [float] NULL,
	[CateID] [int] NULL,
	[Descriptions] [nvarchar](max) NULL,
	[Quantity] [int] NULL,
	[Img1] [nvarchar](max) NULL,
	[Img2] [nvarchar](max) NULL,
	[Img3] [nvarchar](max) NULL,
	[Img4] [nvarchar](max) NULL,
	[Trend] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Category] ON 

GO
INSERT [dbo].[Category] ([ID], [Name], [PreCateID]) VALUES (1, N'ĐỒNG PHỤC CÔNG SỞ', -1)
GO
INSERT [dbo].[Category] ([ID], [Name], [PreCateID]) VALUES (2, N'THỜI TRANG NAM', -1)
GO
INSERT [dbo].[Category] ([ID], [Name], [PreCateID]) VALUES (3, N'THỜI TRANG NỮ', -1)
GO
INSERT [dbo].[Category] ([ID], [Name], [PreCateID]) VALUES (4, N'VEST', 1)
GO
INSERT [dbo].[Category] ([ID], [Name], [PreCateID]) VALUES (5, N'SƠ MI', 1)
GO
INSERT [dbo].[Category] ([ID], [Name], [PreCateID]) VALUES (6, N'ÁO THUN', 1)
GO
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Images] ON 

GO
INSERT [dbo].[Images] ([ID], [NameCode], [Path], [Utility]) VALUES (6, -2, N'/Images/SilderImages/img_blog.jpg', N'')
GO
INSERT [dbo].[Images] ([ID], [NameCode], [Path], [Utility]) VALUES (7, -2, N'/Images/SilderImages/cover2.jpg', N'')
GO
SET IDENTITY_INSERT [dbo].[Images] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

GO
INSERT [dbo].[Product] ([ID], [Name], [Price], [CateID], [Descriptions], [Quantity], [Img1], [Img2], [Img3], [Img4], [Trend]) VALUES (9, N'Vest 1', 30000, 4, N'<p>Trao đổi với b&aacute;o ch&iacute; ng&agrave;y 12/4 về căn cứ t&iacute;nh ph&iacute; BOT, Thứ trưởng Giao th&ocirc;ng Vận tải Nguyễn Hồng Trường cho biết, qu&aacute; tr&igrave;nh x&acirc;y dựng c&aacute;c dự &aacute;n hạ tầng theo h&igrave;nh thức BOT, Bộ Giao th&ocirc;ng v&agrave; Bộ T&agrave;i ch&iacute;nh đ&atilde; duyệt phương &aacute;n t&agrave;i ch&iacute;nh, c&oacute; lộ tr&igrave;nh tăng ph&iacute; trong 3 năm dựa tr&ecirc;n mức tăng chỉ số CPI tr&ecirc;n cả nước v&agrave; ho&agrave;n vốn để đảm bảo lợi &iacute;ch c&aacute;c b&ecirc;n. Những năm trước, chỉ số CPI kh&ocirc;ng thay đổi n&ecirc;n nhiều trạm BOT đ&atilde; c&oacute; thời gian d&agrave;i kh&ocirc;ng tăng ph&iacute;.</p>
', 0, N'/Images/ProductImages/IMG_1036_1.jpg', N'/Images/ProductImages/IMG_1037_1(2).jpg', N'/Images/ProductImages/IMG_1038_1(2).jpg', N'', -5)
GO
INSERT [dbo].[Product] ([ID], [Name], [Price], [CateID], [Descriptions], [Quantity], [Img1], [Img2], [Img3], [Img4], [Trend]) VALUES (10, N'Áo thun 1', 3000, 1, N'<p>&nbsp; &nbsp;&nbsp;</p>
', 0, N'/Images/ProductImages/DSC_0642.jpg', N'/Images/ProductImages/DSC_0649.jpg', N'/Images/ProductImages/DSC_0651.jpg', NULL, -6)
GO
INSERT [dbo].[Product] ([ID], [Name], [Price], [CateID], [Descriptions], [Quantity], [Img1], [Img2], [Img3], [Img4], [Trend]) VALUES (11, N'Váy 1', 3000, 3, N'<p>&nbsp;&nbsp;</p>
', 0, N'/Images/ProductImages/DSC_0689.jpg', N'/Images/ProductImages/DSC_0691.jpg', NULL, NULL, -5)
GO
INSERT [dbo].[Product] ([ID], [Name], [Price], [CateID], [Descriptions], [Quantity], [Img1], [Img2], [Img3], [Img4], [Trend]) VALUES (12, N'Vest 1', 3000, 4, N'<p>&nbsp; &nbsp;&nbsp;</p>
', 0, N'/Images/ProductImages/IMG_1036_1(2).jpg', N'/Images/ProductImages/IMG_1037_1(2).jpg', N'/Images/ProductImages/IMG_1035_1.jpg', NULL, -6)
GO
INSERT [dbo].[Product] ([ID], [Name], [Price], [CateID], [Descriptions], [Quantity], [Img1], [Img2], [Img3], [Img4], [Trend]) VALUES (13, N'Áo thun nam', 3000, 2, N'<p>&nbsp; &nbsp;</p>
', 0, N'/Images/ProductImages/DSC_0729.jpg', N'/Images/ProductImages/DSC_0735.jpg', N'/Images/ProductImages/DSC_0751.jpg', NULL, -6)
GO
INSERT [dbo].[Product] ([ID], [Name], [Price], [CateID], [Descriptions], [Quantity], [Img1], [Img2], [Img3], [Img4], [Trend]) VALUES (14, N'Sơ mi văn phòng', 3000, 5, N'<p>&nbsp; &nbsp;</p>
', 0, N'/Images/ProductImages/IMG_1061_1.jpg', N'/Images/ProductImages/IMG_1062_1.jpg', N'/Images/ProductImages/IMG_1063_1.jpg', NULL, -6)
GO
INSERT [dbo].[Product] ([ID], [Name], [Price], [CateID], [Descriptions], [Quantity], [Img1], [Img2], [Img3], [Img4], [Trend]) VALUES (15, N'Sơ mi văn phòng nữ', 3000, 5, N'<p>&nbsp; &nbsp;</p>
', 0, N'/Images/ProductImages/IMG_1060_1.jpg', N'/Images/ProductImages/IMG_1059_1.jpg', N'/Images/ProductImages/IMG_1058_1.jpg', NULL, -6)
GO
INSERT [dbo].[Product] ([ID], [Name], [Price], [CateID], [Descriptions], [Quantity], [Img1], [Img2], [Img3], [Img4], [Trend]) VALUES (16, N'Váy 2', 30000, 3, N'<p>&nbsp;&nbsp;</p>
', 0, N'/Images/ProductImages/DSC_0756.jpg', N'/Images/ProductImages/DSC_0770.jpg', N'/Images/ProductImages/DSC_0773.jpg', NULL, -6)
GO
INSERT [dbo].[Product] ([ID], [Name], [Price], [CateID], [Descriptions], [Quantity], [Img1], [Img2], [Img3], [Img4], [Trend]) VALUES (17, N'Váy 3', 3000, 3, N'<p>&nbsp;&nbsp;</p>
', 0, N'/Images/ProductImages/DSC_0718.jpg', N'/Images/ProductImages/DSC_0716.jpg', NULL, NULL, -6)
GO
INSERT [dbo].[Product] ([ID], [Name], [Price], [CateID], [Descriptions], [Quantity], [Img1], [Img2], [Img3], [Img4], [Trend]) VALUES (18, N'Vest nữ', 333, 1, N'<p>&nbsp;&nbsp;</p>
', 0, N'/Images/ProductImages/IMG_1044_1.jpg', N'/Images/ProductImages/IMG_1045_1.jpg', N'/Images/ProductImages/IMG_1055_1.jpg', NULL, -5)
GO
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
