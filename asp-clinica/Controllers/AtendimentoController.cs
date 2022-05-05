using asp_clinica.Dados;
using asp_clinica.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace asp_clinica.Controllers
{
    public class AtendimentoController : Controller
    {
        public void carregaPacientes()
        {
            List<SelectListItem> ag = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=bdClinica;User=root;pwd=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbPaciente order by nomePaciente;", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ag.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
                con.Open();
            }


            ViewBag.paciente = new SelectList(ag, "Value", "Text");
        }

        public void carregaMedico()
        {
            List<SelectListItem> med = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=bdClinica;User=root;pwd=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbMedico order by nomeMedico;", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    med.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
                con.Open();
            }


            ViewBag.medico = new SelectList(med, "Value", "Text");
        }

        AcoesAtendimento acAtend = new AcoesAtendimento();
        public ActionResult Index()
        {
            Session["tipo"] = ""; // por enquanto, o cadastro não é necessário
            return View();
        }
        
        public ActionResult CadAtendimento() // abre a tela
        {
            Session["tipo"] = "";
            carregaMedico();
            carregaPacientes();
            return View();
        }
        [HttpPost]
        public ActionResult CadAtendimento(ModelAtendimento cmAtend) // cadastra no banco
        {
            Session["tipo"] = "";
            carregaMedico();
            carregaPacientes();
            acAtend.TestarAgenda(cmAtend);

            if (cmAtend.confAgendamento == "0") 
            {
                ViewBag.msg = "Horário Indisponível! Tente outro.";
            }
            else
            {   cmAtend.codMedico = Request["medico"];
                cmAtend.codPac = Request["paciente"];
                acAtend.InserirAtendimento(cmAtend);
                ViewBag.msg = "Cadastro Realizado com sucesso!";
            }
                
            return View();
        }
    }
}