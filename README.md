# API_NOTICIAS
API de Notícias
Este é um projeto de API Web ASP.NET Core para uma aplicação de notícias, criado como parte de um desafio técnico. A API oferece funcionalidades de autenticação, CRUD de notícias, comentários em notícias, pesquisa e diferentes tipos de autorização (usuário e administrador).

Tecnologias Utilizadas
ASP.NET Core
Entity Framework
PostgreSQL
Swagger
Instruções para Conexão com o Banco de Dados
1. Instalação dos Pacotes Necessários
Certifique-se de que você tenha os seguintes pacotes instalados:

Microsoft.EntityFrameworkCore (versão 7.0.11)
Microsoft.EntityFrameworkCore.Tools (versão 7.0.11)
Npgsql.EntityFrameworkCore.PostgreSQL (versão 7.0.11)
2. Configurar a String de Conexão
No arquivo Program.cs, você encontrará um local marcado para inserir a string de conexão com o banco de dados. Certifique-se de configurá-lo corretamente.

3. Executar as Migrações
Após configurar a string de conexão, abra a interface de linha de comando do gerenciador de pacotes NuGet e execute as migrações com o seguinte comando:

mathematica
Copy code
Update-Database
Isso aplicará as migrações pendentes ao banco de dados, criando ou atualizando-o de acordo com o estado definido pelas migrações.

Após concluir essas etapas, execute o programa e ele deverá funcionar como esperado.

Pacotes Utilizados e Suas Versões
Autenticação por JWT
Microsoft.AspNetCore.Authentication.JwtBearer (versão 7.0.11)
System.IdentityModel.Tokens.Jwt (versão 7.0.2)
Microsoft.IdentityModel.Tokens (versão 7.0.2)
Banco de Dados
Microsoft.EntityFrameworkCore (versão 7.0.11)
Microsoft.EntityFrameworkCore.Tools (versão 7.0.11)
Npgsql.EntityFrameworkCore.PostgreSQL (versão 7.0.11)
Documentação da API (Swagger/OpenAPI)
Microsoft.AspNetCore.OpenApi (versão 7.0.9)
Swashbuckle.AspNetCore (versão 6.5.0)
Este projeto utiliza uma combinação de tecnologias poderosas para oferecer funcionalidades avançadas de notícias. Sinta-se à vontade para explorar a API e contribuir para o seu desenvolvimento.
