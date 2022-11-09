using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SicossModelo.Entidades
{
    public class ModeloHistorialSuperDigito
    {
        public int Id { get; set; }
        public string Digito { get; set; }
        public string Resultado { get; set; }
        public int IdUsuario { get; set; }
        public bool Activo { get; set; }
        public string FechaRegistro { get; set; }
        public string FechaConsulta { get; set; }
        public ModeloHistorialSuperDigito()
        {

        }
        public ModeloHistorialSuperDigito(int id,string digito,string resultado,int idusuario)
        {
            this.Id = id;
            this.Digito = digito;
            this.Resultado = resultado;
            this.IdUsuario = idusuario;
        }
    }
}
