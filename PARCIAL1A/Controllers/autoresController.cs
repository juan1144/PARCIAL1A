using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class autoresController : ControllerBase
    {
        private readonly parcial1aContext _parcial1aContexto;
        public autoresController(parcial1aContext parcial1AContexto)
        {
            _parcial1aContexto = parcial1AContexto;
        }

        [HttpGet]
        [Route("ObtenerTodo")]
        public ActionResult ObtenerTodo()
        {
            List<autores> listado_autores = (from e in _parcial1aContexto.autores select e).ToList();

            if (listado_autores.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listado_autores);
        }

        //AGREGAR UN NUEVO REGISTRO A UNA TABLA
        [HttpPost]
        [Route("Agregar")]
        public IActionResult guardarRegistro([FromBody] autores autores)
        {
            try
            {
                _parcial1aContexto.autores.Add(autores);
                _parcial1aContexto.SaveChanges();
                return Ok(autores);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //ACTUALIZAR UN REGISTRO DE UNA TABLA CON UN ID
        [HttpPut]
        [Route("Actualizar/{id}")]
        public IActionResult actualizarAutores(int id, [FromBody] autores modificarAutores)
        {
            autores? autoresData = (from e in _parcial1aContexto.autores where e.Id == id select e).FirstOrDefault();

            if (autoresData == null)
            {
                return NotFound();
            }

            autoresData.Nombre = modificarAutores.Nombre;

            _parcial1aContexto.Entry(autoresData).State = EntityState.Modified;
            _parcial1aContexto.SaveChanges();

            return Ok(modificarAutores);
        }

        //ELIMINAR UN REGISTRO DE UNA TABLA POR UN ID
        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult delete(int id)
        {
            autores? autoresData = (from e in _parcial1aContexto.autores where e.Id == id select e).FirstOrDefault();
            if (autoresData == null)
            {
                return NotFound();
            }
            _parcial1aContexto.autores.Attach(autoresData);
            _parcial1aContexto.autores.Remove(autoresData);
            _parcial1aContexto.SaveChanges();

            return Ok(autoresData);
        }


    }
}
