using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PARCIAL1A.models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorLibroController : ControllerBase
    {
        private readonly Parcial1ADBContext _parcial1ADBContext;

        public AutorLibroController(Parcial1ADBContext Parcial1ADBContexto)
        {
            _parcial1ADBContext = Parcial1ADBContexto;

        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<AutorLibro> listadoAutorLibro = (from e in _parcial1ADBContext.AutorLibro select e).ToList();

            if (listadoAutorLibro.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoAutorLibro);
        }

        [HttpGet]
        [Route("GetById/{Id}")]

        public IActionResult Get(int Id)
        {
            AutorLibro? AutorLi = (from e in _parcial1ADBContext.AutorLibro where e.AutorId == Id select e).FirstOrDefault();

            if (AutorLi == null)
            {
                return NotFound();
            }

            return Ok(AutorLi);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarAutorLi([FromBody] AutorLibro AutorLi)
        {
            try
            {

                _parcial1ADBContext.AutorLibro.Add(AutorLi);
                _parcial1ADBContext.SaveChanges();
                return Ok(AutorLi);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarAutorLi(int id, [FromBody] AutorLibro AutorLiModificar)
        {
            AutorLibro? AutorLiActual = (from e in _parcial1ADBContext.AutorLibro where e.AutorId == id select e).FirstOrDefault();

            if (AutorLiActual == null)
            {
                return NotFound();
            }

            AutorLiActual.Orden = AutorLiModificar.Orden;

            return Ok(AutorLiModificar);
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]

        public IActionResult EliminarAutor(int id)
        {
            AutorLibro? AutorLi = (from e in _parcial1ADBContext.AutorLibro where e.AutorId == id select e).FirstOrDefault();

            if (AutorLi == null)
            {
                return NotFound();
            }

            _parcial1ADBContext.AutorLibro.Attach(AutorLi);
            _parcial1ADBContext.AutorLibro.Remove(AutorLi);
            _parcial1ADBContext.SaveChanges();

            return Ok(AutorLi);
        }

    }
}

