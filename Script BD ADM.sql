create database ADM;
use ADM;


----------------------------------------------------------------------------------------------------
								     	/*CREACION DE TABLAS*/
----------------------------------------------------------------------------------------------------

/****** Object:  Table [dbo].[TBL_PROVINCIA]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_PROVINCIA](
	[ID_PROVINCIA] [int] NULL,
	[NOMBRE] [varchar](50) NULL
) ON [PRIMARY]
GO

-- Creación de la tabla TBL_USUARIO con el correo único
CREATE TABLE TBL_Usuario (
    PK_IdUsuario BIGINT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Correo VARCHAR(200) NOT NULL UNIQUE,
    Contra VARCHAR(200) NOT NULL,
    Rol VARCHAR(100) NOT NULL,
    Estado BIT NOT NULL,
    FK_Usuario_Creacion VARCHAR(50) NOT NULL,
    FK_Usuario_Modificacion VARCHAR(50) NOT NULL,
    Fecha_Creacion DATETIME NOT NULL,
    Fecha_Modificacion DATETIME NOT NULL
);

/****** Object:  Table [dbo].[TBL_CANTON]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_CANTON](
	[ID_PROVINCIA] [int] NULL,
	[ID_CANTON] [int] NULL,
	[NOMBRE] [varchar](50) NULL
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[TBL_DISTRITO]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_DISTRITO](
	[ID_PROVINCIA] [int] NULL,
	[ID_CANTON] [int] NULL,
	[ID_DISTRITO] [int] NULL,
	[NOMBRE] [varchar](50) NULL
) ON [PRIMARY]
GO


/****** Table [dbo].[TBL_TRANSPORTES]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_TRANSPORTES](
    [PK_Medio_Transporte] [bigint] IDENTITY(1,1) NOT NULL,
    [Descripcion] [varchar](100) NULL,
    [Estado] [bit],
    [FK_Usuario_Creacion] [varchar](50) NOT NULL,
    [FK_Usuario_Modificacion] [varchar](50)  NULL,
    [Fecha_Creacion] [datetime]  NOT NULL,
    [Fecha_Modificacion] [datetime]  NULL,
PRIMARY KEY CLUSTERED 
(
    [PK_Medio_Transporte] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Table [dbo].[TBL_CONDICIONES_PAGO] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_CONDICIONES_PAGO](
    [PK_Condicion_Pago] [int] IDENTITY(1,1) NOT NULL,
    [Descripcion] [varchar](100) NOT NULL,
    [Dias] [int] NULL,
    [Estado] [bit] NULL,
    [FK_Usuario_Creacion] [varchar](50) NOT NULL,
    [FK_Usuario_Modificacion] [varchar](50)  NULL,
    [Fecha_Creacion] [datetime] NOT NULL,
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
	[PK_Vendedor]  [bigint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](200) NULL,
	[Telefono] [varchar](100) NULL,
	[Correo] [varchar](200) NULL,
	[Estado] [bit],
	[FK_Usuario_Creacion] [varchar](50) NOT NULL,
	[FK_Usuario_Modificacion] [varchar](50)  NULL,
	[Fecha_Creacion] [datetime] NOT NULL,
	[Fecha_Modificacion] [datetime]  NULL,
PRIMARY KEY CLUSTERED 
(
    [PK_Vendedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/******  Table [dbo].[TBL_CLIENTES]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_CLIENTES](
    [PK_Cliente] [varchar](50) NOT NULL,
    [TipoIdentificacion] [varchar](10) NULL,
    [Identificacion] [int] NULL,
    [Nombre] [varchar](300) NOT NULL,
    [Telefono] [varchar](100) NULL,
    [Correo] [varchar](300) NULL,
    [Provincia] [varchar](10) NULL,
    [Canton] [varchar](10) NULL,
    [Distrito] [varchar](10) NULL,
    [OtrasSenas] [varchar](max) NULL,
    [Estado] [bit] NOT NULL,
    [FK_CondicionPago] [int] NULL,
    [FK_Transporte] [bigint] NULL,  
    [FK_Vendedor] [bigint] NULL,
    [FK_Usuario_Creacion] [varchar](100) NOT NULL,
    [FK_Usuario_Modificacion] [varchar](100)  NULL,
    [Fecha_Creacion] [datetime] NOT NULL,
    [Fecha_Modificacion] [datetime]  NULL,
 CONSTRAINT [PK_TBL_CLIENTES_1] PRIMARY KEY CLUSTERED 
(
    [PK_Cliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [FK_TBL_CLIENTES_VENDEDORES] FOREIGN KEY([FK_Vendedor])
    REFERENCES [dbo].[TBL_VENDEDORES] ([PK_Vendedor]),
 CONSTRAINT [FK_TBL_CLIENTES_TRANSPORTE] FOREIGN KEY([FK_Transporte])
    REFERENCES [dbo].[TBL_TRANSPORTES] ([PK_Medio_Transporte]),
 CONSTRAINT [FK_TBL_CLIENTES_CONDICION_PAGO] FOREIGN KEY([FK_CondicionPago])
    REFERENCES [dbo].[TBL_CONDICIONES_PAGO] ([PK_Condicion_Pago])
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


/****** Table [dbo].[TBL_PROVEEDORES]  ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TBL_PROVEEDORES](
	[PK_Proveedor] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Telefono] [varchar](100) NOT NULL,
	[Correo] [varchar](200) NOT NULL,
	[Direccion] [varchar](300) NOT NULL,
	[Estado] [bit] NOT NULL,
	[FK_Usuario_Creacion] [varchar](50) NOT NULL,
	[FK_Usuario_Modificacion] [varchar](50)  NULL,
	[Fecha_Creacion] [datetime] NOT NULL,
	[Fecha_Modificacion] [datetime]  NULL,
 CONSTRAINT [PK_TBL_COM_PROVEEDOR] PRIMARY KEY CLUSTERED 
(
	[PK_Proveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
	
GO

/****** Table [dbo].[TBL_ARTICULOS]  ******/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TBL_ARTICULO](
	[PK_Articulo] [varchar](50) NOT NULL,
	[Descripcion] [varchar](500) NOT NULL,
	[Codigo_Barras] [varchar](50) NULL,
	[FK_Proveedor] [varchar](50) NULL,
	[Cantidad] [int] NOT NULL,
	[Costo] [decimal](18, 2) NOT NULL,
	[Precio] [decimal](18, 2) NOT NULL,
	[Estado] [bit] NOT NULL,
	[FK_Usuario_Creacion] [varchar](100) NOT NULL,
	[FK_Usuario_Modificacion] [varchar](100) NULL,
	[Fecha_Creacion] [datetime] NOT NULL,
	[Fecha_Modificacion] [datetime] NULL,
 CONSTRAINT [PK_TBL_INV_ARTICULO] PRIMARY KEY CLUSTERED 
(
	[PK_Articulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [dbo].[TBL_DOCUMENTO_CP]    Script Date: 6/22/2024 9:56:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_DOCUMENTO_CP](
	[PK_Documento] [varchar](50) NOT NULL,
	[FK_Proveedor] [varchar](50) NOT NULL,
	[Fecha_Documento] [datetime] NOT NULL,
	[Fecha_Vence] [datetime] NOT NULL,
	[Observaciones] [varchar](300)  NULL,
	[Monto] [decimal](18, 2) NOT NULL,
	[Saldo_Pendiente] [decimal](18, 2) NOT NULL,
	[Anulado] [varchar](1) NOT NULL,
	[FK_Usuario_Creacion] [varchar](50) NOT NULL,
	[FK_Usuario_Modificacion] [varchar](50) NOT NULL,
	[Fecha_Creacion] [datetime] NOT NULL,
	[Fecha_Modificacion] [datetime] NOT NULL,
 CONSTRAINT [PK_TBL_DOCUMENTO_CP_1] PRIMARY KEY CLUSTERED 
(
	[PK_Documento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_INGRESO_MERCADERIA]    Script Date: 6/22/2024 9:56:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_INGRESO_MERCADERIA](
	[PK_FK_Documento] [varchar](50) NOT NULL,
	[PK_FK_Proveedor] INT NOT NULL,
	[FK_CondicionPago] INT NOT NULL,
	[Fecha_Compra] [datetime] NOT NULL,
	[PK_FK_Articulo] [varchar](50) NOT NULL,
	[Cantidad] INT NOT NULL,
	[Costo] [decimal](18, 2) NOT NULL,
	[Subtotal] [decimal](18, 2) NULL,
	[Linea] [int] IDENTITY(1,1) NOT NULL,	
	[Estado] [varchar](20) NOT NULL,
	[FK_Usuario_Creacion] [varchar](50) NOT NULL,
	[FK_Usuario_Modificacion] [varchar](50) NOT NULL,
	[Fecha_Creacion] [datetime] NOT NULL,
	[Fecha_Modificacion] [datetime] NOT NULL,
 CONSTRAINT [PK_TBL_INGRESO_MERCADERIA_1] PRIMARY KEY CLUSTERED 
(
	[PK_FK_Documento] ASC,
	[PK_FK_Articulo] ASC,
	[PK_FK_Proveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TBL_DOCUMENTO_CP] ADD  CONSTRAINT [DF_TBL_DOCUMENTO_CP_Fecha_Creacion]  DEFAULT (getdate()) FOR [Fecha_Creacion]
GO
ALTER TABLE [dbo].[TBL_DOCUMENTO_CP] ADD  CONSTRAINT [DF_TBL_DOCUMENTO_CP_Fecha_Modificacion]  DEFAULT (getdate()) FOR [Fecha_Modificacion]
GO
ALTER TABLE [dbo].[TBL_INGRESO_MERCADERIA] ADD  CONSTRAINT [DF_TBL_INGRESO_MERCADERIA_Fecha_Creacion]  DEFAULT (getdate()) FOR [Fecha_Creacion]
GO
ALTER TABLE [dbo].[TBL_INGRESO_MERCADERIA] ADD  CONSTRAINT [DF_TBL_INGRESO_MERCADERIA_Fecha_Modificacion]  DEFAULT (getdate()) FOR [Fecha_Modificacion]
GO
ALTER TABLE [dbo].[TBL_INGRESO_MERCADERIA]  WITH NOCHECK ADD  CONSTRAINT [FK_TBL_INGRESO_MERCADERIA_TBL_INV_ARTICULO] FOREIGN KEY([PK_FK_Articulo])
REFERENCES [dbo].[TBL_ARTICULO] ([PK_Articulo])
GO
ALTER TABLE [dbo].[TBL_INGRESO_MERCADERIA] CHECK CONSTRAINT [FK_TBL_INGRESO_MERCADERIA_TBL_INV_ARTICULO]
GO
ALTER TABLE [dbo].[TBL_INGRESO_MERCADERIA]  WITH NOCHECK ADD  CONSTRAINT [FK_TBL_INGRESO_MERCADERIA_TBL_CONDICIONES_PAGO] FOREIGN KEY([FK_CondicionPago])
REFERENCES [dbo].[TBL_CONDICIONES_PAGO] ([PK_Condicion_Pago])
GO
ALTER TABLE [dbo].[TBL_INGRESO_MERCADERIA] CHECK CONSTRAINT [FK_TBL_INGRESO_MERCADERIA_TBL_CONDICIONES_PAGO]
GO


/****** Object:  Table [dbo].[TBL_SALIDA_MERCADERIA]    Script Date: 6/27/2024 7:36:35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TBL_SALIDA_MERCADERIA](
	[PK_FK_Documento] [varchar](50) NOT NULL,
	[Fecha_Salida] [datetime] NOT NULL,
	[Tipo_Salida] [varchar](50) NOT NULL,
	[Observaciones] [varchar](50)  NULL,
	[PK_FK_Articulo] [varchar](50) NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Linea] [int] IDENTITY(1,1) NOT NULL,
	[Estado] [varchar](20) NOT NULL,
	[FK_Usuario_Creacion] [varchar](50) NOT NULL,
	[FK_Usuario_Modificacion] [varchar](50) NOT NULL,
	[Fecha_Creacion] [datetime] NOT NULL,
	[Fecha_Modificacion] [datetime] NOT NULL,
 CONSTRAINT [PK_TBL_SALIDA_MERCADERIA_1] PRIMARY KEY CLUSTERED 
(
	[PK_FK_Documento] ASC,
	[PK_FK_Articulo] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[TBL_SALIDA_MERCADERIA] ADD  CONSTRAINT [DF_TBL_SALIDA_MERCADERIA_Fecha_Creacion]  DEFAULT (getdate()) FOR [Fecha_Creacion]
GO

ALTER TABLE [dbo].[TBL_SALIDA_MERCADERIA] ADD  CONSTRAINT [DF_TBL_SALIDA_MERCADERIA_Fecha_Modificacion]  DEFAULT (getdate()) FOR [Fecha_Modificacion]
GO

ALTER TABLE [dbo].[TBL_SALIDA_MERCADERIA]  WITH NOCHECK ADD  CONSTRAINT [FK_TBL_SALIDA_MERCADERIA_TBL_INV_ARTICULO] FOREIGN KEY([PK_FK_Articulo])
REFERENCES [dbo].[TBL_ARTICULO] ([PK_Articulo])
GO

ALTER TABLE [dbo].[TBL_SALIDA_MERCADERIA] CHECK CONSTRAINT [FK_TBL_SALIDA_MERCADERIA_TBL_INV_ARTICULO]
GO

----------------------------------------------------------------------------------------------------
									/*PROCEDIMIENTOS ALMACENADOS*/
----------------------------------------------------------------------------------------------------

-------------------------------------------------
					/*Usuario*/
-------------------------------------------------

CREATE PROCEDURE sp_ValidarCredenciales
    @Correo VARCHAR(200),
    @Contra VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    -- Declarar una variable para almacenar el ID del usuario encontrado
    DECLARE @IdUsuarioEncontrado BIGINT;

    -- Buscar el usuario que coincida con el correo y la contraseña proporcionados
    SELECT @IdUsuarioEncontrado = PK_IdUsuario
    FROM TBL_USUARIO
    WHERE Correo = @Correo AND Contra = @Contra;

    -- Si se encontró el usuario, devolver sus datos
    IF @IdUsuarioEncontrado IS NOT NULL
    BEGIN
        SELECT PK_IdUsuario,
               Nombre,
               Correo,
               Contra,
               Rol,
               Estado,
               FK_Usuario_Creacion,
               FK_Usuario_Modificacion,
               Fecha_Creacion,
               Fecha_Modificacion
        FROM TBL_USUARIO
        WHERE PK_IdUsuario = @IdUsuarioEncontrado;
    END
END

	
/****** Object:  StoredProcedure [dbo].[sp_ListarUsuario]     ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ListarUsuario]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        PK_IdUsuario,
        Nombre,
        Correo,
        Contra,
        Rol,
        Estado,
        FK_Usuario_Creacion,
        FK_Usuario_Modificacion,
        Fecha_Creacion,
        Fecha_Modificacion
    FROM 
        dbo.TBL_USUARIO
    ORDER BY 
        PK_IdUsuario;
END;
GO

/****** Object:  StoredProcedure [dbo].[sp_ListarUsuarioxUsuario]     ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ListarUsuarioxUsuario]
    @Nombre NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        PK_IdUsuario,
        Nombre,
        Correo,
        Contra,
        Rol,
        Estado,
        FK_Usuario_Creacion,
        FK_Usuario_Modificacion,
        Fecha_Creacion,
        Fecha_Modificacion
    FROM 
        dbo.TBL_USUARIO 
    WHERE 
        Nombre LIKE '%' + @Nombre + '%'
    ORDER BY 
        Nombre;
END;
GO


/****** Object:  StoredProcedure [dbo].[sp_InsertarUsuario]     ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_InsertarUsuario]
    @P_PK_IdUsuario BIGINT,
    @P_Nombre VARCHAR(100),
    @P_Correo VARCHAR(200),
    @P_Contra VARCHAR(200),
    @P_Rol INT,
    @P_Estado BIT,
    @P_FK_Usuario_Creacion VARCHAR(50),
    @P_FK_Usuario_Modificacion VARCHAR(50),
    @P_Fecha_Creacion DATETIME,
    @P_Fecha_Modificacion DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN [sp_InsertarUsuario]
    BEGIN TRY
        IF EXISTS
        (
            SELECT 1
            FROM dbo.TBL_USUARIO WITH (NOLOCK)
            WHERE PK_IdUsuario = @P_PK_IdUsuario
        )
        BEGIN
            UPDATE dbo.TBL_USUARIO
            SET Nombre = @P_Nombre,
                Correo = @P_Correo,
                Contra = @P_Contra,
                Rol = @P_Rol,
                Estado = @P_Estado,
                FK_Usuario_Modificacion = @P_FK_Usuario_Modificacion,
                Fecha_Modificacion = @P_Fecha_Modificacion
            WHERE PK_IdUsuario = @P_PK_IdUsuario;
        END
        ELSE
        BEGIN
            INSERT INTO [dbo].[TBL_USUARIO]
            (             
                Nombre,
                Correo,
                Contra,
                Rol,
                Estado,
                FK_Usuario_Creacion,
                FK_Usuario_Modificacion,
                Fecha_Creacion,
                Fecha_Modificacion
            )
            VALUES
            (
                @P_Nombre,
                @P_Correo,
                @P_Contra,
                @P_Rol,
                @P_Estado,
                @P_FK_Usuario_Creacion,
                @P_FK_Usuario_Modificacion,
                @P_Fecha_Creacion,
                @P_Fecha_Modificacion
            );
        END;

        COMMIT TRANSACTION
        RETURN 1
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        RETURN 0
    END CATCH
END;
GO


/****** Object:  StoredProcedure [dbo].[sp_ModificarUsuario]     ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ModificarUsuario]
    @P_PK_IdUsuario BIGINT,
    @P_Nombre VARCHAR(100),
    @P_Correo VARCHAR(200),
    @P_Contra VARCHAR(200),
    @P_Rol INT,
    @P_Estado BIT,
    @P_FK_Usuario_Creacion VARCHAR(50),
    @P_FK_Usuario_Modificacion VARCHAR(50),
	@P_Fecha_Creacion DATETIME,
    @P_Fecha_Modificacion DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN [sp_ModificarUsuario]
    BEGIN TRY
        UPDATE dbo.TBL_USUARIO
            SET Nombre = @P_Nombre,
                Correo = @P_Correo,
                Contra = @P_Contra,
                Rol = @P_Rol,
                Estado = @P_Estado,
                FK_Usuario_Modificacion = @P_FK_Usuario_Modificacion,
                Fecha_Modificacion = @P_Fecha_Modificacion
            WHERE PK_IdUsuario = @P_PK_IdUsuario;

        COMMIT TRANSACTION
        RETURN 1
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        RETURN 0
    END CATCH
END;
GO


/****** Object:  StoredProcedure [dbo].[sp_EliminarUsuario]     ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_EliminarUsuario]
    @P_PK_IdUsuario BIGINT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN [sp_EliminarUsuario]
    BEGIN TRY
        UPDATE dbo.TBL_USUARIO SET Estado = 0 WHERE PK_IdUsuario = @P_PK_IdUsuario;

        COMMIT TRANSACTION
        RETURN 1
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        RETURN 0
    END CATCH
END;
GO


/****** Object:  StoredProcedure [dbo].[sp_ObtenerUsuario]     ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ObtenerUsuario]
    @PK_IdUsuario BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        PK_IdUsuario,
        Nombre,
        Correo,
        Contra,
        Rol,
        Estado,
        FK_Usuario_Creacion,
        FK_Usuario_Modificacion,
        Fecha_Creacion,
        Fecha_Modificacion
    FROM 
        dbo.TBL_USUARIO 
    WHERE 
        PK_IdUsuario = @PK_IdUsuario
END;
GO



-------------------------------------------------
					/*ARTICULOS*/
-------------------------------------------------

/****** Object:  StoredProcedure [dbo].[sp_ListarArticulosVM]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ListarArticulosVM] 
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        a.PK_Articulo,
        a.Descripcion,
        a.Codigo_Barras,
        p.Nombre AS FK_Proveedor,
        a.Cantidad,
        CAST(a.Costo AS FLOAT) AS Costo,
        CAST(a.Precio AS FLOAT) AS Precio,
        a.Estado
    FROM 
        dbo.TBL_ARTICULO a
    LEFT JOIN 
        dbo.TBL_PROVEEDORES p ON a.FK_Proveedor = CAST(p.PK_Proveedor AS VARCHAR)
    ORDER BY 
        a.PK_Articulo;
END
GO


/****** Object:  StoredProcedure [dbo].[sp_ListarArticulos]    Script Date: 6/11/2024 8:56:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ListarArticulos]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        PK_Articulo,
        Descripcion,
        Codigo_Barras,
        FK_Proveedor,
        Cantidad,
        Costo,
        Precio,
        Estado,
        FK_Usuario_Creacion,
        FK_Usuario_Modificacion,
        Fecha_Creacion,
        Fecha_Modificacion
    FROM 
        dbo.TBL_ARTICULO
    ORDER BY 
        PK_Articulo;
END;
GO

/****** Object:  StoredProcedure [dbo].[sp_ListarArticulosxDescripcion]    Script Date: 6/11/2024 8:56:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ListarArticulosxNombre]
    @Descripcion NVARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        PK_Articulo,
        Descripcion,
        Codigo_Barras,
        FK_Proveedor,
        Cantidad,
        Costo,
        Precio,
        Estado,
        FK_Usuario_Creacion,
        FK_Usuario_Modificacion,
        Fecha_Creacion,
        Fecha_Modificacion
    FROM 
        dbo.TBL_ARTICULO 
    WHERE 
        Descripcion LIKE '%' + @Descripcion + '%'
    ORDER BY 
        PK_Articulo;
END;
GO


/****** Object:  StoredProcedure [dbo].[sp_InsertarArticulos]    Script Date: 6/11/2024 8:56:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_InsertarArticulos]
    @P_PK_Articulo VARCHAR(50),
    @P_Descripcion VARCHAR(500),
    @P_Codigo_Barras VARCHAR(50),
    @P_FK_Proveedor VARCHAR(50),
    @P_Cantidad INT,
    @P_Costo DECIMAL(18, 2),
    @P_Precio DECIMAL(18, 2),
    @P_Estado BIT,
    @P_FK_Usuario_Creacion VARCHAR(100),
    @P_FK_Usuario_Modificacion VARCHAR(100),
    @P_Fecha_Creacion DATETIME,
    @P_Fecha_Modificacion DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN [sp_InsertarArticulo]
    BEGIN TRY
        INSERT INTO [dbo].[TBL_ARTICULO]
        (
            PK_Articulo,
            Descripcion,
            Codigo_Barras,
            FK_Proveedor,
            Cantidad,
            Costo,
            Precio,
            Estado,
            FK_Usuario_Creacion,
            FK_Usuario_Modificacion,
            Fecha_Creacion,
            Fecha_Modificacion
        )
        VALUES
        (
            @P_PK_Articulo,
            @P_Descripcion,
            @P_Codigo_Barras,
            @P_FK_Proveedor,
            @P_Cantidad,
            @P_Costo,
            @P_Precio,
            @P_Estado,
            @P_FK_Usuario_Creacion,
            @P_FK_Usuario_Modificacion,
            GETDATE(),
            GETDATE()
        );

        COMMIT TRANSACTION
        RETURN 1
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        RETURN 0
    END CATCH
END;
GO

/****** Object:  StoredProcedure [dbo].[sp_ModificarArticulos]    Script Date: 6/18/2024 12:10:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ModificarArticulos]
    @P_PK_Articulo VARCHAR(50),
    @P_Descripcion VARCHAR(500),
    @P_Codigo_Barras VARCHAR(50),
    @P_FK_Proveedor VARCHAR(50),
    @P_Cantidad INT,
    @P_Costo DECIMAL(18, 2),
    @P_Precio DECIMAL(18, 2),
    @P_Estado BIT,
    @P_FK_Usuario_Modificacion VARCHAR(100),
    @P_Fecha_Modificacion DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN [sp_ModificarArticulo]
    BEGIN TRY
        UPDATE [dbo].[TBL_ARTICULO]
        SET
            Descripcion = @P_Descripcion,
            Codigo_Barras = @P_Codigo_Barras,
            FK_Proveedor = @P_FK_Proveedor,
            Cantidad = @P_Cantidad,
            Costo = @P_Costo,
            Precio = @P_Precio,
            Estado = @P_Estado,
            FK_Usuario_Modificacion = @P_FK_Usuario_Modificacion,
            Fecha_Modificacion = GETDATE()
        WHERE
            PK_Articulo = @P_PK_Articulo;

        COMMIT TRANSACTION
        RETURN 1
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        RETURN 0
    END CATCH
END;

GO

/****** Object:  StoredProcedure [dbo].[sp_EliminarArticulos]    Script Date: 6/11/2024 8:56:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_EliminarArticulos]
    @P_PK_Articulo VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN [sp_EliminarArticulos]
    BEGIN TRY
        -- Update the Estado to 0 for the specified article
        UPDATE dbo.TBL_ARTICULO
        SET Estado = 0
        WHERE PK_Articulo = @P_PK_Articulo;

        COMMIT TRANSACTION
        RETURN 1
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        RETURN 0
    END CATCH
END;
GO

CREATE PROCEDURE [dbo].[sp_ObtenerArticulo]
    @PK_Articulo varchar(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        PK_Articulo,
        Descripcion,
        Codigo_Barras,
        FK_Proveedor,
        Cantidad,
        Costo,
        Precio,
        Estado,
        FK_Usuario_Creacion,
        FK_Usuario_Modificacion,
        Fecha_Creacion,
        Fecha_Modificacion
    FROM 
        dbo.TBL_ARTICULO 
    WHERE 
        PK_Articulo = @PK_Articulo
END;

GO


-------------------------------------------------
					/*CLIENTES*/
-------------------------------------------------
GO
	
CREATE PROCEDURE dbo.sp_ListarClientes
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        PK_Cliente,
        TipoIdentificacion,
        Identificacion,
        Nombre,
        Telefono,
        Correo,
        Provincia,
        Canton,
        Distrito,
        OtrasSenas,
        Estado,
        FK_CondicionPago,
        FK_Transporte,
        FK_Vendedor,
        FK_Usuario_Creacion,
        FK_Usuario_Modificacion,
        Fecha_Creacion,
        Fecha_Modificacion
    FROM 
        dbo.TBL_CLIENTES
    ORDER BY 
        Nombre;
END

GO

CREATE PROCEDURE dbo.sp_ListarClientesxNombre
    @Nombre NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        PK_Cliente,
        TipoIdentificacion,
        Identificacion,
        Nombre,
        Telefono,
        Correo,
        Provincia,
        Canton,
        Distrito,
        OtrasSenas,
        Estado,
        FK_CondicionPago,
        FK_Transporte,
        FK_Vendedor,
        FK_Usuario_Creacion,
        FK_Usuario_Modificacion,
        Fecha_Creacion,
        Fecha_Modificacion
    FROM 
        dbo.TBL_CLIENTES 
    WHERE 
        Nombre LIKE '%' + @Nombre + '%'
    ORDER BY 
        Nombre;
END;

GO
/** Object:  StoredProcedure [dbo].[sp_ListarClientesVM]   Script Date: 6/11/2024 9:20:20 PM **/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ListarClientesVM] 
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        c.PK_Cliente,
        c.TipoIdentificacion,
        c.Identificacion,
        c.Nombre,
        c.Telefono,
        c.Correo,
        CONCAT(p.NOMBRE, ', ', ca.NOMBRE, ', ', d.NOMBRE, ', ', c.OtrasSenas) AS Direccion,
        c.Estado,
        cp.Descripcion AS CondicionPago,
        t.Descripcion AS Transporte,
        v.Nombre AS Vendedor
    FROM 
        dbo.TBL_CLIENTES c
    LEFT JOIN 
        dbo.TBL_PROVINCIA p ON c.Provincia = CAST(p.ID_PROVINCIA AS VARCHAR)
    LEFT JOIN 
        dbo.TBL_CANTON ca ON c.Provincia = CAST(ca.ID_PROVINCIA AS VARCHAR) AND c.Canton = CAST(ca.ID_CANTON AS VARCHAR)
    LEFT JOIN 
        dbo.TBL_DISTRITO d ON c.Provincia = CAST(d.ID_PROVINCIA AS VARCHAR) AND c.Canton = CAST(d.ID_CANTON AS VARCHAR) AND c.Distrito = CAST(d.ID_DISTRITO AS VARCHAR)
    LEFT JOIN 
        dbo.TBL_CONDICIONES_PAGO cp ON c.FK_CondicionPago = cp.PK_Condicion_Pago
    LEFT JOIN 
        dbo.TBL_TRANSPORTES t ON c.FK_Transporte = t.PK_Medio_Transporte
    LEFT JOIN 
        dbo.TBL_VENDEDORES v ON c.FK_Vendedor = v.PK_Vendedor
    ORDER BY 
        c.Nombre;
END

GO

CREATE PROCEDURE dbo.sp_InsertarClientes
    @P_PK_Cliente VARCHAR(50),
    @P_TipoIdentificacion VARCHAR(500),
    @P_Identificacion INT,
    @P_Nombre VARCHAR(300),
    @P_Telefono VARCHAR(100),
    @P_Correo VARCHAR(500),
    @P_Provincia VARCHAR(500),
    @P_Canton VARCHAR(500),
    @P_Distrito VARCHAR(500),
    @P_OtrasSenas VARCHAR(MAX),
    @P_Estado BIT,
    @P_FK_CondicionPago INT,
    @P_FK_Transporte BIGINT,
    @P_FK_Vendedor BIGINT,
    @P_FK_Usuario_Creacion VARCHAR(500),
    @P_FK_Usuario_Modificacion VARCHAR(500),
    @P_Fecha_Creacion DATETIME,
    @P_Fecha_Modificacion DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN [sp_InsertarClientes]
    BEGIN TRY
           BEGIN
            INSERT INTO [dbo].[TBL_CLIENTES]
            (
                PK_Cliente,
                TipoIdentificacion,
                Identificacion,
                Nombre,
                Telefono,
                Correo,
                Provincia,
                Canton,
                Distrito,
                OtrasSenas,
                Estado,
                FK_CondicionPago,
                FK_Transporte,
                FK_Vendedor,
                FK_Usuario_Creacion,
				FK_Usuario_Modificacion,
                Fecha_Creacion,
				Fecha_Modificacion
            )
            VALUES
            (
                @P_PK_Cliente,
                @P_TipoIdentificacion,
                @P_Identificacion,
                @P_Nombre,
                @P_Telefono,
                @P_Correo,
                @P_Provincia,
                @P_Canton,
                @P_Distrito,
                @P_OtrasSenas,
                @P_Estado,
                @P_FK_CondicionPago,
                @P_FK_Transporte,
                @P_FK_Vendedor,
                @P_FK_Usuario_Creacion,
				@P_FK_Usuario_Modificacion,
				@P_Fecha_Creacion,
				GETDATE()
               
				
            );
        END;

        COMMIT TRANSACTION
        RETURN 1
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        RETURN 0
    END CATCH
END;

GO

CREATE PROCEDURE dbo.sp_ModificarClientes
    @P_PK_Cliente VARCHAR(50),
    @P_TipoIdentificacion VARCHAR(500),
    @P_Identificacion INT,
    @P_Nombre VARCHAR(300),
    @P_Telefono VARCHAR(100),
    @P_Correo VARCHAR(500),
    @P_Provincia VARCHAR(500),
    @P_Canton VARCHAR(500),
    @P_Distrito VARCHAR(500),
    @P_OtrasSenas VARCHAR(MAX),
    @P_Estado BIT,
    @P_FK_CondicionPago INT,
    @P_FK_Transporte BIGINT,
    @P_FK_Vendedor BIGINT,
    @P_FK_Usuario_Modificacion VARCHAR(500),
    @P_Fecha_Modificacion DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN [sp_ModificarClientes]
    BEGIN TRY
        UPDATE [dbo].[TBL_CLIENTES]
        SET
            TipoIdentificacion = @P_TipoIdentificacion,
            Identificacion = @P_Identificacion,
            Nombre = @P_Nombre,
            Telefono = @P_Telefono,
            Correo = @P_Correo,
            Provincia = @P_Provincia,
            Canton = @P_Canton,
            Distrito = @P_Distrito,
            OtrasSenas = @P_OtrasSenas,
            Estado = @P_Estado,
            FK_CondicionPago = @P_FK_CondicionPago,
            FK_Transporte = @P_FK_Transporte,
            FK_Vendedor = @P_FK_Vendedor,
            FK_Usuario_Modificacion = @P_FK_Usuario_Modificacion,
            Fecha_Modificacion = GETDATE()
        WHERE
            PK_Cliente = @P_PK_Cliente;

        COMMIT TRANSACTION
        RETURN 1
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        RETURN 0
    END CATCH
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_EliminarClientes]    Script Date: 6/11/2024 7:27:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_EliminarClientes]
      @P_PK_Cliente VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN [sp_EliminarClientes]
    BEGIN TRY
        -- Update the Estado to 0 for the specified provider
        UPDATE dbo.TBL_CLIENTES
        SET Estado = 0
        WHERE PK_Cliente = @P_PK_Cliente;

        COMMIT TRANSACTION
        RETURN 1
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        RETURN 0
    END CATCH
END;

GO

/** Object:  StoredProcedure [dbo].[sp_ObtenerCliente]    Script Date: 6/17/2024 6:22:02 PM **/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ObtenerCliente]
    @PK_Cliente varchar(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        PK_Cliente,
        TipoIdentificacion,
        Identificacion,
        Nombre,
        Telefono,
        Correo,
        Provincia,
        Canton,
        Distrito,
        OtrasSenas,
        Estado,
        FK_CondicionPago,
        FK_Transporte,
        FK_Vendedor,
        FK_Usuario_Creacion,
        FK_Usuario_Modificacion,
        Fecha_Creacion,
        Fecha_Modificacion
    FROM 
        dbo.TBL_CLIENTES 
    WHERE 
        PK_Cliente = @PK_Cliente
END;

GO

-------------------------------------------------
					/*Proveedores*/
-------------------------------------------------
GO

/****** Object:  StoredProcedure [dbo].[sp_ListarProveedores]    Script Date: 6/9/2024 6:17:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ListarProveedores]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        PK_Proveedor,
        Nombre,
        Telefono,
        Correo,
        Direccion,
        Estado,
        FK_Usuario_Creacion,
        FK_Usuario_Modificacion,
        Fecha_Creacion,
        Fecha_Modificacion
    FROM 
        dbo.TBL_PROVEEDORES
    ORDER BY 
        PK_Proveedor;
END;

GO

/****** Object:  StoredProcedure [dbo].[sp_ListarProveedoresxNombre]    Script Date: 6/9/2024 6:18:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ListarProveedoresxNombre]
    @Nombre NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        PK_Proveedor,
        Nombre,
        Telefono,
        Correo,
        Direccion,
        Estado,
        FK_Usuario_Creacion,
        FK_Usuario_Modificacion,
        Fecha_Creacion,
        Fecha_Modificacion
    FROM 
        dbo.TBL_PROVEEDORES 
    WHERE 
        Nombre LIKE '%' + @Nombre + '%'
    ORDER BY 
        PK_Proveedor;
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_InsertarProveedores]    Script Date: 6/10/2024 10:41:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_InsertarProveedores]
    @P_PK_Proveedor bigint,
    @P_Nombre VARCHAR(100),
    @P_Telefono VARCHAR(100),
    @P_Correo VARCHAR(200),
    @P_Direccion VARCHAR(300),
    @P_Estado BIT,
    @P_FK_Usuario_Creacion VARCHAR(50),
    @P_FK_Usuario_Modificacion VARCHAR(50),
    @P_Fecha_Creacion DATETIME,
    @P_Fecha_Modificacion DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN [sp_InsertarProveedor]
    BEGIN TRY
        INSERT INTO [dbo].[TBL_PROVEEDORES]
        (
            Nombre,
            Telefono,
            Correo,
            Direccion,
            Estado,
            FK_Usuario_Creacion,
            FK_Usuario_Modificacion,
            Fecha_Creacion,
            Fecha_Modificacion
        )
        VALUES
        (
            @P_Nombre,
            @P_Telefono,
            @P_Correo,
            @P_Direccion,
            @P_Estado,
            @P_FK_Usuario_Creacion,
            @P_FK_Usuario_Modificacion,
            GETDATE(),
	    GETDATE()
        );

        COMMIT TRANSACTION
        RETURN 1
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        RETURN 0
    END CATCH
END;

GO

/****** Object:  StoredProcedure [dbo].[sp_ModificarProveedores]    Script Date: 6/10/2024 10:41:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ModificarProveedores]
    @P_PK_Proveedor BIGINT,
    @P_Nombre VARCHAR(100),
    @P_Telefono VARCHAR(100),
    @P_Correo VARCHAR(200),
    @P_Direccion VARCHAR(300),
    @P_Estado BIT,
    @P_FK_Usuario_Creacion VARCHAR(50),
    @P_FK_Usuario_Modificacion VARCHAR(50),
    @P_Fecha_Creacion DATETIME,
    @P_Fecha_Modificacion DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN [sp_ModificarProveedor]
    BEGIN TRY
        UPDATE [dbo].[TBL_PROVEEDORES]
        SET
            Nombre = @P_Nombre,
            Telefono = @P_Telefono,
            Correo = @P_Correo,
            Direccion = @P_Direccion,
            Estado = @P_Estado,
            FK_Usuario_Modificacion = @P_FK_Usuario_Modificacion,
            Fecha_Modificacion = GETDATE()
        WHERE
            PK_Proveedor = @P_PK_Proveedor;

        COMMIT TRANSACTION
        RETURN 1
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        RETURN 0
    END CATCH
END;
GO
	
/****** Object:  StoredProcedure [dbo].[sp_EliminarProveedores]    Script Date: 6/9/2024 6:17:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_EliminarProveedores]
    @P_PK_Proveedor BIGINT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN [sp_EliminarProveedores]
    BEGIN TRY
        -- Update the Estado to 0 for the specified provider
        UPDATE dbo.TBL_PROVEEDORES
        SET Estado = 0
        WHERE PK_Proveedor = @P_PK_Proveedor;

        COMMIT TRANSACTION
        RETURN 1
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        RETURN 0
    END CATCH
END;
GO

/****** Object:  StoredProcedure [dbo].[sp_ObtenerProveedor]    Script Date: 6/18/2024 12:18:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ObtenerProveedor] 
    @PK_Proveedor varchar(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        PK_Proveedor,
        Nombre,
        Telefono,
        Correo,
        Direccion,
        Estado,
        FK_Usuario_Creacion,
        FK_Usuario_Modificacion,
        Fecha_Creacion,
        Fecha_Modificacion
    FROM 
        dbo.TBL_PROVEEDORES
		where PK_Proveedor = @PK_Proveedor

END;

go

	
-------------------------------------------------
					/*Vendedores*/
-------------------------------------------------
/****** Object:  StoredProcedure [dbo].[sp_ListarVendedores]    Script Date: 6/9/2024 9:40:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ListarVendedores]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        PK_Vendedor,
        Nombre,
        Telefono,
        Correo,
        Estado,
        FK_Usuario_Creacion,
        FK_Usuario_Modificacion,
        Fecha_Creacion,
        Fecha_Modificacion
    FROM 
        dbo.TBL_VENDEDORES
    ORDER BY 
        PK_Vendedor;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_ListarVendedoresxNombre]    Script Date: 6/9/2024 9:41:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ListarVendedoresxNombre]
    @Nombre NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        PK_Vendedor,
        Nombre,
        Telefono,
        Correo,
        Estado,
        FK_Usuario_Creacion,
        FK_Usuario_Modificacion,
        Fecha_Creacion,
        Fecha_Modificacion
    FROM 
        dbo.TBL_VENDEDORES 
    WHERE 
        Nombre LIKE '%' + @Nombre + '%'
    ORDER BY 
        Nombre;
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_InsertarVendedores]    Script Date: 6/10/2024 10:35:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_InsertarVendedores]
    @P_PK_Vendedor BIGINT,
    @P_Nombre VARCHAR(200),
    @P_Telefono VARCHAR(100),
    @P_Correo VARCHAR(200),
    @P_Estado BIT,
    @P_FK_Usuario_Creacion VARCHAR(50),
    @P_FK_Usuario_Modificacion VARCHAR(50),
    @P_Fecha_Creacion DATETIME,
    @P_Fecha_Modificacion DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN [sp_InsertarVendedores]
    BEGIN TRY
        IF EXISTS
        (
            SELECT 1
            FROM dbo.TBL_VENDEDORES WITH (NOLOCK)
            WHERE PK_Vendedor = @P_PK_Vendedor
        )
        BEGIN
            UPDATE dbo.TBL_VENDEDORES
            SET Nombre = @P_Nombre,
                Telefono = @P_Telefono,
                Correo = @P_Correo,
                Estado = @P_Estado,
                FK_Usuario_Modificacion = @P_FK_Usuario_Modificacion,
                Fecha_Modificacion = @P_Fecha_Modificacion
            WHERE PK_Vendedor = @P_PK_Vendedor;
        END
        ELSE
        BEGIN
            INSERT INTO [dbo].[TBL_VENDEDORES]
            (             
                Nombre,
                Telefono,
                Correo,
                Estado,
                FK_Usuario_Creacion,
                FK_Usuario_Modificacion,
                Fecha_Creacion,
                Fecha_Modificacion
            )
            VALUES
            (
                @P_Nombre,
                @P_Telefono,
                @P_Correo,
                @P_Estado,
                @P_FK_Usuario_Creacion,
                @P_FK_Usuario_Modificacion,
                @P_Fecha_Creacion,
                @P_Fecha_Modificacion
            );
        END;

        COMMIT TRANSACTION
        RETURN 1
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        RETURN 0
    END CATCH
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_ModificarVendedores]    Script Date: 6/18/2024 12:25:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ModificarVendedores]
    @P_PK_Vendedor BIGINT,
    @P_Nombre VARCHAR(200),
    @P_Telefono VARCHAR(100),
    @P_Correo VARCHAR(200),
    @P_Estado BIT,
    @P_FK_Usuario_Creacion VARCHAR(50),
    @P_FK_Usuario_Modificacion VARCHAR(50),
    @P_Fecha_Creacion DATETIME,
    @P_Fecha_Modificacion DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN [sp_ModificarVendedores]
    BEGIN TRY
        UPDATE dbo.TBL_VENDEDORES
            SET Nombre = @P_Nombre,
                Telefono = @P_Telefono,
                Correo = @P_Correo,
                Estado = @P_Estado,
                FK_Usuario_Modificacion = @P_FK_Usuario_Modificacion,
                Fecha_Modificacion = GETDATE()
            WHERE PK_Vendedor = @P_PK_Vendedor;

        COMMIT TRANSACTION
        RETURN 1
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        RETURN 0
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_EliminarVendedores]    Script Date: 6/9/2024 9:41:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_EliminarVendedores]
    @P_PK_Vendedor BIGINT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN [sp_EliminarVendedores]
    BEGIN TRY
        UPDATE dbo.TBL_VENDEDORES SET ESTADO = 0 WHERE PK_Vendedor = @P_PK_Vendedor;

        COMMIT TRANSACTION
        RETURN 1
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        RETURN 0
    END CATCH
END;
GO

	GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerVendedor]    Script Date: 6/18/2024 12:24:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ObtenerVendedor]
    @PK_Vendedor varchar(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        PK_Vendedor,
        Nombre,
        Telefono,
        Correo,
        Estado,
        FK_Usuario_Creacion,
        FK_Usuario_Modificacion,
        Fecha_Creacion,
        Fecha_Modificacion
    FROM 
        dbo.TBL_VENDEDORES 
    WHERE 
        PK_Vendedor = @PK_Vendedor
END;
	

	/***********************************************************************************************/

/*Listado de Transporte*/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ListarTransportes]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        PK_Medio_Transporte,
        Descripcion,
        Estado,
        FK_Usuario_Creacion,
        FK_Usuario_Modificacion,
        Fecha_Creacion,
        Fecha_Modificacion
    FROM 
        dbo.TBL_TRANSPORTES
    ORDER BY 
        PK_Medio_Transporte;
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_ListarTransportesxNombre]    Script Date: 6/10/2024 11:20:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ListarTransportesxNombre]
    @Descripcion NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
	PK_Medio_Transporte,
    Descripcion,
    Estado,
    FK_Usuario_Creacion,
	FK_Usuario_Modificacion,
    Fecha_Creacion,
	Fecha_Modificacion
    FROM 
        dbo.TBL_TRANSPORTES
    WHERE 
        Descripcion LIKE '%' + @Descripcion + '%'
    ORDER BY 
        PK_Medio_Transporte;
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_InsertarTransportes]    Script Date: 6/10/2024 11:20:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_InsertarTransportes]
	@P_PK_Medio_Transporte BIGINT,
    @P_Descripcion VARCHAR(100),
    @P_Estado BIT,
    @P_FK_Usuario_Creacion VARCHAR(50),
    @P_FK_Usuario_Modificacion VARCHAR(50),
    @P_Fecha_Creacion DATETIME,
    @P_Fecha_Modificacion DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN [sp_InsertarTransportes]
    BEGIN TRY
         BEGIN
            INSERT INTO [dbo].[TBL_TRANSPORTES]
            (
                Descripcion,
                Estado,
                FK_Usuario_Creacion,
				FK_Usuario_Modificacion,
                Fecha_Creacion,
				Fecha_Modificacion
            )
            VALUES
            (
                @P_Descripcion,
                @P_Estado,
                @P_FK_Usuario_Creacion,
				@P_FK_Usuario_Modificacion,
				@P_Fecha_Creacion,
				GETDATE()
               
				
            );
        END;

        COMMIT TRANSACTION
        RETURN 1
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        RETURN 0
    END CATCH
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_ModificarTransportes]    Script Date: 6/10/2024 11:20:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ModificarTransportes]
    @P_PK_Medio_Transporte BIGINT,
    @P_Descripcion VARCHAR(100),
    @P_Estado BIT,
    @P_FK_Usuario_Creacion VARCHAR(50),
    @P_FK_Usuario_Modificacion VARCHAR(50),
    @P_Fecha_Creacion DATETIME,
    @P_Fecha_Modificacion DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN [sp_ModificarTransportes]
    BEGIN TRY
        UPDATE [dbo].[TBL_TRANSPORTES]
        SET
            Descripcion = @P_Descripcion,
            Estado = @P_Estado,
            FK_Usuario_Modificacion = @P_FK_Usuario_Modificacion,
            Fecha_Modificacion = GETDATE()
        WHERE
            PK_Medio_Transporte = @P_PK_Medio_Transporte;

        COMMIT TRANSACTION
        RETURN 1
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        RETURN 0
    END CATCH
