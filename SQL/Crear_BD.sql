use master

create database LAVADERO_DB

use LAVADERO_DB

create TABLE Cliente(
ID Int identity(1,1) PRIMARY key,
Nombre varchar(30) not null,
Apellido varchar(30) not null,
DNI int not null unique,
MAIL varchar(40) not null unique,
TELEFONO VARCHAR(20) not null,
Usuario varchar(20) not null unique,
Contrasenia varchar(20) not null,
)

Create TABLE Empleado(
    
)
