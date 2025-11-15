using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capadedatos.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public int cedula { get; set; }

        public string Nombre { get; set; }

        public string? correo { get; set; }

        public string password { get; set; }
        public int telefono { get; set; }
        public int tipo_usuario_id { get; set; } 
        public UsuarioModel()
        {
            Nombre = string.Empty;


        }




    }
}
