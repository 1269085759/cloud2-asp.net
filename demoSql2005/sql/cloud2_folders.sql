USE [cloud2]
GO
/****** 对象:  Table [dbo].[cloud2_folders]    脚本日期: 05/19/2015 13:59:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cloud2_folders](
	[fd_id]				[int] IDENTITY(1,1) NOT NULL,
	[fd_name]			[varchar](50) COLLATE Chinese_PRC_CI_AS NULL CONSTRAINT [DF_cloud2_folders_fd_name]  DEFAULT (''),
	[fd_pid]			[int] NULL CONSTRAINT [DF_cloud2_folders_fd_pid]  DEFAULT ((0)),
	[fd_uid]			[int] NULL CONSTRAINT [DF_cloud2_folders_fd_uid]  DEFAULT ((0)),
	[fd_length]			[bigint] NULL CONSTRAINT [DF_cloud2_folders_fd_length]  DEFAULT ((0)),
	[fd_size]			[varchar](50) COLLATE Chinese_PRC_CI_AS NULL CONSTRAINT [DF_cloud2_folders_fd_size]  DEFAULT (''),
	[fd_pathLoc]		[varchar](255) COLLATE Chinese_PRC_CI_AS NULL CONSTRAINT [DF_cloud2_folders_fd_pathLoc]  DEFAULT (''),
	[fd_pathSvr]		[varchar](255) COLLATE Chinese_PRC_CI_AS NULL CONSTRAINT [DF_cloud2_folders_fd_pathSvr]  DEFAULT (''),
	[fd_folders]		[int] NULL CONSTRAINT [DF_cloud2_folders_fd_folders]  DEFAULT ((0)),
	[fd_files]			[int] NULL CONSTRAINT [DF_cloud2_folders_fd_files]  DEFAULT ((0)),
	[fd_filesComplete]	[int] NULL CONSTRAINT [DF_cloud2_folders_fd_filesComplete]  DEFAULT ((0)),
	[fd_complete]		[bit] NULL CONSTRAINT [DF_cloud2_folders_fd_complete]  DEFAULT ((0)),
	[fd_delete]			[bit] NULL CONSTRAINT [DF_cloud2_folders_fd_delete]  DEFAULT ((0)),
	[fd_json]			[varchar](max) COLLATE Chinese_PRC_CI_AS NULL CONSTRAINT [DF_cloud2_folders_fd_json]  DEFAULT (''),
	[timeUpload]		[datetime] NULL CONSTRAINT [DF_cloud2_folders_timeUpload]  DEFAULT (getdate()),
	[fd_pidRoot]		[int] NULL CONSTRAINT [DF_cloud2_folders_fd_pidRoot]  DEFAULT ((0)),
	[fd_pathRel]		[nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL CONSTRAINT [DF_cloud2_folders_fd_pathRel]  DEFAULT ('')
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文件夹名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cloud2_folders', @level2type=N'COLUMN',@level2name=N'fd_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父级ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cloud2_folders', @level2type=N'COLUMN',@level2name=N'fd_pid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户ID。' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cloud2_folders', @level2type=N'COLUMN',@level2name=N'fd_uid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数字化的大小。以字节为单位。示例：1023652' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cloud2_folders', @level2type=N'COLUMN',@level2name=N'fd_length'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'格式化的大小。示例：10G' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cloud2_folders', @level2type=N'COLUMN',@level2name=N'fd_size'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文件夹在客户端的路径' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cloud2_folders', @level2type=N'COLUMN',@level2name=N'fd_pathLoc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文件夹在服务端的路径' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cloud2_folders', @level2type=N'COLUMN',@level2name=N'fd_pathSvr'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文件夹数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cloud2_folders', @level2type=N'COLUMN',@level2name=N'fd_folders'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文件数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cloud2_folders', @level2type=N'COLUMN',@level2name=N'fd_files'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'已上传完的文件数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cloud2_folders', @level2type=N'COLUMN',@level2name=N'fd_filesComplete'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否已上传完毕' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cloud2_folders', @level2type=N'COLUMN',@level2name=N'fd_complete'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否已删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cloud2_folders', @level2type=N'COLUMN',@level2name=N'fd_delete'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上传时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cloud2_folders', @level2type=N'COLUMN',@level2name=N'timeUpload'