END;


GO

/*Eliminar un transporte*/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_EliminarTransportes]
    @P_PK_Medio_Transporte BIGINT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN [sp_EliminarTransporte]
    BEGIN TRY
        UPDATE  dbo.TBL_TRANSPORTES SET Estado = 0
        WHERE PK_Medio_Transporte = @P_PK_Medio_Transporte;

        COMMIT TRANSACTION
        RETURN 1
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        RETURN 0
    END CATCH
END;
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ObtenerTransporte]
    @PK_Medio_Transporte bigint
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        PK_Medio_Transporte,
        Descripcion,
        Estado,
        FK_Usuario_Creacion,
        FK_Usuario_Modificacion,
        Fecha_Creacion,
        Fecha_Modificacion
    FROM 
        dbo.TBL_TRANSPORTES
    WHERE 
        PK_Medio_Transporte = @PK_Medio_Transporte;
END;
GO


-------------------------------------------------
					/*Provincia*/
-------------------------------------------------

CREATE PROCEDURE sp_ListarProvincias
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ID_PROVINCIA, NOMBRE
    FROM TBL_PROVINCIA
END
GO

-------------------------------------------------
					/*Canton*/
-------------------------------------------------

CREATE PROCEDURE sp_ListarCantones
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ID_PROVINCIA, ID_CANTON, NOMBRE
    FROM TBL_CANTON
