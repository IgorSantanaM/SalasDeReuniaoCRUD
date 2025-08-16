# Projeto de Gerenciamento de Reservas de Salas

Este é um guia completo para configurar e executar o ambiente de desenvolvimento do sistema de gerenciamento de reservas da prova prática da UCDB - Desenvolvedor.

##  Prerequisites

Antes de começar, certifique-se de que você tem as seguintes ferramentas instaladas em sua máquina:

- **Docker**: Para executar o banco de dados PostgreSQL em um contêiner.
- **.NET SDK**: (Versão 9) Para compilar e executar a API backend.
- **Node.js e Yarn*[*: Para gerenciar as dependências e executar o projeto frontend em React.

---

## ⚙️ Passo 1: Configuração do Banco de Dados (PostgreSQL com Docker)

O banco de dados é executado em um contêiner Docker para garantir um ambiente limpo e isolado.

1.  **Iniciar o Contêiner do Banco de Dados**:
    Abra seu terminal e execute o comando Docker que você forneceu. Ele irá baixar a imagem do PostgreSQL (se ainda não a tiver) e iniciar um contêiner com as credenciais e o banco de dados pré-configurados.

    ```bash
    docker run --name postgresDB \
      -e POSTGRES_USER=salasdereuniaouser \
      -e POSTGRES_PASSWORD=SalasDeReuniao123 \
      -e POSTGRES_DB=salasdereuniaodb \
      -p 5432:5432 \
      -d postgres:16
    ```

2.  **Verificar se o Contêiner está Rodando**:
    Para confirmar que o banco de dados está ativo, execute:

    ```bash
    docker ps
    ```

    Você deverá ver um contêiner com o nome `postgresDB` na lista.

---

## ⚙️ Passo 2: Configuração do Backend (.NET API)

O backend é responsável por toda a lógica de negócio e comunicação com o banco de dados.

1.  **Navegue até a Pasta do Backend**:
    Abra um novo terminal e navegue até o diretório raiz da sua API .NET (a pasta que contém o arquivo `.sln` ou `.csproj` da WebApi).

2.  **Aplicar as Migrations (Criar as Tabelas)**:
    A API utiliza o Entity Framework Core para gerenciar o esquema do banco de dados. Para criar as tabelas necessárias, execute o comando de atualização do banco de dados. Certifique-se de que o projeto da WebApi esteja definido como projeto de inicialização.

    ```bash
    dotnet ef database update
    ```

    *Observação: Se o comando `dotnet ef` não for reconhecido, instale-o globalmente com `dotnet tool install --global dotnet-ef`.*

3.  **Executar a API**:
    Com as tabelas criadas, inicie o servidor da API. Por padrão, ele será executado na porta `8080`, conforme configurado no `launchSettings.json`.

    ```bash
    dotnet run
    ```

    A API agora estará rodando e pronta para receber requisições em `http://localhost:8080`.

---

## ⚙️ Passo 3: Configuração do Frontend (React com Yarn)

O frontend é a interface com a qual o usuário interage, construída em React.

1.  **Navegue até a Pasta do Frontend**:
    Abra um terceiro terminal e navegue até o diretório do seu projeto React (a pasta que contém o arquivo `package.json`).

2.  **Instalar as Dependências**:
    Use o Yarn para instalar todas as dependências necessárias para o projeto.

    ```bash
    yarn install
    ```

3.  **Executar a Aplicação React**:
    Após a instalação, inicie o servidor de desenvolvimento do React.

    ```bash
    yarn start
    ```

    Isso abrirá automaticamente uma aba no seu navegador com a aplicação rodando em `http://localhost:5173`. A aplicação React fará as chamadas para a API .NET que está rodando em `localhost:8080`.

---

## ✅ Resumo

Ao final desses três passos, você terá:

1.  Um contêiner **Docker** com o banco de dados PostgreSQL rodando na porta `5432`.
2.  O servidor **backend .NET** rodando na porta `8080`.
3.  A aplicação **frontend React** rodando na porta `5173`.

Agora o ambiente de desenvolvimento está completo e pronto para a prova!
