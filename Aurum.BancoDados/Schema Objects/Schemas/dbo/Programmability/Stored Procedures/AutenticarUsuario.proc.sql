CREATE PROCEDURE [dbo].[AutenticarUsuario]
	@login VARCHAR(20),
	@senha VARCHAR(MAX)
AS


SELECT CodUsuario, Login, Nome, Email
FROM Usuario
WHERE Ativo = 1
  AND Login = @login
  AND SenhaPass = HASHBYTES('MD5', @senha)


IF @@ROWCOUNT = 0
	RETURN 1


RETURN 0