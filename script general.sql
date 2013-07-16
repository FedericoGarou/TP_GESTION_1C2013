USE GD1C2013
GO
CREATE SCHEMA LOS_VIAJEROS_DEL_ANONIMATO AUTHORIZATION gd;
GO
CREATE TABLE LOS_VIAJEROS_DEL_ANONIMATO.TIPOSERVICIO (
NombreServicio	nvarchar(255) PRIMARY KEY, 
PorcentajeAgregado	numeric(18,2) NOT NULL );
GO
CREATE TABLE LOS_VIAJEROS_DEL_ANONIMATO.CIUDAD ( 
NombreCiudad nvarchar(255) PRIMARY KEY );
GO
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
Intentos_Fallidos int, 
PRIMARY KEY (Username) );


CREATE TABLE LOS_VIAJEROS_DEL_ANONIMATO.MARCA
(
Id_Marca INT  IDENTITY(1,1) ,
Marca NVARCHAR(255) NOT NULL,
  
PRIMARY KEY (Id_Marca)
);



CREATE TABLE LOS_VIAJEROS_DEL_ANONIMATO.MICRO
(
Patente NVARCHAR(255) NOT NULL,
NumeroDeMicro INT  IDENTITY (1,1)  NOT NULL,
Marca INT NOT NULL,
Modelo  NVARCHAR(255) NOT NULL,
FechaAlta  DATETIME  NOT NULL,
TipoServicio   NVARCHAR(255)  NOT NULL,
KG_Disponibles  NUMERIC(18, 0) NOT NULL,
Cantidad_Butacas INT NOT NULL,
BajaPorVidaUtil  BIT ,
FechaBajaDefinitiva  DATETIME,
			
			
PRIMARY KEY (Patente),
FOREIGN KEY (Marca)  REFERENCES LOS_VIAJEROS_DEL_ANONIMATO.MARCA(Id_Marca)
);


CREATE TABLE LOS_VIAJEROS_DEL_ANONIMATO.PeridoFueraDeServicio
(
Patente NVARCHAR(255) NOT NULL,
FechaInicio DATETIME,
FechaFin DATETIME ,
			
			
PRIMARY KEY (Patente, FechaInicio, FechaFin),
FOREIGN KEY (Patente)  REFERENCES LOS_VIAJEROS_DEL_ANONIMATO.Micro(Patente)
);



CREATE TABLE LOS_VIAJEROS_DEL_ANONIMATO.BUTACA_MICRO
(
Patente  NVARCHAR(255) NOT NULL,
NumeroButaca NUMERIC(18, 0) NOT NULL,
CodigoButaca INT IDENTITY(1,1) NOT NULL,
Ubicacion NVARCHAR(255) NOT NULL,
Piso NUMERIC(18, 0) NOT NULL,
            
            
PRIMARY KEY (CodigoButaca),
FOREIGN KEY (Patente) REFERENCES LOS_VIAJEROS_DEL_ANONIMATO.MICRO (Patente)
);



CREATE TABLE LOS_VIAJEROS_DEL_ANONIMATO.VIAJE
(
CodigoViaje INT IDENTITY(0,1),
CodigoRecorrido NUMERIC(18,0),
FechaSalida DATETIME,
PatenteMicro  NVARCHAR(255),
FechaLlegadaEstimada DATETIME,
FechaLlegada DATETIME,
      
      
      
PRIMARY KEY (FechaSalida,PatenteMicro,CodigoRecorrido),
FOREIGN KEY (PatenteMicro) REFERENCES LOS_VIAJEROS_DEL_ANONIMATO.MICRO (Patente),
FOREIGN KEY (CodigoRecorrido) REFERENCES LOS_VIAJEROS_DEL_ANONIMATO.RECORRIDO(CodigoRecorrido)
);


CREATE TABLE LOS_VIAJEROS_DEL_ANONIMATO.PREMIO
(
CodigoProducto INT IDENTITY(1,1) NOT NULL,
DetalleProducto NVARCHAR(255),
CantidadDisponible INT  NOT NULL,
PuntosNecesarios INT NOT NULL ,

PRIMARY KEY (CodigoProducto)
);



CREATE TABLE LOS_VIAJEROS_DEL_ANONIMATO.CANJE
(
CodigoCanje INT IDENTITY (1,1) NOT NULL,
DNI_Usuario  NUMERIC(18, 0) NOT NULL,
CantidadElegida   INT  NOT NULL,
Fecha  DATETIME NOT NULL,
CodigoProducto  INT NOT NULL,

PRIMARY KEY (CodigoCanje),
FOREIGN KEY (CodigoProducto) REFERENCES LOS_VIAJEROS_DEL_ANONIMATO.PREMIO (CodigoProducto)
);

CREATE TABLE LOS_VIAJEROS_DEL_ANONIMATO.COMPRA
(
	NumeroVoucher int IDENTITY ( 1 , 1 ),
	PasajesComprados int,
	KG_por_encomienda numeric(18, 2),
	MontoAPagar numeric(18,2),
	DNI_Pago numeric(18, 0),
	NumeroTarjetaPago int,
	ClaveTarjetaPago nvarchar(25),
	CompaniaTarjetaPago nvarchar(255),
	CodigoViaje INT NOT NULL,
	CodigoPasaje numeric(18,0),-- Campo auxiliar despues será borrado
							
	PRIMARY KEY (NumeroVoucher),
    FOREIGN KEY (DNI_Pago) REFERENCES LOS_VIAJEROS_DEL_ANONIMATO.Usuario (DNI),
);

CREATE TABLE LOS_VIAJEROS_DEL_ANONIMATO.COMPRACLIENTE
(
	CodigoCompra numeric(18,0),
	TipoCompra nvarchar(250),
	DNI_Cliente numeric(18, 0),
	Numero_Voucher int,
	Butaca int,	
	KilosPaquete numeric(18,2),
	MontoUnitario numeric(18,2),
	PRIMARY KEY (CodigoCompra),
	FOREIGN KEY (DNI_Cliente) REFERENCES LOS_VIAJEROS_DEL_ANONIMATO.Usuario (DNI),
    FOREIGN KEY (Numero_Voucher) REFERENCES LOS_VIAJEROS_DEL_ANONIMATO.Compra (NumeroVoucher),
    FOREIGN KEY (Butaca) REFERENCES LOS_VIAJEROS_DEL_ANONIMATO.BUTACA_MICRO (CodigoButaca), 
);

CREATE TABLE LOS_VIAJEROS_DEL_ANONIMATO.PUNTOVF
(
CodigoPuntuacion INT IDENTITY (1,1)NOT NULL,
DNI_Usuario NUMERIC(18, 0)NOT NULL ,
Puntos  NUMERIC(18, 0) NOT NULL,
Fecha  DATETIME ,
CodigoCompra NUMERIC(18, 0)NOT NULL ,
CodigoCanje INT ,

PRIMARY KEY (CodigoPuntuacion),
FOREIGN KEY (DNI_Usuario) REFERENCES LOS_VIAJEROS_DEL_ANONIMATO.USUARIO(DNI),
FOREIGN KEY (CodigoCompra) REFERENCES LOS_VIAJEROS_DEL_ANONIMATO.COMPRACLIENTE(CodigoCompra),
FOREIGN KEY (CodigoCanje)  REFERENCES LOS_VIAJEROS_DEL_ANONIMATO.CANJE(CodigoCanje)
);

CREATE TABLE LOS_VIAJEROS_DEL_ANONIMATO.Devolucion(
CodigoDevolucion  INT IDENTITY (1,1),
CodigoCompra   Numeric(18,0),
NumeroVoucher   INT,
Motivo nvarchar(255) ,
PRIMARY KEY (CodigoDevolucion),
FOREIGN KEY (CodigoCompra) REFERENCES LOS_VIAJEROS_DEL_ANONIMATO.COMPRACLIENTE(CodigoCompra),
FOREIGN KEY (NumeroVoucher) REFERENCES LOS_VIAJEROS_DEL_ANONIMATO.COMPRA(NumeroVoucher)
);





-----------------------------------------------------------------------------------------------------------------------------



/*
*	Rellenar la tabla TIPOSERVICIO. 
*/
INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.TIPOSERVICIO
SELECT DISTINCT 
M.Tipo_Servicio,
( ( AVG(M.Pasaje_Precio) / AVG(M.Recorrido_Precio_BasePasaje) ) - 1 ) -- PORCENTAJE AGREGADO POR TIPO DE SERVICIO
FROM gd_esquema.Maestra M
GROUP BY M.Tipo_Servicio;

/*
*	Llenar la tabla CIUDAD 
*/
INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.CIUDAD
SELECT DISTINCT gd_esquema.Maestra.Recorrido_Ciudad_Destino FROM gd_esquema.Maestra
UNION
SELECT DISTINCT gd_esquema.Maestra.Recorrido_Ciudad_Origen FROM gd_esquema.Maestra;

