CREATE TABLE [dbo].[tbFormulario] (
    [idFormulario]     INT           IDENTITY (1, 1) NOT NULL,
    [nombreFormulario] VARCHAR (100) NOT NULL,
    [descripcion]      VARCHAR (200) NULL,
    [idUsuario] INT NOT NULL, 
    [permiso] BIT NOT NULL, 
    PRIMARY KEY CLUSTERED ([idFormulario] ASC)
);
SELECT * FROM tbFormulario;
