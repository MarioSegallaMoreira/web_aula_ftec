using projeto_web_aula.banco;
using projeto_web_aula.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projeto_web_aula.Controllers
{
    public class usuarioController : Controller
    {
        // GET: usuario
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult salva_doacao(doacao doacao)
        {
            if(ModelState.IsValid)
            {
                HttpContext c = System.Web.HttpContext.Current;
                var usuario_logado = salva_session.GetDataFromSession<usuario>(c, "usuario_logado");
                doacao.cpfcnpj_doador = usuario_logado.cpfcnpj;
                banco_doacao banco_doacao = new banco_doacao();
                banco_doacao.fazer_doacao(doacao);
                return View();
            }
            else
            {
                return View();
            }
        }
    }
}