/*
*	Llenar la tabla RECORRIDO
*/
INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.RECORRIDO
SELECT
		M.Recorrido_Ciudad_Origen, 
		M.Recorrido_Ciudad_Destino, 
		M.Tipo_Servicio,
		MAX(M.Recorrido_Precio_BasePasaje) as precio_base,
		MAX(M.Recorrido_Precio_BaseKG) as precio_base_kg,
		/*	Se usa max porque cada código de recorrido puede tener encomiendas
		*	y pasajes y para las encomiendas el precio_baseKG es 0 y lo mismo
		*	en Precio_BasePasaje para encomiendas
		*/
		1 as habilitacion		
FROM gd_esquema.Maestra M
GROUP BY M.Recorrido_Codigo,M.Recorrido_Ciudad_Origen,M.Recorrido_Ciudad_Destino,M.Tipo_Servicio
ORDER BY M.Recorrido_Codigo;


-- Llenar tabla Usuario
INSERT into LOS_VIAJEROS_DEL_ANONIMATO.Usuario (DNI, Nombre, Apellido, Direccion, Telefono, Mail, Fecha_Nac )
SELECT DISTINCT Cli_Dni, Cli_Nombre, Cli_Apellido, Cli_Dir, Cli_Telefono, Cli_Mail, Cli_Fecha_Nac
FROM gd_esquema.Maestra
;

--Crear los roles que se pide por enunciado
INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Rol values('Administrador', 1)
;
INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Rol values('Cliente', 1)
;

--Creamos las variables con los codigos de cliente y admin, que luego seran utilizadas
declare @cod_cliente int
set @cod_cliente=(SELECT codigo_rol FROM LOS_VIAJEROS_DEL_ANONIMATO.Rol WHERE nombre_rol='Cliente')

declare @cod_admin int
set @cod_admin=(SELECT codigo_rol FROM LOS_VIAJEROS_DEL_ANONIMATO.Rol WHERE nombre_rol='Administrador')

--Asignamos a todos los clientes, el rol Cliente
INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Usuario_Rol
SELECT DISTINCT Cli_DNI, @cod_cliente
FROM gd_esquema.Maestra
;

/*
Se crea el set de usuarios pedido por enunciado, 
decidimos que sean los nombres de los integrantes del grupo y el usuario admin 
*/
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
insert into LOS_VIAJEROS_DEL_ANONIMATO.Usuario
values (0, 'ADMIN', 'FRBABUS', 'Mozart2300', 0, 'admin@frbabus.com', '01-01-01', 'Masculino', 0)
;

--Se les asigna al set de usuarios creados el rol Administrador
INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Usuario_Rol 
values (35990001, @cod_admin)
;
INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Usuario_Rol 
values (30991234, @cod_admin)
;
INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Usuario_Rol 
values (31205999, @cod_admin)
;
INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Usuario_Rol 
values (34604812, @cod_admin)
;
INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Usuario_Rol 
values (0, @cod_admin)
;

--Se crean los usuarios en el sistema
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
INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Login_Usuario 
values ('admin', 'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7', 0, 0)
;

--LLenar tabla de funcionalidades
INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad values ('ABM Rol');
INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad values ('ABM Recorrido');
INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad values ('ABM Micro');
INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad values ('Generacion de viaje');
INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad values ('Registro de llegada a destino');
INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad values ('Compra de pasaje/encomienda');
INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad values ('Devolucion/cancelacion de pasaje y/o encomienda');
INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad values ('Consulta de puntos de pasajero frecuente');
INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad values ('Canje de puntos de pasajero frecuente');
INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad values ('Listado estadistico');

--Asignar funcionalidades a Cliente
INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Rol_Funcionalidad
SELECT @cod_cliente, codigo_funcionalidad FROM LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad WHERE
Nombre_Funcionalidad='Compra de pasaje/encomienda' or
Nombre_Funcionalidad='Consulta de puntos de pasajero frecuente' or
Nombre_Funcionalidad='Canje de puntos de pasajero frecuente'

--Asignar funcionalidades a Administrador
INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Rol_Funcionalidad
SELECT @cod_admin, codigo_funcionalidad FROM LOS_VIAJEROS_DEL_ANONIMATO.Funcionalidad
;


INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.MARCA(Marca)

  SELECT 
    DISTINCT(gd_esquema.Maestra.Micro_Marca)
  
  FROM gd_esquema.Maestra
  
  

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.MICRO
(
        Patente,Marca,Modelo,FechaAlta,
		TipoServicio,KG_Disponibles,Cantidad_Butacas,BajaPorVidaUtil
)
SELECT 
		M.Micro_Patente,
		MC.Id_Marca,
		M.Micro_Modelo,
		GETDATE(),
		M.Tipo_Servicio,
		M.Micro_KG_Disponibles,
		COUNT(DISTINCT (M.Butaca_Nro)) ,
		0
FROM gd_esquema.Maestra M, LOS_VIAJEROS_DEL_ANONIMATO.MARCA MC
WHERE(M.Micro_Marca = MC.Marca)
GROUP BY 
          M.Micro_Patente,
          MC.Id_Marca,
          M.Micro_Modelo,
          M.Tipo_Servicio,
          M.Micro_KG_Disponibles 
          
          
          


INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.BUTACA_MICRO(Patente,NumeroButaca,Ubicacion,Piso)

SELECT 
       DISTINCT (gd_esquema.Maestra.Micro_Patente),
       gd_esquema.Maestra.Butaca_Nro,
       gd_esquema.Maestra.Butaca_Tipo,
       gd_esquema.Maestra.Butaca_Piso

FROM gd_esquema.Maestra





INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.VIAJE
    (CodigoRecorrido,FechaSalida,PatenteMicro,FechaLlegadaEstimada,FechaLlegada)
SELECT 
      
      gd_esquema.Maestra.Recorrido_Codigo,
      gd_esquema.Maestra.FechaSalida,
      gd_esquema.Maestra.Micro_Patente,
      gd_esquema.Maestra.Fecha_LLegada_Estimada,
      gd_esquema.Maestra.FechaLLegada
     

FROM gd_esquema.Maestra


GROUP BY
 
      gd_esquema.Maestra.Recorrido_Codigo,
      gd_esquema.Maestra.FechaSalida,
      gd_esquema.Maestra.Micro_Patente,
      gd_esquema.Maestra.Fecha_LLegada_Estimada,
      gd_esquema.Maestra.FechaLLegada
      
      
      
INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Premio(DetalleProducto,CantidadDisponible,PuntosNecesarios)
VALUES ('Televisor Full HD - SAMSUNG',555,6344);

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Premio(DetalleProducto,CantidadDisponible,PuntosNecesarios)
VALUES ('Consola Play Station 3',822,4112);

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Premio(DetalleProducto,CantidadDisponible,PuntosNecesarios)
VALUES ('Mesa Cosina-Roble',1000,4611);

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Premio(DetalleProducto,CantidadDisponible,PuntosNecesarios)
VALUES ('Microondas Koinor',823,2923);

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Premio(DetalleProducto,CantidadDisponible,PuntosNecesarios)
VALUES ('Pelota Mundial',2000,1000);

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Premio(DetalleProducto,CantidadDisponible,PuntosNecesarios)
VALUES ('Tablero de Ajedres',334,1156);

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Premio(DetalleProducto,CantidadDisponible,PuntosNecesarios)
VALUES ('Juego PC - Ultimate Soccer',495,944);

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Premio(DetalleProducto,CantidadDisponible,PuntosNecesarios)
VALUES ('Reloj De Hogar',1843,772);

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Premio(DetalleProducto,CantidadDisponible,PuntosNecesarios)
VALUES ('Sillas para jardin (X4)-Plastico',566,792);

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Premio(DetalleProducto,CantidadDisponible,PuntosNecesarios)
VALUES ('Tazas de Pokemon(X2)',736,645);

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Premio(DetalleProducto,CantidadDisponible,PuntosNecesarios)
VALUES ('Mazo de Cartas(POKER)',993,371);

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Premio(DetalleProducto,CantidadDisponible,PuntosNecesarios)
VALUES ('Cartuchera-Infantil',2166,267);
     
/*
*	Insertar valores en la tabla COMPRA
*/
INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.COMPRA 
    (PasajesComprados, DNI_Pago, KG_por_encomienda,CodigoViaje, NumeroTarjetaPago, CodigoPasaje, MontoAPagar)
SELECT  
	  (	SELECT CASE M.Paquete_KG
		WHEN 0 THEN 1 --Si no tiene paquete entonces es un pasaje
		ELSE 0 -- Si tiene paquete entonces es una encomienda y el numero de pasajes es cero.
		END ), -- Esto devuelve bien el valor de numeros de pasajes
	  M.Cli_Dni,
      M.Paquete_KG,
      V.CodigoViaje,
      NULL as NumeroTarjeta,
      (SELECT CASE M.Paquete_KG
	  WHEN 0 THEN M.Pasaje_Codigo
	  ELSE M.Paquete_Codigo
	  END), -- Codigo Pasaje
	  
	  (	SELECT CASE M.Paquete_KG
		WHEN 0 THEN 
			M.Pasaje_Precio
		ELSE
			M.Paquete_Precio
		END )
	  
