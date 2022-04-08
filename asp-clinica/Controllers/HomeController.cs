using System.Web.Mvc;
using asp_clinica.Models;
using asp_clinica.NewFolder1;
using System.Web.Security;

namespace asp_clinica.Controllers
{
    public class HomeController : Controller
    {
        AcoesLogin acLogin = new AcoesLogin();
        

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ModelLogin cm)
        {
            acLogin.TestarUsuario(cm);
            if (cm.usuario != null)
            {
                FormsAuthentication.SetAuthCookie(cm.usuario, false);
                return RedirectToAction("About", "Home");
            }
            else
            {



                ViewBag.msgLogar = "Usuário e/ou senha incorreta(s)";
            }
            return View();
        }
        public ActionResult Erro()
        {
            return View();
        }
        public ActionResult About()
        {
            if (Session["usu"] != null && Session["tipo"].ToString() == "1")
            { 
            ViewBag.Message = "Your application description page.";
            return View();
            }
            else
            {
                
                return RedirectToAction("Erro", "Home");
            }
        }

        public ActionResult Contact()
        {
            if (Session["usu"] != null && Session["tipo"].ToString() == "1")
            {
                ViewBag.Message = "Your application description page.";
                return View();
            }
            else
            {

                return RedirectToAction("Erro", "Home");
            }
        }
    }
}