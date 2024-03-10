using System.ComponentModel.DataAnnotations;

namespace PARCIAL1A.models
{
    public class Autores
    {
        [Key]

        public int Id { get; set; }
        public string Nombre { get; set; }

    }
}
