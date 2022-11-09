using RepuestaModelos;
using SicossModelo.Entidades;
using SicossWeb.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SicossWeb.Controllers
{
    public class SuperDigitoController : Controller
    {
        // GET: SuperDigito
        public ActionResult Index()
        {
            ModeloUsuario usuarioFirmado = AyudanteSitio.Instancia.UsuarioFirmado(HttpContext);
            if (usuarioFirmado != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Seguridad");
            }
        }

        [HttpPost]
        public ActionResult ObtenerHistorialUsuario(int idUsuario)
        {
            ModeloRespuesta respuesta = SicossReglas.ReglasEntidades.ReglasHistorialSuperDigito.Instancia.ConsultaPorIdUsuario(idUsuario);
            return AyudanteSitio.Instancia.EnviarRespuestaAjax(respuesta, Response);
        }

        [HttpPost]
        public ActionResult CalcularSuperDigito(int digito, int idUsuario, bool detalle = false)
        {
            ModeloRespuesta respuesta = SicossReglas.ReglasEntidades.ReglasSuperDigito.Instancia.ObtenerSuperDigito(digito, detalle);
            if (respuesta.Exito)
            {
                string resultado = respuesta.ObtenerValorComo<string>();
                ModeloHistorialSuperDigito modeloHistorial = new ModeloHistorialSuperDigito(0, digito.ToString(), resultado, idUsuario);
                SicossReglas.ReglasEntidades.ReglasHistorialSuperDigito.Instancia.Agregar(modeloHistorial);
            }
            return AyudanteSitio.Instancia.EnviarRespuestaAjax(respuesta, Response);
        }
        
        [HttpPost]
        public ActionResult EliminarHistorialPorId(int idRegistro)
        {
            ModeloRespuesta respuesta = SicossReglas.ReglasEntidades.ReglasHistorialSuperDigito.Instancia.Eliminar(idRegistro);
            return AyudanteSitio.Instancia.EnviarRespuestaAjax(respuesta, Response);
        }

        [HttpPost]
        public ActionResult ConsultaPorDigito(int digito)
        {
            ModeloRespuesta respuesta = SicossReglas.ReglasEntidades.ReglasHistorialSuperDigito.Instancia.ConsultaPorDigito(digito);
            return AyudanteSitio.Instancia.EnviarRespuestaAjax(respuesta, Response);
        }

        [HttpPost]
        public ActionResult LimpiparHistorial(int idUsuario)
        {
            ModeloRespuesta respuesta = SicossReglas.ReglasEntidades.ReglasHistorialSuperDigito.Instancia.LimpiarHistorialPorUsuario(idUsuario);
            return AyudanteSitio.Instancia.EnviarRespuestaAjax(respuesta, Response);
        }
    }
}