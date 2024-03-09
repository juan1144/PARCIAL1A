namespace PARCIAL1A.Models
{
    using System.ComponentModel.DataAnnotations;
    public class posts
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public int AutorId { get; set; }
    }
}
