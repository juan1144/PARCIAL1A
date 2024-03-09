namespace PARCIAL1A.Models
{
    using System.ComponentModel.DataAnnotations;
    public class libros
    {
        [Key] 
        public int Id { get; set; }
        public string Titulo { get; set; }
    }
}
