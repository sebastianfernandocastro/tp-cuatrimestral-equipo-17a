use master

create database LAVADERO_DB


use LAVADERO_DB

drop table Usuarios
create TABLE Usuarios(
ID Int identity(1,1) PRIMARY key,
Nombre varchar(30) not null,
Apellido varchar(30) not null,
Usuario varchar(20) not null unique,
Contrasenia varchar(20) not null,
Tipo int not null,
DNI varchar(8) null ,
MAIL varchar(40) null ,
Telefono VARCHAR(20) null,
Legajo varchar(5) null ,
NivelAcceso int null,
estado int not null 
)

CREATE UNIQUE INDEX IX_Usuarios_DNI ON Usuarios(DNI) WHERE DNI IS NOT NULL;
CREATE UNIQUE INDEX IX_Usuarios_MAIL ON Usuarios(MAIL) WHERE MAIL IS NOT NULL;
CREATE UNIQUE INDEX IX_Usuarios_Legajo ON Usuarios(Legajo) WHERE Legajo IS NOT NULL;


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

CREATE TABLE Servicios (
    IdServicio INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(MAX),
    Tiempo DECIMAL(5, 2) NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL
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
    
    CONSTRAINT FK_Turno_Cliente FOREIGN KEY (IdCliente) REFERENCES Usuarios(Id),
    CONSTRAINT FK_Turno_TipoVehiculo FOREIGN KEY (IdTipoVehiculo) REFERENCES TipoVehiculo(IdTipoVehiculo),
    CONSTRAINT FK_Turno_Rubro FOREIGN KEY (IdRubro) REFERENCES Rubros(IdRubro),
    CONSTRAINT FK_Turno_Servicio FOREIGN KEY (IdServicio) REFERENCES Servicios(IdServicio),
    CONSTRAINT FK_Turno_FechaHora FOREIGN KEY (IdFechaHora) REFERENCES FechaHora(IdFechaHora)
);
