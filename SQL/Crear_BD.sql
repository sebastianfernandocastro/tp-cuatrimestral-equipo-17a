USE master;

CREATE DATABASE LAVADERO_DB;

USE LAVADERO_DB;

DROP TABLE IF EXISTS Turnos, Servicios, Rubros, TipoVehiculo, Imagenes, FechaHora, TipoVehiculoRubro, RubroServicio;


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

create table NivelAcceso(
	Id int identity(1,1) primary key,
	Descripcion varchar(40) not null
)
insert into NivelAcceso (Descripcion) values('Admin'), ('Empleado')


CREATE TABLE FechaHora (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATE NOT NULL,
    Hora TIME NOT NULL,
    Disponible BIT NOT NULL
);

CREATE TABLE Imagenes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UrlImagen NVARCHAR(255) NOT NULL
);

CREATE TABLE TipoVehiculo (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(MAX),
    IdImagen INT,
    CONSTRAINT FK_TipoVehiculo_Imagen FOREIGN KEY (IdImagen) REFERENCES Imagenes(Id)
);

CREATE TABLE Rubros (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(MAX),
    IdImagen INT NOT NULL,
    CONSTRAINT FK_Rubro_Imagen FOREIGN KEY (IdImagen) REFERENCES Imagenes(Id)
);

CREATE TABLE TipoVehiculoRubro (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdTipoVehiculo INT NOT NULL,
    IdRubro INT NOT NULL,
    CONSTRAINT FK_TipoVehiculoRubro_TipoVehiculo FOREIGN KEY (IdTipoVehiculo) REFERENCES TipoVehiculo(Id),
    CONSTRAINT FK_TipoVehiculoRubro_Rubro FOREIGN KEY (IdRubro) REFERENCES Rubros(Id)
);

CREATE TABLE Servicios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(MAX),
    Tiempo DECIMAL(5, 2) NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL
);

CREATE TABLE RubroServicio (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdRubro INT NOT NULL,
    IdServicio INT NOT NULL,
    CONSTRAINT FK_RubroServicio_Rubro FOREIGN KEY (IdRubro) REFERENCES Rubros(Id),
    CONSTRAINT FK_RubroServicio_Servicio FOREIGN KEY (IdServicio) REFERENCES Servicios(Id)
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
    CONSTRAINT FK_Turno_TipoVehiculo FOREIGN KEY (IdTipoVehiculo) REFERENCES TipoVehiculo(Id),
    CONSTRAINT FK_Turno_Rubro FOREIGN KEY (IdRubro) REFERENCES Rubros(Id),
    CONSTRAINT FK_Turno_Servicio FOREIGN KEY (IdServicio) REFERENCES Servicios(Id),
    CONSTRAINT FK_Turno_FechaHora FOREIGN KEY (IdFechaHora) REFERENCES FechaHora(Id)
);


--ADMIN--
INSERT INTO Usuarios (Nombre, Apellido, Usuario, Contrasenia, Tipo, DNI, MAIL, Telefono, Legajo, NivelAcceso, Estado) VALUES ('Admin', 'Admin', 'Admin', 'Admin', 1, '0', 'admin', '123456789', 'A001', 1, 1);
INSERT INTO Usuarios (Nombre, Apellido, Usuario, Contrasenia, Tipo, DNI, MAIL, Telefono, Legajo, NivelAcceso, Estado) VALUES ('Miguel', 'Aitor', 'Miguel1', '12345', 3, '1', 'Trabajador', '12345678', 'A002', 2, 1);
INSERT INTO Usuarios (Nombre, Apellido, Usuario, Contrasenia, Tipo, DNI, MAIL, Telefono, Legajo, NivelAcceso, Estado) VALUES ('Jose', 'Sanchez', 'Jose09', '1234', 2, '2', 'Cliente', '1234567', 'A003', 3, 1);


--IMAGENES AUTOS--
INSERT INTO Imagenes (UrlImagen) VALUES ('https://iili.io/2uFNYyx.png');
INSERT INTO Imagenes (UrlImagen) VALUES ('https://iili.io/2uFNcTQ.png');
INSERT INTO Imagenes (UrlImagen) VALUES ('https://iili.io/2uFNljV.png');
INSERT INTO Imagenes (UrlImagen) VALUES ('https://iili.io/2uFN7vj.png');
INSERT INTO Imagenes (UrlImagen) VALUES ('https://iili.io/2uFNM6F.png');
INSERT INTO Imagenes (UrlImagen) VALUES ('https://iili.io/2uFNGG1.png');
INSERT INTO Imagenes (UrlImagen) VALUES ('https://iili.io/2uFNW3g.png');
INSERT INTO Imagenes (UrlImagen) VALUES ('https://iili.io/2uFNXaa.png');
INSERT INTO Imagenes (UrlImagen) VALUES ('https://iili.io/2uFNh8J.png');
INSERT INTO Imagenes (UrlImagen) VALUES ('https://iili.io/2AOnZFa.png');

