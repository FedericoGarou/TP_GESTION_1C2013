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