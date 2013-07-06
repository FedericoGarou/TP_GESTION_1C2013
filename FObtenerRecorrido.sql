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
            
  --  SELECT * FROM LOS_VIAJEROS_DEL_ANONIMATO.VIAJE WHERE
  --  CodigoRecorrido=@codigoRecorrido and 
  --  PatenteMicro=@PatenteMicro
--)    
END;
