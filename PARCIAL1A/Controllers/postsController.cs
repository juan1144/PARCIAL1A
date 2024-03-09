using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class postsController : ControllerBase
    {
        private readonly parcial1aContext _parcial1aContexto;
        public postsController(parcial1aContext parcial1AContexto)
        {
            _parcial1aContexto = parcial1AContexto;
        }

        [HttpGet]
        [Route("ObtenerTodo")]
        public ActionResult ObtenerTodo()
        {
            List<Posts> listado_posts = (from e in _parcial1aContexto.Posts select e).ToList();

            if (listado_posts.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listado_posts);
        }

        //AGREGAR UN NUEVO REGISTRO A UNA TABLA
        [HttpPost]
        [Route("Agregar")]
        public IActionResult guardarRegistro([FromBody] Posts posts)
        {
            try
            {
                _parcial1aContexto.Posts.Add(posts);
                _parcial1aContexto.SaveChanges();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //ACTUALIZAR UN REGISTRO DE UNA TABLA CON UN ID
        [HttpPut]
        [Route("Actualizar/{id}")]
        public IActionResult actualizarAutores(int id, [FromBody] Posts modificarPosts)
        {
            Posts? postsData = (from e in _parcial1aContexto.Posts where e.Id == id select e).FirstOrDefault();

            if (postsData == null)
            {
                return NotFound();
            }

            postsData.Titulo = modificarPosts.Titulo;
            postsData.Contenido = modificarPosts.Contenido;
            postsData.FechaPublicacion = modificarPosts.FechaPublicacion;
            postsData.AutorId = modificarPosts.AutorId;

            _parcial1aContexto.Entry(postsData).State = EntityState.Modified;
            _parcial1aContexto.SaveChanges();

            return Ok(modificarPosts);
        }

        //ELIMINAR UN REGISTRO DE UNA TABLA POR UN ID
        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult delete(int id)
        {
            Posts? postsData = (from e in _parcial1aContexto.Posts where e.Id == id select e).FirstOrDefault();
            if (postsData == null)
            {
                return NotFound();
            }
            _parcial1aContexto.Posts.Attach(postsData);
            _parcial1aContexto.Posts.Remove(postsData);
            _parcial1aContexto.SaveChanges();

            return Ok(postsData);
        }

        [HttpGet]
        [Route("FiltrarPorLibro")]
        public IActionResult FiltrarPorLibro()
        {
            var listadoPost = (from li in _parcial1aContexto.libros
                               join auli in _parcial1aContexto.autorlibro
                                  on li.Id equals auli.LibroId
                               join au in _parcial1aContexto.autores
                                  on auli.AutorId equals au.Id
                               join ps in _parcial1aContexto.Posts
                                  on au.Id equals ps.AutorId
                               select new
                               {
                                   idPosts = ps.Id,
                                   TituloPosts = ps.Titulo,
                                   contenidoPosts = ps.Contenido,
                                   fechaPublicacion = ps.FechaPublicacion,
                                   nombreAutor = au.Nombre,
                                   libroId = li.Id,
                                   libroTitulo = li.Titulo,
                               }).OrderBy(resultado => resultado.libroId).ToList();
            if (listadoPost.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoPost);
        }
    }
}
