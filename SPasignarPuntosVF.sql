create procedure  LOS_VIAJEROS_DEL_ANONIMATO.SPasignarPuntosVF (@codigoDeViaje int, @fechaExacta datetime)
AS BEGIN

declare @dni numeric(18,0)
declare @monto numeric(18,2)
declare @codCompra numeric(18,0)

declare cur cursor

for select cc.CodigoCompra, cc.DNI_Cliente, cc.MontoUnitario
	from LOS_VIAJEROS_DEL_ANONIMATO.VIAJE v 
		join LOS_VIAJEROS_DEL_ANONIMATO.COMPRA c on (v.CodigoViaje=c.CodigoViaje)
		join LOS_VIAJEROS_DEL_ANONIMATO.COMPRACLIENTE cc on (c.NumeroVoucher = cc.Numero_Voucher)
	where v.CodigoViaje= @codigoDeViaje
	
open cur
FETCH cur into @codCompra, @dni, @monto

WHILE @@FETCH_STATUS = 0
BEGIN

	declare @puntos int = @monto / 5	
	INSERT into LOS_VIAJEROS_DEL_ANONIMATO.PUNTOVF values (@dni, @puntos, @fechaExacta, @codCompra, NULL)
	FETCH cur into @codCompra, @dni, @monto
	
END

close cur
deallocate cur
	
END