END
GO

-------------------------------------------------
					/*Distrito*/
-------------------------------------------------

CREATE PROCEDURE sp_ListarDistritos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ID_PROVINCIA, ID_CANTON, ID_DISTRITO, NOMBRE
    FROM TBL_DISTRITO
END
GO

-------------------------------------------------
					/*Ingreso Inventario*/
-------------------------------------------------
/****** Object:  StoredProcedure [dbo].[sp_ListarIngresoMercaderiaVM]    Script Date: 6/23/2024 10:46:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ListarIngresoMercaderiaVM]
AS
BEGIN
    SET NOCOUNT ON;

SELECT 
    im.PK_FK_Documento,
    p.Nombre as PK_FK_Proveedor,
    cp.Descripcion as FK_CondicionPago,
    CONVERT(varchar, im.Fecha_Compra, 23) as Fecha_Compra, -- Formato AAAA-MM-DD
    SUM(im.Subtotal) as Total,
    im.Estado
FROM [dbo].[TBL_INGRESO_MERCADERIA] im
INNER JOIN [dbo].[TBL_PROVEEDORES] p ON im.PK_FK_Proveedor = p.PK_Proveedor
INNER JOIN [dbo].[TBL_CONDICIONES_PAGO] cp ON im.FK_CondicionPago = cp.PK_Condicion_Pago
GROUP BY 
    im.PK_FK_Documento, 
    p.Nombre, 
    cp.Descripcion, 
    CONVERT(varchar, im.Fecha_Compra, 23), -- Formato AAAA-MM-DD
    im.Estado
ORDER BY 
    Fecha_Compra DESC;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertarActualizarIngresoMercaderia]    Script Date: 6/23/2024 10:48:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_InsertarActualizarIngresoMercaderia]
    @PK_FK_Documento varchar(50),
    @PK_FK_Proveedor int,
    @FK_CondicionPago int,
    @Fecha_Compra datetime,
    @PK_FK_Articulo varchar(50),
    @Cantidad decimal(18, 2),
    @Costo decimal(18, 2),
    @FK_Usuario_Creacion varchar(50),
    @FK_Usuario_Modificacion varchar(50)
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si ya existe el ingreso
    IF EXISTS (SELECT 1 FROM [dbo].[TBL_INGRESO_MERCADERIA]
               WHERE [PK_FK_Documento] = @PK_FK_Documento
               AND [PK_FK_Proveedor] = @PK_FK_Proveedor
               AND [PK_FK_Articulo] = @PK_FK_Articulo)
    BEGIN
        -- Actualizar la cantidad y el costo si ya existe
        UPDATE [dbo].[TBL_INGRESO_MERCADERIA]
        SET [Cantidad] = @Cantidad,
            [Costo] = @Costo,
			[Subtotal] = @Cantidad * @Costo
        WHERE [PK_FK_Documento] = @PK_FK_Documento
        AND [PK_FK_Proveedor] = @PK_FK_Proveedor
        AND [PK_FK_Articulo] = @PK_FK_Articulo;
    END
    ELSE
    BEGIN
        -- Insertar un nuevo ingreso si no existe
        INSERT INTO [dbo].[TBL_INGRESO_MERCADERIA] (
            [PK_FK_Documento],
            [PK_FK_Proveedor],
            [FK_CondicionPago],
            [Fecha_Compra],
            [PK_FK_Articulo],
            [Cantidad],
            [Costo],
            [Subtotal],
			[Estado],
            [FK_Usuario_Creacion],
            [FK_Usuario_Modificacion],
            [Fecha_Creacion],
            [Fecha_Modificacion]
        )
        VALUES (
            @PK_FK_Documento,
            @PK_FK_Proveedor,
            @FK_CondicionPago,
            @Fecha_Compra,
            @PK_FK_Articulo,
            @Cantidad,
            @Costo,
            @Cantidad * @Costo,
			'Pendiente',
            @FK_Usuario_Creacion,
            @FK_Usuario_Modificacion,
            GETDATE(),
            GETDATE()
        );
    END
END

GO 
/****** Object:  StoredProcedure [dbo].[sp_GuardarIngresoMercaderia]    Script Date: 6/24/2024 4:15:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GuardarIngresoMercaderia]
    @PK_FK_Documento VARCHAR(50)
AS
BEGIN
    -- Actualizar el estado a 'Completado' en TBL_INGRESO_MERCADERIA
    UPDATE TBL_INGRESO_MERCADERIA
    SET Estado = 'Completado'
    WHERE PK_FK_Documento = @PK_FK_Documento;

    -- Actualizar las cantidades en TBL_ARTICULO
    UPDATE A
    SET A.Cantidad = A.Cantidad + IM.Cantidad,
	A.Costo = IM.Costo
    FROM TBL_ARTICULO A
    INNER JOIN TBL_INGRESO_MERCADERIA IM ON A.PK_Articulo = IM.PK_FK_Articulo
    WHERE IM.PK_FK_Documento = @PK_FK_Documento;

    -- Insertar un documento en TBL_DOCUMENTO_CP si la condición de pago es diferente a 1
    IF EXISTS(SELECT 1 FROM TBL_INGRESO_MERCADERIA WHERE PK_FK_Documento = @PK_FK_Documento AND FK_CondicionPago <> 1)
    BEGIN
        DECLARE @MontoTotal DECIMAL(18,2);
        DECLARE @Dias INT;
        SELECT @MontoTotal = SUM(Subtotal), @Dias = MAX(Dias)
        FROM TBL_INGRESO_MERCADERIA IM
        INNER JOIN TBL_CONDICIONES_PAGO CP ON IM.FK_CondicionPago = CP.PK_Condicion_Pago
        WHERE PK_FK_Documento = @PK_FK_Documento;

        INSERT INTO TBL_DOCUMENTO_CP (PK_Documento, FK_Proveedor, Fecha_Documento, Fecha_Vence, Monto, Saldo_Pendiente, Anulado, FK_Usuario_Creacion, FK_Usuario_Modificacion, Fecha_Creacion, Fecha_Modificacion)
        SELECT TOP 1
            @PK_FK_Documento,
            PK_FK_Proveedor,
            Fecha_Compra,
            DATEADD(DAY, @Dias, Fecha_Compra), -- Fecha de vencimiento basada en los días de la condición de pago
            @MontoTotal, -- Monto total calculado
            @MontoTotal, -- Saldo pendiente igual al monto total inicialmente
            'N', -- No anulado
            FK_Usuario_Creacion, 
            FK_Usuario_Modificacion, 
            Fecha_Creacion,
            Fecha_Modificacion

        FROM TBL_INGRESO_MERCADERIA
        WHERE PK_FK_Documento = @PK_FK_Documento;
    END
END

GO


/** Object:  StoredProcedure [dbo].[sp_ListarIngresoPorDoc]    Script Date: 6/23/2024 10:59:00 AM **/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ListarIngresoPorDoc]
    @PK_FK_Documento varchar(50)
