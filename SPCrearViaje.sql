Create PROCEDURE LOS_VIAJEROS_DEL_ANONIMATO.SPCrearViaje
 ( @PatenteMicro nvarchar(255), 
 @Fecha datetime, 
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
    @Fecha, 
    @PatenteMicro, 
    (@Fecha+0.5),
    (@Fecha+0.5))
    
END;
