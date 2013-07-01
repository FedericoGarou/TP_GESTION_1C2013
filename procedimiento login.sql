create procedure LOS_VIAJEROS_DEL_ANONIMATO.login(@usuario nvarchar(255), @pass nvarchar(255), @respuesta nvarchar(255) output)
AS 
BEGIN                  
 
declare @existeUsuario INT = (SELECT COUNT(*) FROM LOS_VIAJEROS_DEL_ANONIMATO.Login_Usuario WHERE Username = @usuario);                   
                   
if (@existeUsuario = 1)
begin                                          
	declare @cantidadIntentosFallidos INT = (SELECT Intentos_Fallidos FROM LOS_VIAJEROS_DEL_ANONIMATO.Login_Usuario WHERE Username = @usuario);
	    
	if (@cantidadIntentosFallidos < 3)
	begin
		declare @existeUsuarioyContraseña INT = (SELECT COUNT(*) FROM LOS_VIAJEROS_DEL_ANONIMATO.Login_Usuario WHERE Username = @usuario and Passwd = @pass);
		
		if (@existeUsuarioyContraseña = 1)
		begin
			UPDATE LOS_VIAJEROS_DEL_ANONIMATO.Login_Usuario SET Intentos_Fallidos=0 WHERE Username = @usuario;
			set @respuesta='abrir sesion'
			
		--	Close();
		--	new Pantalla_Inicial(usuario).Show();
		end
		else
		begin
			UPDATE LOS_VIAJEROS_DEL_ANONIMATO.Login_Usuario SET Intentos_Fallidos=(Intentos_Fallidos+1) WHERE Username = @usuario;
			set @cantidadIntentosFallidos = (@cantidadIntentosFallidos + 1);
			declare @cantidadIntentosFallidosString nvarchar(255) = @cantidadIntentosFallidos;
			
			set @respuesta = 'Contraseña incorrecta, vuelva a intentarlo;Cantidad de intentos fallidos: ' + (@cantidadIntentosFallidosString);
		--	new Dialogo("Contraseña incorrecta, vuelva a intentarlo;Cantidad de intentos fallidos: " + (cantidadIntentosFallidos + 1), "Aceptar").ShowDialog();
		end

	end
	else
	begin
		set @respuesta = 'Su usuario esta bloqueado, por sobrepasar la cantidad de logueos incorrectos';
	--	new Dialogo("Su usuario esta bloqueado, por sobrepasar la cantidad de logueos incorrectos", "Aceptar").ShowDialog();
	end  
end
else
begin 
set @respuesta = 'No existe el usuario, vuelva a intentarlo';                
--new Dialogo("No existe el usuario, vuelva a intentarlo", "Aceptar").ShowDialog();                        
end
                    
END
