# Knights API

Esta é uma API para gerenciamento de Knights, construída em .NET 7 e MongoDB.

## Executando a API com Docker

### Pré-requisitos

- Docker instalado

### Instruções

1. Clone o repositório e navegue até o diretório do projeto.

   ```bash
   git clone https://github.com/hericksonn/knights_api.git
   cd KnightsApi


2. Criaçãoo do Banco MongoDB

   Abra o terminal e conecte-se ao MongoDB usando o shell:

   ```bash
   mongosh

   Crie o banco de dados knightsDB e a coleção knights:

   ```bash
   use knightsDB
   db.createCollection('knights')

   (Opcional) Insira alguns dados iniciais na coleção para testes:

   ```bash
    db.knights.insertOne({
        name: "Laurenti",
        nickname: "Knight",
        birthday: new Date("1995-01-01"),
        weapons: [
        { name: "sword", mod: 3, attr: "strength", equipped: true }
        ],
        attributes: { strength: 0, dexterity: 0, constitution: 0, intelligence: 0, wisdom: 0, charisma: 0 },
        keyAttribute: "strength"
    })


3. Construa a imagem Docker

   ```bash
   build_and_run.bat

4. Acesso ao Swagger

   ```bash
   https://localhost:7244/swagger/index.html
