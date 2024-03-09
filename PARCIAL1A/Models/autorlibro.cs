namespace PARCIAL1A.Models
{
    using System.ComponentModel.DataAnnotations;
    public class autorlibro
    {
        [Key]
        public int AutorId { get; set; }
        public int LibroId { get; set; }
        public int Orden { get; set; }
    }
}
