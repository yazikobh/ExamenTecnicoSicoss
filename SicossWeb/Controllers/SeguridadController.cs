using RepuestaModelos;
using SicossLectorWebConfig;
using SicossModelo.Entidades;
using SicossWeb.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SicossWeb.Controllers
{
    public class SeguridadController : Controller
    {
        // GET: Seguridad
        public ActionResult IniciarSesion()
        {
            ModeloUsuario user = new ModeloUsuario();
            if (VariablesWebConfig.SessionAsCookie && Request.Cookies[AyudanteSitio.UserSession] != null)
            {
                return RedirectToAction("BuscarDocumento", "Archivo");
            }
            if (user.Id == 0)
                return View(user);
            else
            {
                AyudanteSitio.Instancia.EstablecerSesionUsuario(this.HttpContext, user, false);
                return RedirectToAction("BuscarDocumento", "Archivo");
            }
        }
        [HttpPost]
        public ActionResult IniciarSesion(string usuario, string pass, bool recordar = false)
        {
            ModeloRespuesta respuesta = SicossReglas.ReglasEntidades.ReglasUsuario.Instancia.ObtenerPorUsuarioContrasenia(pass, usuario);//ReglasUsuario.Instancia.ObtenerPorCorreoContrasenia(pass, usuario);
            if (respuesta.Exito)
            {
                ModeloUsuario foundUser = respuesta.ObtenerValorComo<ModeloUsuario>();
                AyudanteSitio.Instancia.EstablecerSesionUsuario(this.HttpContext, foundUser, false);
                if (recordar)
                {
                }
                else
                {
                    Response.Cookies[AyudanteSitio.UserCookie].Expires = DateTime.Now.AddMonths(-1);
                }
            }
            return AyudanteSitio.Instancia.EnviarRespuestaAjax(respuesta, Response);
        }
        public ActionResult Logout()
        {
            AyudanteSitio.Instancia.Logout(Session);
            Response.Cookies[AyudanteSitio.UserCookie].Expires = DateTime.Now.AddMonths(-3);
            Response.Cookies[AyudanteSitio.UserSession].Expires = DateTime.Now.AddMonths(-3);
            return RedirectToAction("IniciarSesion", "Seguridad");
        }
    }
}