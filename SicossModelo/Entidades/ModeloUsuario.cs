using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SicossModelo.Entidades
{
    public class ModeloUsuario
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string IP { get; set; }

        public ModeloUsuario()
        {

        }

        public ModeloUsuario(int id,string usuario, string password)
        {
            this.Id = id;
            this.Usuario = usuario;
            this.Password = password;
        }
    }
}