FROM gd_esquema.Maestra M, LOS_VIAJEROS_DEL_ANONIMATO.VIAJE V
where M.Recorrido_Codigo = V.CodigoRecorrido AND 
	  M.Micro_Patente = V.PatenteMicro AND
	  M.FechaSalida = V.FechaSalida;
	  
/*
*	Insertar valores en la tabla COMPRACLIENTE
*/	  
INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.COMPRACLIENTE
    (CodigoCompra,KilosPaquete, TipoCompra, DNI_Cliente,Numero_Voucher,Butaca,MontoUnitario) -- Cambio el nombre del atributo NombreButaca a Butaca
SELECT  
	 (SELECT CASE M.Paquete_KG
	 WHEN 0 THEN M.Pasaje_Codigo
	 ELSE M.Paquete_Codigo
	 END),--Codigo de pasaje/encomienda (Compra)
	 
	 M.Paquete_KG,--Peso del paquete
	 
	 (SELECT CASE M.Paquete_KG
	 WHEN 0 THEN 'P'
	 ELSE 'E'
	 END),--Tipo de compra	
	 
	 M.Cli_Dni,
	 
	 (SELECT CASE M.Paquete_KG
	 WHEN 0 THEN (	SELECT C.NumeroVoucher
					FROM LOS_VIAJEROS_DEL_ANONIMATO.COMPRA C
					WHERE C.CodigoPasaje = M.Pasaje_Codigo)
	 ELSE (	SELECT C.NumeroVoucher
			FROM LOS_VIAJEROS_DEL_ANONIMATO.COMPRA C
			WHERE C.CodigoPasaje = M.Paquete_Codigo)
	 END),--Numero de Voucher
	 
	(SELECT B2.CodigoButaca
	 FROM LOS_VIAJEROS_DEL_ANONIMATO.BUTACA_MICRO B2
	 WHERE 
		B2.NumeroButaca = M.Butaca_Nro AND 
		B2.Patente = M.Micro_Patente AND
		B2.Piso = M.Butaca_Piso AND
		B2.Ubicacion = M.Butaca_Tipo
	),--Butaca
	
	 (	SELECT CASE M.Paquete_KG
		WHEN 0 THEN 
			M.Pasaje_Precio
		ELSE
			M.Paquete_Precio
		END ) -- Monto unitario
	 
FROM gd_esquema.Maestra M;

-- Se borra la columna CodigoPasaje de la tabla COMPRA ya que solo se usaba
-- de forma auxiliar
ALTER TABLE LOS_VIAJEROS_DEL_ANONIMATO.COMPRA DROP COLUMN CodigoPasaje;

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.PUNTOVF
	(DNI_Usuario,Puntos,Fecha,CodigoCompra)
SELECT 
	M.Cli_Dni,

	(CASE M.Paquete_KG
	WHEN 0 THEN CAST(M.Pasaje_Precio / 5 AS INTEGER)
	ELSE CAST(M.Paquete_Precio / 5 AS INTEGER)
	END),--Puntos ganados por el pasaje/encomienda
	
	M.FechaLLegada,

	(CASE M.Paquete_KG
	WHEN 0 THEN M.Pasaje_Codigo
	ELSE M.Paquete_Codigo 
	END) --Codigo pasaje

FROM gd_esquema.Maestra M;

-- |||||||||| Stored procedures y funciones |||||||||| --

GO
Create PROCEDURE LOS_VIAJEROS_DEL_ANONIMATO.SPRegistrarLLegada
 ( @CodigoViaje nvarchar(255), 
 @FechaExacta datetime 
 )
AS
BEGIN
    
    UPDATE LOS_VIAJEROS_DEL_ANONIMATO.VIAJE SET FechaLlegada=@FechaExacta WHERE CodigoViaje=@CodigoViaje
    
END;
GO
Create PROCEDURE LOS_VIAJEROS_DEL_ANONIMATO.SPMicroOcupadoEnFecha
 ( @PatenteMicro nvarchar(255), @Fecha datetime, @retorno int output )
AS
BEGIN        
    IF (EXISTS (select * from LOS_VIAJEROS_DEL_ANONIMATO.PeridoFueraDeServicio
				WHERE Patente=@PatenteMicro AND
					((@fecha BETWEEN FechaInicio AND FechaFin) OR
					(@fecha+1 BETWEEN FechaInicio AND FechaFin))))
    begin
		
		SET @retorno = 2; -- Micro no disponible por fuera de servicio
	
	end
	ELSE
	begin
    
		IF EXISTS(SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.VIAJE v
				WHERE (
					v.PatenteMicro = @PatenteMicro AND
					FechaSalida > (@fecha-1) AND
					FechaSalida < @fecha+1				
				) )
		begin
			
			SET @retorno = 1; -- Ya tiene viaje asignado
					
		end		
		ELSE
		begin
		
			SET @retorno = 0; -- Micro libre		
			
		end
	end
END;
GO
Create PROCEDURE LOS_VIAJEROS_DEL_ANONIMATO.SPCrearViaje
 ( @PatenteMicro nvarchar(255), 
 @FechaSalida datetime, 
 @FechaLlegadaEstimada datetime,
 @Origen nvarchar(255), 
 @Destino nvarchar(255), 
 @TipoServicio nvarchar(255) )
AS
BEGIN   
       
    declare @codigoRecorrido numeric(18,0) = (select CodigoRecorrido 
											from LOS_VIAJEROS_DEL_ANONIMATO.RECORRIDO 
											where CiudadOrigen=@Origen and CiudadDestino=@Destino and
											 TipoServicio=@TipoServicio)
        
    INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.VIAJE values(
    @codigoRecorrido, 
    @FechaSalida, 
    @PatenteMicro, 
    @FechaLlegadaEstimada, NULL)
    
END;

GO

CREATE PROCEDURE LOS_VIAJEROS_DEL_ANONIMATO.SP_ObtenerMontoEncomienda
(
	@codigoViaje int,
	@monto float output
)
AS
BEGIN
	DECLARE @ViajeRecorrido numeric(18,0);
	SET @ViajeRecorrido = ( SELECT V.CodigoRecorrido 
							FROM LOS_VIAJEROS_DEL_ANONIMATO.VIAJE V 
							WHERE V.CodigoViaje = @codigoViaje );
	SET @monto = (  SELECT R.PrecioBase_KG
					FROM LOS_VIAJEROS_DEL_ANONIMATO.RECORRIDO R
					WHERE R.CodigoRecorrido = @ViajeRecorrido );
END;
GO
CREATE PROCEDURE LOS_VIAJEROS_DEL_ANONIMATO.SP_ObtenerCodigoButaca
(
	@numeroButaca numeric(18,0),
	@codigoViaje int,
	@codigoButaca int output
)
AS
BEGIN
	DECLARE @PatenteMicro nvarchar(255);
	SET @PatenteMicro = ( SELECT V.PatenteMicro
						  FROM LOS_VIAJEROS_DEL_ANONIMATO.VIAJE V 
						  WHERE V.CodigoViaje = @codigoViaje );
	SET @codigoButaca = (  SELECT BM.CodigoButaca
					FROM LOS_VIAJEROS_DEL_ANONIMATO.BUTACA_MICRO BM
					WHERE  
						BM.NumeroButaca = @numeroButaca AND
						BM.Patente = @PatenteMicro );
END;
GO
CREATE PROCEDURE LOS_VIAJEROS_DEL_ANONIMATO.SP_Obtener_Usuario
(
	@DNI numeric(18,0),
	@Nombre nvarchar(255) output,
	@Apellido nvarchar(255) output,
	@Direccion nvarchar(255) output,
	@Telefono numeric(18,0) output,
	@Mail nvarchar(255) output,
	@Fecha_Nac datetime output,
	@Sexo varchar(9) output,
	@discapacidad bit output
)
AS
BEGIN
	SELECT 
		@Nombre = U.Nombre,
		@Apellido = U.Apellido,
		@Direccion = U.Direccion,
		@Telefono = CAST(U.Telefono AS NVARCHAR),
		@Mail = U.Mail,
		@Fecha_Nac = U.Fecha_Nac,
		@Sexo = U.Sexo,
		@discapacidad = ISNULL(U.Discapacidad,0)
	FROM LOS_VIAJEROS_DEL_ANONIMATO.Usuario U
	WHERE U.DNI = @DNI;
END;
GO
CREATE PROCEDURE LOS_VIAJEROS_DEL_ANONIMATO.SP_Existe_Usuario
(
	@DNI numeric(18,0),
	@existe bit output
)
AS
BEGIN
	IF(EXISTS 
		(SELECT 1 FROM LOS_VIAJEROS_DEL_ANONIMATO.Usuario U
		 WHERE U.DNI = @DNI) )
	BEGIN
		SET @existe = 1;
	END
	ELSE
	BEGIN
		SET @existe = 0;
	END		
