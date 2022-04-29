using MySql.Data.MySqlClient;
using asp_clinica.Dados;
using asp_clinica.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace asp_clinica.Controllers
{
    public class EspecialidadeController : Controller
    {
        AcoesEspecialidade ae = new AcoesEspecialidade();

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
            ViewBag.tipoUsu = new SelectList(Esp, "Value", "Text");
        }
        public ActionResult Index()
        {
            Session["tipo"] = "";
            return View();
        }
        public ActionResult CadEsp()
        {
            Session["tipo"] = "";
            CarregaEsp();
            return View();
            
            
        }
        [HttpPost]
        public ActionResult CadEsp(ModelEspecialidade cmEsp)
        {
            Session["tipo"] = "";
            CarregaEsp();
                cmEsp.CodEsp = Request["CodEsp"];
                ae.InserirEsp(cmEsp);
                Response.Write("<script>alert('Cadastro efetuado com sucesso!')</script>");
                return View();
        }
         public ActionResult ConsEsp()
        {
            Session["tipo"] = "";
            GridView dgv = new GridView();
            dgv.DataSource = ae.ConsultarEsp();
            dgv.DataBind();
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            dgv.RenderControl(htw);
            ViewBag.GridViewString = sw.ToString();
            return View();
        }
       



    }
}