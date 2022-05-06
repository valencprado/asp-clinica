using asp_clinica.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace asp_clinica.Dados
{
    public class AcoesMedico
    {
        conexao con = new conexao();

        public void InserirMed(ModelMedico cmMed) //Método para inserir tipo do usuário
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbMedico values (default, @NomeMedico, @Esp)", con.MyConectarBD());
            cmd.Parameters.Add("@nomeMedico", MySqlDbType.VarChar).Value = cmMed.NomeMedico;
            cmd.Parameters.Add("@Esp", MySqlDbType.VarChar).Value = cmMed.Esp;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public DataTable ConsultarMed()//Método de consulta do tipo de usuário
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbMedico", con.MyDesconectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable Med = new DataTable();
            da.Fill(Med);
            con.MyDesconectarBD();
            return Med;
        }

    }
}