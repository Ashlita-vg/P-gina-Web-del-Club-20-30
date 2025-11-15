using Capadedatos;
using Capadedatos.dsCentroDesarrolloTableAdapters;
using Capadedatos.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiCdd2030.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : Controller
    {

        [HttpGet]
        public List<TipoUsuariomodel> Get()
        {
            List<TipoUsuariomodel> ListaUsuario = new List<TipoUsuariomodel>();
            usuarioTableAdapter tableAdapterUsuario = new usuarioTableAdapter();
            dsCentroDesarrollo.usuarioDataTable listaDeTipos = tableAdapterUsuario.GetData();  

            foreach (dsCentroDesarrollo.usuarioRow tipo in listaDeTipos)
            {
                TipoUsuariomodel TipoUsuariotemporal = new TipoUsuariomodel();

               
                ListaUsuario.Add(TipoUsuariotemporal);
            }

            return ListaUsuario;

        }

        [HttpPost]
        public IActionResult InsertarUsuario([FromBody] UsuarioModel usuario)
        {
            if (usuario == null)
                return BadRequest("Datos del usuario no proporcionados.");

            if (usuario.cedula == 0)
                return BadRequest("El número de cédula es inválido.");

            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                return BadRequest("El nombre no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(usuario.correo))
                return BadRequest("El correo no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(usuario.password))
                return BadRequest("La contraseña no puede estar vacía.");

            if (usuario.telefono == 0)
                return BadRequest("El número de teléfono es inválido.");

            try
            {
                usuarioTableAdapter tableAdapterUsuario = new usuarioTableAdapter();
                tableAdapterUsuario.InsertUsuario(
                    usuario.cedula,
                    usuario.Nombre,
                    usuario.correo,
                    usuario.password,
                    usuario.telefono,
                    2
                );

                return Ok("Usuario insertado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al insertar el usuario: {ex.Message}");
            }
        }
    }

        }


     