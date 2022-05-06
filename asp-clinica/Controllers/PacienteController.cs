using asp_clinica.Dados;
using asp_clinica.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace asp_clinica.Controllers
{
    public class PacienteController : Controller
    {
        AcoesPaciente ap = new AcoesPaciente();
        // GET: Paciente
        public void CarregaPac()
        {
            List<SelectListItem> Pac = new List<SelectListItem>();
            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=bdClinica;User=root;pwd=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbPaciente", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Pac.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();

            }
            ViewBag.Pac = new SelectList(Pac, "Value", "Text");
        }
        public ActionResult Index()
        {
            Session["tipo"] = "";
            return View();
        }
        public ActionResult CadPac()
        {
            Session["tipo"] = "";
            CarregaPac();
            return View();

        }
        [HttpPost]
        public ActionResult CadPac(ModelPaciente cmPac)
        {
            Session["tipo"] = "";
            CarregaPac();
            cmPac.CodPac = Request["CodPac"];
            ap.InserirPac(cmPac);
            Response.Write("<script>alert('Cadastro efetuado com sucesso!')</script>");
            return View();

        }
        public ActionResult ConsPac()
        {
            Session["tipo"] = "";
            GridView dgv = new GridView();
            dgv.DataSource = ap.ConsultarPac();
            dgv.DataBind();
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            dgv.RenderControl(htw);
            ViewBag.GridViewString = sw.ToString();
            return View();
        }
    }
}