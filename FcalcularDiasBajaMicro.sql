CREATE FUNCTION LOS_VIAJEROS_DEL_ANONIMATO.FcalcularDiasBajaMicro(@patente nvarchar(255), @año int, @semestre int)
RETURNS int
AS BEGIN
	declare @cantidadDias int = 0
	declare @fechaInicio datetime
	declare @fechaFin datetime
	declare @mesInicial int 
	declare @mesFinal int
	
	if (@semestre = 1)
	begin
		set @mesInicial = 1
		set @mesFinal = 6
	end
	
	if (@semestre = 2)
	begin
		set @mesInicial = 7
		set @mesFinal = 12
	end
	
	
	
	declare cur cursor 
	for select FechaInicio, FechaFin 
		from LOS_VIAJEROS_DEL_ANONIMATO.PeridoFueraDeServicio 
		where Patente = @patente and 
			YEAR(fechaInicio)=@año and YEAR(FechaFin)=@año and 
			MONTH(FechaInicio) BETWEEN @mesInicial AND @mesFinal and
			MONTH(FechaFin) BETWEEN @mesInicial AND @mesFinal
		
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
	
	close cur 
	deallocate cur
	
	
RETURN @cantidadDias

END