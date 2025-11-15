using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Capadedatos;
using Capadedatos.Models;
using Capadedatos.dsCentroDesarrolloTableAdapters;

namespace ApiCdd2030.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TipoUsuarioController : Controller
    {

       [HttpGet] 
        public List<TipoUsuariomodel> Get()
        {
            List<TipoUsuariomodel> ListaTipoUsuario = new List<TipoUsuariomodel>();
            tipo_usuarioTableAdapter tableAdapterTipoUsuario = new tipo_usuarioTableAdapter();
            dsCentroDesarrollo.tipo_usuarioDataTable listaDeTipos = tableAdapterTipoUsuario.GetData();

            foreach (dsCentroDesarrollo.tipo_usuarioRow tipo in listaDeTipos)
            {
                TipoUsuariomodel tipoUsuariotemporal = new TipoUsuariomodel();

                tipoUsuariotemporal.id = tipo.id;
                tipoUsuariotemporal.nombreTipoUsuario = tipo.nombre_tipo_usuario;   
                tipoUsuariotemporal.descripcionTipoUsuario = tipo.descripcion;
                ListaTipoUsuario.Add(tipoUsuariotemporal);
            }
        
            return ListaTipoUsuario;
            
        }


    }

}
