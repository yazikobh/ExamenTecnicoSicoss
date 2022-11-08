using RepuestaModelos;
using RepuestaModelos.Ayudantes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SicossReglas.ReglasEntidades
{
    public class ReglasSuperDigito
    {
        private static ReglasSuperDigito _instancia;
        public static ReglasSuperDigito Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new ReglasSuperDigito();
                return _instancia;
            }
        }
        private ReglasSuperDigito(){}

        public ModeloRespuesta ObtenerSuperDigito(int numero,bool detalle = false)
        {
            ModeloRespuesta resultado = new ModeloRespuesta();
            try
            {
                int digitos = (int)Math.Floor(Math.Log10(numero) + 1);

                if (digitos == 1)
                {
                    resultado.ActualizarRespuesta(CodigosRespuesta.Exito, "Éxito", numero);
                    return resultado;
                }

                if (digitos > 1)
                {
                    string numeroCadena = numero.ToString();
                    string respuesta = (detalle) ? SumarDigitosDetalle(numeroCadena) : SumarDigitos(numeroCadena).ToString(); ;
                    resultado.ActualizarRespuesta(CodigosRespuesta.Exito, "Éxito", respuesta);
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                resultado.ActualizarRespuesta(CodigosRespuesta.Error, "Error - " + ex.Message);
            }
            return resultado;
        }

        public int SumarDigitos(string digito)
        {
            int resultado = 0;
            for (int i = 0; i < digito.Length; i++)
            {
                resultado += int.Parse(digito.Substring(i, 1));
            }
            int digitos = (int)Math.Floor(Math.Log10(resultado) + 1);
            if (digitos == 1) return resultado;
            resultado = SumarDigitos(resultado.ToString());
            return resultado;
        }

        public string SumarDigitosDetalle(string digito)
        {
            string resultado = string.Empty;
            string detalle = string.Empty;
            int valor = 0;
            for (int i = 0; i < digito.Length; i++)
            {
                resultado += digito.Substring(i, 1) + "+";
                valor += int.Parse(digito.Substring(i, 1));
            }
            detalle += $"SumarDigitosDetalle({resultado.Substring(0, resultado.Length - 1)}) = {valor} \n";
            int digitos = (int)Math.Floor(Math.Log10(valor) + 1);
            if (digitos == 1) return detalle;
            detalle += SumarDigitosDetalle(valor.ToString());
            return detalle;
        }
    }
}
