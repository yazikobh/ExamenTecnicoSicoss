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
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaConsulta { get; set; }
    }
}
