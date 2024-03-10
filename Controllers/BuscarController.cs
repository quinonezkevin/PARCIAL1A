using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PARCIAL1A.models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuscarController : ControllerBase
    {
        private readonly Parcial1ADBContext _parcial1ADBContext;

        public BuscarController(Parcial1ADBContext Parcial1ADBContexto)
        {
            _parcial1ADBContext = Parcial1ADBContexto;

        }


        [HttpGet]
        [Route("GetAll/{LibroAutorName}")]
        public IActionResult GetLibroByAutor(string nombre)
        {
            List<Libros> listadoLibros = (from e in _parcial1ADBContext.Libros 
                                          join au in _parcial1ADBContext.AutorLibro
                                          on e.Id equals au.LibroId
                                          join auto in _parcial1ADBContext.Autores
                                          on au.AutorId equals auto.Id
                                          where auto.Nombre == nombre
                                          select e).ToList();



            if (listadoLibros.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoLibros);
        }

        [HttpGet]
        [Route("GetAll/{libro}")]
        public IActionResult GetPostByLibros(string libro)
        {
            var listadoPosts = (from e in _parcial1ADBContext.Libros
                                          join au in _parcial1ADBContext.AutorLibro
                                          on e.Id equals au.LibroId
                                          join auto in _parcial1ADBContext.Autores
                                          on au.AutorId equals auto.Id
                                          join post in _parcial1ADBContext.Posts
                                          on au.AutorId equals post.AutorId
                                          where e.Titulo == libro
                                          select post).ToList();



            if (listadoPosts.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoPosts);
        }

        [HttpGet]

        [Route("GetAll/{Last20Posts}")]
        public IActionResult GetLast20Posts(string Nombre)
        {
            List<Posts> last20Posts = (from p in _parcial1ADBContext.Posts
                                       join a in _parcial1ADBContext.Autores on p.AutorId equals a.Id
                                       where a.Nombre == Nombre
                                       orderby p.FechaPublicacion descending
                                       select p)
                                       .Take(20)
                                       .ToList();

            if (last20Posts.Count() == 0)
            {
                return NotFound();
            }
            return Ok(last20Posts);
        }


    }

}
