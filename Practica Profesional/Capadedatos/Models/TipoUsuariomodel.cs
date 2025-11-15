using Capadedatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Capadedatos.Models
{
    public class TipoUsuariomodel
    {
        public int id { get; set; }
        public string? nombreTipoUsuario{ get; set; }
        public string? descripcionTipoUsuario { get; set; }
    }
}
       