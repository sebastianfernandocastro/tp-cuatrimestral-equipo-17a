use master

create database LAVADERO_DB


use LAVADERO_DB


create TABLE Usuarios(
ID Int identity(1,1) PRIMARY key,
Nombre varchar(30) not null,
Apellido varchar(30) not null,
Usuario varchar(20) not null unique,
Contrasenia varchar(20) not null,
Tipo int not null,
DNI int null unique,
MAIL varchar(40) null unique,
Telefono VARCHAR(20) null,
Legajo varchar(5) null unique,
NivelAcceso int null,
estado int not null 
)
