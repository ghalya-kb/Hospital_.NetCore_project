using System.ComponentModel.DataAnnotations;

namespace ProjeOdevi.Models
{
    public class Randevu
    {
        [Key]
        public int Id { get; set; }
        public int HastaId { get; set; }
        [Required]
        public int DoktorId { get; set; }
        [Required]
        public DateTime Tarih { get; set; }
        public Hasta Hasta { get; set; }
        [Required]
        public Doktor Doktor { get; set; }
    }
}
