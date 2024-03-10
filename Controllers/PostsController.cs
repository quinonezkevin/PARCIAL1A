using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PARCIAL1A.models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {


        private readonly Parcial1ADBContext _Parcial1ADBContext;

        public PostsController(Parcial1ADBContext parcialDBContexto)
        {
            _Parcial1ADBContext = parcialDBContexto;


        }


        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Posts> listadoPosts = (from e in _Parcial1ADBContext.Posts select e).ToList();

            if (listadoPosts.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoPosts);
        }

        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            Posts? post = (from e in _Parcial1ADBContext.Posts where e.Id == id select e).FirstOrDefault();

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarPost([FromBody] Posts post)
        {
            try
            {

                _Parcial1ADBContext.Posts.Add(post);
                _Parcial1ADBContext.SaveChanges();
                return Ok(post);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarPost(int id, [FromBody] Posts postsModificar)
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

        public IActionResult EliminarPost(int id)
        {
            Posts? post = (from e in _Parcial1ADBContext.Posts where e.Id == id select e).FirstOrDefault();

            if (post == null)
            {
                return NotFound();
            }

            _Parcial1ADBContext.Posts.Attach(post);
            _Parcial1ADBContext.Posts.Remove(post);
            _Parcial1ADBContext.SaveChanges();

            return Ok(post);
        }




    }


}
