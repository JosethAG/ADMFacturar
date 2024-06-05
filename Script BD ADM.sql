CREATE DATABASE [ADM]
USE [ADM]


----------------------------------------------------------------------------------------------------
								     	/*CREACION DE TABLAS*/
----------------------------------------------------------------------------------------------------


/****** Table [dbo].[TBL_FAC_MEDIO_TRANSPORTE]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_FAC_MEDIO_TRANSPORTE](
    [PK_Medio_Transporte] [bigint] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](100) NULL,
    [Estado] [bit] NULL,
    [FK_Usuario_Creacion] [varchar](50) NOT NULL,
    [FK_Usuario_Modificacion] [varchar](50) NOT NULL,
    [Fecha_Creacion] [datetime] NOT NULL,
    [Fecha_Modificacion] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
    [PK_Medio_Transporte] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Table [dbo].[TBL_INV_CONDICION_PAGO] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_INV_CONDICION_PAGO](
    [PK_Condicion_Pago] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](100) NOT NULL,
    [Dias] [int] NULL,
    [Estado] [char](1) NULL,
    [FK_Usuario_Creacion] [varchar](50) NOT NULL,
    [FK_Usuario_Modificacion] [varchar](50) NOT NULL,
    [Fecha_Creacion] [datetime] NULL,
    [Fecha_Modificacion] [datetime] NULL,
 CONSTRAINT [PK_MANT_CONDICION_PAGO] PRIMARY KEY CLUSTERED 
(
    [PK_Condicion_Pago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Table [dbo].[TBL_VENDEDORES]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_VENDEDORES](
	[PK_Vendedor] [varchar](100) NOT NULL,
	[Nombre] [varchar](200) NULL,
	[Telefono] [varchar](100) NULL,
	[Correo] [varchar](200) NULL,
	[Estado] [int] NULL,
PRIMARY KEY CLUSTERED 
(
    [PK_Vendedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/******  Table [dbo].[TBL_INV_CLIENTE]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_INV_CLIENTE](
    [PK_Cliente] [varchar](50) NOT NULL,
    [TipoIdentificacion] [varchar](500) NULL,
    [Identificacion] [int] NULL,
    [Nombre] [varchar](300) NOT NULL,
	[Telefono] [varchar](100) NULL,
	[Correo] [varchar](500) NULL,
	[Provincia] [varchar](500) NULL,
	[Canton] [varchar](500) NULL,
	[Distrito] [varchar](500) NULL,
    [OtrasSenas] [varchar](max) NULL,
    [Estado] [bit] NOT NULL,
    [FK_CondicionPago] [int] NULL,
    [FK_Transporte] [bigint] NULL,  
	[FK_Vendedor] [varchar](100) NULL,
    [FK_Usuario_Creacion] [varchar](500) NOT NULL,
    [FK_Usuario_Modificacion] [varchar](500) NOT NULL,
    [Fecha_Creacion] [datetime] NOT NULL,
    [Fecha_Modificacion] [datetime] NOT NULL,
 CONSTRAINT [PK_TBL_INV_CLIENTE_1] PRIMARY KEY CLUSTERED 
(
    [PK_Cliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [FK_TBL_INV_CLIENTE_VENDEDORES] FOREIGN KEY([FK_Vendedor])
    REFERENCES [dbo].[TBL_VENDEDORES] ([PK_Vendedor]),
 CONSTRAINT [FK_TBL_INV_CLIENTE_TRANSPORTE] FOREIGN KEY([FK_Transporte])
    REFERENCES [dbo].[TBL_FAC_MEDIO_TRANSPORTE] ([PK_Medio_Transporte]),
 CONSTRAINT [FK_TBL_INV_CLIENTE_CONDICION_PAGO] FOREIGN KEY([FK_CondicionPago])
    REFERENCES [dbo].[TBL_INV_CONDICION_PAGO] ([PK_Condicion_Pago])
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


----------------------------------------------------------------------------------------------------
									/*PROCEDIMIENTOS ALMACENADOS*/
----------------------------------------------------------------------------------------------------

-------------------------------------------------
					/*CLIENTES*/
-------------------------------------------------

CREATE PROCEDURE dbo.sp_ListarClientes
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        cli.PK_Cliente,
        cli.TipoIdentificacion,
        cli.Identificacion,
        cli.Nombre,
        cli.Telefono,
        cli.Correo,
        cli.Provincia,
        cli.Canton,
        cli.Distrito,
        cli.OtrasSenas,
        cli.Estado,
        cp.Descripcion AS CondicionPago,
        mt.Descripcion AS MedioTransporte,
        v.Nombre AS Vendedor,
        cli.FK_Usuario_Creacion,
        cli.FK_Usuario_Modificacion,
        cli.Fecha_Creacion,
        cli.Fecha_Modificacion
    FROM 
        dbo.TBL_INV_CLIENTE cli
    LEFT JOIN 
        dbo.TBL_INV_CONDICION_PAGO cp ON cli.FK_CondicionPago = cp.PK_Condicion_Pago
    LEFT JOIN 
        dbo.TBL_FAC_MEDIO_TRANSPORTE mt ON cli.FK_Transporte = mt.PK_Medio_Transporte
    LEFT JOIN 
        dbo.TBL_VENDEDORES v ON cli.FK_Vendedor = v.PK_Vendedor
    ORDER BY 
        cli.Nombre;
END
GO

---------------------------------------------------------------------------------------------