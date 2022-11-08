using JDFramework.WebHelpers;
using Newtonsoft.Json;
using SicossLectorWebConfig;
using SicossModelo.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SicossWeb.App_Start
{
    public class AyudanteSitio
    {
        public static Random random = new Random();
        public const string UserCookie = "uihduie3wrniOwO";
        public const string UserSession = "lkcmhuernyufufr";
        public const string JsonContentType = "application/json; charset=utf-8";
        private static AyudanteSitio _instancia;
        public static AyudanteSitio Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new AyudanteSitio();
                return _instancia;
            }
        }

        public ActionResult EnviarRespuestaAjax(object obj, HttpResponseBase Response)
        {
            string json = JsonConvert.SerializeObject(obj);
            Response.ContentType = JsonContentType;
            Response.Write(json);
            return null;
        }
        internal void EstablecerSesionUsuario(HttpContextBase context, ModeloUsuario user, bool preservarPrevio = true)
        {
            ModeloUsuario previo = null;
            if (preservarPrevio)
            {
                previo = UsuarioFirmado(context);
            }


            if (VariablesWebConfig.SessionAsCookie)
            {
                user.IP = context.Request.UserHostAddress;
                string json = JsonConvert.SerializeObject(user);
                context.Response.Cookies[UserSession].Value = CrearValorCookie(json, context.Server);
                context.Response.Cookies[UserSession].Expires = DateTime.Now.AddHours(VariablesWebConfig.SessionCookieTime);
            }
            else
                context.Session[ConstantesSitio.IdentificadorSesion] = user;
        }
        public ModeloUsuario UsuarioFirmado(HttpContextBase context)
        {
            ModeloUsuario usuario = null;
            if (VariablesWebConfig.SessionAsCookie)
            {
                if (context.Request.Cookies[AyudanteSitio.UserSession] != null)
                {
                    String val = LeerValorCookie(context.Request.Cookies[AyudanteSitio.UserSession].Value.ToString(), context.Server);
                    ModeloUsuario temp = JsonConvert.DeserializeObject<ModeloUsuario>(val);
                    usuario = temp;
                }
            }
            else
                usuario = UsuarioFirmado(context.Session);
            return usuario;
        }
        public string LeerValorCookie(string valor, HttpServerUtilityBase server)
        {
            string result = server.UrlDecode(valor);
            result = Encrypter.DecryptParam(result);
            return result;
        }
        private ModeloUsuario UsuarioFirmado(HttpSessionStateBase Sesion)
        {
            ModeloUsuario usuario = null;
            if (Sesion[ConstantesSitio.IdentificadorSesion] != null)
                usuario = Sesion[ConstantesSitio.IdentificadorSesion] as ModeloUsuario;
            return usuario;
        }
        public string CrearValorCookie(string valor, HttpServerUtilityBase server)
        {
            string result = Encrypter.EncryptParam(valor);
            result = server.UrlEncode(result);
            return result;
        }
        internal void Logout(HttpSessionStateBase Session)
        {
            Session[ConstantesSitio.IdentificadorSesion] = null;
            Session.Abandon();
        }
    }
    public class ConstantesSitio
    {
        public const string IdentificadorSesion = "SiteUser";
        public const string CaptchaNumber = "CaptchaNumber";
    }
}