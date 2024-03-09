namespace PARCIAL1A.Models
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.SqlServer;
    using System.ComponentModel.DataAnnotations;
    public class autores
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
