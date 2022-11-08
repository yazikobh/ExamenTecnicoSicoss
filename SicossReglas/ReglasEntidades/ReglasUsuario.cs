using JDFramework.WebHelpers;
using RepuestaModelos;
using RepuestaModelos.Ayudantes;
using SicossModelo.Entidades;
using SicossModelo.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SicossReglas.ReglasEntidades
{
    public class ReglasUsuario : OperacionesBasicas<ModeloUsuario>
    {
        private static ReglasUsuario _instancia;
        public static ReglasUsuario Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new ReglasUsuario();
                return _instancia;
            }
        }
        private  ReglasUsuario(){}

        public ModeloRespuesta Agregar(ModeloUsuario item)
        {
            ModeloRespuesta resultado = new ModeloRespuesta();
            try
            {
                using (SicossDAL.PruebaSicossEntities entidad = new SicossDAL.PruebaSicossEntities())
                {
                    SicossDAL.opeUsuario opeUsuario = new SicossDAL.opeUsuario();
                    opeUsuario.cUsuario = item.Usuario;
                    opeUsuario.cPassword = Encrypter.EncryptParam(item.Password);
                    opeUsuario.dtFechaRegistro = DateTime.Now;
                    entidad.opeUsuario.Add(opeUsuario);
                    entidad.SaveChanges();
                    resultado.ActualizarRespuesta(CodigosRespuesta.Exito,"Éxito");
                }
            }
            catch (Exception ex)
            {
                resultado.ActualizarRespuesta(CodigosRespuesta.Error, "Error - " + ex.Message);
            }
            return resultado;
        }

        public ModeloRespuesta Actualizar(ModeloUsuario item)
        {
            throw new NotImplementedException();
        }

        public ModeloRespuesta Eliminar(int Id)
        {
            throw new NotImplementedException();
        }

        public ModeloRespuesta ConsultaCompleta()
        {
            throw new NotImplementedException();
        }

        public ModeloRespuesta ConsultaPorId(int Id)
        {
            throw new NotImplementedException();
        }

        public ModeloRespuesta ConsultaPorIdUsuario(int Id)
        {
            throw new NotImplementedException();
        }

        public ModeloRespuesta ObtenerPorCorreoContrasenia(string passw, string usuario)
        {
            ModeloRespuesta respuesta = new ModeloRespuesta();
            respuesta.ActualizarRespuesta(CodigosRespuesta.Exito, "El usuario ha sido autentificado con éxito.");
            try
            {
                using (SicossDAL.PruebaSicossEntities entidad = new SicossDAL.PruebaSicossEntities())
                {
                    SicossDAL.opeUsuario opeUsuario = null;
                    ModeloUsuario usr = null;

                    opeUsuario = entidad.opeUsuario.SingleOrDefault(u => u.cUsuario == usuario);
                    if (opeUsuario == null)
                    {
                        respuesta.ActualizarRespuesta(CodigosRespuesta.Error, "Hubo un problema al firmar el usuario solicitado.");
                    }
                    if (respuesta.Exito)
                    {
                        String encrypt = Encrypter.EncryptParam(passw);
                        if (opeUsuario.cPassword != encrypt)
                        {
                            respuesta.ActualizarRespuesta(CodigosRespuesta.Error, "Hubo un problema al firmar el usuario solicitado.");
                        }
                    }
                    if (respuesta.Exito)
                    {
                        usr = new ModeloUsuario(opeUsuario.Id, opeUsuario.cUsuario, "");
                    }
                    if (respuesta.Exito)
                    {
                        respuesta.ActualizarRespuesta(CodigosRespuesta.Exito, "Éxito", usr);
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.ActualizarRespuesta(CodigosRespuesta.Error, "Error - "+ex.Message);
            }
            return respuesta;
        }
    }
}
