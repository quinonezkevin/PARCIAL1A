using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PARCIAL1A.models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly Parcial1ADBContext _parcial1ADBContext;

        public AutoresController(Parcial1ADBContext Parcial1ADBContexto)
        {
            _parcial1ADBContext = Parcial1ADBContexto;

        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Autores> listadoAutores = (from e in _parcial1ADBContext.Autores select e).ToList();

            if (listadoAutores.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoAutores);
        }

        [HttpGet]
        [Route("GetById/{Id}")]
          
        public IActionResult Get(int Id)
        {
            Autores? Autor = (from e in _parcial1ADBContext.Autores where e.Id == Id select e).FirstOrDefault();

            if (Autor == null)
            {
                return NotFound();
            }

            return Ok(Autor);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarAutor([FromBody] Autores Autor)
        {
            try
            {

                _parcial1ADBContext.Autores.Add(Autor);
                _parcial1ADBContext.SaveChanges();
                return Ok(Autor);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarAutor(int id, [FromBody] Autores AutorModificar)
        {
            Autores? AutorActual = (from e in _parcial1ADBContext.Autores where e.Id == id select e).FirstOrDefault();

            if (AutorActual == null)
            {
                return NotFound();
            }

            AutorActual.Nombre = AutorModificar.Nombre;

            return Ok(AutorModificar);
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]

        public IActionResult EliminarAutor(int id)
        {
            Autores? Autor = (from e in _parcial1ADBContext.Autores where e.Id == id select e).FirstOrDefault();

            if (Autor == null)
            {
                return NotFound();
            }

            _parcial1ADBContext.Autores.Attach(Autor);
            _parcial1ADBContext.Autores.Remove(Autor);
            _parcial1ADBContext.SaveChanges();

            return Ok(Autor);
        }

    }
}
