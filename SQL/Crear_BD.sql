USE master;

CREATE DATABASE LAVADERO_DB;

USE LAVADERO_DB;

DROP TABLE IF EXISTS Turnos, Servicios, Rubros, TipoVehiculo, Imagenes, FechaHora, TipoVehiculoRubro, RubroServicio;

DROP TABLE IF EXISTS Precios

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

CREATE TABLE HorariosTurnos (
    Id INT PRIMARY KEY IDENTITY(1,1),
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

ALTER TABLE Rubros ADD Estado INT NOT NULL DEFAULT 1;
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
    Tiempo DECIMAL(5, 2) NOT NULL
);

CREATE TABLE RubroServicio (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdRubro INT NOT NULL,
    IdServicio INT NOT NULL,
    CONSTRAINT FK_RubroServicio_Rubro FOREIGN KEY (IdRubro) REFERENCES Rubros(Id),
    CONSTRAINT FK_RubroServicio_Servicio FOREIGN KEY (IdServicio) REFERENCES Servicios(Id)
);

ALTER TABLE Turnos
ALTER COLUMN Precio DECIMAL(10, 2) NOT NULL;

CREATE TABLE Turnos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdCliente INT NOT NULL,
    IdTipoVehiculo INT NOT NULL,
    IdRubro INT NOT NULL,
    IdServicio INT NOT NULL,
    Aclaracion NVARCHAR(MAX),
    FechaHora DateTime NOT NULL,
    IdEstado INT NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL,
    CONSTRAINT FK_Turno_Cliente FOREIGN KEY (IdCliente) REFERENCES Usuarios(Id),
    CONSTRAINT FK_Turno_TipoVehiculo FOREIGN KEY (IdTipoVehiculo) REFERENCES TipoVehiculo(Id),
    CONSTRAINT FK_Turno_Rubro FOREIGN KEY (IdRubro) REFERENCES Rubros(Id),
    CONSTRAINT FK_Turno_Servicio FOREIGN KEY (IdServicio) REFERENCES Servicios(Id),
    CONSTRAINT FK_Turno_Estado FOREIGN KEY (IdEstado) REFERENCES EstadoTurnos(Id),
);

CREATE TABLE Precios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdTipoVehiculo INT NOT NULL,
    IdRubro INT NOT NULL,
    IdServicio INT NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL,
    CONSTRAINT FK_Precio_TipoVehiculo FOREIGN KEY (IdTipoVehiculo) REFERENCES TipoVehiculo(Id),
    CONSTRAINT FK_Precio_Rubro FOREIGN KEY (IdRubro) REFERENCES Rubros(Id),
    CONSTRAINT FK_Precio_Servicio FOREIGN KEY (IdServicio) REFERENCES Servicios(Id)
);


select * from Precios

INSERT INTO Precios (IdTipoVehiculo, IdRubro, IdServicio, Precio)
VALUES
-- COUPE
(1, 1, 1, 15.00), -- COUPE, LAVADERO, Lavado basico
(1, 1, 2, 20.00), -- COUPE, LAVADERO, Lavado Premium
(1, 1, 3, 30.00), -- COUPE, LAVADERO, Pulido y Encerado
(1, 2, 5, 55.00), -- COUPE, DETAILING, Detailing Completo
(1, 3, 4, 35.00), -- COUPE, LUBRICENTRO, Cambio de Aceite

-- DEPORTIVO
(2, 1, 1, 20.00), -- DEPORTIVO, LAVADERO, Lavado basico
(2, 1, 2, 25.00), -- DEPORTIVO, LAVADERO, Lavado Premium
(2, 1, 3, 35.00), -- DEPORTIVO, LAVADERO, Pulido y Encerado
(2, 2, 5, 60.00), -- DEPORTIVO, DETAILING, Detailing Completo
(2, 3, 4, 40.00), -- DEPORTIVO, LUBRICENTRO, Cambio de Aceite

-- CAMION
(3, 1, 1, 30.00), -- CAMION, LAVADERO, Lavado basico
(3, 1, 2, 35.00), -- CAMION, LAVADERO, Lavado Premium
(3, 1, 3, 45.00), -- CAMION, LAVADERO, Pulido y Encerado
(3, 2, 5, 70.00), -- CAMION, DETAILING, Detailing Completo
(3, 3, 4, 50.00), -- CAMION, LUBRICENTRO, Cambio de Aceite

