using asp_clinica.Models;
using MySql.Data.MySqlClient;
using System;

namespace asp_clinica.Dados
{
    public class AcoesLogin
    {
        conexao con = new conexao();

        public void TestarUsuario(ModelLogin user)
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbLogin where usuario = @usuario and senha = @senha", con.MyConectarBD());

            cmd.Parameters.Add("@usuario", MySqlDbType.VarChar).Value = user.usuario;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = user.senha;

            MySqlDataReader leitor;

            leitor = cmd.ExecuteReader();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {
                    user.usuario = Convert.ToString(leitor["usuario"]);
                    user.senha = Convert.ToString(leitor["senha"]);
                    user.codTipoUsuario = Convert.ToString(leitor["codTipoUsuario"]);
                }
            }
            else
            {
                user.usuario = null;
                user.senha = null;
                user.codTipoUsuario = null;
            }

            con.MyDesconectarBD();
        }
    }
}