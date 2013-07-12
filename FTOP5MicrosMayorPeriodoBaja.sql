CREATE FUNCTION LOS_VIAJEROS_DEL_ANONIMATO_FTOP5MicrosMayorPeriodoBaja ()
RETURNS TABLE
AS
RETURN(
select TOP 5 Patente, LOS_VIAJEROS_DEL_ANONIMATO.FcalcularDiasBajaMicro(Patente) AS cantidadDias
from LOS_VIAJEROS_DEL_ANONIMATO.PeridoFueraDeServicio
group by Patente
order by cantidadDias desc);