-- MINI
(4, 1, 1, 10.00), -- MINI, LAVADERO, Lavado basico
(4, 1, 2, 15.00), -- MINI, LAVADERO, Lavado Premium
(4, 1, 3, 25.00), -- MINI, LAVADERO, Pulido y Encerado
(4, 2, 5, 50.00), -- MINI, DETAILING, Detailing Completo
(4, 3, 4, 30.00), -- MINI, LUBRICENTRO, Cambio de Aceite

-- 4X4
(5, 1, 1, 25.00), -- 4X4, LAVADERO, Lavado basico
(5, 1, 2, 30.00), -- 4X4, LAVADERO, Lavado Premium
(5, 1, 3, 40.00), -- 4X4, LAVADERO, Pulido y Encerado
(5, 2, 5, 65.00), -- 4X4, DETAILING, Detailing Completo
(5, 3, 4, 45.00), -- 4X4, LUBRICENTRO, Cambio de Aceite

-- SEDAN
(6, 1, 1, 15.00), -- SEDAN, LAVADERO, Lavado basico
(6, 1, 2, 20.00), -- SEDAN, LAVADERO, Lavado Premium
(6, 1, 3, 30.00), -- SEDAN, LAVADERO, Pulido y Encerado
(6, 2, 5, 55.00), -- SEDAN, DETAILING, Detailing Completo
(6, 3, 4, 35.00), -- SEDAN, LUBRICENTRO, Cambio de Aceite

-- SUV
(7, 1, 1, 25.00), -- SUV, LAVADERO, Lavado basico
(7, 1, 2, 35.00), -- SUV, LAVADERO, Lavado Premium
(7, 1, 3, 45.00), -- SUV, LAVADERO, Pulido y Encerado
(7, 2, 5, 70.00), -- SUV, DETAILING, Detailing Completo
(7, 3, 4, 50.00), -- SUV, LUBRICENTRO, Cambio de Aceite

-- VAN
(8, 1, 1, 30.00), -- VAN, LAVADERO, Lavado basico
(8, 1, 2, 40.00), -- VAN, LAVADERO, Lavado Premium
(8, 1, 3, 50.00), -- VAN, LAVADERO, Pulido y Encerado
(8, 2, 5, 75.00), -- VAN, DETAILING, Detailing Completo
(8, 3, 4, 55.00), -- VAN, LUBRICENTRO, Cambio de Aceite

-- FAMILIAR
(9, 1, 1, 30.00), -- FAMILIAR, LAVADERO, Lavado basico
(9, 1, 2, 40.00), -- FAMILIAR, LAVADERO, Lavado Premium
(9, 1, 3, 50.00), -- FAMILIAR, LAVADERO, Pulido y Encerado
(9, 2, 5, 75.00), -- FAMILIAR, DETAILING, Detailing Completo
(9, 3, 4, 55.00); -- FAMILIAR, LUBRICENTRO, Cambio de Aceite


SELECT * from Precios


CREATE TABLE EstadoTurnos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Descripcion NVARCHAR(255)
);

INSERT INTO EstadoTurnos (Descripcion)
VALUES 
    ('Pendiente'), 
    ('En Proceso'),
    ('Completado'), 
    ('Cancelado');



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
INSERT INTO Imagenes (UrlImagen) VALUES ('https://iili.io/2uKGiYl.png'); --DETAILING
INSERT INTO Imagenes (UrlImagen) VALUES ('https://iili.io/2uKG44f.png'); --LUBRICENTRO
INSERT INTO Imagenes (UrlImagen) VALUES ('https://iili.io/2RF4GrG.png'); --MECANICA 
INSERT INTO Imagenes (UrlImagen) VALUES ('https://iili.io/2RF4V2f.png'); --CUBIERTAS 

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
INSERT INTO TipoVehiculo(Nombre, Descripcion, IdImagen) values ('MOTO','Vehiculo chico', 10)

--RUBROS--

INSERT INTO Rubros(Nombre, Descripcion, IdImagen) Values ('LAVADERO','Servicio: Lavado, aspirado y encerado', 11)
INSERT INTO Rubros(Nombre, Descripcion, IdImagen) Values ('DETAILING','Servicio: Detallado, pulido y encerado', 12)
INSERT INTO Rubros(Nombre, Descripcion, IdImagen) Values ('LUBRICENTRO','Servicio: Cambio aceite, cambio de filtros y cambio de fluidos', 13)
INSERT INTO Rubros(Nombre, Descripcion, IdImagen) Values ('MECANICA','Servicio: Mecanica integral', 14)
INSERT INTO Rubros(Nombre, Descripcion, IdImagen) Values ('CUBIERTAS','Servicio: Cambio de cubiertas, alineacion y balanceo', 15)

