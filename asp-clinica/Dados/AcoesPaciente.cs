using asp_clinica.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace asp_clinica.Dados
{
    public class AcoesPaciente
    {
        conexao con = new conexao();

        public void InserirPac(ModelPaciente cmPac) //Método para inserir tipo do usuário
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbPaciente values (default, @NomePac, @TelPac, @EmailPac)", con.MyConectarBD());
            cmd.Parameters.Add("@NomePac", MySqlDbType.VarChar).Value = cmPac.NomePac;
            cmd.Parameters.Add("@TelPac", MySqlDbType.VarChar).Value = cmPac.TelPac;
            cmd.Parameters.Add("@EmailPac", MySqlDbType.VarChar).Value = cmPac.EmailPac;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public DataTable ConsultarPac()//Método de consulta do tipo de usuário
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbPaciente", con.MyDesconectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable Pac = new DataTable();
            da.Fill(Pac);
            con.MyDesconectarBD();
            return Pac;
        }
           
    }
}