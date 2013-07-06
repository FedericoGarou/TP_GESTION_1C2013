Create PROCEDURE LOS_VIAJEROS_DEL_ANONIMATO.SPRegistrarLLegada
 ( @CodigoViaje nvarchar(255), 
 @FechaExacta datetime 
 )
AS
BEGIN
    
    UPDATE LOS_VIAJEROS_DEL_ANONIMATO.VIAJE SET FechaLlegada=@FechaExacta WHERE CodigoViaje=@CodigoViaje
    
END;
