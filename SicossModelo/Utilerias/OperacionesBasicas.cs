using RepuestaModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SicossModelo.Utilerias
{
    public interface OperacionesBasicas<T>
    {
        ModeloRespuesta Agregar(T item);
        ModeloRespuesta Actualizar(T item);
        ModeloRespuesta Eliminar(int Id);
        ModeloRespuesta ConsultaCompleta();
        ModeloRespuesta ConsultaPorId(int Id);
        ModeloRespuesta ConsultaPorIdUsuario(int Id);
    }
}