AS
BEGIN
    SET NOCOUNT ON;

SELECT 
    im.PK_FK_Articulo AS PK_Articulo,
    A.Descripcion,
    im.Cantidad,
    im.Costo,
    (im.Cantidad * im.Costo) AS Precio
FROM [dbo].[TBL_INGRESO_MERCADERIA] im
INNER JOIN [dbo].[TBL_ARTICULO] a ON im.PK_FK_Articulo = a.PK_Articulo
WHERE PK_FK_Documento = @PK_FK_Documento
ORDER BY 
    Linea DESC;
END;

GO

/****** Object:  StoredProcedure [dbo].[sp_EliminarArticuloIngreso]    Script Date: 6/26/2024 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento almacenado para eliminar un artículo de ingreso de mercadería
CREATE PROCEDURE [dbo].[sp_EliminarArticuloIngreso]
    @PK_FK_Documento varchar(50),
    @PK_FK_Articulo varchar(50)
AS
BEGIN
    -- Evita que el procedimiento se detenga y muestre un error en caso de no encontrar el registro
    SET NOCOUNT ON;

    -- Elimina el registro específico de la tabla TBL_INGRESO_MERCADERIA
    DELETE FROM [dbo].[TBL_INGRESO_MERCADERIA]
    WHERE [PK_FK_Documento] = @PK_FK_Documento AND [PK_FK_Articulo] = @PK_FK_Articulo;



END
GO

/****** Object:  StoredProcedure [dbo].[sp_EliminarIngreso]    Script Date: 6/29/2024 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento almacenado para eliminar ingresos de mercadería por documento
CREATE PROCEDURE [dbo].[sp_EliminarIngreso]
    @PK_FK_Documento varchar(50)
AS
BEGIN
    -- Evita que el procedimiento se detenga y muestre un error en caso de no encontrar el registro
    SET NOCOUNT ON;

    -- Elimina los registros que coincidan con el PK_FK_Documento proporcionado
    DELETE FROM [dbo].[TBL_INGRESO_MERCADERIA]
    WHERE [PK_FK_Documento] = @PK_FK_Documento;

  

END
GO

	/****** Object:  StoredProcedure [dbo].[sp_ObtenerIngresoMercaderia]    Script Date: 6/30/2024 3:37:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ObtenerIngresoMercaderia] 
    @PK_FK_Documento varchar(50)

AS
BEGIN
    SET NOCOUNT ON;

SELECT 
    PK_FK_Documento,
	Fecha_Compra,
    PK_FK_Proveedor,
    FK_CondicionPago
FROM [dbo].[TBL_INGRESO_MERCADERIA] 
WHERE PK_FK_Documento = @PK_FK_Documento
GROUP BY 
    PK_FK_Documento, 
    PK_FK_Proveedor, 
    FK_CondicionPago,
	Fecha_Compra

END


-------------------------------------------------
					/*Salida Inventario*/
