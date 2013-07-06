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
