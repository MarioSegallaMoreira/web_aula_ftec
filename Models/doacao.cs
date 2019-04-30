using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projeto_web_aula.Models
{
    public class doacao
    {
        public string doador { get; set; }
        public int cpfcnpj_doador { get; set; }
        public int cpfcnpj_donatario { get; set; }
        public string email_donatario { get; set; }
        public double valor { get; set; }
        public string doacao_bens { get; set; }
    }
}