END;
GO
CREATE PROCEDURE LOS_VIAJEROS_DEL_ANONIMATO.SP_EstablecerPago
(
	@numeroVoucher int,
	@DNI_Pago numeric(18,0),
	@TipoPago nvarchar(255),
	@NumeroTarjetaPago int,
	@ClaveTarjetaPago nvarchar(20),
	@CompaniaTarjetaPago nvarchar(255)
)
AS
BEGIN TRANSACTION
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE ;
	
	IF NOT EXISTS (SELECT 1
				   FROM LOS_VIAJEROS_DEL_ANONIMATO.COMPRA C
				   WHERE C.NumeroVoucher = @numeroVoucher)
	BEGIN
		ROLLBACK;
		RAISERROR ('No existe la compra', 11, 0);
	END
	ELSE
	BEGIN
		
		UPDATE LOS_VIAJEROS_DEL_ANONIMATO.COMPRA
		SET 
			DNI_Pago = @DNI_Pago,
			MontoAPagar = ( SELECT SUM(CC.MontoUnitario)
							FROM LOS_VIAJEROS_DEL_ANONIMATO.COMPRACLIENTE CC
							WHERE CC.Numero_Voucher = @numeroVoucher )
		WHERE NumeroVoucher = @numeroVoucher;
		
		IF (@TipoPago = 'Con tarjeta')
		BEGIN 
			UPDATE LOS_VIAJEROS_DEL_ANONIMATO.COMPRA
			SET 
				NumeroTarjetaPago = @NumeroTarjetaPago,
				ClaveTarjetaPago = @ClaveTarjetaPago,
				CompaniaTarjetaPago = @CompaniaTarjetaPago
			WHERE NumeroVoucher = @numeroVoucher;
		END
		
		COMMIT;
	END	
GO

CREATE PROCEDURE LOS_VIAJEROS_DEL_ANONIMATO.SP_EliminarTutorOAtutorado
(
	@DNI_Atutorado_Tutor numeric(18,0),
	@numeroVoucher int
)
AS
BEGIN TRANSACTION
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE ;
	
	IF EXISTS ( SELECT 1 FROM LOS_VIAJEROS_DEL_ANONIMATO.COMPRACLIENTE CC 
				WHERE CC.MontoUnitario = 0 AND CC.DNI_Cliente != @DNI_Atutorado_Tutor AND CC.Numero_Voucher = @numeroVoucher)
	BEGIN
		DECLARE @DNI_A_Borrar numeric(18,0);
		
		SET @DNI_A_Borrar = (SELECT CC.DNI_Cliente 
							 FROM LOS_VIAJEROS_DEL_ANONIMATO.COMPRACLIENTE CC 
							 WHERE 
								CC.MontoUnitario = 0 AND 
								CC.DNI_Cliente != @DNI_Atutorado_Tutor AND 
								CC.Numero_Voucher = @numeroVoucher);
								
		DELETE FROM LOS_VIAJEROS_DEL_ANONIMATO.COMPRACLIENTE
		WHERE DNI_Cliente = @DNI_A_Borrar AND Numero_Voucher = @numeroVoucher;
		
		UPDATE LOS_VIAJEROS_DEL_ANONIMATO.COMPRA
		SET PasajesComprados = (SELECT PasajesComprados - 1)
		WHERE NumeroVoucher = @numeroVoucher;
		
		COMMIT;
	
	END
	ELSE
	BEGIN
		ROLLBACK;
	END
GO
CREATE PROCEDURE LOS_VIAJEROS_DEL_ANONIMATO.SP_EliminarPasajeSinCancelar
(	
		@codigoViaje int,
		@numeroVoucher int,
		@DNI_pasajero numeric(18,0),
		@numeroButaca numeric(18,0),
		@piso numeric(18,0),
		@ubicacion nvarchar(255)
)
AS
BEGIN TRANSACTION
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE ;
	
	DECLARE @patente nvarchar(255);
	
	SET @patente = (SELECT V.PatenteMicro
					FROM LOS_VIAJEROS_DEL_ANONIMATO.VIAJE V
					WHERE V.CodigoViaje = @codigoViaje);
	
	DECLARE @codButaca int;
	
	SET @codButaca = (SELECT BM.CodigoButaca
					  FROM LOS_VIAJEROS_DEL_ANONIMATO.BUTACA_MICRO BM
					  WHERE 
						@patente = BM.Patente AND
						@numeroButaca = BM.NumeroButaca AND
						@ubicacion = BM.Ubicacion AND
						@piso = BM.Piso);
	
	DECLARE @codPasaje numeric(18,0);
	
	SET @codPasaje = (SELECT CC.CodigoCompra
					  FROM LOS_VIAJEROS_DEL_ANONIMATO.COMPRACLIENTE CC
					  WHERE 
						CC.Butaca = @codButaca AND
						CC.Numero_Voucher = @numeroVoucher AND
						CC.DNI_Cliente = @DNI_pasajero AND
						CC.TipoCompra = 'P');
	
	IF NOT EXISTS (SELECT 1 
			   FROM LOS_VIAJEROS_DEL_ANONIMATO.COMPRACLIENTE CC1
			   WHERE CC1.CodigoCompra = @codPasaje)
	BEGIN
		ROLLBACK;
		RAISERROR ('No existe el pasaje', 11, 0);
	END
	ELSE
	BEGIN
		DELETE FROM LOS_VIAJEROS_DEL_ANONIMATO.COMPRACLIENTE
		WHERE CodigoCompra = @codPasaje;
		
		UPDATE LOS_VIAJEROS_DEL_ANONIMATO.COMPRA
		SET PasajesComprados = (SELECT PasajesComprados - 1)
		WHERE NumeroVoucher = @numeroVoucher;
		
		COMMIT;
	END
GO
CREATE PROCEDURE LOS_VIAJEROS_DEL_ANONIMATO.SP_EliminarEncomiendaSinCancelar
(	
		@numeroVoucher int,
		@kilosPaquete numeric(18,2),
		@codigoEncomienda numeric(18,0)
)
AS
BEGIN TRANSACTION
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE ;
	
	IF NOT EXISTS (SELECT 1 
				   FROM LOS_VIAJEROS_DEL_ANONIMATO.COMPRACLIENTE CC1
				   WHERE CC1.CodigoCompra = @codigoEncomienda)
	BEGIN
		ROLLBACK;
		RAISERROR ('No existe la encomienda', 11, 0);
	END
	ELSE
	BEGIN
		DELETE FROM LOS_VIAJEROS_DEL_ANONIMATO.COMPRACLIENTE
		WHERE CodigoCompra = @codigoEncomienda;
		
		UPDATE LOS_VIAJEROS_DEL_ANONIMATO.COMPRA
		SET KG_por_encomienda = (SELECT KG_por_encomienda - @kilosPaquete)
		WHERE NumeroVoucher = @numeroVoucher;
		
		COMMIT;
	END
GO
CREATE PROCEDURE LOS_VIAJEROS_DEL_ANONIMATO.SP_CancelarCompraSinDevolver
(
	@numeroVoucher int
)
AS
BEGIN TRANSACTION
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE ;
	
		
	IF NOT EXISTS (SELECT 1 
				   FROM LOS_VIAJEROS_DEL_ANONIMATO.COMPRA C
				   WHERE C.NumeroVoucher = @numeroVoucher)
	BEGIN
		ROLLBACK;
		RAISERROR ('No existe la compra', 11, 0);
	END				   
	ELSE
	BEGIN
		DELETE FROM LOS_VIAJEROS_DEL_ANONIMATO.COMPRACLIENTE
		WHERE Numero_Voucher = @numeroVoucher;
		
		DELETE FROM LOS_VIAJEROS_DEL_ANONIMATO.COMPRA
		WHERE NumeroVoucher = @numeroVoucher;
	
		COMMIT;
	END
GO
CREATE PROCEDURE LOS_VIAJEROS_DEL_ANONIMATO.SP_Actualizar_Cliente
(
	@DNI nvarchar(255),
	@Nombre nvarchar(255),
	@Apellido nvarchar(255),
	@Direccion nvarchar(255),
	@Telefono numeric(18,0),
	@Mail nvarchar(255),
	@Fecha_nac datetime,
	@Sexo varchar(9),
	@discapacidad bit
)
AS
BEGIN
	UPDATE LOS_VIAJEROS_DEL_ANONIMATO.Usuario
	SET 
		Nombre = @Nombre,
		Apellido = @Apellido,
		Telefono = @Telefono,
		Direccion = @Direccion,
		Mail = @Mail,
		Fecha_Nac = @Fecha_nac,
		Sexo = @Sexo,
		Discapacidad = @discapacidad
	WHERE DNI = @DNI
END;
GO

