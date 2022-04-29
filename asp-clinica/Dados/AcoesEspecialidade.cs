using asp_clinica.Models;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace asp_clinica.Dados
{
    public class AcoesEspecialidade
    {
        conexao con = new conexao();
        public void InserirEsp(ModelEspecialidade cmEspecialidade) //Método para inserir tipo do usuário
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbEspecialidade values (default, @Esp)", con.MyConectarBD());
            cmd.Parameters.Add("@Esp", MySqlDbType.VarChar).Value = cmEspecialidade.Esp;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public DataTable ConsultarEsp()//Método de consulta do tipo de usuário
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbEspecialidade", con.MyDesconectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable Esp = new DataTable();
            da.Fill(Esp);
            con.MyDesconectarBD();
            return Esp;
        }
    }
}