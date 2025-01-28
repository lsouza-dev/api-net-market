# API .NET Market

Bem-vindo ao repositório da **API .NET Market**! Este projeto é uma implementação abrangente de uma API que permite a gestão de clientes, produtos e compras. Desenvolvida em .NET, esta API oferece endpoints para a criação, listagem, edição e exclusão de recursos. 

## Sumário

- [Estrutura do Projeto](#estrutura-do-projeto)
- [DTOs (Data Transfer Objects)](#dtos-data-transfer-objects)
- [Endpoints Detalhados](#endpoints-detalhados)
- [Como Executar](#como-executar)
- [Contribuição](#contribuição)
- [Licença](#licença)

## Estrutura do Projeto

O projeto está organizado em diferentes diretórios, cada um com uma função específica dentro da arquitetura da aplicação.

### Diretórios Principais

#### Context

- **MarketContext**: Faz a ponte entre as entidades do .NET e o banco de dados, permitindo operações CRUD simplificadas.

#### Models

- **Entidades**: Representam as tabelas do banco de dados, como `Cliente`, `Compra`, `Produto`, e `CompraProduto`.

#### DTOs (Data Transfer Objects)

Os DTOs são fundamentais para a troca de dados entre a API e o cliente. Eles encapsulam e validam os dados sem expor diretamente as entidades do banco de dados.

#### Services

- **Serviços**: Contêm a lógica de negócios da aplicação e interagem com o `MarketContext` para acessar e manipular dados.

#### Controllers

- **Controllers**: Gerenciam as requisições HTTP, interagem com os serviços e retornam respostas aos clientes.

## DTOs (Data Transfer Objects)

Os DTOs são usados para transferir dados de forma segura e estruturada. Abaixo estão os exemplos de DTOs em formato JSON, que mostram como os dados são estruturados.

### Clientes

#### ClienteCriacaoDTO
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

#### ClienteEdicaoDTO
Usado para editar um cliente existente.

```json
{
  "Nome": "João Silva",
  "Cpf": "123.456.789-00",
  "Email": "joao.silva@email.com",
  "Idade": 31
}
```

#### ClienteExibicaoDTO
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

#### CompraExibicaoDTO
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

#### ProdutoCriacaoDTO
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

#### ProdutoEdicaoDTO
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

#### ProdutoExibicaoDTO
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

#### CompraProdutoExibicaoDTO
Usado para exibir os detalhes de produtos associados a uma compra.

```json
{
  "Id": 100,
  "CompraId": 10,
  "ProdutoId": 50,
  "Quantidade": 2
}
```

## Endpoints Detalhados

Conheça os endpoints disponíveis e como utilizá-los de forma eficiente.

### Clientes

- **POST /api/clientes**
  - **Uso**: Cria um novo cliente.
  - **Parâmetros**: `ClienteCriacaoDTO` no corpo da requisição.
  - **Resposta**: Retorna `201 Created` com `ClienteExibicaoDTO`.

- **GET /api/clientes**
  - **Uso**: Lista todos os clientes.
  - **Resposta**: Retorna `200 OK` com uma lista de `ClienteExibicaoDTO`.

- **GET /api/clientes/{id}**
  - **Uso**: Busca um cliente por ID.
  - **Parâmetros**: `id` na URL.
  - **Resposta**: `200 OK` com `ClienteExibicaoDTO`, `404 Not Found` caso não encontrado.

- **PUT /api/clientes/{id}**
  - **Uso**: Edita informações de um cliente.
  - **Parâmetros**: `id` na URL, `ClienteEdicaoDTO` no corpo da requisição.
  - **Resposta**: `200 OK` com `ClienteExibicaoDTO`.

- **PUT /api/clientes/{id}/depositar**
  - **Uso**: Realiza depósito no saldo do cliente.
  - **Parâmetros**: `id` na URL, `valor` no corpo.
  - **Resposta**: `200 OK` com mensagem de sucesso ou `400 Bad Request` em caso de erro.

### Compras

- **POST /api/compras/{clienteId}**
  - **Uso**: Cria nova compra para um cliente.
  - **Parâmetros**: `clienteId` na URL.
  - **Resposta**: `201 Created` com `CompraExibicaoDTO` ou `400 Bad Request` com `ErroDTO`.

- **GET /api/compras**
  - **Uso**: Lista todas as compras.
  - **Resposta**: `200 OK` com lista de `CompraExibicaoDTO`.

- **GET /api/compras/pendentes**
  - **Uso**: Lista compras pendentes.
  - **Resposta**: `200 OK` com lista de compras pendentes.

- **GET /api/compras/pagas**
  - **Uso**: Lista compras pagas.
  - **Resposta**: `200 OK` com lista de compras pagas.

- **GET /api/compras/{id}**
  - **Uso**: Busca compra por ID.
  - **Parâmetros**: `id` na URL.
  - **Resposta**: `200 OK` com `CompraExibicaoDTO` ou `404 Not Found`.

- **POST /api/compras/{id}/finalizar**
  - **Uso**: Finaliza uma compra.
  - **Parâmetros**: `id` na URL, `valor` no corpo.
  - **Resposta**: `200 OK` ou `400 Bad Request` com `ErroDTO` em caso de erro.

- **POST /api/compras/{id}/adicionar-produto/{produtoId}/quantidade/{quantidade}**
  - **Uso**: Adiciona produto a uma compra.
  - **Parâmetros**: `id`, `produtoId`, `quantidade` na URL.
  - **Resposta**: `200 OK` com `CompraExibicaoDTO` ou `400 Bad Request` com `ErroDTO`.

### Produtos

- **GET /api/produtos/listar**
  - **Uso**: Lista todos os produtos.
  - **Resposta**: `200 OK` com lista de `ProdutoExibicaoDTO`.

- **GET /api/produtos/{id}**
  - **Uso**: Busca produto por ID.
  - **Parâmetros**: `id` na URL.
  - **Resposta**: `200 OK` com `ProdutoExibicaoDTO` ou `404 Not Found`.

- **POST /api/produtos**
  - **Uso**: Cadastra novo produto.
  - **Parâmetros**: `ProdutoCriacaoDTO` no corpo.
  - **Resposta**: `201 Created` com `ProdutoExibicaoDTO`.

- **PUT /api/produtos/{id}**
  - **Uso**: Edita um produto.
  - **Parâmetros**: `id` na URL, `ProdutoEdicaoDTO` no corpo.
  - **Resposta**: `200 OK` com `ProdutoExibicaoDTO`.

- **PUT /api/produtos/{id}/repor-quantidade/{quantidade}**
  - **Uso**: Repõe a quantidade de um produto.
  - **Parâmetros**: `id`, `quantidade` na URL.
  - **Resposta**: `200 OK` com `ProdutoExibicaoDTO` ou `400 Bad Request` com `ErroDTO`.

- **DELETE /api/produtos/{id}**
  - **Uso**: Exclui um produto.
  - **Parâmetros**: `id` na URL.
  - **Resposta**: `204 No Content` ou `404 Not Found`.

## Como Executar

Para executar o projeto, siga as etapas abaixo:

1. **Clone o repositório**:

   ```sh
   git clone https://github.com/lsouza-dev/api-net-market.git
   ```

2. **Navegue até o diretório do projeto**:

   ```sh
   cd api-net-market
   ```

3. **Restaure as dependências do projeto**:

   ```sh
   dotnet restore
   ```

4. **Execute a aplicação**:

   ```sh
   dotnet run
   ```

5. **Acesse a API** através do endpoint padrão (`http://localhost:5000` ou similar).

## Contribuição

Se você deseja contribuir para este projeto, sinta-se à vontade para abrir uma issue ou enviar um pull request. Todos os tipos de contribuições são bem-vindos!

## Licença

Este projeto está licenciado sob a [MIT License](LICENSE).
