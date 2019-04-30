using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projeto_web_aula.Models
{
    public class usuario
    {
        public Guid Id { get; set; }

        public string nome_usuario { get; set; }
        public string email { get; set; }


        public int cpfcnpj { get; set; }
        public int telefone { get; set; }
        public string endereco { get; set; }
        public int cep { get; set; }
        public string cidade { get; set; }
        public int permissao { get; set; }

        public usuario()
        {
            Id = Guid.NewGuid();
        }
    }
}