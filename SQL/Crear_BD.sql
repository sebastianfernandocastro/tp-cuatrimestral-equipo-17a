use master

create database LAVADERO_DB

use LAVADERO_DB

create TABLE USUARIOS(
ID Int PRIMARY key,
Nombre varchar(30) not null,
Apellido varchar(30) not null,
DNI int not null,
MAIL varchar(40) not null,
TELEFONO VARCHAR(20) not null,
Usuario varchar(20) not null,
Contrasenia varchar(20) not null,
)
