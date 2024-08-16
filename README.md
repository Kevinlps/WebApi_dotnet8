readme_content = """
# Web API com .NET 8

Este projeto é uma Web API desenvolvida em C# utilizando .NET 8. A API permite a manipulação de informações sobre autores e livros, oferecendo endpoints para criar, listar, editar e excluir registros.

## Tecnologias Utilizadas

- C#
- .NET 8
- ASP.NET Core
- Entity Framework Core

## Estrutura do Projeto

O projeto é organizado em dois controladores principais:

1. **AutorController**: Gerencia as operações relacionadas aos autores.
2. **LivroController**: Gerencia as operações relacionadas aos livros.

## Endpoints

### AutorController

- **GET** `api/Autor/ListarAutores`: Lista todos os autores cadastrados.
  
- **GET** `api/Autor/BuscarAutorPorId/{idAutor}`: Busca um autor específico pelo ID.

- **GET** `api/Autor/BuscarAutorPorIdLivro/{idLivro}`: Busca um autor específico pelo ID do livro.

- **POST** `api/Autor/CriarAutor`: Cria um novo autor.

### LivroController

- **GET** `api/Livro/ListarLivros`: Lista todos os livros cadastrados.
  
- **GET** `api/Livro/BuscarLivroPorId/{idLivro}`: Busca um livro específico pelo ID.

- **GET** `api/Livro/BuscarLivroPorIdAutor/{idAutor}`: Busca livros de um autor específico pelo ID do autor.

- **POST** `api/Livro/CriarLivro`: Cria um novo livro.
