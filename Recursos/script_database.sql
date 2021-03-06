USE [master]
GO
/****** Object:  Database [AgroMaketDB]    Script Date: 4/7/2017 8:22:37 PM ******/
CREATE DATABASE [AgroMaketDB]
GO
ALTER DATABASE [AgroMaketDB] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AgroMaketDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AgroMaketDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AgroMaketDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AgroMaketDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AgroMaketDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AgroMaketDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [AgroMaketDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AgroMaketDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AgroMaketDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AgroMaketDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AgroMaketDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AgroMaketDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AgroMaketDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AgroMaketDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AgroMaketDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AgroMaketDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AgroMaketDB] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [AgroMaketDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AgroMaketDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [AgroMaketDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AgroMaketDB] SET  MULTI_USER 
GO
ALTER DATABASE [AgroMaketDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AgroMaketDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [AgroMaketDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO)
GO
USE [AgroMaketDB]
GO
/****** Object:  Table [dbo].[acceso_log]    Script Date: 4/7/2017 8:22:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[acceso_log](
	[usuario] [nvarchar](50) NOT NULL,
	[solicitud] [nvarchar](250) NOT NULL,
	[endpoint] [nvarchar](150) NULL,
	[fecha_creacion] [datetime] NOT NULL
)

GO
/****** Object:  Table [dbo].[cuenta]    Script Date: 4/7/2017 8:22:39 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[error_log]    Script Date: 4/7/2017 8:22:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[error_log](
	[mensaje] [nvarchar](250) NOT NULL,
	[excepcion] [text] NULL,
	[stacktrace] [text] NULL,
	[usuario] [nvarchar](50) NULL,
	[fecha_creacion] [datetime] NOT NULL
)

GO
/****** Object:  Table [dbo].[errores]    Script Date: 4/7/2017 8:22:41 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[firma]    Script Date: 4/7/2017 8:22:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[firma](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[vendedor] [int] NULL,
	[comprador] [int] NULL,
	[intencion_compra_id] [int] NOT NULL,
	[intencion_venta_id] [int] NOT NULL,
	[fecha_creacion] [datetime] NOT NULL,
	[fecha_final] [datetime] NULL,
 CONSTRAINT [PK_firma] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[intencion_compra]    Script Date: 4/7/2017 8:22:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[intencion_compra](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[usuario_id] [int] NOT NULL,
	[fecha_creacion] [datetime] NOT NULL,
	[fecha_expiracion] [datetime] NOT NULL,
	[activo] [bit] NOT NULL,
 CONSTRAINT [PK_intencion_compra] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[intencion_venta]    Script Date: 4/7/2017 8:22:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[intencion_venta](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[usuario_id] [int] NOT NULL,
	[intencion_compra_id] [int] NOT NULL,
	[fecha_creacion] [datetime] NOT NULL,
	[fecha_expiracion] [datetime] NOT NULL,
	[activo] [bit] NOT NULL,
 CONSTRAINT [PK_intencion_venta] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[oferta]    Script Date: 4/7/2017 8:22:45 PM ******/
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
	[usuario_id] [int] NOT NULL,
	[fecha_creacion] [datetime] NOT NULL,
	[activo] [bit] NOT NULL,
 CONSTRAINT [PK_oferta] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[producto]    Script Date: 4/7/2017 8:22:45 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[producto_intencion_compra]    Script Date: 4/7/2017 8:22:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[producto_intencion_compra](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[producto_id] [int] NOT NULL,
	[tipo_unidad_id] [int] NOT NULL,
	[id_intencion_compra] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
	[precio_unidad] [numeric](18, 2) NOT NULL,
 CONSTRAINT [PK_producto_intencion_compra] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[producto_intencion_venta]    Script Date: 4/7/2017 8:22:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[producto_intencion_venta](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[producto_id] [int] NOT NULL,
	[tipo_unidad_id] [int] NOT NULL,
	[id_intencion_venta] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
	[precio_unidad] [numeric](18, 2) NOT NULL,
 CONSTRAINT [PK_producto_intencion_venta] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[sesion]    Script Date: 4/7/2017 8:22:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sesion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[usuario_id] [int] NOT NULL,
	[token] [nvarchar](350) NOT NULL,
	[fecha_expiracion] [datetime] NOT NULL,
	[activo] [bit] NOT NULL,
 CONSTRAINT [PK_sesion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[tipo_unidad]    Script Date: 4/7/2017 8:22:47 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[tipo_usuario]    Script Date: 4/7/2017 8:22:48 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[usuario]    Script Date: 4/7/2017 8:22:48 PM ******/
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
	[activo] [bit] NOT NULL,
 CONSTRAINT [PK_usuario] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Venta]    Script Date: 4/7/2017 8:22:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venta](
	[VentaId] [int] IDENTITY(1,1) NOT NULL,
	[FirmaId] [int] NOT NULL,
	[IntencionVentaId] [int] NOT NULL,
	[IntencionCompraId] [int] NOT NULL,
 CONSTRAINT [PK_Venta] PRIMARY KEY CLUSTERED 
(
	[VentaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_errores]    Script Date: 4/7/2017 8:22:50 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_errores] ON [dbo].[errores]
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
/****** Object:  Index [IX_firma]    Script Date: 4/7/2017 8:22:50 PM ******/
CREATE NONCLUSTERED INDEX [IX_firma] ON [dbo].[firma]
(
	[vendedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
/****** Object:  Index [IX_firma_1]    Script Date: 4/7/2017 8:22:50 PM ******/
CREATE NONCLUSTERED INDEX [IX_firma_1] ON [dbo].[firma]
(
	[comprador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
/****** Object:  Index [IX_firma_2]    Script Date: 4/7/2017 8:22:50 PM ******/
CREATE NONCLUSTERED INDEX [IX_firma_2] ON [dbo].[firma]
(
	[comprador] ASC,
	[vendedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
/****** Object:  Index [IX_intencion_venta]    Script Date: 4/7/2017 8:22:50 PM ******/
CREATE NONCLUSTERED INDEX [IX_intencion_venta] ON [dbo].[intencion_venta]
(
	[usuario_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
/****** Object:  Index [IX_intencion_venta_1]    Script Date: 4/7/2017 8:22:50 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_intencion_venta_1] ON [dbo].[intencion_venta]
(
	[intencion_compra_id] ASC,
	[usuario_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [Unique_codigo]    Script Date: 4/7/2017 8:22:50 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [Unique_codigo] ON [dbo].[producto]
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
/****** Object:  Index [IX_producto_intencion_compra]    Script Date: 4/7/2017 8:22:50 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_producto_intencion_compra] ON [dbo].[producto_intencion_compra]
(
	[id_intencion_compra] ASC,
	[producto_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
/****** Object:  Index [IX_producto_intencion_venta]    Script Date: 4/7/2017 8:22:50 PM ******/
CREATE NONCLUSTERED INDEX [IX_producto_intencion_venta] ON [dbo].[producto_intencion_venta]
(
	[id_intencion_venta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
/****** Object:  Index [IX_producto_intencion_venta_1]    Script Date: 4/7/2017 8:22:50 PM ******/
CREATE NONCLUSTERED INDEX [IX_producto_intencion_venta_1] ON [dbo].[producto_intencion_venta]
(
	[producto_id] ASC,
	[id_intencion_venta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [unique_usuario]    Script Date: 4/7/2017 8:22:50 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [unique_usuario] ON [dbo].[usuario]
(
	[usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
ALTER TABLE [dbo].[acceso_log] ADD  CONSTRAINT [DF_acceso_log_fecha_creacion]  DEFAULT (getdate()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[cuenta] ADD  CONSTRAINT [DF_cuenta_activo]  DEFAULT ((1)) FOR [activo]
GO
ALTER TABLE [dbo].[error_log] ADD  CONSTRAINT [DF_error_log_fecha_creacion]  DEFAULT (getdate()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[firma] ADD  CONSTRAINT [DF_firma_fecha_creacion]  DEFAULT (getdate()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[intencion_compra] ADD  CONSTRAINT [DF_intencion_compra_fecha_creacion]  DEFAULT (getdate()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[intencion_compra] ADD  CONSTRAINT [DF_intencion_compra_activo]  DEFAULT ((1)) FOR [activo]
GO
ALTER TABLE [dbo].[intencion_venta] ADD  CONSTRAINT [DF_intencion_venta_fecha_creacion]  DEFAULT (getdate()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[intencion_venta] ADD  CONSTRAINT [DF_intencion_venta_activo]  DEFAULT ((1)) FOR [activo]
GO
ALTER TABLE [dbo].[oferta] ADD  CONSTRAINT [DF_oferta_fecha_creacion]  DEFAULT (getdate()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[oferta] ADD  CONSTRAINT [DF_oferta_activo]  DEFAULT ((1)) FOR [activo]
GO
ALTER TABLE [dbo].[sesion] ADD  CONSTRAINT [DF_sesion_activo]  DEFAULT ((1)) FOR [activo]
GO
ALTER TABLE [dbo].[usuario] ADD  CONSTRAINT [DF_usuario_activo]  DEFAULT ((1)) FOR [activo]
GO
ALTER TABLE [dbo].[firma]  WITH CHECK ADD  CONSTRAINT [FK_firma_intencion_compra] FOREIGN KEY([intencion_compra_id])
REFERENCES [dbo].[intencion_compra] ([id])
GO
ALTER TABLE [dbo].[firma] CHECK CONSTRAINT [FK_firma_intencion_compra]
GO
ALTER TABLE [dbo].[firma]  WITH CHECK ADD  CONSTRAINT [FK_firma_intencion_venta] FOREIGN KEY([intencion_venta_id])
REFERENCES [dbo].[intencion_venta] ([id])
GO
ALTER TABLE [dbo].[firma] CHECK CONSTRAINT [FK_firma_intencion_venta]
GO
ALTER TABLE [dbo].[firma]  WITH CHECK ADD  CONSTRAINT [FK_firma_usuario] FOREIGN KEY([vendedor])
REFERENCES [dbo].[usuario] ([id])
GO
ALTER TABLE [dbo].[firma] CHECK CONSTRAINT [FK_firma_usuario]
GO
ALTER TABLE [dbo].[firma]  WITH CHECK ADD  CONSTRAINT [FK_firma_usuario1] FOREIGN KEY([comprador])
REFERENCES [dbo].[usuario] ([id])
GO
ALTER TABLE [dbo].[firma] CHECK CONSTRAINT [FK_firma_usuario1]
GO
ALTER TABLE [dbo].[intencion_compra]  WITH CHECK ADD  CONSTRAINT [FK_intencion_compra_usuario] FOREIGN KEY([usuario_id])
REFERENCES [dbo].[usuario] ([id])
GO
ALTER TABLE [dbo].[intencion_compra] CHECK CONSTRAINT [FK_intencion_compra_usuario]
GO
ALTER TABLE [dbo].[intencion_venta]  WITH CHECK ADD  CONSTRAINT [FK_intencion_venta_intencion_compra] FOREIGN KEY([intencion_compra_id])
REFERENCES [dbo].[intencion_compra] ([id])
GO
ALTER TABLE [dbo].[intencion_venta] CHECK CONSTRAINT [FK_intencion_venta_intencion_compra]
GO
ALTER TABLE [dbo].[intencion_venta]  WITH CHECK ADD  CONSTRAINT [FK_intencion_venta_usuario] FOREIGN KEY([usuario_id])
REFERENCES [dbo].[usuario] ([id])
GO
ALTER TABLE [dbo].[intencion_venta] CHECK CONSTRAINT [FK_intencion_venta_usuario]
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
ALTER TABLE [dbo].[producto_intencion_compra]  WITH CHECK ADD  CONSTRAINT [FK_producto_intencion_compra_intencion_compra] FOREIGN KEY([id_intencion_compra])
REFERENCES [dbo].[intencion_compra] ([id])
GO
ALTER TABLE [dbo].[producto_intencion_compra] CHECK CONSTRAINT [FK_producto_intencion_compra_intencion_compra]
GO
ALTER TABLE [dbo].[producto_intencion_compra]  WITH CHECK ADD  CONSTRAINT [FK_producto_intencion_compra_producto] FOREIGN KEY([producto_id])
REFERENCES [dbo].[producto] ([id])
GO
ALTER TABLE [dbo].[producto_intencion_compra] CHECK CONSTRAINT [FK_producto_intencion_compra_producto]
GO
ALTER TABLE [dbo].[producto_intencion_compra]  WITH CHECK ADD  CONSTRAINT [FK_producto_intencion_compra_tipo_unidad] FOREIGN KEY([tipo_unidad_id])
REFERENCES [dbo].[tipo_unidad] ([id])
GO
ALTER TABLE [dbo].[producto_intencion_compra] CHECK CONSTRAINT [FK_producto_intencion_compra_tipo_unidad]
GO
ALTER TABLE [dbo].[producto_intencion_venta]  WITH CHECK ADD  CONSTRAINT [FK_producto_intencion_venta_intencion_venta] FOREIGN KEY([id_intencion_venta])
REFERENCES [dbo].[intencion_venta] ([id])
GO
ALTER TABLE [dbo].[producto_intencion_venta] CHECK CONSTRAINT [FK_producto_intencion_venta_intencion_venta]
GO
ALTER TABLE [dbo].[producto_intencion_venta]  WITH CHECK ADD  CONSTRAINT [FK_producto_intencion_venta_producto] FOREIGN KEY([producto_id])
REFERENCES [dbo].[producto] ([id])
GO
ALTER TABLE [dbo].[producto_intencion_venta] CHECK CONSTRAINT [FK_producto_intencion_venta_producto]
GO
ALTER TABLE [dbo].[producto_intencion_venta]  WITH CHECK ADD  CONSTRAINT [FK_producto_intencion_venta_tipo_unidad] FOREIGN KEY([tipo_unidad_id])
REFERENCES [dbo].[tipo_unidad] ([id])
GO
ALTER TABLE [dbo].[producto_intencion_venta] CHECK CONSTRAINT [FK_producto_intencion_venta_tipo_unidad]
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
ALTER TABLE [dbo].[Venta]  WITH CHECK ADD  CONSTRAINT [FK_Venta_firma] FOREIGN KEY([FirmaId])
REFERENCES [dbo].[firma] ([id])
GO
ALTER TABLE [dbo].[Venta] CHECK CONSTRAINT [FK_Venta_firma]
GO
ALTER TABLE [dbo].[Venta]  WITH CHECK ADD  CONSTRAINT [FK_Venta_intencion_compra] FOREIGN KEY([IntencionCompraId])
REFERENCES [dbo].[intencion_compra] ([id])
GO
ALTER TABLE [dbo].[Venta] CHECK CONSTRAINT [FK_Venta_intencion_compra]
GO
ALTER TABLE [dbo].[Venta]  WITH CHECK ADD  CONSTRAINT [FK_Venta_intencion_venta] FOREIGN KEY([IntencionVentaId])
REFERENCES [dbo].[intencion_venta] ([id])
GO
ALTER TABLE [dbo].[Venta] CHECK CONSTRAINT [FK_Venta_intencion_venta]
GO
USE [master]
GO
ALTER DATABASE [AgroMaketDB] SET  READ_WRITE 
GO
