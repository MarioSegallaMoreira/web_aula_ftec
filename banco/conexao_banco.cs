using projeto_web_aula.Models;
using System;
using Npgsql;
using System.Collections.Generic;

namespace projeto_web_aula.banco
{
    public class conexao_banco 
    {

        string conn_banco = "User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=web_aula";
        private int permissao;

        public void salva_usuario(usuario usuario)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(conn_banco))
                {
                    conn.Open();
                    NpgsqlCommand comando = conn.CreateCommand();
                    comando.CommandText = "insert into usuario values(@nome_usuario,@cpfcnpj,@email,@telefone,@endereco,@cep,@cidade,@permissao)";
                    
                    comando.Parameters.Add("@nome_usuario", NpgsqlTypes.NpgsqlDbType.Varchar).Value = usuario.nome_usuario;
                    comando.Parameters.Add("@cpfcnpj", NpgsqlTypes.NpgsqlDbType.Integer).Value = usuario.cpfcnpj;
                    comando.Parameters.Add("@email", NpgsqlTypes.NpgsqlDbType.Varchar).Value = usuario.email;
                    comando.Parameters.Add("@telefone", NpgsqlTypes.NpgsqlDbType.Integer).Value = usuario.telefone;
                    comando.Parameters.Add("@endereco", NpgsqlTypes.NpgsqlDbType.Varchar).Value = usuario.endereco;
                    comando.Parameters.Add("@cep", NpgsqlTypes.NpgsqlDbType.Integer).Value = usuario.cep;
                    comando.Parameters.Add("@cidade", NpgsqlTypes.NpgsqlDbType.Varchar).Value = usuario.cidade;
                    comando.Parameters.Add("@permissao", NpgsqlTypes.NpgsqlDbType.Integer).Value = 0;

                    comando.ExecuteNonQuery();

                }
            }
            catch (Exception ex )
            {

                Console.WriteLine(ex.ToString());
            }
            
        }

        public int login_usuario(string usuario,int cpfcnpj)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(conn_banco))
                {
                    conn.Open();
                    NpgsqlCommand comando = conn.CreateCommand();
                    comando.CommandText = "select * from usuario where nome_usuario = '"+usuario.ToUpper()+"' and cpfcnpj = "+ cpfcnpj;                 

                    var resultado = comando.ExecuteReader();
                    if(resultado.HasRows)
                    {
                        resultado.Read();
                        permissao = Convert.ToInt32(resultado["permissao"]);
                    }
                    else
                    {
                        permissao = 0;
                    }
                    return permissao;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return permissao;          
            }
        }


        public List<usuario> lista_usuario()
        {
            List<usuario> lista_usuarios = new List<usuario>();
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(conn_banco))
                {
                    conn.Open();
                    NpgsqlCommand comando = conn.CreateCommand();
                    comando.CommandText = "select * from usuario where permissao != 99";

                    var resultado = comando.ExecuteReader();
                    while (resultado.HasRows)
                    {
                        resultado.Read();
                        usuario usuario = new usuario()
                        {
                            nome_usuario = resultado["nome_usuario"].ToString(),
                            cpfcnpj = Convert.ToInt32(resultado["cpfcnpj"]),
                            email = resultado["email"].ToString(),
                            telefone = Convert.ToInt32(resultado["telefone"]),
                            endereco = resultado["endereco"].ToString(),
                            cep = Convert.ToInt32(resultado["cep"]),
                            cidade = resultado["cidade"].ToString(),
                            permissao = Convert.ToInt32(resultado["permissao"])
                        };
                        lista_usuarios.Add(usuario);
                    }
                
                   

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
               
            }
            return lista_usuarios;
        }

        public int atualiza_usuario(usuario usuario)
        {
            int resultado = 0;
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(conn_banco))
                {
                    conn.Open();
                    NpgsqlCommand comando = conn.CreateCommand();
                    comando.CommandText = "update usuario set permissao = 1 where cpfcnpj = "+ usuario.cpfcnpj;

                    resultado = comando.ExecuteNonQuery();
                    return resultado = 0;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return resultado = -99;
            }
        }

        public void salva_lista_usuario(List<usuario> lista_usuario)
        {
            try
            {

                using (NpgsqlConnection conn = new NpgsqlConnection(conn_banco))
                {
                    conn.Open();
                    for (int i = 0; i < lista_usuario.Count; i++)
                    {
                        NpgsqlCommand comando = conn.CreateCommand();
                        comando.CommandText = "insert into usuario values(@nome_usuario,@cpfcnpj,@email,@telefone,@endereco,@cep,@cidade,@permissao)";

                        comando.Parameters.Add("@nome_usuario", NpgsqlTypes.NpgsqlDbType.Varchar).Value = lista_usuario[i].nome_usuario;
                        comando.Parameters.Add("@cpfcnpj", NpgsqlTypes.NpgsqlDbType.Integer).Value = lista_usuario[i].cpfcnpj;
                        comando.Parameters.Add("@email", NpgsqlTypes.NpgsqlDbType.Varchar).Value = lista_usuario[i].email;
                        comando.Parameters.Add("@telefone", NpgsqlTypes.NpgsqlDbType.Integer).Value = lista_usuario[i].telefone;
                        comando.Parameters.Add("@endereco", NpgsqlTypes.NpgsqlDbType.Varchar).Value = lista_usuario[i].endereco;
                        comando.Parameters.Add("@cep", NpgsqlTypes.NpgsqlDbType.Integer).Value = lista_usuario[i].cep;
                        comando.Parameters.Add("@cidade", NpgsqlTypes.NpgsqlDbType.Varchar).Value = lista_usuario[i].cidade;
                        comando.Parameters.Add("@permissao", NpgsqlTypes.NpgsqlDbType.Integer).Value = 0;

                        comando.ExecuteNonQuery();

                    }

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
        }


    }
}