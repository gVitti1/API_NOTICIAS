
using System.Collections.Generic;
using System.Linq;
using API_NOTICIAS.Constans;
using API_NOTICIAS.Models;
using API_NOTICIAS.Persistence;
using API_NOTICIAS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_NOTICIAS.Regras
{
    public class RegrasUsuario
    {
        //Classe que contém toda a lógica e métodos para o gerenciamento de usuários
        public readonly NoticiasDbContext _db;

        public RegrasUsuario(NoticiasDbContext dbContetx)
        {
            _db = dbContetx;
        }

        //Método para cadastro de um novo usuário, verifica se já existe um cadastro com o email inserido e depois cria um novo usuario e grava no banco.
        public IActionResult Cadastar(string nome, string email, string senha)
        {
            if (_db.Usuarios.Any(e => e.Email == email))
            {
                return new BadRequestObjectResult("Email já cadastrado.");
            }

            var newUser = new Usuario
            {
                Admin = false,
                Id = Guid.NewGuid(),
                Name = nome,
                Email = email,
                Senha = senha
            };

            _db.Entry(newUser).State = EntityState.Added;
            _db.SaveChanges();

            return new OkObjectResult($"Cadastro realizado com sucesso, este é seu Id cadastral, NUNCA o compartilhe : {newUser.Id}");
        }


        //Método que realiza o login, verifica se o email inserido é cadastrado, verifica se a senha está correta e se o usuario é admin ou não.
        //Se o usuário não for Admin ele recebe um token de User, se for Admin, recebe um token de Admin.
        public IActionResult Logar(string email, string senha)
        {
            var usuario = _db.Usuarios.FirstOrDefault(e => e.Email == email);

            if (usuario == null)
            {
                return new BadRequestObjectResult("Usuário não encontrado");
            }

            if (senha == usuario.Senha)
            {

                if (usuario.Admin == true)
                {
                    //Retorna o JWT de Admin caso o usuario seja admin.
                    var IdAdmin = usuario.Id;
                    var tokenAdmin = AuthServices.GenerateTokenAdmin(usuario);

                    var authResultAdmin = new AuthResult
                    {
                        Token = tokenAdmin,
                        IdUsuario = IdAdmin
                    };

                    return new OkObjectResult(authResultAdmin);
                }

                //Retorna o JWT de User caso o usuario não seja admin.
                var IdUsuario = usuario.Id;
                var tokenUser = AuthServices.GenerateTokenUser(usuario);

                var authResultUser = new AuthResult
                {
                    Token = tokenUser,
                    IdUsuario = IdUsuario
                };

                return new OkObjectResult(authResultUser);
            }

            return new BadRequestObjectResult("Senha incorreta");
        }


        //Método que torna um usuário do nível User em um usuário de nível Admin
        //Basicamente é um campo que pede um código. Se o código inserido for correto, o User se torna Admin.
        //Se o user já tiver o cargo de Admin, retorna uma string dizendo que ele já é um admin.
        public IActionResult verificaAdmin(string email, string codigo)
        {
            var usuario = _db.Usuarios.Where(e => e.Email == email).FirstOrDefault();
            if (usuario == null)
            {
                return new BadRequestObjectResult("Email não cadastrado.");
            }

            if (codigo == CodigoAdmin.codigoAdmin) //CodigoAdmin é uma classe armazenada na pasta Constans.
            {
                if (usuario.Admin == true)
                {
                    return new BadRequestObjectResult("Você já tem acesso de Administrador.");
                }

                usuario.Admin = true;
                _db.SaveChanges();
                return new OkObjectResult("Código correto, acesso de administrador liberado, faça login novamente.");
            }

            return new BadRequestObjectResult("Código incorreto, acesso de administrador negado.");

        }
    }
}