--IMAGENES RUBROS--
INSERT INTO Imagenes (UrlImagen) VALUES ('https://iili.io/2uKGP24.png'); --LAVADERO
INSERT INTO Imagenes (UrlImagen) VALUES ('https://iili.io/2uKGiYl.png'); -- DETAILING
INSERT INTO Imagenes (UrlImagen) VALUES ('https://iili.io/2uKG44f.png'); -- LUBRICENTRO

--AUTOS--
INSERT INTO TipoVehiculo(Nombre, Descripcion, IdImagen) values ('COUPE','Auto chico',1)
INSERT INTO TipoVehiculo(Nombre, Descripcion, IdImagen) values ('DEPORTIVO','Auto mediano',2)
INSERT INTO TipoVehiculo(Nombre, Descripcion, IdImagen) values ('CAMION','Auto grande', 3)
INSERT INTO TipoVehiculo(Nombre, Descripcion, IdImagen) values ('MINI','Auto chico', 4)
INSERT INTO TipoVehiculo(Nombre, Descripcion, IdImagen) values ('4X4','Auto grande',5)
INSERT INTO TipoVehiculo(Nombre, Descripcion, IdImagen) values ('SEDAN','Auto mediano',6)
INSERT INTO TipoVehiculo(Nombre, Descripcion, IdImagen) values ('SUV','Auto grande', 7)
INSERT INTO TipoVehiculo(Nombre, Descripcion, IdImagen) values ('VAN','Auto grande', 8)
INSERT INTO TipoVehiculo(Nombre, Descripcion, IdImagen) values ('FAMILIAR','Auto grande', 9)
INSERT INTO TipoVehiculo(Nombre, Descripcion, IdImagen) values ('MOTO','Vehiculo chico', 13)

--RUBROS--

INSERT INTO Rubros(Nombre, Descripcion, IdImagen) Values ('LAVADERO','Servicio: Lavado, aspirado y encerado', 10)
INSERT INTO Rubros(Nombre, Descripcion, IdImagen) Values ('DETAILING','Servicio: Detallado, pulido y encerado', 11)
INSERT INTO Rubros(Nombre, Descripcion, IdImagen) Values ('LUBRICENTRO','Servicio: Cambio aceite, cambio de filtros y cambio de fluidos', 12)

INSERT INTO Servicios (Nombre, Descripcion, Tiempo, Precio)
VALUES 
('Lavado Básico', 'Limpieza exterior rápida', 0.5, 15.00),
('Lavado Premium', 'Limpieza exterior e interior completa', 1.0, 25.00),
('Pulido y Encerado', 'Pulido exterior y aplicación de cera protectora', 2.0, 50.00),
('Cambio de Aceite', 'Cambio de aceite y revisión básica del motor', 1.5, 35.00),
('Detailing Completo', 'Limpieza a fondo de todo el vehículo', 4.0, 100.00);


INSERT INTO RubroServicio (IdRubro, IdServicio)
VALUES 
((SELECT Id FROM Rubros WHERE Nombre = 'LAVADERO'), (SELECT Id FROM Servicios WHERE Nombre = 'Lavado Básico')),
((SELECT Id FROM Rubros WHERE Nombre = 'LAVADERO'), (SELECT Id FROM Servicios WHERE Nombre = 'Lavado Premium')),
((SELECT Id FROM Rubros WHERE Nombre = 'LAVADERO'), (SELECT Id FROM Servicios WHERE Nombre = 'Pulido y Encerado'));

INSERT INTO RubroServicio (IdRubro, IdServicio)
VALUES 
((SELECT Id FROM Rubros WHERE Nombre = 'LUBRICENTRO'), (SELECT Id FROM Servicios WHERE Nombre = 'Cambio de Aceite'));

INSERT INTO RubroServicio (IdRubro, IdServicio)
VALUES 
((SELECT Id FROM Rubros WHERE Nombre = 'Detailing'), (SELECT Id FROM Servicios WHERE Nombre = 'Detailing Completo'));

--agregar horarios y fechas
INSERT INTO FechaHora (Fecha, Hora, Disponible)
VALUES 
('2024-12-12', '08:00:00', 1),
('2024-12-12', '09:00:00', 1),
('2024-12-12', '10:00:00', 1),
('2024-12-12', '11:00:00', 1),
('2024-12-12', '12:00:00', 1),
('2024-12-12', '13:00:00', 1),
('2024-12-12', '14:00:00', 1),
('2024-12-12', '15:00:00', 1),
('2024-12-12', '16:00:00', 1),
('2024-12-12', '17:00:00', 1),
('2024-12-12', '18:00:00', 1);

SELECT R.Nombre AS Rubro, S.Nombre AS Servicio
FROM Rubros R
INNER JOIN RubroServicio RS ON R.Id = RS.IdRubro
INNER JOIN Servicios S ON RS.IdServicio = S.Id;


SELECT r.Nombre AS Rubro, s.Nombre AS Servicio
FROM RubroServicio rs
INNER JOIN Rubros r ON r.Id = rs.IdRubro
INNER JOIN Servicios s ON s.Id = rs.IdServicio
