USE master;

CREATE DATABASE LAVADERO_DB;

USE LAVADERO_DB;

DROP TABLE IF EXISTS Turnos, Servicios, Rubros, TipoVehiculo, Imagenes, FechaHora, Usuarios, TipoVehiculoRubro, RubroServicio;

CREATE TABLE Usuarios(
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(30) NOT NULL,
    Apellido VARCHAR(30) NOT NULL,
    Usuario VARCHAR(20) NOT NULL UNIQUE,
    Contrasenia VARCHAR(20) NOT NULL,
    Tipo INT NOT NULL,
    DNI VARCHAR(8) NULL,
    MAIL VARCHAR(40) NULL,
    Telefono VARCHAR(20) NULL,
    Legajo VARCHAR(5) NULL,
    NivelAcceso INT NULL,
    Estado INT NOT NULL
);

CREATE UNIQUE INDEX IX_Usuarios_DNI ON Usuarios(DNI) WHERE DNI IS NOT NULL;
CREATE UNIQUE INDEX IX_Usuarios_MAIL ON Usuarios(MAIL) WHERE MAIL IS NOT NULL;
CREATE UNIQUE INDEX IX_Usuarios_Legajo ON Usuarios(Legajo) WHERE Legajo IS NOT NULL;

drop table NivelAcceso
create table NivelAcceso(
	Id int identity(1,1) primary key,
	Descripcion varchar(40) not null
)
delete NivelAcceso
insert into NivelAcceso (Descripcion) values('Admin'), ('Empleado')


CREATE TABLE FechaHora (
    IdFechaHora INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATE NOT NULL,
    Hora TIME NOT NULL,
    Disponible BIT NOT NULL
);

CREATE TABLE Imagenes (
    IdImagen INT PRIMARY KEY IDENTITY(1,1),
    UrlImagen NVARCHAR(255) NOT NULL
);

CREATE TABLE TipoVehiculo (
    IdTipoVehiculo INT PRIMARY KEY IDENTITY(1,1),
    Codigo INT NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(MAX),
    IdImagen INT,
    CONSTRAINT FK_TipoVehiculo_Imagen FOREIGN KEY (IdImagen) REFERENCES Imagenes(IdImagen)
);

CREATE TABLE Rubros (
    IdRubro INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(MAX)
);

CREATE TABLE TipoVehiculoRubro (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdTipoVehiculo INT NOT NULL,
    IdRubro INT NOT NULL,
    CONSTRAINT FK_TipoVehiculoRubro_TipoVehiculo FOREIGN KEY (IdTipoVehiculo) REFERENCES TipoVehiculo(IdTipoVehiculo),
    CONSTRAINT FK_TipoVehiculoRubro_Rubro FOREIGN KEY (IdRubro) REFERENCES Rubros(IdRubro)
);

CREATE TABLE Servicios (
    IdServicio INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(MAX),
    Tiempo DECIMAL(5, 2) NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL
);

CREATE TABLE RubroServicio (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdRubro INT NOT NULL,
    IdServicio INT NOT NULL,
    CONSTRAINT FK_RubroServicio_Rubro FOREIGN KEY (IdRubro) REFERENCES Rubros(IdRubro),
    CONSTRAINT FK_RubroServicio_Servicio FOREIGN KEY (IdServicio) REFERENCES Servicios(IdServicio)
);

CREATE TABLE Turnos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdCliente INT NOT NULL,
    IdTipoVehiculo INT NOT NULL,
    IdRubro INT NOT NULL,
    IdServicio INT NOT NULL,
    Aclaracion NVARCHAR(MAX),
    IdFechaHora INT NOT NULL,
    Estado NVARCHAR(50),
    CONSTRAINT FK_Turno_Cliente FOREIGN KEY (IdCliente) REFERENCES Usuarios(ID),
    CONSTRAINT FK_Turno_TipoVehiculo FOREIGN KEY (IdTipoVehiculo) REFERENCES TipoVehiculo(IdTipoVehiculo),
    CONSTRAINT FK_Turno_Rubro FOREIGN KEY (IdRubro) REFERENCES Rubros(IdRubro),
    CONSTRAINT FK_Turno_Servicio FOREIGN KEY (IdServicio) REFERENCES Servicios(IdServicio),
    CONSTRAINT FK_Turno_FechaHora FOREIGN KEY (IdFechaHora) REFERENCES FechaHora(IdFechaHora)
);


-- Relacionar servicios con el rubro 'Lavado'
INSERT INTO RubroServicio (IdRubro, IdServicio)
VALUES 
((SELECT IdRubro FROM Rubros WHERE Nombre = 'Lavado'), (SELECT IdServicio FROM Servicios WHERE Nombre = 'Lavado Básico')),
((SELECT IdRubro FROM Rubros WHERE Nombre = 'Lavado'), (SELECT IdServicio FROM Servicios WHERE Nombre = 'Lavado Premium')),
((SELECT IdRubro FROM Rubros WHERE Nombre = 'Lavado'), (SELECT IdServicio FROM Servicios WHERE Nombre = 'Pulido y Encerado'));

-- Relacionar servicios con el rubro 'Lubricentro'
INSERT INTO RubroServicio (IdRubro, IdServicio)
VALUES 
((SELECT IdRubro FROM Rubros WHERE Nombre = 'Lubricentro'), (SELECT IdServicio FROM Servicios WHERE Nombre = 'Cambio de Aceite'));

-- Relacionar servicios con el rubro 'Detailing'
INSERT INTO RubroServicio (IdRubro, IdServicio)
VALUES 
((SELECT IdRubro FROM Rubros WHERE Nombre = 'Detailing'), (SELECT IdServicio FROM Servicios WHERE Nombre = 'Detailing Completo'));


SELECT R.Nombre AS Rubro, S.Nombre AS Servicio
FROM Rubros R
INNER JOIN RubroServicio RS ON R.IdRubro = RS.IdRubro
INNER JOIN Servicios S ON RS.IdServicio = S.IdServicio;



SELECT COLUMN_NAME, IS_NULLABLE, DATA_TYPE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Turnos';


INSERT INTO FechaHora (Fecha, Hora, Disponible)
VALUES ('2023-12-01', '10:00:00', 1);

INSERT INTO Turnos (IdCliente, IdTipoVehiculo, IdRubro, IdServicio, IdFechaHora, Estado, Aclaracion)
VALUES (1, 1, 1, 1, 1, 'Pendiente', 'Ninguna aclaración');
