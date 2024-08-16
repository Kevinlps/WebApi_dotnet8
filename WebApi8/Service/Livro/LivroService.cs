﻿using Microsoft.EntityFrameworkCore;
using WebApi8.Data;
using WebApi8.Dto.Autor;
using WebApi8.Dto.Livro;
using WebApi8.Models;

namespace WebApi8.Service.Livro
{
    public class LivroService : ILivroInterface
    {
        private readonly AppDbContext _context;

        public LivroService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro)
        {
            ResponseModel<LivroModel> resposta = new ResponseModel<LivroModel>();
            try
            {

                var livro = await _context.Livros.Include(a => a.Autor)
                    .FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);
                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum livro localizado!";
                    return resposta;
                }

                resposta.Dados = livro;
                resposta.Mensagem = "livro foi localizado!";

                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> BuscarLivroPorIdAutor(int idAutor)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {

                var livro = await _context.Livros
                    .Include(a => a.Autor)
                    .Where(livroBanco => livroBanco.Id == idAutor)
                    .ToListAsync();
                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum Livro localizado!";
                    return resposta;
                }
                resposta.Dados = livro;
                resposta.Mensagem = "Livro localizado!";

                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }


        public async Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroCriacaoDto livroCriacaoDto)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {

                var autor = await _context.Autores
                    .FirstOrDefaultAsync(autorBanco => autorBanco.Id == livroCriacaoDto.Autor.Id);
                if (autor == null)
                {
                    resposta.Mensagem = "Nenhum registro de autor realizado!";
                    return resposta;
                }

                var livro = new LivroModel()
                {
                    Titulo = livroCriacaoDto.Titulo,
                    Autor = autor
                };
                _context.Add(livro);
                await _context.SaveChangesAsync();  

                resposta.Dados = await _context.Livros.Include(a => a.Autor).ToListAsync(); 
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> EditarLivro(LivroEdicaoDto livroEdicaoDto)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {
                var livro = await _context.Livros
                    .Include(autor => autor.Autor)
                    .FirstOrDefaultAsync(livroBanco => livroBanco.Id == livroEdicaoDto.Id);
                var autor = await _context.Autores
                    .FirstOrDefaultAsync(autorbanco => autorbanco.Id == livroEdicaoDto.Autor.Id);
                
                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum livro localizado!";
                    return resposta;
                }
                if (autor == null)
                {
                    resposta.Mensagem = "Nenhum livro localizado!";
                    return resposta;
                }

                livro.Autor = autor;
                livro.Titulo = livroEdicaoDto.Titulo;


                _context.Update(livro);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Livros.ToListAsync();
                resposta.Mensagem = "livro foi alterado!";

                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int idLivro)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {

                var livro = await _context.Livros.Include(a => a.Autor)
                    .FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);
                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum livro localizado!";
                    
                    return resposta;
                }
                _context.Remove(livro);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Livros.ToListAsync();
                resposta.Mensagem = "livro foi apagado!";

                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> ListarLivros()
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {

                var livros = await _context.Livros.Include(a => a.Autor).ToListAsync();

                resposta.Dados = livros;
                resposta.Mensagem = "Todos os livros foram coletados!";

                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

    }
}

