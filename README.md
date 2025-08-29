desafio-usuarios

👤 API de Usuários (ASP.NET Core + PostgreSQL)

API REST simples pra cadastrar, listar, atualizar e remover usuários. Enum de Gênero vai como texto no JSON e no banco. Tem validação e Swagger pra testar.

 Tecnologias

.NET 8 (ASP.NET Core)

Entity Framework Core (Npgsql)

PostgreSQL

FluentValidation

Swagger (Swashbuckle)

⚙️ O que a API faz

👥 CRUD de usuários

🧭 Paginação em listagem (page, size)

🔤 Genero como string: Masculino, Feminino, Outro

✅ Validações básicas e e-mail único

📘 Swagger pra testar os endpoints

▶️ Como rodar (Docker Compose)

Já tem Dockerfile e docker-compose.yml na raiz.

# na raiz do projeto
docker compose down -v
docker compose up -d --build
docker compose ps

Abra: http://localhost:5076/swagger

Para parar e manter os dados: docker compose down
Para parar e apagar os dados: docker compose down -v

Como rodar local (sem Docker)

Configure sua connection string em appsettings.json:

{
  "ConnectionStrings": {
    "Default": "Host=localhost;Port=5432;Database=desafio;Username=postgres;Password=postgres"
  }
}

Restaure e rode:

dotnet restore
dotnet run

Swagger: veja a porta no console (ou em launchSettings.json).

As migrations rodam no startup (o schema é criado/atualizado ao iniciar).

Base: /api/v1/usuarios

GET /api/v1/usuarios?page=1&size=10 

GET /api/v1/usuarios/{id} 

POST /api/v1/usuarios 

PUT /api/v1/usuarios/{id} 

DELETE /api/v1/usuarios/{id} 
