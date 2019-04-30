using projeto_web_aula.banco;
using projeto_web_aula.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using System.Web.Mvc;

namespace projeto_web_aula.Controllers
{
    public class adminController : Controller
    {
        public ActionResult admin()
        {
            var usuario = cria_sessao();
            return View(usuario);
        }
        private List<usuario> cria_sessao()
        {
            System.Web.HttpContext c = System.Web.HttpContext.Current;
            conexao_banco conn = new conexao_banco();
            var usuario = conn.lista_usuario();
            salva_session.SetDataToSession<List<usuario>>(c, "usuarios", usuario);
            return usuario;
        }
    }
}