using Npgsql;
using projeto_web_aula.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace projeto_web_aula.banco
{
    public class banco_doacao
    {
        string conn_banco = "User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=web_aula";

        public void fazer_doacao(doacao doacao)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(conn_banco))
                {
                    conn.Open();
                    NpgsqlCommand comando = conn.CreateCommand();
                    comando.CommandText = "insert into doacao values(@doador_cfpcnpj,@donatario_cfpcnpj,@valor,@bem,@email)";

                    comando.Parameters.Add("@doador_cfpcnpj", NpgsqlTypes.NpgsqlDbType.Integer).Value = doacao.cpfcnpj_doador;
                    comando.Parameters.Add("@donatario_cfpcnpj", NpgsqlTypes.NpgsqlDbType.Integer).Value = doacao.cpfcnpj_donatario;
                    comando.Parameters.Add("@valor", NpgsqlTypes.NpgsqlDbType.Numeric).Value = doacao.valor != 0 ? doacao.valor :0 ;
                    comando.Parameters.Add("@bem", NpgsqlTypes.NpgsqlDbType.Varchar).Value = doacao.doacao_bens ?? "" ;
                    comando.Parameters.Add("@email", NpgsqlTypes.NpgsqlDbType.Varchar).Value = doacao.email_donatario;                

                    comando.ExecuteNonQuery();                 
                 
                   
                    string mensagem = doacao.valor != 0 ? "voce recebeu uma doação no valor de R$: " + doacao.valor.ToString() : "Você recebeu um(a): " + doacao.doacao_bens.ToString();


                    SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
                    var mail = new MailMessage();
                    //o email que deseja usar para enviar.Nao salvei no banco senha/usuario por questao de segurança;
                    mail.From = new MailAddress("seu_email_aqui@hotmail.com");
                    mail.To.Add(doacao.email_donatario);
                    mail.Subject = "VOCE RECEBEU UMA DOACAO";  
                    mail.Body = mensagem;
                    SmtpServer.Port = 587;
                    SmtpServer.UseDefaultCredentials = false;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("seu_email_aqui@hotmail.com", "sua_senha_aqui");
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(mail);


                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
        }


        public List<doacao> busca_lista_doacao(int cpfcnpj)
        {
            List<doacao> lista_doacao= new List<doacao>();
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(conn_banco))
                {
                    conn.Open();
                    NpgsqlCommand comando = conn.CreateCommand();
                    comando.CommandText = "select * from doacao where donatario_cfpcnpj = " + cpfcnpj;

                    var resultado = comando.ExecuteReader();
                    while (resultado.HasRows)
                    {
                        resultado.Read();
                        doacao usuario = new doacao()
                        {
                            cpfcnpj_doador = Convert.ToInt32(resultado["doador_cfpcnpj"]),
                            cpfcnpj_donatario = Convert.ToInt32(resultado["donatario_cfpcnpj"]),
                            valor = Convert.ToDouble(resultado["valor"]),
                            doacao_bens = resultado["bem"].ToString(),
                            email_donatario = resultado["email"].ToString(),

                        };
                        lista_doacao.Add(usuario);
                    }



                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }
            return lista_doacao;
        }
    }
}