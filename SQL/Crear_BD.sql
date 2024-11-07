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
