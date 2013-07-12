CREATE FUNCTION LOS_VIAJEROS_DEL_ANONIMATO.FcalcularDiasBajaMicro(@patente nvarchar(255))
RETURNS int
AS BEGIN
	declare @cantidadDias int = 0
	declare @fechaInicio datetime
	declare @fechaFin datetime
	
	declare cur cursor 
	for select FechaInicio, FechaFin 
		from LOS_VIAJEROS_DEL_ANONIMATO.PeridoFueraDeServicio 
		where Patente = @patente
		
	open cur 
	fetch cur into @fechaInicio, @fechaFin
	
	while @@FETCH_STATUS = 0
	begin	
			
			--A FINES PRACTICOS TOMAMOS TODOS LOS MESES DE 30 DIAS
			
			set @cantidadDias = @cantidadDias + ((YEAR(@fechaFin) - YEAR(@fechaInicio))*360)
			
			set @cantidadDias = @cantidadDias + ((MONTH(@fechaFin) - MONTH(@fechaInicio))*30)
			
			set @cantidadDias = @cantidadDias +  (DAY(@fechaFin) - DAY(@fechaInicio))
			
			
		fetch cur into @fechaInicio, @fechaFin
	
	end
	
	
RETURN @cantidadDias

END