-------------------------------------------------
GO	
/****** Object:  StoredProcedure [dbo].[sp_ListarSalidaMercaderia]    Script Date: 6/27/2024 7:41:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ListarSalidaMercaderia]
AS
BEGIN
    SET NOCOUNT ON;

SELECT 
    PK_FK_Documento,
    CONVERT(varchar, Fecha_Salida, 23) AS Fecha_Salida,
    Tipo_Salida,
	Observaciones,
	Estado
FROM [dbo].[TBL_SALIDA_MERCADERIA] 
GROUP BY 
    PK_FK_Documento,
    CONVERT(varchar, Fecha_Salida, 23),
    Tipo_Salida,
	Observaciones,
	Estado
ORDER BY 
    Fecha_Salida DESC;
END


GO
/****** Object:  StoredProcedure [dbo].[sp_InsertarActualizarSalidaMercaderia]    Script Date: 6/27/2024 9:41:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_InsertarActualizarSalidaMercaderia]
    @PK_FK_Documento varchar(50),
    @Fecha_Salida datetime,
    @Tipo_Salida varchar(50),
    @Observaciones varchar(100),
    @PK_FK_Articulo varchar(50),
    @Cantidad int,
    @FK_Usuario_Creacion varchar(50),
    @FK_Usuario_Modificacion varchar(50)
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si ya existe el ingreso
    IF EXISTS (SELECT 1 FROM [dbo].[TBL_SALIDA_MERCADERIA]
               WHERE [PK_FK_Documento] = @PK_FK_Documento   
               AND [PK_FK_Articulo] = @PK_FK_Articulo)
    BEGIN
        -- Actualizar la cantidad y el costo si ya existe
        UPDATE [dbo].[TBL_SALIDA_MERCADERIA]
        SET [Cantidad] = @Cantidad
        WHERE [PK_FK_Documento] = @PK_FK_Documento
        AND [PK_FK_Articulo] = @PK_FK_Articulo;
    END
    ELSE
    BEGIN
        -- Insertar un nuevo ingreso si no existe
        INSERT INTO [dbo].[TBL_SALIDA_MERCADERIA] (
            [PK_FK_Documento],
			[Fecha_Salida],
			[Tipo_Salida],
			[Observaciones],
            [PK_FK_Articulo],
            [Cantidad],
			[Estado],
            [FK_Usuario_Creacion],
            [FK_Usuario_Modificacion],
            [Fecha_Creacion],
            [Fecha_Modificacion]
        )
        VALUES (
            @PK_FK_Documento,
            @Fecha_Salida,
			@Tipo_Salida,
            @Observaciones,
            @PK_FK_Articulo,
            @Cantidad,
			'Pendiente',
            @FK_Usuario_Creacion,
            @FK_Usuario_Modificacion,
            GETDATE(),
            GETDATE()
        );
    END
END


GO
/****** Object:  StoredProcedure [dbo].[[sp_ListarSalidaPorDoc]]    Script Date: 6/27/2024 10:14:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ListarSalidaPorDoc]
    @PK_FK_Documento varchar(50)
AS
BEGIN
    SET NOCOUNT ON;

SELECT 
    im.PK_FK_Articulo AS PK_Articulo,
    A.Descripcion,
    im.Cantidad
FROM [dbo].[TBL_SALIDA_MERCADERIA] im
INNER JOIN [dbo].[TBL_ARTICULO] a ON im.PK_FK_Articulo = a.PK_Articulo
WHERE PK_FK_Documento = @PK_FK_Documento
ORDER BY 
    Linea DESC;
END


GO
/****** Object:  StoredProcedure [dbo].[sp_EliminarArticuloSalida]    Script Date: 6/27/2024 10:22:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento almacenado para eliminar un artículo de ingreso de mercadería
CREATE PROCEDURE [dbo].[sp_EliminarArticuloSalida]
    @PK_FK_Documento varchar(50),
    @PK_FK_Articulo varchar(50)
AS
BEGIN
    -- Evita que el procedimiento se detenga y muestre un error en caso de no encontrar el registro
    SET NOCOUNT ON;

    -- Elimina el registro específico de la tabla TBL_INGRESO_MERCADERIA
    DELETE FROM [dbo].[TBL_SALIDA_MERCADERIA]
    WHERE [PK_FK_Documento] = @PK_FK_Documento AND [PK_FK_Articulo] = @PK_FK_Articulo;

    -- Opcional: Agregar manejo de errores o lógica adicional aquí

END

GO
/****** Object:  StoredProcedure [dbo].[sp_GuardarSalidaMercaderia]    Script Date: 6/27/2024 10:38:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GuardarSalidaMercaderia] 
    @PK_FK_Documento VARCHAR(50)
AS
BEGIN
    -- Actualizar el estado a 'Completado' en TBL_SALIDA_MERCADERIA
    UPDATE TBL_SALIDA_MERCADERIA
    SET Estado = 'Completado'
    WHERE PK_FK_Documento = @PK_FK_Documento;

    -- Actualizar las cantidades en TBL_ARTICULO
    UPDATE A
    SET A.Cantidad = A.Cantidad - IM.Cantidad
    FROM TBL_ARTICULO A
    INNER JOIN TBL_SALIDA_MERCADERIA IM ON A.PK_Articulo = IM.PK_FK_Articulo
    WHERE IM.PK_FK_Documento = @PK_FK_Documento;

END

GO
	

/****** Object:  StoredProcedure [dbo].[sp_ObtenerSalidaMercaderia]    Script Date: 6/30/2024 3:37:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ObtenerSalidaMercaderia]   
    @PK_FK_Documento varchar(50)

AS
BEGIN
    SET NOCOUNT ON;

SELECT 
    PK_FK_Documento,
	Fecha_Salida,
	Tipo_Salida,
    Observaciones
FROM [dbo].[TBL_SALIDA_MERCADERIA] 
WHERE PK_FK_Documento = @PK_FK_Documento
GROUP BY 
    PK_FK_Documento, 
	Fecha_Salida,   
	Tipo_Salida,
	Observaciones

END

GO

/****** Object:  StoredProcedure [dbo].[sp_EliminarSalida]    Script Date: 6/29/2024 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento almacenado para eliminar ingresos de mercadería por documento
CREATE PROCEDURE [dbo].[sp_EliminarSalida]
    @PK_FK_Documento varchar(50)
AS
BEGIN
    -- Evita que el procedimiento se detenga y muestre un error en caso de no encontrar el registro
    SET NOCOUNT ON;

    -- Elimina los registros que coincidan con el PK_FK_Documento proporcionado
    DELETE FROM [dbo].[TBL_SALIDA_MERCADERIA]
    WHERE [PK_FK_Documento] = @PK_FK_Documento;

    -- Opcional: Agregar manejo de errores o lógica adicional aquí

END
GO
	
----------------------------------------------------------------------------------------------------
									/*INSERCION DE DATOS*/
