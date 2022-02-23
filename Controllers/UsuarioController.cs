using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller

    {
        public IActionResult ListaDeUsuarios()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);
            List<Usuario> listagem = new UsuarioService().Listar();
            return View(listagem);
        }
        public IActionResult editarUsuario(int id)
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);
            Usuario user = new UsuarioService().Listar(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult editarUsuario(Usuario userEditado)
        {
            userEditado.Senha = Cripotografo.TextoCriptografado(userEditado.Senha);
            UsuarioService us = new UsuarioService();
            us.editarUsuario(userEditado);
            return RedirectToAction("ListaDeUsuarios");
        }
        public IActionResult RegistrarUsuarios()
        {
            //autenticacao
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarUsuarios(Usuario novoUser)
        {
            //cuidar atenticaçao
            //questoes de criptografia de senha
            
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);
            novoUser.Senha = Cripotografo.TextoCriptografado(novoUser.Senha);

            UsuarioService us = new UsuarioService();
            us.incluirUsuario(novoUser);
            return RedirectToAction("ListaDeUsuarios");
        }

        public IActionResult ExcluirUsuario(int id)
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            UsuarioService us = new UsuarioService();
            us.excluirUsuario(id);
            return RedirectToAction("ListaDeUsuarios");
        }

        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult NeedAdmin()
        {
            //Cuidar Questao de Autenticacao
            Autenticacao.CheckLogin(this);

            return View();
        }
    }
}