CREATE PROCEDURE LOS_VIAJEROS_DEL_ANONIMATO.InsertarEncomienda
(	
		@codigoViaje int,
		@numeroVoucher int,
		@DNI_pasajero numeric(18,0),
		@kilosPaqueteString varchar(18),
		@codigoEncomienda numeric(18,0) output
)
AS
BEGIN TRANSACTION
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE ;
	
	DECLARE @codCompra numeric(18,0);
	DECLARE @kgOcupados numeric(18,2);
	DECLARE @kgMicro numeric(18,2);
	DECLARE @kgTotalCompra numeric(18,2);
	DECLARE @kgPaquete numeric(18,2);
	DECLARE @codigoButaca int;
	DECLARE @Precio_Por_KG numeric(18,2);
	
	SET @Precio_Por_KG = (SELECT R.PrecioBase_KG
						  FROM LOS_VIAJEROS_DEL_ANONIMATO.RECORRIDO R 
						  WHERE 
							R.CodigoRecorrido = (SELECT V.CodigoRecorrido 
												 FROM LOS_VIAJEROS_DEL_ANONIMATO.VIAJE V
												 WHERE V.CodigoViaje = @codigoViaje)
							);
	
	SET @kgPaquete = ( SELECT CONVERT(numeric(18,2),@kilosPaqueteString) );
	
	SET @kgOcupados = (SELECT SUM(C.KG_por_encomienda)
					   FROM LOS_VIAJEROS_DEL_ANONIMATO.COMPRA C
					   WHERE C.CodigoViaje = @codigoViaje);
					   
	SET @kgTotalCompra = (SELECT SUM(C.KG_por_encomienda)
			     		  FROM LOS_VIAJEROS_DEL_ANONIMATO.COMPRA C
					      WHERE C.NumeroVoucher = @numeroVoucher);
		
	SET @kgMicro = (SELECT M.KG_Disponibles
					FROM LOS_VIAJEROS_DEL_ANONIMATO.MICRO M
					WHERE 
						M.Patente = (SELECT V.PatenteMicro 
									 FROM LOS_VIAJEROS_DEL_ANONIMATO.VIAJE V 
									 WHERE V.CodigoViaje = @codigoViaje) 
					);
					
	SET @codigoButaca = (SELECT BM.CodigoButaca
			 			 FROM LOS_VIAJEROS_DEL_ANONIMATO.BUTACA_MICRO BM
						 WHERE 
							BM.Patente = (SELECT V.PatenteMicro
										  FROM LOS_VIAJEROS_DEL_ANONIMATO.VIAJE V
										  WHERE V.CodigoViaje = @codigoViaje) AND
							BM.NumeroButaca = 0 AND
							BM.Piso = 0 AND
							BM.Ubicacion = 0
						);
	
	IF @kgMicro < ( @kgOcupados + @kgPaquete )
	BEGIN
		ROLLBACK;
		RAISERROR ('El paquete supera la capacidad del micro', 11, 0);
	END
	ELSE
	BEGIN
		SET @codCompra = (SELECT MAX(CC.CodigoCompra) + 1 FROM LOS_VIAJEROS_DEL_ANONIMATO.COMPRACLIENTE CC)
		
		INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.COMPRACLIENTE (CodigoCompra,TipoCompra,Numero_Voucher,DNI_Cliente,KilosPaquete,Butaca,MontoUnitario)
		VALUES (@codCompra,'E',@numeroVoucher,@DNI_pasajero,@kgPaquete,@codigoButaca,(SELECT @Precio_Por_KG*@kgPaquete));
		
		UPDATE LOS_VIAJEROS_DEL_ANONIMATO.COMPRA
		SET KG_por_encomienda = @kgTotalCompra + @kgPaquete
		WHERE NumeroVoucher = @numeroVoucher;
		
		SET @codigoEncomienda = (SELECT @codCompra);
		
		COMMIT;
	END
GO
CREATE FUNCTION LOS_VIAJEROS_DEL_ANONIMATO.F_CalcularMonto
(
	@codigoViaje int,
	@DNI numeric(18,0)
)
RETURNS numeric(18,2)
AS
BEGIN
	DECLARE @costoPasaje numeric(18,2);
	DECLARE @monto numeric(18,2);
	
	SET @costoPasaje = ( SELECT R.PrecioBase * (SELECT 1 + Ts.PorcentajeAgregado
						   					    FROM LOS_VIAJEROS_DEL_ANONIMATO.TIPOSERVICIO TS 
										        WHERE TS.NombreServicio = R.TipoServicio)
						 FROM LOS_VIAJEROS_DEL_ANONIMATO.RECORRIDO R
						 WHERE R.CodigoRecorrido = (SELECT V.CodigoRecorrido 
							   				 	    FROM LOS_VIAJEROS_DEL_ANONIMATO.VIAJE V 
											        WHERE V.CodigoViaje = @codigoViaje)
					   );


	
	IF (SELECT US.DNI
		FROM LOS_VIAJEROS_DEL_ANONIMATO.Usuario US 
		WHERE 
			(US.DNI = @DNI) AND
			(
			( US.Sexo = 'Masculino' AND ((SELECT DATEDIFF(year,US.Fecha_Nac,GETDATE())) >= 65 ) ) OR
			( US.Sexo = 'Femenino' AND ((SELECT DATEDIFF(year,US.Fecha_Nac,GETDATE())) >= 60 ) ) 
			)
		) IS NOT NULL
	BEGIN
		SET @monto = (@costoPasaje / 2);
	END
	ELSE
	BEGIN
		SET @monto = @costoPasaje;
	END;
	
	IF (SELECT US.Discapacidad FROM LOS_VIAJEROS_DEL_ANONIMATO.Usuario US WHERE US.DNI = @DNI) = 1
	BEGIN 
		SET @monto = 0;
	END;
	
	RETURN @monto;
END
GO
CREATE PROCEDURE LOS_VIAJEROS_DEL_ANONIMATO.InsertarPasajeTutor
(	 
		@codigoViaje int,
		@numeroVoucher int,
		@DNI_pasajero numeric(18,0),
		@codigoButaca int
)
AS
BEGIN TRANSACTION
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE ;
	DECLARE @codPasaje numeric(18,0);
	DECLARE @fechaSalidaPasaje datetime;
	DECLARE @fechaLlegadaPasaje datetime;
	
	SET @fechaSalidaPasaje = (SELECT V2.FechaSalida FROM LOS_VIAJEROS_DEL_ANONIMATO.VIAJE V2
							  WHERE V2.CodigoViaje = @codigoViaje);
							  
	SET @fechaLlegadaPasaje = (SELECT V2.FechaLlegadaEstimada FROM LOS_VIAJEROS_DEL_ANONIMATO.VIAJE V2
							   WHERE V2.CodigoViaje = @codigoViaje);
	
	
	IF EXISTS (SELECT 1 
			   FROM LOS_VIAJEROS_DEL_ANONIMATO.COMPRACLIENTE CC1
			   JOIN LOS_VIAJEROS_DEL_ANONIMATO.COMPRA C1
			   ON C1.NumeroVoucher = CC1.Numero_Voucher
			   JOIN LOS_VIAJEROS_DEL_ANONIMATO.VIAJE V1
			   ON C1.CodigoViaje = V1.CodigoViaje
			   WHERE 
					CC1.DNI_Cliente = @DNI_pasajero AND
					CC1.TipoCompra = 'P' AND
					(
						(@fechaSalidaPasaje BETWEEN V1.FechaSalida AND V1.FechaLlegadaEstimada) 
						OR
						(@fechaLlegadaPasaje BETWEEN V1.FechaSalida AND V1.FechaLlegadaEstimada) 
						OR
						(V1.FechaSalida BETWEEN @fechaSalidaPasaje AND @fechaLlegadaPasaje)
						OR
						(V1.FechaLlegadaEstimada BETWEEN @fechaSalidaPasaje AND @fechaLlegadaPasaje)
					)
			  )
	BEGIN
		ROLLBACK;
		RAISERROR ('El cliente ya adquirió un pasaje para la fecha del viaje', 11, 0);
	END
	ELSE
	BEGIN
			IF (SELECT LOS_VIAJEROS_DEL_ANONIMATO.F_ButacasLibres(@codigoViaje)) <= 0
			BEGIN
				ROLLBACK;
				RAISERROR ('No se puede comprar el pasaje;El viaje esta lleno', 11, 1);
			END
			ELSE
			BEGIN
				SET @codPasaje = (SELECT MAX(CC.CodigoCompra) + 1 FROM LOS_VIAJEROS_DEL_ANONIMATO.COMPRACLIENTE CC);
				
				INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.COMPRACLIENTE (CodigoCompra,TipoCompra,Numero_Voucher,DNI_Cliente,Butaca,KilosPaquete,MontoUnitario)
				VALUES (@codPasaje,'P',@numeroVoucher,@DNI_pasajero,@codigoButaca,0, 0);
				
				UPDATE LOS_VIAJEROS_DEL_ANONIMATO.COMPRA
				SET PasajesComprados = (SELECT PasajesComprados + 1)
				WHERE NumeroVoucher = @numeroVoucher;
				
				COMMIT;
			END
	END;
