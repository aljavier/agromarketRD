USE [master]
GO
/****** Object:  Database [AgroMarketDB]    Script Date: 3/25/2017 8:55:54 AM ******/
CREATE DATABASE [AgroMarketDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AgroMarketDB', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\AgroMarketDB.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'AgroMarketDB_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\AgroMarketDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [AgroMarketDB] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AgroMarketDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AgroMarketDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AgroMarketDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AgroMarketDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AgroMarketDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AgroMarketDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [AgroMarketDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AgroMarketDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AgroMarketDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AgroMarketDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AgroMarketDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AgroMarketDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AgroMarketDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AgroMarketDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AgroMarketDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AgroMarketDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AgroMarketDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AgroMarketDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AgroMarketDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AgroMarketDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AgroMarketDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AgroMarketDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AgroMarketDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AgroMarketDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AgroMarketDB] SET  MULTI_USER 
GO
ALTER DATABASE [AgroMarketDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AgroMarketDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AgroMarketDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AgroMarketDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [AgroMarketDB]
GO
/****** Object:  User [mrx]    Script Date: 3/25/2017 8:55:54 AM ******/
CREATE USER [mrx] FOR LOGIN [mrx] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[cuenta]    Script Date: 3/25/2017 8:55:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cuenta](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[monto] [numeric](18, 2) NOT NULL,
	[limite_credito] [numeric](18, 2) NOT NULL,
	[total_consumido] [numeric](18, 2) NOT NULL,
	[total_disponible] [numeric](18, 2) NOT NULL,
	[descripcion] [nvarchar](250) NOT NULL,
	[activo] [bit] NOT NULL,
 CONSTRAINT [PK_cuenta] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[demanda]    Script Date: 3/25/2017 8:55:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[demanda](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[producto_id] [int] NOT NULL,
	[tipo_unidad_id] [int] NOT NULL,
	[usuario_id] [int] NOT NULL,
	[comentario] [nvarchar](250) NOT NULL,
	[activo] [bit] NOT NULL,
 CONSTRAINT [PK_demanda] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[errores]    Script Date: 3/25/2017 8:55:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[errores](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[codigo] [nvarchar](50) NOT NULL,
	[descripcion] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_errores] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[intencion_compra]    Script Date: 3/25/2017 8:55:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[intencion_compra](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[demanda_id] [int] NOT NULL,
	[oferta_id] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
	[fecha_creacion] [datetime] NOT NULL,
	[fecha_expiracion] [datetime] NOT NULL,
	[activo] [bit] NOT NULL,
 CONSTRAINT [PK_intencion_compra] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[oferta]    Script Date: 3/25/2017 8:55:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[oferta](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[producto_id] [int] NOT NULL,
	[tipo_unidad_id] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
	[precio_unidad] [numeric](18, 2) NOT NULL,
	[monto_total] [numeric](18, 2) NOT NULL,
	[usuario_id] [int] NOT NULL,
	[fecha_creacion] [datetime] NOT NULL,
	[activo] [bit] NOT NULL,
 CONSTRAINT [PK_oferta] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[producto]    Script Date: 3/25/2017 8:55:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[producto](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[codigo] [nvarchar](50) NOT NULL,
	[descripcion] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_producto] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[sesion]    Script Date: 3/25/2017 8:55:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sesion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[usuario_id] [int] NOT NULL,
	[token] [nvarchar](350) NOT NULL,
	[fecha_expiracion] [datetime] NOT NULL,
 CONSTRAINT [PK_sesion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tipo_unidad]    Script Date: 3/25/2017 8:55:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tipo_unidad](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_tipo_unidad] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tipo_usuario]    Script Date: 3/25/2017 8:55:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tipo_usuario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_tipo_usuario] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[usuario]    Script Date: 3/25/2017 8:55:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](150) NOT NULL,
	[rnc] [nvarchar](20) NOT NULL,
	[usuario] [nvarchar](50) NOT NULL,
	[contrasena] [nvarchar](350) NOT NULL,
	[cuenta_id] [int] NOT NULL,
	[tipo_usuario_id] [int] NOT NULL,
	[activo] [bit] NULL,
 CONSTRAINT [PK_usuario] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[venta]    Script Date: 3/25/2017 8:55:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[venta](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[intencion_compra_id] [int] NOT NULL,
	[fecha_creacion] [datetime] NOT NULL,
 CONSTRAINT [PK_venta] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_errores]    Script Date: 3/25/2017 8:55:54 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_errores] ON [dbo].[errores]
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [Unique_codigo]    Script Date: 3/25/2017 8:55:54 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [Unique_codigo] ON [dbo].[producto]
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [unique_usuario]    Script Date: 3/25/2017 8:55:54 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [unique_usuario] ON [dbo].[usuario]
(
	[usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cuenta] ADD  CONSTRAINT [DF_cuenta_activo]  DEFAULT ((1)) FOR [activo]
GO
ALTER TABLE [dbo].[demanda] ADD  CONSTRAINT [DF_demanda_activo]  DEFAULT ((1)) FOR [activo]
GO
ALTER TABLE [dbo].[intencion_compra] ADD  CONSTRAINT [DF_intencion_compra_fecha_creacion]  DEFAULT (getdate()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[intencion_compra] ADD  CONSTRAINT [DF_intencion_compra_activo]  DEFAULT ((1)) FOR [activo]
GO
ALTER TABLE [dbo].[oferta] ADD  CONSTRAINT [DF_oferta_fecha_creacion]  DEFAULT (getdate()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[oferta] ADD  CONSTRAINT [DF_oferta_activo]  DEFAULT ((1)) FOR [activo]
GO
ALTER TABLE [dbo].[usuario] ADD  CONSTRAINT [DF_usuario_activo]  DEFAULT ((1)) FOR [activo]
GO
ALTER TABLE [dbo].[venta] ADD  CONSTRAINT [DF_venta_fecha_creacion]  DEFAULT (getdate()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[demanda]  WITH CHECK ADD  CONSTRAINT [FK_demanda_producto] FOREIGN KEY([producto_id])
REFERENCES [dbo].[producto] ([id])
GO
ALTER TABLE [dbo].[demanda] CHECK CONSTRAINT [FK_demanda_producto]
GO
ALTER TABLE [dbo].[demanda]  WITH CHECK ADD  CONSTRAINT [FK_demanda_tipo_unidad] FOREIGN KEY([tipo_unidad_id])
REFERENCES [dbo].[tipo_unidad] ([id])
GO
ALTER TABLE [dbo].[demanda] CHECK CONSTRAINT [FK_demanda_tipo_unidad]
GO
ALTER TABLE [dbo].[demanda]  WITH CHECK ADD  CONSTRAINT [FK_demanda_usuario] FOREIGN KEY([usuario_id])
REFERENCES [dbo].[usuario] ([id])
GO
ALTER TABLE [dbo].[demanda] CHECK CONSTRAINT [FK_demanda_usuario]
GO
ALTER TABLE [dbo].[intencion_compra]  WITH CHECK ADD  CONSTRAINT [FK_intencion_compra_demanda] FOREIGN KEY([demanda_id])
REFERENCES [dbo].[demanda] ([id])
GO
ALTER TABLE [dbo].[intencion_compra] CHECK CONSTRAINT [FK_intencion_compra_demanda]
GO
ALTER TABLE [dbo].[intencion_compra]  WITH CHECK ADD  CONSTRAINT [FK_intencion_compra_oferta] FOREIGN KEY([oferta_id])
REFERENCES [dbo].[oferta] ([id])
GO
ALTER TABLE [dbo].[intencion_compra] CHECK CONSTRAINT [FK_intencion_compra_oferta]
GO
ALTER TABLE [dbo].[oferta]  WITH CHECK ADD  CONSTRAINT [FK_oferta_producto] FOREIGN KEY([producto_id])
REFERENCES [dbo].[producto] ([id])
GO
ALTER TABLE [dbo].[oferta] CHECK CONSTRAINT [FK_oferta_producto]
GO
ALTER TABLE [dbo].[oferta]  WITH CHECK ADD  CONSTRAINT [FK_oferta_tipo_unidad] FOREIGN KEY([tipo_unidad_id])
REFERENCES [dbo].[tipo_unidad] ([id])
GO
ALTER TABLE [dbo].[oferta] CHECK CONSTRAINT [FK_oferta_tipo_unidad]
GO
ALTER TABLE [dbo].[oferta]  WITH CHECK ADD  CONSTRAINT [FK_oferta_usuario] FOREIGN KEY([usuario_id])
REFERENCES [dbo].[usuario] ([id])
GO
ALTER TABLE [dbo].[oferta] CHECK CONSTRAINT [FK_oferta_usuario]
GO
ALTER TABLE [dbo].[sesion]  WITH CHECK ADD  CONSTRAINT [FK_sesion_usuario] FOREIGN KEY([usuario_id])
REFERENCES [dbo].[usuario] ([id])
GO
ALTER TABLE [dbo].[sesion] CHECK CONSTRAINT [FK_sesion_usuario]
GO
ALTER TABLE [dbo].[usuario]  WITH CHECK ADD  CONSTRAINT [FK_usuario_cuenta] FOREIGN KEY([cuenta_id])
REFERENCES [dbo].[cuenta] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[usuario] CHECK CONSTRAINT [FK_usuario_cuenta]
GO
ALTER TABLE [dbo].[usuario]  WITH CHECK ADD  CONSTRAINT [FK_usuario_tipo_usuario] FOREIGN KEY([tipo_usuario_id])
REFERENCES [dbo].[tipo_usuario] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[usuario] CHECK CONSTRAINT [FK_usuario_tipo_usuario]
GO
ALTER TABLE [dbo].[venta]  WITH CHECK ADD  CONSTRAINT [FK_venta_intencion_compra] FOREIGN KEY([intencion_compra_id])
REFERENCES [dbo].[intencion_compra] ([id])
GO
ALTER TABLE [dbo].[venta] CHECK CONSTRAINT [FK_venta_intencion_compra]
GO
USE [master]
GO
ALTER DATABASE [AgroMarketDB] SET  READ_WRITE 
GO
