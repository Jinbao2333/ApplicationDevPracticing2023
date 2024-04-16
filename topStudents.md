``` sql
USE [students]
GO
/****** Object:  Table [dbo].[tblTopStudents]    Script Date: 07/10/2023 15:55:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblTopStudents](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[studentNo] [nvarchar](50) NOT NULL,
	[studentName] [nvarchar](50) NOT NULL,
	[Gender] [bit] NOT NULL,
	[Birthday] [datetime] NOT NULL,
	[Major] [nvarchar](50) NOT NULL,
	[QQ] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[Intro] [nvarchar](50) NULL,
	[Province] [nvarchar](50) NULL,
	[LoginTimes] [int] NOT NULL,
	[face] [nvarchar](max) NULL,
 CONSTRAINT [PK_tblTopStudents] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_tblTopStudents] UNIQUE NONCLUSTERED 
(
	[studentNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblTopStudents] ON
INSERT [dbo].[tblTopStudents] ([id], [studentNo], [studentName], [Gender], [Birthday], [Major], [QQ], [Email], [Phone], [Intro], [Province], [LoginTimes], [face]) VALUES (2, N'31021211014', N'蒲鹏', 1, CAST(0x00008EAC00000000 AS DateTime), N'计算机', N'68519112', N'ppu@cc.ecnu.edu.cn', N'13612311111', N'这个家伙很懒', N'山东', 0, N'')
INSERT [dbo].[tblTopStudents] ([id], [studentNo], [studentName], [Gender], [Birthday], [Major], [QQ], [Email], [Phone], [Intro], [Province], [LoginTimes], [face]) VALUES (4, N'10224602123', N'木卜燕', 0, CAST(0x0000954600000000 AS DateTime), N'数据科学与大数据技术', N'2952095123', N'2952095622@qq.com', N'12312341234', N'这家伙也很懒', N'吉林', 1, N'团员')
INSERT [dbo].[tblTopStudents] ([id], [studentNo], [studentName], [Gender], [Birthday], [Major], [QQ], [Email], [Phone], [Intro], [Province], [LoginTimes], [face]) VALUES (5, N'10225501447', N'姜嘉祺', 1, CAST(0x000092F400000000 AS DateTime), N'数据科学与大数据技术', N'1366171607', N'jjq03@foxmail.com', N'17705293826', N'没有留下任何文字', N'上海', 0, N'')
INSERT [dbo].[tblTopStudents] ([id], [studentNo], [studentName], [Gender], [Birthday], [Major], [QQ], [Email], [Phone], [Intro], [Province], [LoginTimes], [face]) VALUES (6, N'10225102444', N'李文奇', 1, CAST(0x0000956200000000 AS DateTime), N'计算机', N'2407383236', N'2697806417@qq.com', N'12345678901', N'你叠', N'江苏', 0, N'')
INSERT [dbo].[tblTopStudents] ([id], [studentNo], [studentName], [Gender], [Birthday], [Major], [QQ], [Email], [Phone], [Intro], [Province], [LoginTimes], [face]) VALUES (7, N'10222140408', N'谷杰', 1, CAST(0x000094FB00000000 AS DateTime), N'计算机', N'1776127334', N'1776127334@qq.com', N'13701835198', N'V我50再说', N'湖南', 0, N'')
INSERT [dbo].[tblTopStudents] ([id], [studentNo], [studentName], [Gender], [Birthday], [Major], [QQ], [Email], [Phone], [Intro], [Province], [LoginTimes], [face]) VALUES (8, N'10225102480', N'额尔琪', 1, CAST(0x000092BD00000000 AS DateTime), N'计算机', N'1320610652', N'erqi@ecnu.edu.cn', N'114514aaaaaa', N'高山流水，相逢是缘', N'内蒙古', 0, N'')
INSERT [dbo].[tblTopStudents] ([id], [studentNo], [studentName], [Gender], [Birthday], [Major], [QQ], [Email], [Phone], [Intro], [Province], [LoginTimes], [face]) VALUES (11, N'10225102459', N'杨鸣谦', 1, CAST(0x0000958C00000000 AS DateTime), N'计算机', N'3041712310', N'3041712310@qq.com', N'13815555341', N'懒死我了算了', N'江苏', 0, N'团员')
INSERT [dbo].[tblTopStudents] ([id], [studentNo], [studentName], [Gender], [Birthday], [Major], [QQ], [Email], [Phone], [Intro], [Province], [LoginTimes], [face]) VALUES (14, N'10225102509', N'童言', 1, CAST(0x0000949A00000000 AS DateTime), N'计算机', N'68519112', N'10225102509@stu.ecnu.edu.cn', N'18917438532', N'这个家伙很懒', N'上海', 0, N'')
INSERT [dbo].[tblTopStudents] ([id], [studentNo], [studentName], [Gender], [Birthday], [Major], [QQ], [Email], [Phone], [Intro], [Province], [LoginTimes], [face]) VALUES (16, N'10215300402', N'朱维清', 1, CAST(0x0000939A00000000 AS DateTime), N'计算机', N'94546750', N'10215300402@stu.ecnu.edu.cn', N'17521366169', N'峡谷电报员', N'上海', 0, N'')
INSERT [dbo].[tblTopStudents] ([id], [studentNo], [studentName], [Gender], [Birthday], [Major], [QQ], [Email], [Phone], [Intro], [Province], [LoginTimes], [face]) VALUES (17, N'10225101469', N'朱陈媛', 0, CAST(0x000094BF00000000 AS DateTime), N'软件工程', N'2945259300', N'10225101469@ecnu.edu.com', N'12332111233', N'啥玩意', N'江苏', 0, N'团员')
INSERT [dbo].[tblTopStudents] ([id], [studentNo], [studentName], [Gender], [Birthday], [Major], [QQ], [Email], [Phone], [Intro], [Province], [LoginTimes], [face]) VALUES (21, N'10225102494', N'陈稷豪', 1, CAST(0x000094C300000000 AS DateTime), N'计算机', N'1953911577', N'1953911577@qq.com', N'18472839421', N'母鸡啊', N'上海', 0, N'')
INSERT [dbo].[tblTopStudents] ([id], [studentNo], [studentName], [Gender], [Birthday], [Major], [QQ], [Email], [Phone], [Intro], [Province], [LoginTimes], [face]) VALUES (22, N'10223903406', N'曹可心', 0, CAST(0x0000952600000000 AS DateTime), N'数据科学与大数据技术', N'2411631923', N'2411631923@qq.com', N'12312341234', N'这家伙也很懒', N'安徽', 1, N'团员')
INSERT [dbo].[tblTopStudents] ([id], [studentNo], [studentName], [Gender], [Birthday], [Major], [QQ], [Email], [Phone], [Intro], [Province], [LoginTimes], [face]) VALUES (25, N'10225101535', N'徐翔宇', 1, CAST(0x0000941B00000000 AS DateTime), N'软件工程', N'110120', N'10225101535@edu.dcnu.stu.cn', N'4008117117', N'不留', N'上海', 0, N'')
INSERT [dbo].[tblTopStudents] ([id], [studentNo], [studentName], [Gender], [Birthday], [Major], [QQ], [Email], [Phone], [Intro], [Province], [LoginTimes], [face]) VALUES (26, N'10225101529', N'田亦海', 1, CAST(0x000096A100000000 AS DateTime), N'软件工程', N'2324899904', N'2324899904@qq.com', N'19370570730', N'null', N'河南', 1, N'null')
INSERT [dbo].[tblTopStudents] ([id], [studentNo], [studentName], [Gender], [Birthday], [Major], [QQ], [Email], [Phone], [Intro], [Province], [LoginTimes], [face]) VALUES (29, N'10224507041', N'姚凯文', 1, CAST(0x0000955500000000 AS DateTime), N'数据科学与大数据技术', N'1123581321', N'10224507041@ecnu.edu.cn', N'1123581321', N'NULL', N'陕西', 0, N'')
INSERT [dbo].[tblTopStudents] ([id], [studentNo], [studentName], [Gender], [Birthday], [Major], [QQ], [Email], [Phone], [Intro], [Province], [LoginTimes], [face]) VALUES (30, N'10225101447', N'唐硕', 1, CAST(0x0000946500000000 AS DateTime), N'软件工程', N'1965772044', N'1965772044@qq.com', N'19619527233', N'。。。', N'江苏', 0, N'团员')
INSERT [dbo].[tblTopStudents] ([id], [studentNo], [studentName], [Gender], [Birthday], [Major], [QQ], [Email], [Phone], [Intro], [Province], [LoginTimes], [face]) VALUES (31, N'10225101419', N'贺云航', 1, CAST(0x000094C400000000 AS DateTime), N'软件工程', N'2323175054', N'10225101419@stu.ecnu.edu.cn', N'18888888888', N'这家伙很忄', N'天津', 99, NULL)
INSERT [dbo].[tblTopStudents] ([id], [studentNo], [studentName], [Gender], [Birthday], [Major], [QQ], [Email], [Phone], [Intro], [Province], [LoginTimes], [face]) VALUES (32, N'10225101483', N'谢瑞阳', 1, CAST(0x000007BB00000000 AS DateTime), N'软件工程', N'1823748191', N'1823748191@qq.com', N'18067993956', N'原神，启动！', N'浙江', 42, N'团员')
INSERT [dbo].[tblTopStudents] ([id], [studentNo], [studentName], [Gender], [Birthday], [Major], [QQ], [Email], [Phone], [Intro], [Province], [LoginTimes], [face]) VALUES (33, N'10225102491', N'张宇昂', 1, CAST(0x0000950500000000 AS DateTime), N'计算机', N'3525169851', N'10225102491@stu.ecnu.edu.cn', N'13061855220', N'这个家伙很懒', N'上海', 0, N'')
INSERT [dbo].[tblTopStudents] ([id], [studentNo], [studentName], [Gender], [Birthday], [Major], [QQ], [Email], [Phone], [Intro], [Province], [LoginTimes], [face]) VALUES (35, N'10225501435', N'王雪飞', 1, CAST(0x0000951D00000000 AS DateTime), N'数据科学与大数据技术', N'1396114066', N'10225501435@ecnu.edu.cn', N'15139432595', N'', N'河南', 0, N'团员')
INSERT [dbo].[tblTopStudents] ([id], [studentNo], [studentName], [Gender], [Birthday], [Major], [QQ], [Email], [Phone], [Intro], [Province], [LoginTimes], [face]) VALUES (38, N'10222140454', N'陈予曈', 0, CAST(0x0000945900000000 AS DateTime), N'数据科学与大数据技术', N'12345678', N'12345678', N'12345678', N'', N'上海', 0, N'')
INSERT [dbo].[tblTopStudents] ([id], [studentNo], [studentName], [Gender], [Birthday], [Major], [QQ], [Email], [Phone], [Intro], [Province], [LoginTimes], [face]) VALUES (40, N'10225101440', N'韩晨旭', 1, CAST(0x0000958800000000 AS DateTime), N'软件工程', N'981354012', N'10225101440@stu.ecnu.edu.cn', N'19370576305', N'アルクェイド', N'江西', 1, N'共青团员')
INSERT [dbo].[tblTopStudents] ([id], [studentNo], [studentName], [Gender], [Birthday], [Major], [QQ], [Email], [Phone], [Intro], [Province], [LoginTimes], [face]) VALUES (41, N'10224101327', N'甘雨', 0, CAST(0x0000ACAE00000000 AS DateTime), N'冰', N'null', N'null', N'null', N'月海亭的秘书，体内流淌着仙兽「麒麟」的血脉。', N'璃月', 1, N'null')
INSERT [dbo].[tblTopStudents] ([id], [studentNo], [studentName], [Gender], [Birthday], [Major], [QQ], [Email], [Phone], [Intro], [Province], [LoginTimes], [face]) VALUES (45, N'10225100000', N'胡桃', 1, CAST(0x0000AD7200000000 AS DateTime), N'往生堂', N'-----', N'-----', N'-------', N'唷，找本堂主有何贵干呀？', N'璃月', 0, N'')
INSERT [dbo].[tblTopStudents] ([id], [studentNo], [studentName], [Gender], [Birthday], [Major], [QQ], [Email], [Phone], [Intro], [Province], [LoginTimes], [face]) VALUES (53, N'10224602413', N'朴祉燕', 0, CAST(0x0000954600000000 AS DateTime), N'数据科学与大数据技术', N'2952095123', N'2952095622@qq.com', N'12312341234', N'这家伙也很懒', N'吉林', 1, N'')
INSERT [dbo].[tblTopStudents] ([id], [studentNo], [studentName], [Gender], [Birthday], [Major], [QQ], [Email], [Phone], [Intro], [Province], [LoginTimes], [face]) VALUES (54, N'1000', N'Mr.Oven', 1, CAST(0x00008EAC00000000 AS DateTime), N'Judge', N'11235813210', N'10987654321@ecnu.edu.cn', N'11235813210', N'And then there is no one。', N'士兵岛', 0, N'')
INSERT [dbo].[tblTopStudents] ([id], [studentNo], [studentName], [Gender], [Birthday], [Major], [QQ], [Email], [Phone], [Intro], [Province], [LoginTimes], [face]) VALUES (55, N'10225102463', N'李畅', 1, CAST(0x000092F900000000 AS DateTime), N'计算机', N'865373641', N'865373641@qq,com', N'136xxxxxxxx', N'poker face.jpg', N'江苏', 0, N'pokerface.jpg')
INSERT [dbo].[tblTopStudents] ([id], [studentNo], [studentName], [Gender], [Birthday], [Major], [QQ], [Email], [Phone], [Intro], [Province], [LoginTimes], [face]) VALUES (58, N'10225501448', N'李度', 1, CAST(0x0000954B00000000 AS DateTime), N'数据科学与大数据技术', N'2559978119', N'2559978119@qq.com', N'18017897067', N'...', N'上海', 0, N'')
INSERT [dbo].[tblTopStudents] ([id], [studentNo], [studentName], [Gender], [Birthday], [Major], [QQ], [Email], [Phone], [Intro], [Province], [LoginTimes], [face]) VALUES (59, N'10225501422', N'林童奕凡', 1, CAST(0x0000941D00000000 AS DateTime), N'数据科学与大数据技术', N'2806146119', N'2806146119@qq.com', N'15267317219', N'pp大帅哥', N'浙江', 0, N'')
INSERT [dbo].[tblTopStudents] ([id], [studentNo], [studentName], [Gender], [Birthday], [Major], [QQ], [Email], [Phone], [Intro], [Province], [LoginTimes], [face]) VALUES (61, N'10225100001', N'アルクェイド　ブリュ－ンスタド', 0, CAST(0x0000958800000000 AS DateTime), N'软件工程', N' ', N' ', N' ', N'お姫様', N'千年城', 1, N' ')
INSERT [dbo].[tblTopStudents] ([id], [studentNo], [studentName], [Gender], [Birthday], [Major], [QQ], [Email], [Phone], [Intro], [Province], [LoginTimes], [face]) VALUES (68, N'114514', N'李白', 1, CAST(0x000007D600000000 AS DateTime), N'王者荣耀', N'123', N'321', N'12321', N'我超，农！', N'王者峡谷', 111, N'试用角色')
INSERT [dbo].[tblTopStudents] ([id], [studentNo], [studentName], [Gender], [Birthday], [Major], [QQ], [Email], [Phone], [Intro], [Province], [LoginTimes], [face]) VALUES (72, N'1145141919810', N'杰哥', 1, CAST(0x00008EAC00000000 AS DateTime), N'表演与艺术', N'1123581321', N'114514@ecnu.edu.cn', N'114514', N'不要啊！', N'保加利亚', 0, N'')
SET IDENTITY_INSERT [dbo].[tblTopStudents] OFF

```