----------------------------------------------------------------------------------------------------

INSERT [dbo].[TBL_PROVINCIA] ([ID_PROVINCIA], [NOMBRE]) VALUES (1, N'San Jose')
GO
INSERT [dbo].[TBL_PROVINCIA] ([ID_PROVINCIA], [NOMBRE]) VALUES (2, N'Alajuela')
GO
INSERT [dbo].[TBL_PROVINCIA] ([ID_PROVINCIA], [NOMBRE]) VALUES (3, N'Cartago')
GO
INSERT [dbo].[TBL_PROVINCIA] ([ID_PROVINCIA], [NOMBRE]) VALUES (4, N'Heredia')
GO
INSERT [dbo].[TBL_PROVINCIA] ([ID_PROVINCIA], [NOMBRE]) VALUES (5, N'Guanacaste')
GO
INSERT [dbo].[TBL_PROVINCIA] ([ID_PROVINCIA], [NOMBRE]) VALUES (6, N'Puntarenas')
GO
INSERT [dbo].[TBL_PROVINCIA] ([ID_PROVINCIA], [NOMBRE]) VALUES (7, N'Limon')
GO

INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (1, 1, N'San José')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (1, 2, N'Escazú')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (1, 3, N'Desamparados')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (1, 4, N'Puriscal')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (1, 5, N'Tarrazú')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (1, 6, N'Aserrí')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (1, 7, N'Mora')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (1, 8, N'Goicoechea')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (1, 9, N'Santa Ana')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (1, 10, N'Alajuelita')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (1, 11, N'Vásquez de Coronado')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (1, 12, N'Acosta')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (1, 13, N'Tibás')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (1, 14, N'Moravia')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (1, 15, N'Montes de Oca')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (1, 16, N'Turrubares')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (1, 17, N'Dota')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (1, 18, N'Curridabat')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (1, 19, N'Pérez Zeledón')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (1, 20, N'León Cortéz Castro')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (2, 1, N'Alajuela')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (2, 2, N'San Ramón')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (2, 3, N'Grecia')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (2, 4, N'San Mateo')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (2, 5, N'Atenas')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (2, 6, N'Naranjo')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (2, 7, N'Palmares')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (2, 8, N'Poás')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (2, 9, N'Orotina')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (2, 10, N'San Carlos')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (2, 11, N'Zarcero')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (2, 12, N'Valverde Vega')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (2, 13, N'Upala')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (2, 14, N'Los Chiles')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (2, 15, N'Guatuso')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (3, 1, N'Cartago')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (3, 2, N'Paraíso')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (3, 3, N'La Unión')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (3, 4, N'Jiménez')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (3, 5, N'Turrialba')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (3, 6, N'Alvarado')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (3, 7, N'Oreamuno')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (3, 8, N'El Guarco')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (4, 1, N'Heredia')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (4, 2, N'Barva')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (4, 3, N'Santo Domingo')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (4, 4, N'Santa Bárbara')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (4, 5, N'San Rafaél')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (4, 6, N'San Isidro')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (4, 7, N'Belén')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (4, 8, N'Flores')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (4, 9, N'San Pablo')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (4, 10, N'Sarapiquí')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (5, 1, N'Liberia')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (5, 2, N'Nicoya')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (5, 3, N'Santa Cruz')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (5, 4, N'Bagaces')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (5, 5, N'Carrillo')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (5, 6, N'Cañas')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (5, 7, N'Abangáres')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (5, 8, N'Tilarán')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (5, 9, N'Nandayure')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (5, 10, N'La Cruz')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (5, 11, N'Hojancha')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (6, 1, N'Puntarenas')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (6, 2, N'Esparza')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (6, 3, N'Buenos Aires')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (6, 4, N'Montes de Oro')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (6, 5, N'Osa')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (6, 6, N'Aguirre')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (6, 7, N'Golfito')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (6, 8, N'Coto Brus')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (6, 9, N'Parrita')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (6, 10, N'Corredores')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (6, 11, N'Garabito')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (7, 1, N'Limón')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (7, 2, N'Pococí')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (7, 3, N'Siquirres')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (7, 4, N'Talamanca')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (7, 5, N'Matina')
GO
INSERT [dbo].[TBL_CANTON] ([ID_PROVINCIA], [ID_CANTON], [NOMBRE]) VALUES (7, 6, N'Guácimo')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 1, 1, N'CARMEN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 1, 2, N'MERCED')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 1, 3, N'HOSPITAL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 1, 4, N'CATEDRAL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 1, 5, N'ZAPOTE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 1, 6, N'SAN FRANCISCO DE DOS RÍOS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 1, 7, N'URUCA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 1, 8, N'MATA REDONDA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 1, 9, N'PAVAS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 1, 10, N'HATILLO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 1, 11, N'SAN SEBASTIÁN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 2, 1, N'ESCAZÚ')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 2, 2, N'SAN ANTONIO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 2, 3, N'SAN RAFAEL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 3, 1, N'DESAMPARADOS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 3, 2, N'SAN MIGUEL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 3, 3, N'SAN JUAN DE DIOS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 3, 4, N'SAN RAFAEL ARRIBA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 3, 5, N'SAN ANTONIO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 3, 6, N'FRAILES')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 3, 7, N'PATARRÁ')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 3, 8, N'SAN CRISTÓBAL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 3, 9, N'ROSARIO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 3, 10, N'DAMAS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 3, 11, N'SAN RAFAEL ABAJO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 3, 12, N'GRAVILIAS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 3, 13, N'LOS GUIDO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 4, 1, N'SANTIAGO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 4, 2, N'MERCEDES SUR')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 4, 3, N'BARBACOAS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 4, 4, N'GRIFO ALTO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 4, 5, N'SAN RAFAEL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 4, 6, N'CANDELARITA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 4, 7, N'DESAMPARADITOS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 4, 8, N'SAN ANTONIO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 4, 9, N'CHIRES')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 5, 1, N'SAN MARCOS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 5, 2, N'SAN LORENZO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 5, 3, N'SAN CARLOS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 6, 1, N'ASERRI')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 6, 2, N'TARBACA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 6, 3, N'VUELTA DE JORCO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 6, 4, N'SAN GABRIEL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 6, 5, N'LEGUA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 6, 6, N'MONTERREY')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 6, 7, N'SALITRILLOS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 7, 1, N'COLÓN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 7, 2, N'GUAYABO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 7, 3, N'TABARCIA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 7, 4, N'PIEDRAS NEGRAS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 7, 5, N'PICAGRES')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 7, 6, N'JARIS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 7, 7, N'QUITIRRISI')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 8, 1, N'GUADALUPE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 8, 2, N'SAN FRANCISCO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 8, 3, N'CALLE BLANCOS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 8, 4, N'MATA DE PLÁTANO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 8, 5, N'IPÍS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 8, 6, N'RANCHO REDONDO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 8, 7, N'PURRAL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 9, 1, N'SANTA ANA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 9, 2, N'SALITRAL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 9, 3, N'POZOS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 9, 4, N'URUCA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 9, 5, N'PIEDADES')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 9, 6, N'BRASIL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 10, 1, N'ALAJUELITA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 10, 2, N'SAN JOSECITO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 10, 3, N'SAN ANTONIO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 10, 4, N'CONCEPCIÓN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 10, 5, N'SAN FELIPE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 11, 1, N'SAN ISIDRO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 11, 2, N'SAN RAFAEL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 11, 3, N'DULCE NOMBRE DE JESÚS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 11, 4, N'PATALILLO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 11, 5, N'CASCAJAL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 12, 1, N'SAN IGNACIO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 12, 2, N'GUAITIL Villa')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 12, 3, N'PALMICHAL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 12, 4, N'CANGREJAL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 12, 5, N'SABANILLAS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 13, 1, N'SAN JUAN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 13, 2, N'CINCO ESQUINAS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 13, 3, N'ANSELMO LLORENTE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 13, 4, N'LEON XIII')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 13, 5, N'COLIMA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 14, 1, N'SAN VICENTE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 14, 2, N'SAN JERÓNIMO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 14, 3, N'LA TRINIDAD')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 15, 1, N'SAN PEDRO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 15, 2, N'SABANILLA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 15, 3, N'MERCEDES')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 15, 4, N'SAN RAFAEL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 16, 1, N'SAN PABLO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 16, 2, N'SAN PEDRO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 16, 3, N'SAN JUAN DE MATA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 16, 4, N'SAN LUIS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 16, 5, N'CARARA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 17, 1, N'SANTA MARÍA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 17, 2, N'JARDÍN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 17, 3, N'COPEY')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 18, 1, N'CURRIDABAT')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 18, 2, N'GRANADILLA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 18, 3, N'SÁNCHEZ')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 18, 4, N'TIRRASES')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 19, 1, N'SAN ISIDRO DE EL GENERAL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 19, 2, N'EL GENERAL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 19, 3, N'DANIEL FLORES')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 19, 4, N'RIVAS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 19, 5, N'SAN PEDRO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 19, 6, N'PLATANARES')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 19, 7, N'PEJIBAYE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 19, 8, N'CAJÓN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 19, 9, N'BARÚ')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 19, 10, N'RÍO NUEVO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 19, 11, N'PÁRAMO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 20, 1, N'SAN PABLO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 20, 2, N'SAN ANDRÉS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 20, 3, N'LLANO BONITO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 20, 4, N'SAN ISIDRO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 20, 5, N'SANTA CRUZ')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (1, 20, 6, N'SAN ANTONIO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 1, 1, N'ALAJUELA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 1, 2, N'SAN JOSÉ')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 1, 3, N'CARRIZAL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 1, 4, N'SAN ANTONIO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 1, 5, N'GUÁCIMA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 1, 6, N'SAN ISIDRO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 1, 7, N'SABANILLA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 1, 8, N'SAN RAFAEL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 1, 9, N'RÍO SEGUNDO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 1, 10, N'DESAMPARADOS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 1, 11, N'TURRÚCARES')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 1, 12, N'TAMBOR')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 1, 13, N'GARITA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 1, 14, N'SARAPIQUÍ')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 2, 1, N'SAN RAMÓN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 2, 2, N'SANTIAGO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 2, 3, N'SAN JUAN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 2, 4, N'PIEDADES NORTE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 2, 5, N'PIEDADES SUR')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 2, 6, N'SAN RAFAEL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 2, 7, N'SAN ISIDRO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 2, 8, N'ÁNGELES')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 2, 9, N'ALFARO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 2, 10, N'VOLIO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 2, 11, N'CONCEPCIÓN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 2, 12, N'ZAPOTAL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 2, 13, N'PEÑAS BLANCAS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 3, 1, N'GRECIA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 3, 2, N'SAN ISIDRO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 3, 3, N'SAN JOSÉ')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 3, 4, N'SAN ROQUE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 3, 5, N'TACARES')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 3, 6, N'RÍO CUARTO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 3, 7, N'PUENTE DE PIEDRA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 3, 8, N'BOLÍVAR')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 4, 1, N'SAN MATEO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 4, 2, N'DESMONTE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 4, 3, N'JESÚS MARÍA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 4, 4, N'LABRADOR')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 5, 1, N'ATENAS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 5, 2, N'JESÚS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 5, 3, N'MERCEDES')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 5, 4, N'SAN ISIDRO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 5, 5, N'CONCEPCIÓN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 5, 6, N'SAN JOSE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 5, 7, N'SANTA EULALIA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 5, 8, N'ESCOBAL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 6, 1, N'NARANJO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 6, 2, N'SAN MIGUEL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 6, 3, N'SAN JOSÉ')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 6, 4, N'CIRRÍ SUR')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 6, 5, N'SAN JERÓNIMO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 6, 6, N'SAN JUAN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 6, 7, N'EL ROSARIO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 6, 8, N'PALMITOS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 7, 1, N'PALMARES')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 7, 2, N'ZARAGOZA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 7, 3, N'BUENOS AIRES')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 7, 4, N'SANTIAGO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 7, 5, N'CANDELARIA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 7, 6, N'ESQUÍPULAS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 7, 7, N'LA GRANJA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 8, 1, N'SAN PEDRO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 8, 2, N'SAN JUAN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 8, 3, N'SAN RAFAEL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 8, 4, N'CARRILLOS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 8, 5, N'SABANA REDONDA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 9, 1, N'OROTINA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 9, 2, N'EL MASTATE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 9, 3, N'HACIENDA VIEJA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 9, 4, N'COYOLAR')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 9, 5, N'LA CEIBA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 10, 1, N'QUESADA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 10, 2, N'FLORENCIA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 10, 3, N'BUENAVISTA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 10, 4, N'AGUAS ZARCAS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 10, 5, N'VENECIA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 10, 6, N'PITAL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 10, 7, N'LA FORTUNA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 10, 8, N'LA TIGRA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 10, 9, N'LA PALMERA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 10, 10, N'VENADO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 10, 11, N'CUTRIS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 10, 12, N'MONTERREY')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 10, 13, N'POCOSOL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 11, 1, N'ZARCERO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 11, 2, N'LAGUNA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 11, 4, N'GUADALUPE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 11, 5, N'PALMIRA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 11, 6, N'ZAPOTE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 11, 7, N'BRISAS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 12, 1, N'SARCHÍ NORTE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 12, 2, N'SARCHÍ SUR')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 12, 3, N'TORO AMARILLO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 12, 4, N'SAN PEDRO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 12, 5, N'RODRÍGUEZ')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 13, 1, N'UPALA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 13, 2, N'AGUAS CLARAS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 13, 3, N'SAN JOSÉ o PIZOTE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 13, 4, N'BIJAGUA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 13, 5, N'DELICIAS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 13, 6, N'DOS RÍOS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 13, 7, N'YOLILLAL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 13, 8, N'CANALETE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 14, 1, N'LOS CHILES')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 14, 3, N'EL AMPARO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 14, 4, N'SAN JORGE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 15, 2, N'BUENAVISTA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 15, 3, N'COTE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 15, 4, N'KATIRA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 1, 1, N'ORIENTAL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 1, 2, N'OCCIDENTAL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 1, 3, N'CARMEN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 1, 4, N'SAN NICOLÁS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 1, 5, N'AGUACALIENTE o SAN FRANCISCO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 1, 6, N'GUADALUPE o ARENILLA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 1, 7, N'CORRALILLO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 1, 8, N'TIERRA BLANCA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 1, 9, N'DULCE NOMBRE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 1, 10, N'LLANO GRANDE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 1, 11, N'QUEBRADILLA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 2, 1, N'PARAÍSO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 2, 2, N'SANTIAGO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 2, 3, N'OROSI')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 2, 4, N'CACHÍ')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 2, 5, N'LLANOS DE SANTA LUCÍA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 3, 1, N'TRES RÍOS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 3, 2, N'SAN DIEGO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 3, 3, N'SAN JUAN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 3, 4, N'SAN RAFAEL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 3, 5, N'CONCEPCIÓN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 3, 6, N'DULCE NOMBRE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 3, 7, N'SAN RAMÓN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 3, 8, N'RÍO AZUL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 4, 1, N'JUAN VIÑAS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 4, 2, N'TUCURRIQUE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 4, 3, N'PEJIBAYE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 5, 1, N'TURRIALBA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 5, 2, N'LA SUIZA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 5, 3, N'PERALTA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 5, 4, N'SANTA CRUZ')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 5, 5, N'SANTA TERESITA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 5, 6, N'PAVONES')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 5, 7, N'TUIS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 5, 8, N'TAYUTIC')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 5, 9, N'SANTA ROSA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 5, 10, N'TRES EQUIS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 5, 11, N'LA ISABEL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 5, 12, N'CHIRRIPÓ')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 6, 1, N'PACAYAS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 6, 2, N'CERVANTES')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 6, 3, N'CAPELLADES')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 7, 1, N'SAN RAFAEL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 7, 2, N'COT')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 7, 3, N'POTRERO CERRADO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 7, 4, N'CIPRESES')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 7, 5, N'SANTA ROSA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 8, 1, N'EL TEJAR')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 8, 2, N'SAN ISIDRO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 8, 3, N'TOBOSI')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (3, 8, 4, N'PATIO DE AGUA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 1, 1, N'HEREDIA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 1, 2, N'MERCEDES')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 1, 3, N'SAN FRANCISCO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 1, 4, N'ULLOA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 1, 5, N'VARABLANCA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 2, 1, N'BARVA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 2, 2, N'SAN PEDRO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 2, 3, N'SAN PABLO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 2, 4, N'SAN ROQUE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 2, 5, N'SANTA LUCÍA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 2, 6, N'SAN JOSÉ DE LA MONTAÑA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 3, 2, N'SAN VICENTE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 3, 3, N'SAN MIGUEL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 3, 4, N'PARACITO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 3, 5, N'SANTO TOMÁS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 3, 6, N'SANTA ROSA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 3, 7, N'TURES')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 3, 8, N'PARÁ')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 4, 1, N'SANTA BÁRBARA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 4, 2, N'SAN PEDRO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 4, 3, N'SAN JUAN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 4, 4, N'JESÚS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 4, 5, N'SANTO DOMINGO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 4, 6, N'PURABÁ')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 5, 1, N'SAN RAFAEL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 5, 2, N'SAN JOSECITO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 5, 3, N'SANTIAGO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 5, 4, N'ÁNGELES')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 5, 5, N'CONCEPCIÓN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 6, 1, N'SAN ISIDRO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 6, 2, N'SAN JOSÉ')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 6, 3, N'CONCEPCIÓN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 6, 4, N'SAN FRANCISCO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 7, 1, N'SAN ANTONIO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 7, 2, N'LA RIBERA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 7, 3, N'LA ASUNCIÓN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 8, 1, N'SAN JOAQUÍN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 8, 2, N'BARRANTES')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 8, 3, N'LLORENTE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 9, 1, N'SAN PABLO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 10, 1, N'PUERTO VIEJO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 10, 2, N'LA VIRGEN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 10, 3, N'LAS HORQUETAS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 10, 4, N'LLANURAS DEL GASPAR')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (4, 10, 5, N'CUREÑA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 1, 1, N'LIBERIA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 1, 2, N'CAÑAS DULCES')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 1, 3, N'MAYORGA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 1, 4, N'NACASCOLO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 1, 5, N'CURUBANDÉ')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 2, 1, N'NICOYA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 2, 2, N'MANSIÓN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 2, 3, N'SAN ANTONIO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 2, 4, N'QUEBRADA HONDA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 2, 5, N'SÁMARA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 2, 6, N'NOSARA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 2, 7, N'BELÉN DE NOSARITA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 3, 1, N'SANTA CRUZ')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 3, 2, N'BOLSÓN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 3, 3, N'VEINTISIETE DE ABRIL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 3, 4, N'TEMPATE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 3, 5, N'CARTAGENA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 3, 6, N'CUAJINIQUIL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 3, 7, N'DIRIÁ')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 3, 8, N'CABO VELAS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 3, 9, N'TAMARINDO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 4, 1, N'BAGACES')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 4, 2, N'LA FORTUNA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 4, 3, N'MOGOTE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 4, 4, N'RÍO NARANJO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 5, 1, N'FILADELFIA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 5, 2, N'PALMIRA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 5, 3, N'SARDINAL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 5, 4, N'BELÉN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 6, 1, N'CAÑAS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 6, 2, N'PALMIRA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 6, 3, N'SAN MIGUEL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 6, 4, N'BEBEDERO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 6, 5, N'POROZAL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 7, 1, N'LAS JUNTAS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 7, 2, N'SIERRA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 7, 3, N'SAN JUAN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 7, 4, N'COLORADO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 8, 1, N'TILARÁN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 8, 2, N'QUEBRADA GRANDE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 8, 3, N'TRONADORA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 8, 4, N'SANTA ROSA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 8, 5, N'LÍBANO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 8, 6, N'TIERRAS MORENAS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 8, 7, N'ARENAL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 9, 1, N'CARMONA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 9, 2, N'SANTA RITA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 9, 3, N'ZAPOTAL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 9, 4, N'SAN PABLO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 9, 5, N'PORVENIR')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 9, 6, N'BEJUCO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 10, 1, N'LA CRUZ')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 10, 2, N'SANTA CECILIA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 10, 3, N'LA GARITA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 10, 4, N'SANTA ELENA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 11, 1, N'HOJANCHA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 11, 2, N'MONTE ROMO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 11, 3, N'PUERTO CARRILLO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (5, 11, 4, N'HUACAS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 1, 1, N'PUNTARENAS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 1, 2, N'PITAHAYA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 1, 3, N'CHOMES')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 1, 4, N'LEPANTO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 1, 5, N'PAQUERA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 1, 6, N'MANZANILLO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 1, 7, N'GUACIMAL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 1, 8, N'BARRANCA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 1, 9, N'MONTE VERDE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 1, 11, N'CÓBANO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 1, 12, N'CHACARITA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 1, 13, N'CHIRA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 1, 14, N'ACAPULCO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 1, 15, N'EL ROBLE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 1, 16, N'ARANCIBIA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 2, 1, N'ESPÍRITU SANTO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 2, 2, N'SAN JUAN GRANDE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 2, 3, N'MACACONA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 2, 4, N'SAN RAFAEL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 2, 5, N'SAN JERÓNIMO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 2, 6, N'CALDERA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 3, 1, N'BUENOS AIRES')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 3, 2, N'VOLCÁN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 3, 3, N'POTRERO GRANDE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 3, 4, N'BORUCA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 3, 5, N'PILAS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 3, 6, N'COLINAS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 3, 7, N'CHÁNGUENA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 3, 8, N'BIOLLEY')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 3, 9, N'BRUNKA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 4, 1, N'MIRAMAR')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 4, 2, N'LA UNIÓN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 4, 3, N'SAN ISIDRO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 5, 1, N'PUERTO CORTÉS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 5, 2, N'PALMAR')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 5, 3, N'SIERPE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 5, 4, N'BAHÍA BALLENA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 5, 5, N'PIEDRAS BLANCAS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 5, 6, N'BAHÍA DRAKE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 6, 1, N'QUEPOS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 6, 2, N'SAVEGRE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 6, 3, N'NARANJITO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 7, 1, N'GOLFITO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 7, 2, N'PUERTO JIMÉNEZ')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 7, 3, N'GUAYCARÁ')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 7, 4, N'PAVÓN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 8, 1, N'SAN VITO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 8, 2, N'SABALITO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 8, 3, N'AGUABUENA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 8, 4, N'LIMONCITO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 8, 5, N'PITTIER')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 8, 6, N'GUTIERREZ BRAUN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 9, 1, N'PARRITA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 10, 1, N'CORREDOR')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 10, 2, N'LA CUESTA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 10, 3, N'CANOAS')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 10, 4, N'LAUREL')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 11, 1, N'JACÓ')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (6, 11, 2, N'TÁRCOLES')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (7, 1, 1, N'LIMÓN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (7, 1, 2, N'VALLE LA ESTRELLA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (7, 1, 4, N'MATAMA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (7, 2, 1, N'GUÁPILES')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (7, 2, 2, N'JIMÉNEZ')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (2, 14, 2, N'CAÑO NEGRO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (7, 2, 3, N'RITA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (7, 2, 4, N'ROXANA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (7, 2, 5, N'CARIARI')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (7, 2, 6, N'COLORADO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (7, 2, 7, N'LA COLONIA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (7, 3, 1, N'SIQUIRRES')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (7, 3, 2, N'PACUARITO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (7, 3, 3, N'FLORIDA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (7, 3, 4, N'GERMANIA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (7, 3, 5, N'EL CAIRO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (7, 3, 6, N'ALEGRÍA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (7, 4, 1, N'BRATSI')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (7, 4, 2, N'SIXAOLA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (7, 4, 3, N'CAHUITA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (7, 4, 4, N'TELIRE')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (7, 5, 1, N'MATINA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (7, 5, 2, N'BATÁN')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (7, 5, 3, N'CARRANDI')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (7, 6, 1, N'GUÁCIMO')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (7, 6, 2, N'MERCEDES')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (7, 6, 3, N'POCORA')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (7, 6, 4, N'RÍO JIMÉNEZ')
GO
INSERT [dbo].[TBL_DISTRITO] ([ID_PROVINCIA], [ID_CANTON], [ID_DISTRITO], [NOMBRE]) VALUES (7, 6, 5, N'DUACARÍ')
GO





INSERT INTO [dbo].[TBL_TRANSPORTES] 
(Descripcion, Estado, FK_Usuario_Creacion, FK_Usuario_Modificacion, Fecha_Creacion, Fecha_Modificacion)
VALUES 
('Personal', '1', 'user1', NULL, GETDATE(), NULL),
('Caribeños', '1', 'user2', NULL, GETDATE(), NULL);


INSERT INTO [dbo].[TBL_CONDICIONES_PAGO] 
(Descripcion, Dias, Estado, FK_Usuario_Creacion, FK_Usuario_Modificacion, Fecha_Creacion, Fecha_Modificacion)
VALUES 
('Contado', 0, '1', 'user1', NULL, GETDATE(), NULL),
('Crédito 30 días', 30, '1', 'user2', NULL, GETDATE(), NULL),
('Crédito 60 días', 60, '1', 'user3', NULL, GETDATE(), NULL),
('Crédito 90 días', 90, '1', 'user4', NULL, GETDATE(), NULL),
('Anticipo 50%', 15, '1', 'user5', NULL, GETDATE(), NULL);


INSERT INTO [dbo].[TBL_VENDEDORES] 
    (Nombre, Telefono, Correo, Estado, FK_Usuario_Creacion, Fecha_Creacion)
VALUES 
    ('Juan Perez', '1234567890', 'juan.perez@example.com', 1, 'UsuarioCreador', GETDATE()),
    ('Maria Lopez', '0987654321', 'maria.lopez@example.com', 1, 'UsuarioCreador', GETDATE()),
    ('Carlos Jimenez', '1122334455', 'carlos.jimenez@example.com', 1, 'UsuarioCreador', GETDATE()),
    ('Ana Gonzalez', '2233445566', 'ana.gonzalez@example.com', 1, 'UsuarioCreador', GETDATE()),
    ('Luis Fernandez', '3344556677', 'luis.fernandez@example.com', 1, 'UsuarioCreador', GETDATE());


INSERT INTO [dbo].[TBL_CLIENTES] 
(PK_Cliente, TipoIdentificacion, Identificacion, Nombre, Telefono, Correo, Provincia, Canton, Distrito, OtrasSenas, Estado, FK_CondicionPago, FK_Transporte, FK_Vendedor, FK_Usuario_Creacion, FK_Usuario_Modificacion, Fecha_Creacion, Fecha_Modificacion)
VALUES 
('C001', 'Fisico', 101010101, 'Empresa ABC', '876543210', 'contacto@empresaabc.com', '1', '1', '1', 'Dirección 1', '1', 1, 1, 1, 'user1', NULL, GETDATE(), NULL),
('C002', 'Fisico', 202020202, 'Empresa XYZ', '987654321', 'contacto@empresaxyz.com', '2', '2', '2', 'Dirección 2', '1', 2, 2, 2, 'user2', NULL, GETDATE(), NULL),
('C003', 'Fisico', 303030303, 'Empresa DEF', '654321098', 'contacto@empresadef.com', '3', '3', '3', 'Dirección 3', '1', 3, 1, 3, 'user3', NULL, GETDATE(), NULL),
('C004', 'Fisico', 404040404, 'Empresa GHI', '543210987', 'contacto@empresaghi.com', '1', '2', '3', 'Dirección 4', '1', 4, 2, 4, 'user4', NULL, GETDATE(), NULL),
('C005', 'Fisico', 505050505, 'Empresa JKL', '432109876', 'contacto@empresajkl.com', '4', '1', '1', 'Dirección 5', '1', 5, 1, 5, 'user5', NULL, GETDATE(), NULL);

INSERT INTO [dbo].[TBL_PROVEEDORES]
( Nombre, Telefono, Correo, Direccion, Estado, FK_Usuario_Creacion, Fecha_Creacion)
VALUES
 ('Proveedor 1', '123-456-7890', 'proveedor1@empresa.com', 'Calle 1, Ciudad A', 1, 'usuario_creacion1', GETDATE()),
 ('Proveedor 2', '234-567-8901', 'proveedor2@empresa.com', 'Calle 2, Ciudad B', 1, 'usuario_creacion2', GETDATE()),
 ('Proveedor 3', '345-678-9012', 'proveedor3@empresa.com', 'Calle 3, Ciudad C', 1, 'usuario_creacion3', GETDATE()),
 ('Proveedor 4', '456-789-0123', 'proveedor4@empresa.com', 'Calle 4, Ciudad D', 1, 'usuario_creacion4', GETDATE()),
 ('Proveedor 5', '567-890-1234', 'proveedor5@empresa.com', 'Calle 5, Ciudad E', 1, 'usuario_creacion5', GETDATE());

-- Insertar datos de ejemplo en la tabla TBL_ARTICULO
INSERT INTO [dbo].[TBL_ARTICULO] 
(
    PK_Articulo, 
    Descripcion, 
    Codigo_Barras, 
    FK_Proveedor, 
    Cantidad, 
    Costo, 
    Precio, 
    Estado, 
    FK_Usuario_Creacion, 
    FK_Usuario_Modificacion, 
    Fecha_Creacion, 
    Fecha_Modificacion
) 
VALUES 
(
    'ART001', 
    'Camiseta de Algodón Blanca', 
    '123456789012', 
    'PROV001', 
    100, 
    5.00, 
    10.00, 
    1, 
    'Usuario1', 
    'Usuario1', 
    GETDATE(), 
    GETDATE()
);

INSERT INTO [dbo].[TBL_ARTICULO] 
(
    PK_Articulo, 
    Descripcion, 
    Codigo_Barras, 
    FK_Proveedor, 
    Cantidad, 
    Costo, 
    Precio, 
    Estado, 
    FK_Usuario_Creacion, 
    FK_Usuario_Modificacion, 
    Fecha_Creacion, 
    Fecha_Modificacion
) 
VALUES 
(
    'ART002', 
    'Jeans Denim Azul', 
    '123456789013', 
    'PROV002', 
    50, 
    20.00, 
    40.00, 
    1, 
    'Usuario1', 
    'Usuario1', 
    GETDATE(), 
    GETDATE()
);

INSERT INTO [dbo].[TBL_ARTICULO] 
(
    PK_Articulo, 
    Descripcion, 
    Codigo_Barras, 
    FK_Proveedor, 
    Cantidad, 
    Costo, 
    Precio, 
    Estado, 
    FK_Usuario_Creacion, 
    FK_Usuario_Modificacion, 
    Fecha_Creacion, 
    Fecha_Modificacion
) 
VALUES 
(
    'ART003', 
    'Chaqueta de Cuero Negra', 
    '123456789014', 
    'PROV003', 
    30, 
    50.00, 
    100.00, 
    1, 
    'Usuario1', 
    'Usuario1', 
    GETDATE(), 
    GETDATE()
);

INSERT INTO [dbo].[TBL_ARTICULO] 
(
    PK_Articulo, 
    Descripcion, 
    Codigo_Barras, 
    FK_Proveedor, 
    Cantidad, 
    Costo, 
    Precio, 
    Estado, 
    FK_Usuario_Creacion, 
    FK_Usuario_Modificacion, 
    Fecha_Creacion, 
    Fecha_Modificacion
) 
VALUES 
(
    'ART004', 
    'Falda Plisada Roja', 
    '123456789015', 
    'PROV004', 
    75, 
    15.00, 
    30.00, 
    1, 
    'Usuario1', 
    'Usuario1', 
    GETDATE(), 
    GETDATE()
);

INSERT INTO [dbo].[TBL_ARTICULO] 
(
    PK_Articulo, 
    Descripcion, 
    Codigo_Barras, 
    FK_Proveedor, 
    Cantidad, 
    Costo, 
    Precio, 
    Estado, 
    FK_Usuario_Creacion, 
    FK_Usuario_Modificacion, 
    Fecha_Creacion, 
    Fecha_Modificacion
) 
VALUES 
(
    'ART005', 
    'Suéter de Lana Gris', 
    '123456789016', 
    'PROV005', 
    60, 
    25.00, 
    50.00, 
    1, 
    'Usuario1', 
    'Usuario1', 
    GETDATE(), 
    GETDATE()
);

INSERT INTO [dbo].[TBL_ARTICULO] 
(
    [PK_Articulo], 
    [Descripcion], 
    [Codigo_Barras], 
    [FK_Proveedor], 
    [Cantidad], 
    [Costo], 
    [Precio], 
    [Estado], 
    [FK_Usuario_Creacion], 
    [Fecha_Creacion]
)
VALUES 
(
    'ART011', 
    'Artículo A', 
    '123456789012', 
    (SELECT TOP 1 PK_Proveedor FROM [dbo].[TBL_PROVEEDORES] WHERE [Nombre] = 'Proveedor 1'),
    50, 
    25.00, 
    45.00, 
    1, 
    'usuario_creacion2', 
    GETDATE()
);


INSERT INTO TBL_USUARIO (PK_IdUsuario, Nombre, Correo, Contra, Rol, Estado, FK_Usuario_Creacion, FK_Usuario_Modificacion, Fecha_Creacion, Fecha_Modificacion)
VALUES 
    (1, 'usuario1', 'usuario1@example.com', 'password123', 'Administrador', 1, 'admin', 'admin', GETDATE(), GETDATE()),
    (2, 'usuario2', 'usuario2@example.com', 'qwerty456', 'Usuario', 1, 'admin', 'admin', GETDATE(), GETDATE()),
    (3, 'usuario3', 'usuario3@example.com', 'abcde789', 'Usuario', 0, 'admin', 'admin', GETDATE(), GETDATE());


