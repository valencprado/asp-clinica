using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using asp_clinica.Dados;
using asp_clinica.Models;
using MySql.Data.MySqlClient;

namespace asp_clinica.Controllers
{
    public class MedicoController : Controller
    {
        AcoesMedico am =   new AcoesMedico();
        // GET: Medico
        public void CarregaEsp()
        {
            List<SelectListItem> Esp = new List<SelectListItem>();
            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=bdClinica;User=root;pwd=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbEspecialidade", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Esp.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();

            }
            ViewBag.especialidade = new SelectList(Esp, "Value", "Text");
        }
        public ActionResult Index()
        {
            Session["tipo"] = "";
            return View();
        }
        public ActionResult CadMed()
        {
            Session["tipo"] = "";
            CarregaEsp();
            return View();

        }
        [HttpPost]
        public ActionResult CadMed(ModelMedico cmMed)
        {
            Session["tipo"] = "";
            CarregaEsp();
            cmMed.Esp = Request["especialidade"];
            am.InserirMed(cmMed);
            Response.Write("<script>alert('Cadastro efetuado com sucesso!')</script>");
            return View();

        }
        public ActionResult ConsMed()
        {
            Session["tipo"] = "";
            GridView dgv = new GridView();
            dgv.DataSource = am.ConsultarMed();
            dgv.DataBind();
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            dgv.RenderControl(htw);
            ViewBag.GridViewString = sw.ToString();
            return View();
        }
        
    }
}