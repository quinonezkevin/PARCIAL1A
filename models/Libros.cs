using System.ComponentModel.DataAnnotations;

namespace PARCIAL1A.models
{
    public class Libros
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }

    }
}
