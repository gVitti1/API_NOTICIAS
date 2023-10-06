using API_NOTICIAS.Persistence;
using API_NOTICIAS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace API_NOTICIAS.Regras
{
    public class RegrasNoticia
    {
        private readonly NoticiasDbContext _db;

        public RegrasNoticia(NoticiasDbContext context)
        {
            _db = context;
        }

        //Cria e registra uma nova notícia baseada nos parâmetros inseridos no método.
        public IActionResult Criar(string categoria, string nome, string texto)
        {
            List<string> comentarios = new List<string>();

            var novaNoticia = new Noticia
            {
                Id = new Guid(),
                Nome = nome,
                Categoria = categoria,
                Texto = texto,
                Comentarios = comentarios
            };

            _db.Entry(novaNoticia).State = EntityState.Added;
            _db.SaveChanges();
            return new OkObjectResult("Noticia cadastrada com sucesso");
        }

        //Método de exibição das notícias.
        //Retorna uma lista de notícias extraída do banco de dados.
        public IActionResult Exibir()
        {
            var noticias = _db.Noticias.ToList();
            if (noticias.Count == 0)
            {
                return new BadRequestObjectResult("Erro ao exibir as notícias, não há notícias cadastradas.");
            }

            return new OkObjectResult(noticias);
        }

        //Método para atualizar uma Notícia.
        //Encontra a notícia pelo Id inserido, e as altera com os parâmetros inseridos.
        public IActionResult Atualizar(Guid id, string categoria, string nome, string texto)
        {
            var NoticiaAlterada = _db.Noticias.FirstOrDefault(i => i.Id == id);

            if (NoticiaAlterada == null)
            {
                return new BadRequestObjectResult("Noticia solicitada para alteração não encontrada!");
            }

            NoticiaAlterada.Categoria = categoria;
            NoticiaAlterada.Nome = nome;
            NoticiaAlterada.Texto = texto;

            _db.SaveChanges();
            return new OkObjectResult("Notícia atualizada com sucesso.");
        }

        //Método de Deletar uma Nótícia.
        //Encontra a notícia atraves do Id inserido e apaga seu registro.
        public IActionResult Deletar(Guid id)
        {
            var NoticiaDeletar = _db.Noticias.FirstOrDefault(i => i.Id == id);

            if (NoticiaDeletar == null)
            {
                return new BadRequestObjectResult("Noticia solicitada para exclusão não encontrada!");
            }

            _db.Noticias.Remove(NoticiaDeletar);
            _db.SaveChanges();
            return new OkObjectResult("Notícia excluída com sucesso.");
        }

        //METODO DE BARRA DE PESQUISA 
        //Recebe uma string que é comparada com os dados das colunas Categoria e Texto da tabela de Noticias.
        //Retorna qualquer notícia que tenha o Texto ou Nome parecidas com o parâmetro do método.
        public IActionResult Pesquisar(string termo)
        {
            var noticiasEncontradas = _db.Noticias
                .Where(n => EF.Functions.ILike(n.Nome, $"%{termo}%") || EF.Functions.ILike(n.Categoria, $"%{termo}%"))
                .ToList();

            if (noticiasEncontradas.Count == 0)
            {
                return new BadRequestObjectResult($"Nenhma notícia encontrada com o termo : {termo}");
            }

            return new OkObjectResult(noticiasEncontradas);
        }

        //MÉTODO PARA ADICIONAR UM COMENTARIO A UMA NOTICIA
        public IActionResult Comentar(Guid UsuarioId, string tituloNoticia, string comentario)
        {
            var usuarioComentando = _db.Usuarios.Where(i => i.Id == UsuarioId).FirstOrDefault();
            if (usuarioComentando == null)
            {
                return new BadRequestObjectResult("Id de usuário incorreto");
            }

            var noticiaDesejada = _db.Noticias.FirstOrDefault(n => EF.Functions.ILike(n.Nome, $"%{tituloNoticia}%"));

            if (noticiaDesejada == null)
            {
                return new BadRequestObjectResult($"Nenhuma notícia encontrada com o titulo:{tituloNoticia}");
            }

            noticiaDesejada.Comentarios.Add($"{usuarioComentando.Name} : {comentario}");
            _db.SaveChanges();

            return new OkObjectResult("Comentário adicionado com sucesso!");

        }
    }
}
