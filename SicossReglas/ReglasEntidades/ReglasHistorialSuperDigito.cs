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
    public class ReglasHistorialSuperDigito : OperacionesBasicas<ModeloHistorialSuperDigito>
    {
        private static ReglasHistorialSuperDigito _instancia;
        public static ReglasHistorialSuperDigito Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new ReglasHistorialSuperDigito();
                return _instancia;
            }
        }
        private ReglasHistorialSuperDigito(){}

        public ModeloRespuesta Agregar(ModeloHistorialSuperDigito item)
        {
            ModeloRespuesta resultado = new ModeloRespuesta();
            try
            {
                using (SicossDAL.PruebaSicossEntities entidad = new SicossDAL.PruebaSicossEntities())
                {
                    SicossDAL.opeHistorialSuperDigito opeHistorialSuperDigito = new SicossDAL.opeHistorialSuperDigito();
                    opeHistorialSuperDigito.cDigito = item.Digito;
                    opeHistorialSuperDigito.cResultado = item.Resultado;
                    opeHistorialSuperDigito.fkIdUsuario = item.IdUsuario;
                    opeHistorialSuperDigito.bActivo = true;
                    opeHistorialSuperDigito.dtFechaRegistro = DateTime.Now;
                    entidad.opeHistorialSuperDigito.Add(opeHistorialSuperDigito);
                    entidad.SaveChanges();
                    resultado.ActualizarRespuesta(CodigosRespuesta.Exito, "Éxito");
                }
            }
            catch (Exception ex)
            {
                resultado.ActualizarRespuesta(CodigosRespuesta.Error, "Error - " + ex.Message);
            }
            return resultado;
        }

        public ModeloRespuesta Actualizar(ModeloHistorialSuperDigito item)
        {
            throw new NotImplementedException();
        }

        public ModeloRespuesta Eliminar(int Id)
        {
            ModeloRespuesta resultado = new ModeloRespuesta();
            try
            {
                using (SicossDAL.PruebaSicossEntities entidad = new SicossDAL.PruebaSicossEntities())
                {
                    SicossDAL.opeHistorialSuperDigito opeHistorialSuperDigito = entidad.opeHistorialSuperDigito.FirstOrDefault(h => h.Id == Id);
                    if (opeHistorialSuperDigito == null)
                    {
                        resultado.ActualizarRespuesta(CodigosRespuesta.Error, "El elemento que desea eliminar, no se encuentra");
                        return resultado;
                    }
                    opeHistorialSuperDigito.bActivo = false;
                    entidad.SaveChanges();
                    resultado.ActualizarRespuesta(CodigosRespuesta.Exito, "Éxito");
                }
            }
            catch (Exception ex)
            {
                resultado.ActualizarRespuesta(CodigosRespuesta.Error, "Error - " + ex.Message);
            }
            return resultado;
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
            ModeloRespuesta resultado = new ModeloRespuesta();
            List<ModeloHistorialSuperDigito> lstHistorial = new List<ModeloHistorialSuperDigito>();
            try
            {
                using (SicossDAL.PruebaSicossEntities entidad = new SicossDAL.PruebaSicossEntities())
                {
                    var historial = entidad.opeHistorialSuperDigito.Where(h => h.fkIdUsuario == Id && h.bActivo == true).ToList();

                    foreach (var item in historial)
                    {
                        lstHistorial.Add(new ModeloHistorialSuperDigito()
                        {
                            Id = item.Id,
                            Digito = item.cDigito,
                            Resultado = item.cResultado,
                            Activo = item.bActivo,
                            IdUsuario = item.fkIdUsuario,
                            FechaRegistro = item.dtFechaRegistro,
                            FechaConsulta = item.dtFechaConsulta
                        });
                    }
                    resultado.ActualizarRespuesta(CodigosRespuesta.Exito, "Éxito", lstHistorial);
                }
            }
            catch (Exception ex)
            {
                resultado.ActualizarRespuesta(CodigosRespuesta.Error, "Error - " + ex.Message);
            }
            return resultado;
        }
    }

}
