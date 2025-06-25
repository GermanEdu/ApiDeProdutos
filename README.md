# 🧩 ApiDeProdutos - ASP.NET Core Web API

Esta é uma API RESTful desenvolvida com **ASP.NET Core 8**, utilizando **Entity Framework Core**, **SQL Server** e **autenticação via JWT**.  
Ela fornece funcionalidades para **autenticação de usuários** e **CRUD de produtos**, servindo como backend para uma aplicação frontend React.

---

## 🚀 Funcionalidades

- ✅ Login e Registro com autenticação JWT
- ✅ CRUD completo de Produtos
- ✅ Busca de produtos por nome
- ✅ Proteção de rotas com `[Authorize]`
- ✅ Swagger UI para documentação
- ✅ Suporte a CORS para frontend local (React)

---

## 🛠️ Tecnologias

- ASP.NET Core 8
- Entity Framework Core
- SQL Server LocalDB
- Identity
- JWT Bearer Token
- Swagger (Swashbuckle)

---

## ⚙️ Instalação

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [SQL Server LocalDB ou SQL Server Express](https://learn.microsoft.com/pt-br/sql/database-engine/configure-windows/sql-server-express-localdb)

---

### Passo a passo

```bash
# 1. Clone o repositório
git clone https://github.com/seu-usuario/ApiDeProdutos.git
cd ApiDeProdutos

# 2. Restaure os pacotes
dotnet restore

# 3. Crie o banco de dados e aplique as migrations
dotnet ef database update

# 4. Rode o projeto
dotnet run

A API estará disponível em http://localhost:5240
Acesse http://localhost:5240/swagger para ver a documentação.

🔐 Endpoints principais
Autenticação
POST /api/Usuario/CriarUsuario

POST /api/Usuario/LogarUsuario

Produtos (JWT obrigatório)
GET /api/Produtos

GET /api/Produtos/{id}

GET /api/Produtos/ProdutosPorNome?nome=...

POST /api/Produtos

PUT /api/Produtos/{id}

DELETE /api/Produtos/{id}

🌐 CORS
A política de CORS permite acesso do frontend (React):

csharp
Copiar
Editar
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5174")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

app.UseCors("ReactPolicy");
📂 Estrutura do Projeto
mathematica
Copiar
Editar
ApiDeProdutos/
├── Controllers/
│   └── ProdutosController.cs
│   └── UsuarioController.cs
├── Models/
│   └── Produto.cs
├── Services/
│   └── ProdutoService.cs
│   └── AuthenticateService.cs
├── Context/
│   └── AppDbContext.cs
├── ViewModels/
│   └── LoginModel.cs
│   └── RegisterModel.cs
├── Migrations/
├── appsettings.json
└── Program.cs
🔒 Segurança
Tokens JWT com Authorization: Bearer {token}

Rotas protegidas com [Authorize]

🧪 Comandos úteis
bash
Copiar
Editar
dotnet ef migrations add NomeDaMigration
dotnet ef database update
dotnet run
👨‍💻 Desenvolvedor
Nome: Seu Nome Aqui

GitHub: https://github.com/GermanEdu
