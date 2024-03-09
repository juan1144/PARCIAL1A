using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class librosController : ControllerBase
    {
        private readonly parcial1aContext _parcial1aContexto;
        public librosController(parcial1aContext parcial1AContexto)
        {
            _parcial1aContexto = parcial1AContexto;
        }

        [HttpGet]
        [Route("ObtenerTodo")]
        public ActionResult ObtenerTodo()
        {
            List<libros> listado_libros = (from e in _parcial1aContexto.libros select e).ToList();

            if (listado_libros.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listado_libros);
        }

        //AGREGAR UN NUEVO REGISTRO A UNA TABLA
        [HttpPost]
        [Route("Agregar")]
        public IActionResult guardarRegistro([FromBody] libros libros)
        {
            try
            {
                _parcial1aContexto.libros.Add(libros);
                _parcial1aContexto.SaveChanges();
                return Ok(libros);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //ACTUALIZAR UN REGISTRO DE UNA TABLA CON UN ID
        [HttpPut]
        [Route("Actualizar/{id}")]
        public IActionResult actualizarAutores(int id, [FromBody] libros modificarLibros)
        {
            libros? librosData = (from e in _parcial1aContexto.libros where e.Id == id select e).FirstOrDefault();

            if (librosData == null)
            {
                return NotFound();
            }
            librosData.Titulo = modificarLibros.Titulo;

            _parcial1aContexto.Entry(librosData).State = EntityState.Modified;
            _parcial1aContexto.SaveChanges();

            return Ok(modificarLibros);
        }

        //ELIMINAR UN REGISTRO DE UNA TABLA POR UN ID
        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult delete(int id)
        {
            libros? LibrosData = (from e in _parcial1aContexto.libros where e.Id == id select e).FirstOrDefault();
            if (LibrosData == null)
            {
                return NotFound();
            }
            _parcial1aContexto.libros.Attach(LibrosData);
            _parcial1aContexto.libros.Remove(LibrosData);
            _parcial1aContexto.SaveChanges();

            return Ok(LibrosData);
        }

        [HttpGet]
        [Route("FiltrarPorAutor/{filter}")]
        public IActionResult findByName(String filter)
        {
            var listadoEquipo = (from au in _parcial1aContexto.autores
                                 join auLi in _parcial1aContexto.autorlibro
                                    on au.Id equals auLi.AutorId
                                 join li in _parcial1aContexto.libros
                                    on auLi.LibroId equals li.Id
                                 where au.Nombre.Contains(filter)
                                 select new
                                 {
                                     idLibro = li.Id,
                                     tituloLibro = li.Titulo,
                                     autorLibro = au.Nombre

                                 }).ToList();
            if (listadoEquipo.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoEquipo);
        }
    }
}
