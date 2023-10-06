using API_NOTICIAS.Models;
using API_NOTICIAS.Constans;
using API_NOTICIAS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_NOTICIAS.Regras;

namespace API_NOTICIAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        //Controlador onde estão os endpoints referentes a usuários.
        //Os endpoints não necessitam de nenhuma autorização para serem acessados.

        private readonly RegrasUsuario _regasUsuario;

        public UsuarioController(RegrasUsuario regasUsuario)
        {
            _regasUsuario = regasUsuario;
        }

        //Endpoint para cadastro de um novo usuário.
        [HttpPost("/cadastro")]
        public IActionResult Cadastro(string nome, string email, string senha) =>
            _regasUsuario.Cadastar(nome, email, senha);

        //Endpoint para login.
        [HttpGet("/login")]
        public IActionResult Login(string email, string senha) =>
            _regasUsuario.Logar(email, senha);

        //Endpoint que torna um User em Admin.
        [Authorize(Roles = $"{Claims.User},{Claims.Admin}")]
        [HttpGet("/validar-admin")]
        public IActionResult VerificaAdmin(string email, string codigo) =>
            _regasUsuario.verificaAdmin(email, codigo);
    }
}


