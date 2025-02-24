# 📋 API de Gerenciamento de Tarefas

Esta API RESTful foi desenvolvida em .NET Core 8.0 para gerenciar uma lista de tarefas, permitindo criar, listar, atualizar e excluir tarefas de forma eficiente e segura.

---

## 🚀 **Visão Geral**
Esta API tem como objetivo fornecer uma solução simples e escalável para o gerenciamento de tarefas. Ela permite que os usuários:
- 📄 **Listem** todas as tarefas cadastradas.
- 📝 **Criem** novas tarefas com informações detalhadas.
- 🔄 **Atualizem** tarefas existentes.
- ❌ **Excluam** tarefas que não são mais necessárias.

---

## 🛠️ **Tecnologias Utilizadas**
- **.NET Core 8.0** – Framework principal da aplicação.
- **ASP.NET Core Web API** – Criação dos endpoints RESTful.
- **Entity Framework Core** – Acesso e manipulação do banco de dados.
- **SQLite** – Banco de dados leve para testes e desenvolvimento.
- **CQRS com MediatR** – Separação de comandos e consultas.
- **JWT (JSON Web Tokens)** – Autenticação e autorização dos usuários.
- **Swagger** – Documentação automática e interativa da API.
- **xUnit** – Testes unitários.

---

## 🔒 **Requisitos Não Funcionais**
### ✅ **Segurança**
- Implementação de autenticação via JWT para proteger os endpoints.
- Proteção contra ataques comuns (ex.: injeção de SQL via EF Core, validação de dados).

### ⚡ **Desempenho**
- Respostas rápidas e otimizadas com uso de consultas eficientes.
- Paginação para listas de tarefas extensas (se aplicável).

### 📈 **Escalabilidade**
- Arquitetura modular com CQRS para facilitar futuras expansões.
- Banco de dados substituível sem grandes mudanças no código.

---

## 📑 **Endpoints Disponíveis**

### 🔍 **Tarefas** (`/api/tasks`)
- **GET** `/api/Tasks` – Listar todas as tarefas.
- **POST** `/api/Tasks` – Criar uma nova tarefa.
- **GET** `/api/Tasks/{id}` – Obter uma tarefa específica.
- **PUT** `/api/Tasks/{id}` – Atualizar uma tarefa existente.
- **DELETE** `/api/Tasks/{id}` – Excluir uma tarefa.

### 📝 **Exemplo de Requisição - Criar Tarefa (POST)**
```json
{
  "title": "Estudar .NET Core",
  "description": "Revisar conceitos de API RESTful e CQRS.",
  "completionDate": "2025-03-01T00:00:00"
  "isCompleted": false
}
```

### ✅ **Exemplo de Resposta (201 Created)**
```json
{
  "id": "a9a1b761-724f-462a-adf1-ecc9eab97043",
  "title": "Estudar .NET Core",
  "description": "Revisar conceitos de API RESTful e CQRS.",
  "completionDate": "2025-03-01T00:00:00"
  "isCompleted": false
}
```

---

## 🏗️ **Como Executar o Projeto Localmente**
1. Clone o repositório:
   ```bash
   git clone https://github.com/JoseAilsonJam/JoseApiRest.git
   ```
2. Navegue até a pasta do projeto e restaure os pacotes:
   ```bash
   cd JoseA
   dotnet restore
   ```
3. **Execução das Migrations:**  
   A API está configurada para **rodar automaticamente as migrations** ao iniciar a aplicação. Isso cria e atualiza o banco de dados automaticamente.
4. Inicie a aplicação:
   ```bash
   dotnet run
   ```
5. Acesse o Swagger para testar os endpoints: [http://localhost:44335/swagger](http://localhost:44335/swagger)

---

## 🧪 **Testes**
Para rodar os testes unitários:
```bash
dotnet test
```

---


## 📝 **Licença**
Este projeto está sob a licença MIT.


