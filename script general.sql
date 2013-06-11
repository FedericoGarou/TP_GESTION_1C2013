CREATE TABLE LOS_VIAJEROS_DEL_ANONIMATO.TIPOSERVICIO (
NombreServicio	nvarchar(255) PRIMARY KEY, 
PorcentajeAgregado	numeric(18,2) NOT NULL );

CREATE TABLE LOS_VIAJEROS_DEL_ANONIMATO.CIUDAD ( 
NombreCiudad nvarchar(255) PRIMARY KEY );

CREATE TABLE LOS_VIAJEROS_DEL_ANONIMATO.RECORRIDO (	
CodigoRecorrido numeric(18,0) IDENTITY ( 77774461 , 1 )PRIMARY KEY, 
--Cambiar esto harcodeado por el resultado de un subselect
CiudadOrigen nvarchar(255) REFERENCES LOS_VIAJEROS_DEL_ANONIMATO.CIUDAD(NombreCiudad),
CiudadDestino nvarchar(255) REFERENCES LOS_VIAJEROS_DEL_ANONIMATO.CIUDAD(NombreCiudad),
TipoServicio nvarchar(255) REFERENCES LOS_VIAJEROS_DEL_ANONIMATO.TIPOSERVICIO(NombreServicio),
PrecioBase numeric(18,2) NOT NULL,
PrecioBase_KG numeric(18,2) NOT NULL,
Habilitado bit NOT NULL );

CREATE TABLE LOS_VIAJEROS_DEL_ANONIMATO.Usuario (
DNI numeric(18,0), 
Nombre nvarchar(255), 
Apellido nvarchar(255), 
Direccion nvarchar(255),
Telefono numeric(18,0), 
Mail nvarchar(255),
Fecha_Nac datetime, 
Sexo varchar(9), 
Discapacidad bit, 
PRIMARY KEY(DNI) );

CREATE TABLE LOS_VIAJEROS_DEL_ANONIMATO.Rol (
Codigo_Rol int IDENTITY(1,1),
Nombre_Rol varchar (20), 
Habilitacion bit, 
PRIMARY KEY(Codigo_Rol) );

CREATE TABLE LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad (
Codigo_Funcionalidad int IDENTITY(1,1),
Nombre_Funcionalidad nvarchar(255), 
PRIMARY KEY(Codigo_Funcionalidad) );


CREATE TABLE LOS_VIAJEROS_DEL_ANONIMATO.Usuario_Rol (
DNI numeric(18,0) FOREIGN KEY REFERENCES LOS_VIAJEROS_DEL_ANONIMATO.Usuario(DNI) , 
Codigo_Rol int FOREIGN KEY REFERENCES LOS_VIAJEROS_DEL_ANONIMATO.Rol(Codigo_Rol), 
PRIMARY KEY (DNI, Codigo_Rol) );


CREATE TABLE LOS_VIAJEROS_DEL_ANONIMATO.Rol_Funcionalidad (
Codigo_Rol int FOREIGN KEY REFERENCES LOS_VIAJEROS_DEL_ANONIMATO.Rol(Codigo_Rol), 
Codigo_Funcionalidad int FOREIGN KEY REFERENCES LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad(Codigo_Funcionalidad), 
PRIMARY KEY (Codigo_Rol, Codigo_Funcionalidad) );


CREATE TABLE LOS_VIAJEROS_DEL_ANONIMATO.Login_Usuario (
Username nvarchar(255),
Passwd nvarchar(255), 
DNI_Usuario numeric(18,0) FOREIGN KEY REFERENCES LOS_VIAJEROS_DEL_ANONIMATO.Usuario(DNI), 
Intentos_Fallidos char(1), 
PRIMARY KEY (Username) );

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.TIPOSERVICIO
SELECT DISTINCT 
M.Tipo_Servicio,
( ( AVG(M.Pasaje_Precio) / AVG(M.Recorrido_Precio_BasePasaje) ) - 1 ) 
-- PORCENTAJE AGREGADO POR TIPO DE SERVICIO
FROM gd_esquema.Maestra M
GROUP BY M.Tipo_Servicio;

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.CIUDAD
SELECT DISTINCT gd_esquema.Maestra.Recorrido_Ciudad_Destino FROM gd_esquema.Maestra
UNION
SELECT DISTINCT gd_esquema.Maestra.Recorrido_Ciudad_Origen FROM gd_esquema.Maestra;

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.RECORRIDO
SELECT
		M.Recorrido_Ciudad_Origen, 
		M.Recorrido_Ciudad_Destino, 
		M.Tipo_Servicio,
		MAX(M.Recorrido_Precio_BasePasaje) as precio_base,
		MAX(M.Recorrido_Precio_BaseKG) as precio_base_kg,
		1 as habilitacion		
FROM gd_esquema.Maestra M
GROUP BY M.Recorrido_Codigo,M.Recorrido_Ciudad_Origen,M.Recorrido_Ciudad_Destino,M.Tipo_Servicio
ORDER BY M.Recorrido_Codigo;

INSERT into LOS_VIAJEROS_DEL_ANONIMATO.Usuario (DNI, Nombre, Apellido, Direccion, Telefono, Mail, Fecha_Nac )
SELECT distinct Cli_Dni, Cli_Nombre, Cli_Apellido, Cli_Dir, Cli_Telefono, Cli_Mail, Cli_Fecha_Nac
FROM gd_esquema.Maestra
;

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Rol values('Administrador', 1)
;

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Rol values('Cliente', 1)
;

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Usuario_Rol
SELECT DISTINCT Cli_DNI, 2
FROM gd_esquema.Maestra
;

insert into LOS_VIAJEROS_DEL_ANONIMATO.Usuario
values (35990001, 'ELOY', 'Kramar', 'CalleFalsa123', 47094444, 'elokramar@hotmail.com', '09-07-91', 'Masculino', 0)
;

insert into LOS_VIAJEROS_DEL_ANONIMATO.Usuario
values (30991234, 'LUCAS', 'Costas', 'Avenida Mitre1234', 48345469, 'lucascostasutn@gmail.com', '15-04-90', 'Masculino', 0)
;

insert into LOS_VIAJEROS_DEL_ANONIMATO.Usuario
values (31205999, 'MATIAS', 'Lorenzo', 'Roca4444', 47614860, 'mlorenzo@gmail.com', '30-10-90', 'Masculino', 0)
;

insert into LOS_VIAJEROS_DEL_ANONIMATO.Usuario
values (34604812, 'PABLO', 'Marbian', 'Avenida San Marin648', 47195488, 'pablo.marbian@hotmail.com', '24-01-89', 'Masculino', 0)
;

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Usuario_Rol 
values (35990001, 1)
;

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Usuario_Rol 
values (30991234, 1)
;

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Usuario_Rol 
values (31205999, 1)
;

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Usuario_Rol 
values (34604812, 1)
;

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Login_Usuario 
values ('eloykramar', 'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7', 35990001, 0)
;

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Login_Usuario 
values ('lucascostas', 'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7', 30991234, 0)
;

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Login_Usuario 
values ('matiaslorenzo', 'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7', 31205999, 0)
;

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Login_Usuario 
values ('pablomarbian', 'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7', 34604812, 0)
;

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad values ('Comprar pasaje');

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad values ('Ver viajes disponibles');




