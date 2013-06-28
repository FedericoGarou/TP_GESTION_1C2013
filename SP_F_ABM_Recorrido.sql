-- PROCEDURES

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

-- FUNCTIONS

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
	    
CREATE FUNCTION LOS_VIAJEROS_DEL_ANONIMATO.F_Ciudades ()
RETURNS TABLE
AS
RETURN (SELECT 0 as RN,'No seleccionado' as NombreCiudad
	    UNION
	    SELECT ROW_NUMBER() OVER(ORDER BY C.NombreCiudad ASC) as RN,C.NombreCiudad FROM LOS_VIAJEROS_DEL_ANONIMATO.CIUDAD C
	    );
	    
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