GO
CREATE PROCEDURE LOS_VIAJEROS_DEL_ANONIMATO.InsertarPasaje
(	 
		@codigoViaje int,
		@numeroVoucher int,
		@DNI_pasajero numeric(18,0),
		@codigoButaca int
)
AS
BEGIN TRANSACTION
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE ;
	DECLARE @codPasaje numeric(18,0);
	DECLARE @fechaSalidaPasaje datetime;
	DECLARE @fechaLlegadaPasaje datetime;
	
	SET @fechaSalidaPasaje = (SELECT V2.FechaSalida FROM LOS_VIAJEROS_DEL_ANONIMATO.VIAJE V2
							  WHERE V2.CodigoViaje = @codigoViaje);
							  
	SET @fechaLlegadaPasaje = (SELECT V2.FechaLlegadaEstimada FROM LOS_VIAJEROS_DEL_ANONIMATO.VIAJE V2
							   WHERE V2.CodigoViaje = @codigoViaje);
	
	
	IF EXISTS (SELECT 1 
			   FROM LOS_VIAJEROS_DEL_ANONIMATO.COMPRACLIENTE CC1
			   JOIN LOS_VIAJEROS_DEL_ANONIMATO.COMPRA C1
			   ON C1.NumeroVoucher = CC1.Numero_Voucher
			   JOIN LOS_VIAJEROS_DEL_ANONIMATO.VIAJE V1
			   ON C1.CodigoViaje = V1.CodigoViaje
			   WHERE 
					CC1.DNI_Cliente = @DNI_pasajero AND
					CC1.TipoCompra = 'P' AND
					(
						(@fechaSalidaPasaje BETWEEN V1.FechaSalida AND V1.FechaLlegadaEstimada) 
						OR
						(@fechaLlegadaPasaje BETWEEN V1.FechaSalida AND V1.FechaLlegadaEstimada) 
						OR
						(V1.FechaSalida BETWEEN @fechaSalidaPasaje AND @fechaLlegadaPasaje)
						OR
						(V1.FechaLlegadaEstimada BETWEEN @fechaSalidaPasaje AND @fechaLlegadaPasaje)
					)
			  )
	BEGIN
		ROLLBACK;
		RAISERROR ('El cliente ya adquirió un pasaje para la fecha del viaje', 11, 0);
	END
	ELSE
	BEGIN
			IF (SELECT LOS_VIAJEROS_DEL_ANONIMATO.F_ButacasLibres(@codigoViaje)) <= 0
			BEGIN
				ROLLBACK;
				RAISERROR ('No se puede comprar el pasaje;El viaje esta lleno', 11, 1);
			END
			ELSE
			BEGIN
				SET @codPasaje = (SELECT MAX(CC.CodigoCompra) + 1 FROM LOS_VIAJEROS_DEL_ANONIMATO.COMPRACLIENTE CC);
				
				INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.COMPRACLIENTE (CodigoCompra,TipoCompra,Numero_Voucher,DNI_Cliente,Butaca,KilosPaquete,MontoUnitario)
				VALUES (@codPasaje,'P',@numeroVoucher,@DNI_pasajero,@codigoButaca,0, (SELECT LOS_VIAJEROS_DEL_ANONIMATO.F_CalcularMonto(@codigoViaje,@DNI_pasajero) ) );
				
				UPDATE LOS_VIAJEROS_DEL_ANONIMATO.COMPRA
				SET PasajesComprados = (SELECT PasajesComprados + 1)
				WHERE NumeroVoucher = @numeroVoucher;
				
				COMMIT;
			END
	END;
GO
CREATE PROCEDURE LOS_VIAJEROS_DEL_ANONIMATO.GenerarCompra
(
	@codigoViaje int,
	@nroVoucher int output
)
AS
BEGIN
	INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.COMPRA (PasajesComprados,KG_por_encomienda,CodigoViaje)
	VALUES(0,0,@codigoViaje);
	SET @nroVoucher = (SELECT TOP 1 C.NumeroVoucher FROM LOS_VIAJEROS_DEL_ANONIMATO.COMPRA C
					   WHERE C.CodigoViaje = @codigoViaje
					   ORDER BY C.NumeroVoucher DESC)
END
GO
CREATE FUNCTION LOS_VIAJEROS_DEL_ANONIMATO.F_ObtenerEncomiendasDeUnaCompra
(
	@numeroVoucher int
)
RETURNS TABLE
AS
RETURN 
(
	SELECT 
		CC.DNI_Cliente as DNI,
		Us.Apellido as Apellido,
		Us.Nombre as Nombre,
		CC.KilosPaquete as Kilos,
		CC.MontoUnitario as Monto,
		CC.CodigoCompra as Codigo
	FROM LOS_VIAJEROS_DEL_ANONIMATO.COMPRACLIENTE CC
	JOIN LOS_VIAJEROS_DEL_ANONIMATO.Usuario US
	ON (US.DNI = CC.DNI_Cliente)
	WHERE 
		CC.TipoCompra = 'E' AND
		CC.Numero_Voucher = @numeroVoucher
 )
 GO
CREATE FUNCTION LOS_VIAJEROS_DEL_ANONIMATO.F_ObtenerPasajesDeUnaCompra
(
	@numeroVoucher int,
	@tipoBusqueda nvarchar(25),
	@DNI_Opcional numeric(18,0)
)
RETURNS TABLE
AS
RETURN 
(
	SELECT 
		CC.DNI_Cliente as DNI,
		Us.Apellido as Apellido,
		Us.Nombre as Nombre,
		BM.NumeroButaca as Numero,
		BM.Piso as Piso,
		BM.Ubicacion as Ubicacion,
		CC.MontoUnitario as Monto
	FROM LOS_VIAJEROS_DEL_ANONIMATO.COMPRACLIENTE CC
	JOIN LOS_VIAJEROS_DEL_ANONIMATO.Usuario US
	ON (US.DNI = CC.DNI_Cliente)
	JOIN LOS_VIAJEROS_DEL_ANONIMATO.BUTACA_MICRO BM
	ON (BM.CodigoButaca = CC.Butaca)
	WHERE 
		CC.TipoCompra = 'P' AND
		CC.Numero_Voucher = @numeroVoucher AND
		(
			(@tipoBusqueda != 'SINTUTOR' ) OR
			(@tipoBusqueda = 'SINTUTOR' AND @DNI_Opcional != US.DNI)
		)
 )
GO
CREATE FUNCTION LOS_VIAJEROS_DEL_ANONIMATO.F_KGDisponibles(@codigoViaje int)
RETURNS numeric(18,2)
AS
BEGIN
	declare @KGMicro numeric(18,0);
	declare @KGOcupados numeric(18,0);
	declare @patenteMicro nvarchar(255);
	
	SET @patenteMicro = (	SELECT V.PatenteMicro
							FROM LOS_VIAJEROS_DEL_ANONIMATO.VIAJE V 
							WHERE V.CodigoViaje = @codigoViaje);
	
	SET @KGMicro =		   (SELECT M.KG_Disponibles
							FROM LOS_VIAJEROS_DEL_ANONIMATO.MICRO M
							WHERE M.Patente = @patenteMicro);
		
	SET @KGOcupados = 
		(SELECT SUM(C.KG_por_encomienda)
		 FROM LOS_VIAJEROS_DEL_ANONIMATO.COMPRA C
		 WHERE @codigoViaje = C.CodigoViaje
		);
	
	
	RETURN (SELECT @KGMicro - @KGOcupados);
END;
GO
CREATE FUNCTION LOS_VIAJEROS_DEL_ANONIMATO.F_ButacasLibres(@codigoViaje int)
RETURNS int
AS
BEGIN
	declare @cantidadButacas int;
	declare @butacasOcupadas int;
	declare @patenteMicro nvarchar(255);
	
	SET @patenteMicro = (	SELECT V.PatenteMicro
							FROM LOS_VIAJEROS_DEL_ANONIMATO.VIAJE V 
							WHERE V.CodigoViaje = @codigoViaje);
	
	SET @cantidadButacas = (SELECT M.Cantidad_Butacas
							FROM LOS_VIAJEROS_DEL_ANONIMATO.MICRO M
							WHERE M.Patente = @patenteMicro);
		
	SET @butacasOcupadas = 
		(SELECT SUM(C.PasajesComprados)
		FROM LOS_VIAJEROS_DEL_ANONIMATO.COMPRA C
		WHERE @codigoViaje = C.CodigoViaje
		);
	
	
	RETURN (SELECT @cantidadButacas - @butacasOcupadas);
END;
GO
CREATE PROCEDURE LOS_VIAJEROS_DEL_ANONIMATO.EstablecerTutor
(
	@numeroVoucher int,
	@DNI_Tutor numeric(18,0)
)
AS
BEGIN
	UPDATE LOS_VIAJEROS_DEL_ANONIMATO.COMPRACLIENTE
	SET MontoUnitario = 0
	WHERE
		DNI_Cliente = @DNI_Tutor AND
		Numero_Voucher = @numeroVoucher;
END
GO
CREATE PROCEDURE LOS_VIAJEROS_DEL_ANONIMATO.CantidadDiscapacitados
(
	@numeroVoucher int,
	@cantidadDiscapacitados int output
)
AS
BEGIN
	SET @cantidadDiscapacitados = (SELECT COUNT(DISTINCT CC.CodigoCompra)
								   FROM LOS_VIAJEROS_DEL_ANONIMATO.COMPRACLIENTE CC
								   WHERE 
									CC.Numero_Voucher = @numeroVoucher AND
									-- Es discapacitado
									(SELECT US.Discapacidad 
									 FROM LOS_VIAJEROS_DEL_ANONIMATO.Usuario US
									 WHERE US.DNI = CC.DNI_Cliente) = 1
								  );
