using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capadedatos.Models
{
    public class TipoServicioModel
    {
        public int id { get; set; }
        public string nombreTipoServicio { get; set; }
        public string descripcionTipoServicio { get; set; }

        // Constructor 
        public TipoServicioModel(int idParametro, string nombreTipoServicioParametro, string descripcionTipoServicioParametro )
        {

         this.id = idParametro; 
         this.nombreTipoServicio = nombreTipoServicioParametro;
         this.descripcionTipoServicio = descripcionTipoServicioParametro;



        }



    }
}
