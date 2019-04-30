using projeto_web_aula.banco;
using projeto_web_aula.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace projeto_web_aula.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult admin()
        {
            var usuario = cria_sessao();
            return View(usuario);
        }
        public ActionResult usuario()
        {
            return View();
        }
        public ActionResult login_usuario(usuario usuario)
        {
            if (ModelState.IsValid)
            {
                conexao_banco insere_usuario = new conexao_banco();
                var permissao = insere_usuario.login_usuario(usuario.nome_usuario, usuario.cpfcnpj);

                if (permissao == 99)
                {

                    return RedirectToAction("admin");
                }
                else if (permissao == 1)
                {
                    System.Web.HttpContext c = System.Web.HttpContext.Current;
                    salva_session.SetDataToSession<usuario>(c, "usuario_logado", usuario);
                    return RedirectToAction("usuario");
                    
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
        private List<usuario> cria_sessao()
        {
            System.Web.HttpContext c = System.Web.HttpContext.Current;
            conexao_banco conn = new conexao_banco();
            var usuario = conn.lista_usuario();
            var lista_usuarios = salva_session.GetDataFromSession<List<usuario>>(c, "usuarios");

            if(lista_usuarios!=null)
            {
                foreach (var item in usuario)
                {
                    foreach (var user in lista_usuarios)
                    {
                        if (item.cpfcnpj != user.cpfcnpj)
                        {
                            salva_session.SetDataToSession<List<usuario>>(c, "usuarios", item);
                        }
                    }
                }                
            }
           
            return usuario;
         }        
        [HttpPost]
        public ActionResult dados_usuario(Guid ID)
        {
            System.Web.HttpContext c = System.Web.HttpContext.Current;

            var lista_usuarios = salva_session.GetDataFromSession<List<usuario>>(c,"usuarios");

            var usuario = lista_usuarios.FirstOrDefault(d => d.Id == ID);
            if (usuario.permissao == 1)
            {
                usuario.permissao = 0;
            }
            else
            {
                usuario.permissao = 1;

            }
            salva_session.SetDataToSession<List<usuario>>(c, "usuarios", lista_usuarios);

            return Json(usuario);
        }
        public ActionResult gravar_lista_usuario()
        {
            System.Web.HttpContext c = System.Web.HttpContext.Current;
            var lista_usuarios = salva_session.GetDataFromSession<List<usuario>>(c, "usuarios");
            if (ModelState.IsValid)
            {
                conexao_banco insere_usuario = new conexao_banco();
                insere_usuario.salva_lista_usuario(lista_usuarios);

                return View();
            }
            else
            {
                return View();
            }
        }

        private List<doacao> busca_doacao()
        {
            HttpContext c = System.Web.HttpContext.Current;
            var usuario_logado = salva_session.GetDataFromSession<usuario>(c, "usuario_logado");
            var cpf = usuario_logado.cpfcnpj;


            banco_doacao banco_doacao = new banco_doacao();
            var lista =  banco_doacao.busca_lista_doacao(cpf);
            return lista;
        }
    }
}