using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PARCIAL1A.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class autorLibroController : ControllerBase
    {
        private readonly parcial1aContext _parcial1aContexto;
        public autorLibroController(parcial1aContext parcial1AContexto)
        {
            _parcial1aContexto = parcial1AContexto;
        }

        [HttpGet]
        [Route("ObtenerTodo")]
        public ActionResult ObtenerTodo()
        {
            List<autorlibro> listado_autorLibro = (from e in _parcial1aContexto.autorlibro select e).ToList();

            if (listado_autorLibro.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listado_autorLibro);
        }

        //AGREGAR UN NUEVO REGISTRO A UNA TABLA
        [HttpPost]
        [Route("Agregar")]
        public IActionResult guardarRegistro([FromBody] autorlibro autorLibro)
        {
            try
            {
                _parcial1aContexto.autorlibro.Add(autorLibro);
                _parcial1aContexto.SaveChanges();
                return Ok(autorLibro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //ACTUALIZAR UN REGISTRO DE UNA TABLA CON UN ID
        [HttpPut]
        [Route("Actualizar/{id}")]
        public IActionResult actualizarAutores(int id, [FromBody] autorlibro modificarAutorLibro)
        {
            autorlibro? autorLibroData = (from e in _parcial1aContexto.autorlibro where e.AutorId == id select e).FirstOrDefault();

            if (autorLibroData == null)
            {
                return NotFound();
            }
            autorLibroData.Orden = modificarAutorLibro.Orden;

            _parcial1aContexto.Entry(autorLibroData).State = EntityState.Modified;
            _parcial1aContexto.SaveChanges();

            return Ok(modificarAutorLibro);
        }

        //ELIMINAR UN REGISTRO DE UNA TABLA POR UN ID
        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult delete(int id)
        {
            autorlibro? autorLibroData = (from e in _parcial1aContexto.autorlibro where e.AutorId == id select e).FirstOrDefault();
            if (autorLibroData == null)
            {
                return NotFound();
            }
            _parcial1aContexto.autorlibro.Attach(autorLibroData);
            _parcial1aContexto.autorlibro.Remove(autorLibroData);
            _parcial1aContexto.SaveChanges();

            return Ok(autorLibroData);
        }
    }
}
