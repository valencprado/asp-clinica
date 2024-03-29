﻿using MySql.Data.MySqlClient;
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
    public class HomeController : Controller
    {
        AcoesLogin acL = new AcoesLogin();
        AcoesTipoUsu acTipo = new AcoesTipoUsu();

        public void carregaTipoUsu() 
        {
            List<SelectListItem> tipoUsu = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=bdClinica;User=root;pwd=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbTipoUsuario", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    tipoUsu.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();

            }
            ViewBag.tipoUsu = new SelectList(tipoUsu, "Value", "Text");
        }

        public ActionResult Index() 
        {
            Session["tipo"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult Index(ModelLogin cm) 
        {
            acL.TestarUsuario(cm);

            if (cm.usuario != null)
            {
                FormsAuthentication.SetAuthCookie(cm.usuario, false);
                Session["usu"] = cm.usuario;
                Session["tipo"] = cm.codTipoUsuario;

                return RedirectToAction("About", "Home");
            }
            else
            {
                ViewBag.msgLogar = "Usuário e/ou senha incorreto(s)";
                return View();
            }
        }

        public ActionResult SemAcesso() 
        {
            Response.Write("<script>alert('Sem Acesso - Faça o login no sistema')</script>");
            return View();
        }

        public ActionResult About() 
        {
            if (Session["usu"] != null)
            {
                ViewBag.Message = "Your application description page.";
                ViewBag.Usuario = Session["usu"];
                return View();
            }
            else
            {
                return RedirectToAction("SemAcesso", "Home");
            }
        }

        public ActionResult Contact()
        {
            if (Session["usu"] != null && Session["tipo"].ToString() == "1")
            {
                ViewBag.Message = "Your contact page.";
                return View();
            }
            else
            {
                return RedirectToAction("SemAcesso", "Home");
            }
        }

        public ActionResult Logout()
        {
            Session["usu"] = null;
            Session["tipo"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult CadTipo()
        {
            if (Session["usu"] != null && Session["tipo"].ToString() == "1")
            {
                return View();
            }
            else
            {

                return RedirectToAction("SemAcesso", "Home");
            }
        }

        [HttpPost]
        public ActionResult CadTipo(ModelTipoUser cmTipo) 
        {
            acTipo.inserirTipoUsu(cmTipo);
            ViewBag.msgCad = "Cadastro Efetuado";
            return View();
        }

        public ActionResult ConsCadTipo() 
        {
            if (Session["usu"] != null && Session["tipo"].ToString() == "1")
            {
                GridView dgv = new GridView();
                dgv.DataSource = acTipo.ConsultaTipo();
                dgv.DataBind();
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                dgv.RenderControl(htw);
                ViewBag.GridViewString = sw.ToString();
                return View();
            }
            else 
            {
                return RedirectToAction("SemAcesso", "Home");
            }
        }

        public ActionResult CadLogin() 
        {
            if (Session["usu"] != null && Session["tipo"].ToString() == "1")
            {
                carregaTipoUsu();
                return View();
            }
            else
            {

                return RedirectToAction("SemAcesso", "Home");
            }
        }
        [HttpPost]
        public ActionResult CadLogin(ModelLogin cmLogin) 
        {
            carregaTipoUsu();
            if (cmLogin.senha == cmLogin.confirmaSenha)
            { 

                cmLogin.codTipoUsuario = Request["tipoUsu"];
                acL.inserirLogin(cmLogin);
                Response.Write("<script>alert('Cadastro efetuado com sucesso!')</script>");
            }
            else
            {
                Response.Write("<script>alert('As senhas não estão iguais.')</script>");
            }
            return View();
        }

        
    }
}