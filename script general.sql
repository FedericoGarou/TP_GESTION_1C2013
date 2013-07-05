--CREATE SCHEMA LOS_VIAJEROS_DEL_ANONIMATO;

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
BajaPorFueraDeServicio BIT ,
FechaPorFueraDeServicio DATETIME,
FechaReinicioServicio DATETIME ,
FechaBajaDefinitiva  DATETIME,
			
			
PRIMARY KEY (Patente),
FOREIGN KEY (Marca)  REFERENCES LOS_VIAJEROS_DEL_ANONIMATO.MARCA(Id_Marca)
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
	KG_por_encomienda numeric(18, 0),
	DNI_Pago numeric(18, 0),
	NumeroTarjetaPago int,
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
		TipoServicio,KG_Disponibles,Cantidad_Butacas,BajaPorVidaUtil,
		BajaPorFueraDeServicio
)


SELECT 
		M.Micro_Patente,
		MC.Id_Marca,
		M.Micro_Modelo,
		GETDATE(),
		M.Tipo_Servicio,
		M.Micro_KG_Disponibles,
		COUNT(DISTINCT (M.Butaca_Nro)) ,
		0,
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
    (PasajesComprados, DNI_Pago, KG_por_encomienda,CodigoViaje, NumeroTarjetaPago, CodigoPasaje)
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
	  END) -- Codigo Pasaje
FROM gd_esquema.Maestra M, LOS_VIAJEROS_DEL_ANONIMATO.VIAJE V
where M.Recorrido_Codigo = V.CodigoRecorrido AND 
	  M.Micro_Patente = V.PatenteMicro AND
	  M.FechaSalida = V.FechaSalida;
	  
/*
*	Insertar valores en la tabla COMPRACLIENTE
*/	  
INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.COMPRACLIENTE
    (CodigoCompra,KilosPaquete, TipoCompra, DNI_Cliente,Numero_Voucher,Butaca) -- Cambio el nombre del atributo NombreButaca a Butaca
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
	)--Butaca
	 
	 
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