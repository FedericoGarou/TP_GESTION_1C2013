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


INSERT INTO
              LOS_VIAJEROS_DEL_ANONIMATO.MICRO
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
         
               
FROM gd_esquema.Maestra M JOIN LOS_VIAJEROS_DEL_ANONIMATO.MARCA MC
    ON (M.Micro_Marca = MC.Marca)
             
               
GROUP BY 
          M.Micro_Patente,
          MC.Id_Marca,
		      M.Micro_Modelo,
		      M.Tipo_Servicio,
		      M.Micro_KG_Disponibles 
          
          
          
          
CREATE TABLE LOS_VIAJEROS_DEL_ANONIMATO.MARCA
(
  Id_Marca INT  IDENTITY(1,1) UNIQUE,
  Marca NVARCHAR(255) NOT NULL,
  
  PRIMARY KEY (Id_Marca)

)



INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.MARCA(Marca)

SELECT 
    DISTINCT(gd_esquema.Maestra.Micro_Marca)
  
FROM gd_esquema.Maestra      





CREATE TABLE LOS_VIAJEROS_DEL_ANONIMATO.VIAJE
     (
  	  CodigoViaje INT IDENTITY(0,1),
		  CodigoRecorrido NUMERIC(18,0),
		  FechaSalida DATETIME,
		  PatenteMicro  NVARCHAR(255),
		  FechaLlegadaEstimada DATETIME,
		  FechaLlegada DATETIME,
      
      
      
    PRIMARY KEY (FechaSalida,PatenteMicro,CodigoViaje),
    FOREIGN KEY (PatenteMicro) REFERENCES LOS_VIAJEROS_DEL_ANONIMATO.MICRO (Patente),
    FOREIGN KEY (CodigoRecorrido) REFERENCES LOS_VIAJEROS_DEL_ANONIMATO.RECORRIDO(CodigoRecorrido)


);

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
      
      
      
CREATE TABLE LOS_VIAJEROS_DEL_ANONIMATO.BUTACA_MICRO
          (
            Patente  NVARCHAR(255) NOT NULL,
            NumeroButaca NUMERIC(18, 0) NOT NULL,
            CodigoButaca INT IDENTITY(1,1) NOT NULL,
            Ubicacion NVARCHAR(255) NOL NULL,
            Piso NUMERIC(18, 0) NOT NULL,
            
            
        PRIMARY KEY (CodigoButaca),
        FOREIGN KEY (Patente) REFERENCES LOS_VIAJEROS_DEL_ANONIMATO.MICRO (Patente)
);

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.BUTACA_MICRO(Patente,NumeroButaca,Ubicacion,Piso)

SELECT 
       DISTINCT (gd_esquema.Maestra.Micro_Patente),
       gd_esquema.Maestra.Butaca_Nro,
       gd_esquema.Maestra.Butaca_Tipo,
       gd_esquema.Maestra.Butaca_Piso

FROM gd_esquema.Maestra





CREATE TABLE LOS_VIAJEROS_DEL_ANONIMATO.PREMIO(
CodigoProducto INT NOT NULL,
DetalleProducto NVARCHAR(255),
CantidadDisponible INT NOT NULL,
PuntosNecesarios INT  NOT NULL,
PRIMARY KEY (CodigoProducto)


INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Premio(CodigoProducto,DetalleProducto,CantidadDisponible,PuntosNecesarios)
VALUES (0023,'Televisor Full HD - SAMSUNG',555,6344);

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Premio(CodigoProducto,DetalleProducto,CantidadDisponible,PuntosNecesarios)
VALUES (0052,'Consola Play Station 3',822,4112);

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Premio(CodigoProducto,DetalleProducto,CantidadDisponible,PuntosNecesarios)
VALUES (0123,'Mesa Cosina-Roble',1000,4611);

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Premio(CodigoProducto,DetalleProducto,CantidadDisponible,PuntosNecesarios)
VALUES (0166,'Microondas Koinor',823,2923);

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Premio(CodigoProducto,DetalleProducto,CantidadDisponible,PuntosNecesarios)
VALUES (0274,'Pelota Mundial',2000,1000);

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Premio(CodigoProducto,DetalleProducto,CantidadDisponible,PuntosNecesarios)
VALUES (0364,'Tablero de Ajedres',334,1156);

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Premio(CodigoProducto,DetalleProducto,CantidadDisponible,PuntosNecesarios)
VALUES (0444,'Juego PC - Ultimate Soccer',495,944);

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Premio(CodigoProducto,DetalleProducto,CantidadDisponible,PuntosNecesarios)
VALUES (0627,'Reloj De Hogar',1843,772);

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Premio(CodigoProducto,DetalleProducto,CantidadDisponible,PuntosNecesarios)
VALUES (0699,'Sillas para jardin (X4)-Plastico',566,792);

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Premio(CodigoProducto,DetalleProducto,CantidadDisponible,PuntosNecesarios)
VALUES (0733,'Tazas de Pokemon(X2)',736,645);

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Premio(CodigoProducto,DetalleProducto,CantidadDisponible,PuntosNecesarios)
VALUES (0813,'Mazo de Cartas(POKER)',993,371);

INSERT INTO LOS_VIAJEROS_DEL_ANONIMATO.Premio(CodigoProducto,DetalleProducto,CantidadDisponible,PuntosNecesarios)
VALUES (0479,'Cartuchera-Infantil',2166,267);
          



CREATE TABLE LOS_VIAJEROS_DEL_ANONIMATO.CANJE(
CodigoCanje INT IDENTITY (1,1) NOT NULL,
DNI_Usuario  NUMERIC(18, 0) NOT NULL,
CantidadElegida   INT  NOT NULL,
Fecha  DATETIME NOT NULL,
CodigoProducto  INT NOT NULL,


PRIMARY KEY (CodigoCanje),
FOREIGN KEY (CodigoProducto) REFERENCES LOS_VIAJEROS_DEL_ANONIMATO.PREMIO (CodigoProducto)
);



CREATE TABLE LOS_VIAJEROS_DEL_ANONIMATO.PUNTOVF(
CodigoPuntuacion INT NOT NULL,
DNI_Usuario NUMERIC(18, 0)NOT NULL ,
Puntos  NUMERIC(18, 0) NOT NULL,
Fecha  DATETIME ,
CodigoPasaje NUMERIC(18, 0)NOT NULL ,
CodigoCanje INT NOT NULL,

PRIMARY KEY (CodigoPuntuacion),
FOREIGN KEY (DNI_Usuario) REFERENCES LOS_VIAJEROS_DEL_ANONIMATO.USUARIO(DNI),
FOREIGN KEY (CodigoPasaje) REFERENCES LOS_VIAJEROS_DEL_ANONIMATO.COMPRA_CLIENTE(CodigoPasaje),
FOREIGN KEY (CodigoCanje)  REFERENCES LOS_VIAJEROS_DEL_ANONIMATO.CANJE(CodigoCanje)

);





CREATE TABLE LOS_VIAJEROS_DEL_ANONIMATO.Devolucion(
CodigoDevolucion  INT IDENTITY (1,1),
CodigoPasaje   INT,
NumeroVoucher   INT,
Motivo nvarchar(255) ,

PRIMARY KEY (CodigoDevolucion),
FOREIGN KEY (CodigoPasaje) REFERENCES LOS_VIAJEROS_DEL_ANONIMATO.COMPRA_CLIENTE
);
