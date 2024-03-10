using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PARCIAL1A.models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {

        private readonly Parcial1ADBContext _Parcial1ADBContext;

        public LibrosController(Parcial1ADBContext parcialDBContexto)
        {
            _Parcial1ADBContext = parcialDBContexto;


        }


        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Libros> listadoLibros = (from e in _Parcial1ADBContext.Libros select e).ToList();

            if (listadoLibros.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoLibros);
        }

        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            Libros? Libro = (from e in _Parcial1ADBContext.Libros where e.Id == id select e).FirstOrDefault();

            if (Libro == null)
            {
                return NotFound();
            }

            return Ok(Libro);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarLibro([FromBody] Libros libro)
        {
            try
            {

                _Parcial1ADBContext.Libros.Add(libro);
                _Parcial1ADBContext.SaveChanges();
                return Ok(libro);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarLibros(int id, [FromBody] Posts postsModificar)
        {
            Posts? postActual = (from e in _Parcial1ADBContext.Posts where e.Id == id select e).FirstOrDefault();

            if (postActual == null)
            {
                return NotFound();
            }

            postActual.Titulo = postsModificar.Titulo;
            postActual.Contenido = postsModificar.Contenido;
            postActual.FechaPublicacion = postsModificar.FechaPublicacion;
            postActual.AutorId = postsModificar.AutorId;

            return Ok(postsModificar);
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]

        public IActionResult EliminarLibro(int id)
        {
            Libros? libro = (from e in _Parcial1ADBContext.Libros where e.Id == id select e).FirstOrDefault();

            if (libro == null)
            {
                return NotFound();
            }

            _Parcial1ADBContext.Libros.Attach(libro);
            _Parcial1ADBContext.Libros.Remove(libro);
            _Parcial1ADBContext.SaveChanges();

            return Ok(libro);
        }




    }

}
