# 🚀 API .NET Market

Bem-vindo ao repositório da **API .NET Market**! Este projeto em .NET é uma solução completa para gerenciamento de um mercado, permitindo operações robustas de gerenciamento de clientes, produtos e compras. Com um design modular, a API é escalável e fácil de manter, oferecendo uma riqueza de funcionalidades por meio de endpoints RESTful.

## Sumário

- [Visão Geral do Projeto](#visão-geral-do-projeto)
- [Arquitetura e Estrutura de Pastas](#arquitetura-e-estrutura-de-pastas)
- [DTOs (Data Transfer Objects)](#dtos-data-transfer-objects)
- [Detalhamento de Endpoints](#detalhamento-de-endpoints)
- [Configuração e Execução](#configuração-e-execução)
- [Contribuição](#contribuição)
- [Licença](#licença)

## Visão Geral do Projeto

Esta API foi desenvolvida para simplificar o gerenciamento de um mercado, focando em operações essenciais como:
- **Clientes**: Cadastro, edição, listagem e gerenciamento de saldo.
- **Compras**: Criação, listagem e finalização de compras, além de gestão de itens.
- **Produtos**: Cadastro, edição, listagem, e controle de inventário.

## Arquitetura e Estrutura de Pastas

A API é organizada em uma estrutura de pastas que segue melhores práticas de desenvolvimento, garantindo clareza e modularidade.

### Diretórios Principais

- **`Context/`**: Contém o `MarketContext` para interação com o banco de dados via Entity Framework.
- **`Models/`**: Define as entidades principais (`Cliente`, `Compra`, `Produto`, `CompraProduto`) que representam as tabelas no banco de dados.
- **`DTOs/`**: Data Transfer Objects para comunicação entre a API e os clientes sem expor diretamente as entidades de banco de dados.
- **`Services/`**: Encapsula a lógica de negócios e interage com o `MarketContext` para manipulação de dados.
- **`Controllers/`**: Define as rotas e controla o fluxo de dados entre a API e os usuários.

## DTOs (Data Transfer Objects)

Os DTOs são fundamentais para garantir que os dados sejam estruturados de forma segura e eficiente. A seguir, exemplos de DTOs em JSON para ilustrar o formato esperado:

### Clientes

#### `ClienteCriacaoDTO`
Usado para criar um novo cliente.

```json
{
  "Nome": "João Silva",
  "Cpf": "123.456.789-00",
  "Email": "joao.silva@email.com",
  "Idade": 30,
  "Saldo": 1000.0
}
```

#### `ClienteEdicaoDTO`
Usado para editar um cliente existente.

```json
{
  "Nome": "João Silva",
  "Cpf": "123.456.789-00",
  "Email": "joao.silva@email.com",
  "Idade": 31
}
```

#### `ClienteExibicaoDTO`
Usado para exibir os detalhes de um cliente.

```json
{
  "Id": 1,
  "Nome": "João Silva",
  "Cpf": "123.456.789-00",
  "Email": "joao.silva@email.com",
  "Idade": 31,
  "Saldo": 1200.0
}
```

### Compras

#### `CompraExibicaoDTO`
Usado para exibir os detalhes de uma compra.

```json
{
  "Id": 10,
  "ClienteId": 1,
  "Nome": "João Silva",
  "Email": "joao.silva@email.com",
  "ValorTotal": "R$ 500,00",
  "DataInicio": "01/01/2023 12:00:00",
  "DataFim": "01/01/2023 12:30:00",
  "Status": "Finalizada",
  "CompraProdutos": [
    {
      "Id": 100,
      "CompraId": 10,
      "ProdutoId": 50,
      "Quantidade": 2
    }
  ]
}
```

### Produtos

#### `ProdutoCriacaoDTO`
Usado para criar um novo produto.

```json
{
  "Nome": "Notebook",
  "Descricao": "Notebook 15 polegadas",
  "Codigo": "NB15",
  "Preco": 3000.0,
  "QuantidadeEstoque": 50
}
```

#### `ProdutoEdicaoDTO`
Usado para editar um produto existente.

```json
{
  "Nome": "Notebook",
  "Descricao": "Notebook 15 polegadas com SSD",
  "Codigo": "NB15SSD",
  "Preco": 3200.0,
  "QuantidadeEstoque": 45,
  "Ativo": true
}
```

#### `ProdutoExibicaoDTO`
Usado para exibir os detalhes de um produto.

```json
{
  "Id": 50,
  "Nome": "Notebook",
  "Descricao": "Notebook 15 polegadas com SSD",
  "Codigo": "NB15SSD",
  "Preco": 3200.0,
  "QuantidadeEstoque": 45,
  "Ativo": true
}
```

### CompraProduto

#### `CompraProdutoExibicaoDTO`
Usado para exibir os detalhes de produtos associados a uma compra.

```json
{
  "Id": 100,
  "CompraId": 10,
  "ProdutoId": 50,
  "Quantidade": 2
}
```

## Detalhamento de Endpoints

Cada endpoint é projetado para desempenhar uma função específica dentro da API. Aqui está uma lista abrangente de endpoints com descrições detalhadas:

### Clientes

- **POST /api/clientes**
  - **Uso**: Criação de um novo cliente.
  - **Requisição**: Envie um `ClienteCriacaoDTO` no corpo da requisição.
  - **Resposta**: `201 Created` com `ClienteExibicaoDTO`.

- **GET /api/clientes**
  - **Uso**: Recupera a lista de clientes.
  - **Resposta**: `200 OK` com uma lista de `ClienteExibicaoDTO`.

- **GET /api/clientes/{id}**
  - **Uso**: Recupera um cliente específico por ID.
  - **Parâmetros**: `id` na URL.
  - **Resposta**: `200 OK` com `ClienteExibicaoDTO` ou `404 Not Found` se não encontrado.

- **PUT /api/clientes/{id}**
  - **Uso**: Atualiza os detalhes de um cliente.
  - **Requisição**: Envie um `ClienteEdicaoDTO` no corpo.
  - **Resposta**: `200 OK` com `ClienteExibicaoDTO`.

- **PUT /api/clientes/{id}/depositar**
  - **Uso**: Adiciona saldo ao cliente.
  - **Requisição**: Valor a ser depositado.
  - **Resposta**: `200 OK` com sucesso, ou `400 Bad Request` se houver erro.

### Compras

- **POST /api/compras/{clienteId}**
  - **Uso**: Inicia uma nova compra para um cliente.
  - **Parâmetros**: `clienteId` na URL.
  - **Resposta**: `201 Created` com `CompraExibicaoDTO`, ou `400 Bad Request` em caso de falha.

- **GET /api/compras**
  - **Uso**: Lista todas as compras realizadas.
  - **Resposta**: `200 OK` com lista de `CompraExibicaoDTO`.

- **GET /api/compras/pendentes**
  - **Uso**: Lista todas as compras pendentes.
  - **Resposta**: `200 OK` com lista de compras pendentes.

- **GET /api/compras/pagas**
  - **Uso**: Lista todas as compras pagas.
  - **Resposta**: `200 OK` com lista de compras pagas.

- **GET /api/compras/{id}**
  - **Uso**: Busca uma compra específica por ID.
  - **Parâmetros**: `id` na URL.
  - **Resposta**: `200 OK` com `CompraExibicaoDTO` ou `404 Not Found`.

- **POST /api/compras/{id}/finalizar**
  - **Uso**: Finaliza uma compra ativa.
  - **Parâmetros**: `id` na URL.
  - **Resposta**: `200 OK` ou `400 Bad Request` com mensagem de erro.

- **POST /api/compras/{id}/adicionar-produto/{produtoId}/quantidade/{quantidade}**
  - **Uso**: Adiciona um produto a uma compra existente.
  - **Parâmetros**: `id`, `produtoId`, `quantidade` na URL.
  - **Resposta**: `200 OK` com `CompraExibicaoDTO` atualizado ou `400 Bad Request` em caso de erro.

### Produtos

- **GET /api/produtos/listar**
  - **Uso**: Lista todos os produtos disponíveis.
  - **Resposta**: `200 OK` com lista de `ProdutoExibicaoDTO`.

- **GET /api/produtos/{id}**
  - **Uso**: Busca um produto específico por ID.
  - **Parâmetros**: `id` na URL.
  - **Resposta**: `200 OK` com `ProdutoExibicaoDTO` ou `404 Not Found`.

- **POST /api/produtos**
  - **Uso**: Cadastra um novo produto.
  - **Requisição**: `ProdutoCriacaoDTO` no corpo.
  - **Resposta**: `201 Created` com `ProdutoExibicaoDTO`.

- **PUT /api/produtos/{id}**
  - **Uso**: Atualiza informações de um produto.
  - **Requisição**: `ProdutoEdicaoDTO` no corpo.
  - **Resposta**: `200 OK` com `ProdutoExibicaoDTO`.

- **PUT /api/produtos/{id}/repor-quantidade/{quantidade}**
  - **Uso**: Atualiza a quantidade em estoque de um produto.
  - **Parâmetros**: `id`, `quantidade` na URL.
  - **Resposta**: `200 OK` ou `400 Bad Request` com mensagem de erro.

- **DELETE /api/produtos/{id}**
  - **Uso**: Remove um produto do sistema.
  - **Parâmetros**: `id` na URL.
  - **Resposta**: `204 No Content`, ou `404 Not Found` se não encontrado.

### CompraProduto

- **GET /api/compras-produtos**
  - **Uso**: Lista todas as relações de produtos e compras.
  - **Resposta**: `200 OK` com lista de `CompraProdutoExibicaoDTO`.

- **GET /api/compras-produtos/produto/{idProduto}**
  - **Uso**: Lista todas as compras que incluem um produto específico.
  - **Parâmetros**: `idProduto` na URL.
  - **Resposta**: `200 OK` com lista de `CompraProdutoExibicaoDTO`.

- **GET /api/compras-produtos/cliente/{clienteId}**
  - **Uso**: Lista todos os produtos comprados por um cliente específico.
  - **Parâmetros**: `clienteId` na URL.
  - **Resposta**: `200 OK` com lista de produtos, ou `404 Not Found` se nenhum produto for encontrado.

## Configuração e Execução

Para configurar e executar o projeto localmente, siga estes passos:

1. **Clone o Repositório**:

   ```sh
   git clone https://github.com/lsouza-dev/api-net-market.git
   ```

2. **Navegue até o Diretório do Projeto**:

   ```sh
   cd api-net-market
   ```

3. **Restaure as Dependências**:

   ```sh
   dotnet restore
   ```

4. **Inicie a Aplicação**:

   ```sh
   dotnet run
   ```

5. **Acesse a API**: Utilize `http://localhost:5000` (ou outra porta especificada) para interagir com a API.

## Contribuição

Quer contribuir para o projeto? Aqui está como você pode começar:

1. Faça um fork deste repositório.
2. Crie uma branch para suas alterações: `git checkout -b minha-nova-feature`.
3. Faça commit das suas alterações: `git commit -m 'Adiciona nova feature'`.
4. Envie para o repositório remoto: `git push origin minha-nova-feature`.
5. Abra um Pull Request.

Apreciamos qualquer tipo de contribuição e estamos ansiosos para colaborar com você!

## Licença

Este projeto está licenciado sob a [MIT License](LICENSE), o que significa que você pode usá-lo livremente, desde que siga os termos da licença.