END
GO
CREATE PROCEDURE LOS_VIAJEROS_DEL_ANONIMATO.SP_ObtenerMontoPasaje
(
	@codigoViaje int,
	@monto float output
)
AS
BEGIN
	DECLARE @ViajeRecorrido numeric(18,0);
	SET @ViajeRecorrido = ( SELECT V.CodigoRecorrido 
							FROM LOS_VIAJEROS_DEL_ANONIMATO.VIAJE V 
							WHERE V.CodigoViaje = @codigoViaje );
	SET @monto = (  SELECT 
						R.PrecioBase * 
						( 1 + 
							(SELECT TS.PorcentajeAgregado 
							 FROM LOS_VIAJEROS_DEL_ANONIMATO.TIPOSERVICIO TS
							 WHERE TS.NombreServicio = R.TipoServicio )
						)
					FROM LOS_VIAJEROS_DEL_ANONIMATO.RECORRIDO R
					WHERE R.CodigoRecorrido = @ViajeRecorrido );
END;
GO
CREATE PROCEDURE LOS_VIAJEROS_DEL_ANONIMATO.PElRecorridoEstaHabilitado
	(@Origen nvarchar(255),
	 @Destino nvarchar(255),
	 @Servicio nvarchar(255),
	 @Retorno bit output)
AS
BEGIN
	SET @Retorno = (SELECT R.Habilitado FROM LOS_VIAJEROS_DEL_ANONIMATO.RECORRIDO R 
					WHERE 	R.CiudadOrigen = @Origen AND
							R.CiudadDestino = @Destino AND
							R.TipoServicio = @Servicio);
END;
GO
CREATE PROCEDURE LOS_VIAJEROS_DEL_ANONIMATO.SP_ObtenerDatosDeRecorrido
	(@Origen nvarchar(255),
	 @Destino nvarchar(255),
	 @Servicio nvarchar(255),
	 @Habilitacion bit output,
	 @PrecioBase_KG numeric(18,2) output,
	 @PrecioBase numeric(18,2) output)
AS
BEGIN
	(SELECT 
		@Habilitacion =	R.Habilitado,
		@PrecioBase_KG = R.PrecioBase_KG,
		@PrecioBase = R.PrecioBase
	FROM LOS_VIAJEROS_DEL_ANONIMATO.RECORRIDO R 
	WHERE 	R.CiudadOrigen = @Origen AND
			R.CiudadDestino = @Destino AND
			R.TipoServicio = @Servicio);
END;
GO
CREATE PROCEDURE LOS_VIAJEROS_DEL_ANONIMATO.SP_ModificarRecorrido
	@origen nvarchar(255),
	@destino nvarchar(255),
	@servicio nvarchar(255),
	@basePasaje numeric(18,2),
	@baseKG numeric(18,2),
	@habilitacion bit
AS
BEGIN
	UPDATE LOS_VIAJEROS_DEL_ANONIMATO.RECORRIDO
	SET PrecioBase = @basePasaje,
		PrecioBase_KG = @baseKG,
		Habilitado = @habilitacion 
	WHERE	CiudadOrigen = @origen AND
			CiudadDestino = @destino AND
			TipoServicio = @servicio
END;
GO
CREATE PROCEDURE LOS_VIAJEROS_DEL_ANONIMATO.insertarRecorrido
@origen nvarchar(255),
@destino nvarchar(255),
@servicio nvarchar(255),
@basePasaje numeric(18,2),
@baseKg numeric(18,2)
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.RECORRIDO
	VALUES(@origen,@destino,@servicio,@basePasaje,@baseKg,1);
END;
GO
CREATE PROCEDURE LOS_VIAJEROS_DEL_ANONIMATO.eliminarRecorrido
@origen nvarchar(255),
@destino nvarchar(255),
@servicio nvarchar(255)
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE LOS_VIAJEROS_DEL_ANONIMATO.RECORRIDO
	SET Habilitado = 0
	WHERE	CiudadOrigen = @origen AND
			CiudadDestino = @destino AND
			TipoServicio = @servicio;
END;
GO
CREATE PROCEDURE LOS_VIAJEROS_DEL_ANONIMATO.SPexisteElRecorrido 
 ( @Origen nvarchar(255), @Destino nvarchar(255),@Servicio nvarchar(255), @retorno bit output )
AS
BEGIN
    IF EXISTS(SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.RECORRIDO R 
				WHERE (
					R.CiudadOrigen = @Origen AND
					R.CiudadDestino = @Destino AND
					R.TipoServicio = @Servicio
				) )
		SET @retorno = 1; -- Si existe
	ELSE
		SET @retorno = 0; -- Si no existe
END;
GO
CREATE FUNCTION LOS_VIAJEROS_DEL_ANONIMATO.F_Servicios ()
RETURNS TABLE
AS
RETURN (SELECT 0 as RN,'No seleccionado' as NombreServicio
	    UNION
	    SELECT 
	    ROW_NUMBER() OVER(ORDER BY TS.NombreServicio ASC) as RN,
	    TS.NombreServicio 
	    FROM LOS_VIAJEROS_DEL_ANONIMATO.TIPOSERVICIO TS
	    );
GO	    
CREATE FUNCTION LOS_VIAJEROS_DEL_ANONIMATO.F_Ciudades ()
RETURNS TABLE
AS
RETURN (SELECT 0 as RN,'No seleccionado' as NombreCiudad
	    UNION
	    SELECT ROW_NUMBER() OVER(ORDER BY C.NombreCiudad ASC) as RN,C.NombreCiudad FROM LOS_VIAJEROS_DEL_ANONIMATO.CIUDAD C
	    );
GO	    
CREATE FUNCTION LOS_VIAJEROS_DEL_ANONIMATO.F_AplicarFiltrosRecorridos
(
	@origen nvarchar(255),
	@destino nvarchar(255),
	@servicio nvarchar(255),
	@filtroHabilitado bit,
	@habilitado bit,
	@precioDesde numeric(18,2),
	@preciohasta numeric(18,2),
	@precioKGDesde numeric(18,2),
	@precioKGHasta numeric(18,2)
)
RETURNS TABLE
AS
RETURN
(
	SELECT *
	FROM LOS_VIAJEROS_DEL_ANONIMATO.RECORRIDO R
	WHERE
	 	( @origen = 'No seleccionado' OR R.CiudadOrigen = @origen ) AND
		( @destino = 'No seleccionado' OR R.CiudadDestino = @destino ) AND
		( @servicio = 'No seleccionado' OR R.TipoServicio = @servicio ) AND
		( @filtroHabilitado = 0 OR (R.Habilitado = @habilitado AND @filtroHabilitado = 1) ) AND
		( @precioDesde = 0 OR R.PrecioBase > @precioDesde ) AND
		( @preciohasta = 0 OR R.PrecioBase < @preciohasta ) AND
		( @precioKGDesde = 0 OR R.PrecioBase_KG > @precioKGDesde ) AND
		( @precioKGHasta = 0 OR R.PrecioBase_KG < @precioKGHasta )
);
GO
create procedure LOS_VIAJEROS_DEL_ANONIMATO.login(@usuario nvarchar(255), @pass nvarchar(255), @respuesta nvarchar(255) output)
AS 
BEGIN                  
 
declare @existeUsuario INT = (SELECT COUNT(*) FROM LOS_VIAJEROS_DEL_ANONIMATO.Login_Usuario WHERE Username = @usuario);                   
                   
if (@existeUsuario = 1)
begin                                          
	declare @cantidadIntentosFallidos INT = (SELECT Intentos_Fallidos FROM LOS_VIAJEROS_DEL_ANONIMATO.Login_Usuario WHERE Username = @usuario);
	    
	if (@cantidadIntentosFallidos < 3)
	begin
		declare @existeUsuarioyContraseña INT = (SELECT COUNT(*) FROM LOS_VIAJEROS_DEL_ANONIMATO.Login_Usuario WHERE Username = @usuario and Passwd = @pass);
		
		if (@existeUsuarioyContraseña = 1)
		begin
			UPDATE LOS_VIAJEROS_DEL_ANONIMATO.Login_Usuario SET Intentos_Fallidos=0 WHERE Username = @usuario;
			declare @existeRol INT = (SELECT COUNT(*) FROM LOS_VIAJEROS_DEL_ANONIMATO.Login_Usuario l join LOS_VIAJEROS_DEL_ANONIMATO.Usuario_Rol ur on (l.DNI_Usuario = ur.DNI) WHERE Username = @usuario)
			
			if (@existeRol = 0)
			begin
				set @respuesta='El usuario no tiene asignado un rol, o el rol ha sido inhabilitado'
			end
			else
			begin			
				set @respuesta='abrir sesion'
			end
			
		--	Close();
		--	new Pantalla_Inicial(usuario).Show();
		end
		else
		begin
			UPDATE LOS_VIAJEROS_DEL_ANONIMATO.Login_Usuario SET Intentos_Fallidos=(Intentos_Fallidos+1) WHERE Username = @usuario;
			set @cantidadIntentosFallidos = (@cantidadIntentosFallidos + 1);
			declare @cantidadIntentosFallidosString nvarchar(255) = @cantidadIntentosFallidos;
			
			set @respuesta = 'Contraseña incorrecta, vuelva a intentarlo;Cantidad de intentos fallidos: ' + (@cantidadIntentosFallidosString);
		--	new Dialogo("Contraseña incorrecta, vuelva a intentarlo;Cantidad de intentos fallidos: " + (cantidadIntentosFallidos + 1), "Aceptar").ShowDialog();
		end

	end
	else
	begin
		set @respuesta = 'Su usuario esta bloqueado, por sobrepasar la cantidad de logueos incorrectos';
	--	new Dialogo("Su usuario esta bloqueado, por sobrepasar la cantidad de logueos incorrectos", "Aceptar").ShowDialog();
	end  
