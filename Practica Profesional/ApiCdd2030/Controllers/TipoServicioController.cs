using Capadedatos;
using Capadedatos.dsCentroDesarrolloTableAdapters;
using Capadedatos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCdd2030.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TipoServicioController : Controller
    {
        [HttpGet]
        public List<TipoServicioModel> Get()
        {
            List<TipoServicioModel> ListaTipoServicio = new List<TipoServicioModel>();
            tipo_servicioTableAdapter tableAdapterTipoServicio = new tipo_servicioTableAdapter();
            dsCentroDesarrollo.tipo_servicioDataTable listaDeTipos = tableAdapterTipoServicio.GetData();

            foreach (dsCentroDesarrollo.tipo_servicioRow tipo in listaDeTipos)
            {
                TipoServicioModel tipoServiciotemporal = new TipoServicioModel(
                    tipo.id,
                    tipo.nombre_tipo_servicio,
                    tipo.descripcion
                    );
                ListaTipoServicio.Add(tipoServiciotemporal);
            }
            return ListaTipoServicio;


        }








    }

}
