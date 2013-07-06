Create PROCEDURE LOS_VIAJEROS_DEL_ANONIMATO.SPMicroOcupadoEnFecha
 ( @PatenteMicro nvarchar(255), @Fecha datetime, @retorno bit output )
AS
BEGIN
    IF EXISTS(SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.VIAJE v
				WHERE (
					v.PatenteMicro = @PatenteMicro AND
					FechaSalida > (@fecha-1) AND
					FechaSalida < @fecha+1				
				) )
		Begin
		SET @retorno = 1; -- Ya tiene viaje asignado		
		end
	ELSE
		begin
		SET @retorno = 0; -- Micro libre		
		end
END;