end
else
begin 
set @respuesta = 'No existe el usuario, vuelva a intentarlo';                
--new Dialogo("No existe el usuario, vuelva a intentarlo", "Aceptar").ShowDialog();                        
end
                    
END
GO
Create FUNCTION LOS_VIAJEROS_DEL_ANONIMATO.F_ObetenerRecorrido
 ( @PatenteMicro nvarchar(255),  
 @Origen nvarchar(255), 
 @Destino nvarchar(255), 
 @TipoServicio nvarchar(255) )
RETURNS numeric(18,0)
AS 
BEGIN
	declare @codigoRecorrido numeric(18,0) = (select CodigoRecorrido 
											from LOS_VIAJEROS_DEL_ANONIMATO.RECORRIDO 
											where CiudadOrigen=@Origen and CiudadDestino=@Destino and
											 TipoServicio=@TipoServicio)


RETURN(@codigoRecorrido)
END;
GO
CREATE FUNCTION LOS_VIAJEROS_DEL_ANONIMATO.F_ObtenerButacasDisponibles
(
	@CodigoViaje int,
	@Piso numeric(18,0),
	@Ubicacion nvarchar(255)
)
RETURNS TABLE
AS
RETURN
	(SELECT 
		BM.NumeroButaca as NumeroButaca,
		BM.Piso as Piso,
		BM.Ubicacion as Ubicacion
	 FROM LOS_VIAJEROS_DEL_ANONIMATO.BUTACA_MICRO BM
	 WHERE 
		(
			-- Filtro por piso
			( @Piso = 3 OR BM.Piso = @Piso) AND
			-- Filtro por ubicación
			( @Ubicacion = 'Cualquier ubicación' OR BM.Ubicacion = @Ubicacion) AND
			-- La butaca pertenezca al micro que buscamos
			( BM.Patente = (SELECT V.PatenteMicro FROM LOS_VIAJEROS_DEL_ANONIMATO.VIAJE V WHERE V.CodigoViaje = @CodigoViaje) ) AND
			-- La butaca no este comprada por nadie
			( BM.CodigoButaca NOT IN ( SELECT CC.Butaca FROM LOS_VIAJEROS_DEL_ANONIMATO.COMPRACLIENTE CC 
									 JOIN LOS_VIAJEROS_DEL_ANONIMATO.COMPRA C 
									 ON (CC.Numero_Voucher = C.NumeroVoucher)
									 WHERE C.CodigoViaje = @CodigoViaje ) )
			
		)
	);
GO
CREATE FUNCTION LOS_VIAJEROS_DEL_ANONIMATO.F_TraerViajesParaComprar
(
	@origen nvarchar(255),
	@destino nvarchar(255),
	@fechaSalida datetime
)
RETURNS TABLE
AS
RETURN 
(
SELECT 
	V.CodigoViaje as CodigoViaje,
	
	LOS_VIAJEROS_DEL_ANONIMATO.F_ButacasLibres(V.CodigoViaje) as ButacasLibres,
	
	LOS_VIAJEROS_DEL_ANONIMATO.F_KGDisponibles(V.CodigoViaje) as KGDisponibles,
	
	(SELECT M.TipoServicio 
	 FROM LOS_VIAJEROS_DEL_ANONIMATO.MICRO M
	 WHERE M.Patente = V.PatenteMicro) as TipoServicio
	
FROM LOS_VIAJEROS_DEL_ANONIMATO.VIAJE V 
JOIN LOS_VIAJEROS_DEL_ANONIMATO.RECORRIDO R
ON (V.CodigoRecorrido = R.CodigoRecorrido)
WHERE
	R.CiudadOrigen = @origen AND
	R.CiudadDestino = @destino AND
	(NOT EXISTS (SELECT 1 
	 			 FROM LOS_VIAJEROS_DEL_ANONIMATO.PeridoFueraDeServicio PFS
				 WHERE 
					PFS.Patente = V.PatenteMicro AND 
					(
						(PFS.FechaInicio BETWEEN V.FechaSalida AND V.FechaLlegadaEstimada) 
						OR
						(PFS.FechaFin BETWEEN V.FechaSalida AND V.FechaLlegadaEstimada) 
						OR
						(V.FechaSalida BETWEEN PFS.FechaInicio AND PFS.FechaFin)
						OR
						(V.FechaLlegadaEstimada BETWEEN PFS.FechaInicio AND PFS.FechaFin)
					) 
				) )
	AND
	(SELECT M.BajaPorVidaUtil FROM LOS_VIAJEROS_DEL_ANONIMATO.MICRO M 
	 WHERE M.Patente = V.PatenteMicro) != 1 AND
	@fechaSalida = ( SELECT DATEADD( dd, 0, DATEDIFF(dd, 0, V.FechaSalida) ) )
);
GO

CREATE FUNCTION LOS_VIAJEROS_DEL_ANONIMATO.FcalcularDiasBajaMicro(@patente nvarchar(255), @año int, @semestre int)
RETURNS int
AS BEGIN
	declare @cantidadDias int = 0
	declare @fechaInicio datetime
	declare @fechaFin datetime
	declare @mesInicial int 
	declare @mesFinal int
	
	if (@semestre = 1)
	begin
		set @mesInicial = 1
		set @mesFinal = 6
	end
	
	if (@semestre = 2)
	begin
		set @mesInicial = 7
		set @mesFinal = 12
	end
		
	declare cur cursor 
	for select FechaInicio, FechaFin 
		from LOS_VIAJEROS_DEL_ANONIMATO.PeridoFueraDeServicio 
		where Patente = @patente and 
			YEAR(fechaInicio)=@año and YEAR(FechaFin)=@año and 
			MONTH(FechaInicio) BETWEEN @mesInicial AND @mesFinal and
			MONTH(FechaFin) BETWEEN @mesInicial AND @mesFinal
		
	open cur 
	fetch cur into @fechaInicio, @fechaFin
	
	while @@FETCH_STATUS = 0
	begin	
			
			--A FINES PRACTICOS TOMAMOS TODOS LOS MESES DE 30 DIAS
			
			set @cantidadDias = @cantidadDias + ((YEAR(@fechaFin) - YEAR(@fechaInicio))*360)
			
			set @cantidadDias = @cantidadDias + ((MONTH(@fechaFin) - MONTH(@fechaInicio))*30)
			
			set @cantidadDias = @cantidadDias +  (DAY(@fechaFin) - DAY(@fechaInicio))
			
			
		fetch cur into @fechaInicio, @fechaFin
	
	end
	
	
RETURN @cantidadDias

END
GO

CREATE FUNCTION LOS_VIAJEROS_DEL_ANONIMATO.FcalcularPasajesCompradosEn(@ciudaDestino nvarchar(255), @año int, @semestre int)
RETURNS int
AS BEGIN

declare @mesInicial int 
declare @mesFinal int
	
if (@semestre = 1)
begin
	set @mesInicial = 1
	set @mesFinal = 6
end
	
if (@semestre = 2)
begin
	set @mesInicial = 7
	set @mesFinal = 12
end
	

declare @cantidadPasajes int = (
select COUNT(r.CiudadDestino) AS PasajesComprados
from LOS_VIAJEROS_DEL_ANONIMATO.COMPRACLIENTE cc 
join LOS_VIAJEROS_DEL_ANONIMATO.COMPRA c on (cc.Numero_Voucher = c.NumeroVoucher)
join LOS_VIAJEROS_DEL_ANONIMATO.VIAJE v on (c.CodigoViaje = v.CodigoViaje)
join LOS_VIAJEROS_DEL_ANONIMATO.RECORRIDO r on (v.CodigoRecorrido = r.CodigoRecorrido)

where cc.TipoCompra='P' and YEAR(v.FechaSalida)=@año and YEAR(v.FechaLlegada)=@año and
MONTH(v.FechaSalida) BETWEEN @mesInicial AND @mesFinal and
MONTH(v.FechaLlegada) BETWEEN @mesInicial AND @mesFinal and
r.CiudadDestino = @ciudaDestino

group by r.CiudadDestino
)

RETURN @cantidadPasajes

END
GO
