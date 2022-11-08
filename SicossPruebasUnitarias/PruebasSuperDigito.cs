using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepuestaModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SicossPruebasUnitarias
{
    [TestClass]
    public class PruebasSuperDigito
    {
        [TestMethod]
        public void ObtenerSuperDigito()
        {
            ModeloRespuesta resultado = SicossReglas.ReglasEntidades.ReglasSuperDigito.Instancia.ObtenerSuperDigito(187,true);
            Console.WriteLine(resultado.ObtenerValorComo<string>());
        }
    }
}