INSERT INTO Servicios (Nombre, Descripcion, Tiempo) VALUES 
-- Lavadero (ID Rubro 1)
('Lavado Completo', 'Lavado interior y exterior de vehículos', 2.5),
('Lavado Exterior', 'Solo limpieza externa del vehículo', 1.5),
('Lavado Motor', 'Limpieza del motor con productos especiales', 1.0),
('Lavado Alfombras', 'Limpieza profunda de alfombras del vehículo', 1.2),
('Pulido y Encerado', 'Pulido y encerado completo de la carrocería', 3.0),

-- Detailing (ID Rubro 2)
('Pulido Detallado', 'Pulido especializado para brillo y corrección de pintura', 4.0),
('Protección Cerámica', 'Aplicación de revestimiento cerámico', 5.0),
('Lavado Premium', 'Lavado detallado con productos de alta calidad', 2.0),
('Limpieza Interior Profunda', 'Desinfección y limpieza de tapizados y plásticos', 3.0),
('Restauración Faros', 'Restauración de ópticas opacas', 1.5),

-- Lubricentro (ID Rubro 3)
('Cambio de Aceite', 'Sustitución de aceite y filtro', 0.5),
('Cambio de Filtros', 'Reemplazo de filtros de aire y combustible', 0.8),
('Engrase de Partes', 'Lubricación de partes móviles', 1.0),
('Revisión de Líquidos', 'Chequeo y llenado de líquidos esenciales', 0.7),
('Cambio de Aceite Sintético', 'Cambio de aceite sintético para motores modernos', 0.6),

-- Mecánica (ID Rubro 4)
('Revisión General', 'Diagnóstico completo del vehículo', 1.5),
('Cambio de Pastillas de Freno', 'Reemplazo de pastillas de freno delanteras o traseras', 2.0),
('Alineación y Balanceo', 'Ajuste de suspensión y balanceo de ruedas', 1.5),
('Reparación de Suspensión', 'Reparación de amortiguadores y partes relacionadas', 3.5),
('Cambio de Correa', 'Sustitución de correa de distribución', 4.0),

-- Gomería (ID Rubro 5)
('Cambio de Neumáticos', 'Reemplazo de neumáticos', 1.0),
('Reparación de Pinchazos', 'Reparación de pinchaduras en neumáticos', 0.5),
('Inflado de Neumáticos', 'Ajuste de presión de neumáticos', 0.2),
('Rotación de Neumáticos', 'Cambio de posición de las ruedas', 1.0),
('Balanceo de Ruedas', 'Equilibrado dinámico de ruedas', 1.2);



INSERT INTO RubroServicio (IdRubro, IdServicio) VALUES 
-- Lavadero
(1, 1), (1, 2), (1, 3), (1, 4), (1, 5),

-- Detailing
(2, 6), (2, 7), (2, 8), (2, 9), (2, 10),

-- Lubricentro
(3, 11), (3, 12), (3, 13), (3, 14), (3, 15),

-- Mecánica
(4, 16), (4, 17), (4, 18), (4, 19), (4, 20),

-- Gomería
(5, 21), (5, 22), (5, 23), (5, 24), (5, 25);

--agregar horarios y fechas
drop table HorariosTurnos
delete from HorariosTurnos
INSERT INTO HorariosTurnos(Hora, Disponible)
VALUES 
('08:00:00', 1),
('09:00:00', 1),
('10:00:00', 1),
('11:00:00', 1),
('12:00:00', 1),
('13:00:00', 1),
('14:00:00', 1),
('15:00:00', 1),
('16:00:00', 1),
('17:00:00', 1),
('18:00:00', 1),
('19:00:00', 1);

select *from HorariosTurnos


SELECT R.Nombre AS Rubro, S.Nombre AS Servicio
FROM Rubros R
INNER JOIN RubroServicio RS ON R.Id = RS.IdRubro
INNER JOIN Servicios S ON RS.IdServicio = S.Id;


SELECT r.Nombre AS Rubro, s.Nombre AS Servicio
FROM RubroServicio rs
INNER JOIN Rubros r ON r.Id = rs.IdRubro
INNER JOIN Servicios s ON s.Id = rs.IdServicio




