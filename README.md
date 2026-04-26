# 🤖 Chat IA com Blazor e Ollama

Aplicação de chat com inteligência artificial desenvolvida em **C# Blazor**, utilizando **Ollama** (llama3.2:3b) para processamento local de IA e **PostgreSQL** para persistência de dados.

## ✨ Funcionalidades

- 💬 **Chat em tempo real** com IA local (Ollama)
- 📝 **Múltiplos chats** gerenciados independentemente
- 💾 **Persistência** de histórico de conversas no PostgreSQL
- 🎨 **Interface moderna** inspirada em design moderno
- ⚡ **Streaming de respostas** em tempo real
- 🚀 **Sem necessidade de autenticação** - acesso direto

## 🏗️ Arquitetura

```
CC-CHATIA/
├── Components/
|   └── Layout/
│       └── MainLayout.razor     # Componente de layout com side bar
│   └── Pages/
│       └── Home.razor           # Componente principal do chat
├── Data/
│   └── AppDbContext.cs          # Contexto do Entity Framework
├── Interfaces/
    └── IChatService.cs          # Interface do serviço de gerenciamento de chats
    ├── IOllamaService.cs        # Interface do serviço de integração com Ollama
├── Models/
│   ├── Chat.cs                  # Modelo de chat
│   └── ChatMessage.cs           # Modelo de mensagem
├── Services/
    └── ChatService.cs           # Serviço de gerenciamento de chats
    ├── OllamaService.cs         # Serviço de integração com Ollama

```

## 🛠️ Tecnologias Utilizadas

- **Blazor Server** - Framework web interativo
- **Entity Framework Core** - ORM para acesso ao banco de dados
- **PostgreSQL** - Banco de dados relacional
- **Ollama** - Motor de IA local (llama3.2:3b)
- **Npgsql** - Provider PostgreSQL para .NET

## 📋 Pré-requisitos

Antes de começar, certifique-se de ter instalado:

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [PostgreSQL 15+](https://www.postgresql.org/download/)
- [Ollama](https://ollama.ai/download)
- Modelo llama3.2:3b instalado no Ollama

### Instalando o Ollama e o modelo

```bash
# Instalar Ollama (Windows)
# Baixe o instalador em: https://ollama.com/download/windows

# Baixar o modelo llama3.2:3b
ollama run llama3.2:3b

# Verificar se o Ollama está rodando
ollama list
```

## 🚀 Instalação e Configuração

### 1. Clone o repositório

```bash
git clone <seu-repositorio>
cd CC-CHATIA
```

### 2. Execute as migrations

```bash
# Aplicar as migrations no banco de dados
dotnet ef database update
```

### 7. Execute a aplicação

```bash
dotnet watch run
```

Acesse: `https://localhost:5001` ou `http://localhost:5000`

## 📦 Estrutura do Banco de Dados

### Tabela `chats`

| Campo      | Tipo      | Descrição                    |
|------------|-----------|------------------------------|
| Id         | UUID      | Identificador único          |
| Title      | VARCHAR   | Título do chat               |
| CreatedAt  | TIMESTAMP | Data de criação              |
| UpdatedAt  | TIMESTAMP | Data da última atualização   |

### Tabela `messages`

| Campo      | Tipo      | Descrição                    |
|------------|-----------|------------------------------|
| Id         | UUID      | Identificador único          |
| ChatId     | UUID      | Referência ao chat (FK)      |
| Content    | TEXT      | Conteúdo da mensagem         |
| Role       | VARCHAR   | "user" ou "assistant"        |
| CreatedAt  | TIMESTAMP | Data de criação              |

## 🎯 Como Usar

1. **Criar novo chat**: Clique no botão "+ NOVO CHAT" na sidebar
2. **Selecionar chat**: Clique em um chat existente na lista
3. **Enviar mensagem**: Digite sua mensagem e pressione Enter ou clique no botão de enviar
4. **Aguardar resposta**: A IA processará sua mensagem e responderá em tempo real com streaming

**Feito com Blazor 🔥 e Ollama 🦙**
