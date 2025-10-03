CREATE DATABASE proyectoLab;
GO
USE proyectoLab;

CREATE TABLE tbUsuario(
	idUsuario INT PRIMARY KEY IDENTITY(1,1),
	nombreUsuario VARCHAR(50) NOT NULL,
	correo VARCHAR(100) NOT NULL,
	pass VARCHAR(255) NOT NULL,
	rol VARCHAR(20) NOT NULL DEFAULT 'User',
	estado BIT NOT NULL DEFAULT 1
);

CREATE TABLE tbCatPreguntas(
    idCatPregunta INT PRIMARY KEY IDENTITY(1,1),
    pregunta VARCHAR(200) NOT NULL
);

CREATE TABLE tbPreguntaSeguridad(
    idPregunta INT PRIMARY KEY IDENTITY(1,1),
    idUsuario INT NOT NULL,
    idCatPregunta1 INT NOT NULL,
    respuesta1 VARCHAR(200) NOT NULL,
    idCatPregunta2 INT NOT NULL,
    respuesta2 VARCHAR(200) NOT NULL,
    FOREIGN KEY (idUsuario) REFERENCES tbUsuario(idUsuario),
    FOREIGN KEY (idCatPregunta1) REFERENCES tbCatPreguntas(idCatPregunta),
    FOREIGN KEY (idCatPregunta2) REFERENCES tbCatPreguntas(idCatPregunta)
);

CREATE TABLE tbFormulario(
	idFormulario INT PRIMARY KEY IDENTITY(1,1),
	nombreFormulario VARCHAR(100) NOT NULL,
	descripcion VARCHAR(200)
);

CREATE TABLE tbPermisoFormulario(
	idPermiso INT PRIMARY KEY IDENTITY(1,1),
	idUsuario INT NOT NULL,
	idFormulario INT NOT NULL,
	lectura BIT NOT NULL DEFAULT 0,
	escritura BIT NOT NULL DEFAULT 0,
	eliminacion BIT NOT NULL DEFAULT 0,
	FOREIGN KEY (idUsuario) REFERENCES tbUsuario(idUsuario),
	FOREIGN KEY (idFormulario) REFERENCES tbFormulario(idFormulario)
);

CREATE TABLE tbAccesoFormulario(
	idAcceso INT PRIMARY KEY IDENTITY(1,1),
	idUsuario INT NOT NULL,
	idFormulario INT NOT NULL,
	FOREIGN KEY (idUsuario) REFERENCES tbUsuario(idUsuario),
	FOREIGN KEY (idFormulario) REFERENCES tbFormulario(idFormulario)
);
INSERT INTO tbCatPreguntas VALUES ('Mejor amigo');
INSERT INTO tbCatPreguntas VALUES ('Primera Mascota');
--Insertando formularios por default
INSERT INTO tbFormulario (nombreFormulario, descripcion) VALUES ('Gestionar Permisos', 'Formulario para el manejo de permisos de los usuarios');
INSERT INTO tbFormulario (nombreFormulario, descripcion) VALUES ('Gestionar Usuarios', 'Formulario para el manejo de Usuarios');
SELECT * FROM tbFormulario;
SELECT * FROM tbUsuario;