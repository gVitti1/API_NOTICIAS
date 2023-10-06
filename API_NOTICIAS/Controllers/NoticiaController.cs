using API_NOTICIAS.Constans;
using API_NOTICIAS.Models;
using API_NOTICIAS.Regras;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace API_NOTICIAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoticiaController : ControllerBase
    {
        //Controlador onde estão os endpoints referentes a notícias.
        //Alguns endpoints são autorizados apenas para Admins e outros livres para Users e Admins.

        private readonly RegrasNoticia _regrasNoticia;

        public NoticiaController(RegrasNoticia regrasNoticia)
        {
            _regrasNoticia = regrasNoticia;
        }


        //Endpoint de criação de notícia, autorizado apenas para Admins.
        [Authorize(Roles = Claims.Admin)]
        [HttpPost("/noticia/criar")]
        public IActionResult CadastrarNoticia(string categoria, string nome, string texto)
        {
            var cadastrado = _regrasNoticia.Criar(categoria, nome, texto);
            return Ok(cadastrado);
        }

        //Endpoint de exibição das notícias, autorizado para Users e Admins.
        [HttpGet("/noticia/exibir")]
        [Authorize(Roles = $"{Claims.User},{Claims.Admin}")]
        public IActionResult VerNoticias()
        {
            var exibir = _regrasNoticia.Exibir();
            return Ok(exibir);
        }

        //Endpoint de atualização de uma notícia, autorizado apenas para Admins.
        [Authorize(Roles = Claims.Admin)]
        [HttpPatch("noticias/atualizar")]
        public IActionResult AtualizarNoticia(Guid id, string categoria, string nome, string texto)
        {
            var atualizada = _regrasNoticia.Atualizar(id, categoria, nome, texto);
            return Ok(atualizada);
        }

        //Endpoint de exclusão de uma notícia, autorizado apenas para Admins.
        [Authorize(Roles = Claims.Admin)]
        [HttpDelete("/noticia/excluir")]
        public IActionResult ExcluirNoticia(Guid id)
        {
            var deletada = _regrasNoticia.Deletar(id);
            return Ok(deletada);
        }

        //Endpoint que se comporta como uma barra de pesquisa, autorizado para Users e Admins.
        [HttpGet("/noticia/pesquisar")]
        [Authorize(Roles = $"{Claims.User},{Claims.Admin}")]

        public IActionResult Pesquisa(string termo)
        {
            var pesquisa = _regrasNoticia.Pesquisar(termo);
            return Ok(pesquisa);
        }

        //Endpoint que permite o usuário comentar em uma notícia, autorizado para Users e Admins.
        //Pede para que o usuário coloque o seu Id cadastral, o título da notícia em que deseja comentar e o texto do comentário.
        [HttpPost("/noticia/comentar")]
        [Authorize(Roles = $"{Claims.User},{Claims.Admin}")]
        public IActionResult Comentario(Guid UsuarioId, string tituloNoticia, string comentario)
        {
            var comentar = _regrasNoticia.Comentar(UsuarioId, tituloNoticia, comentario);
            return Ok(comentar);
        }
    }
}
