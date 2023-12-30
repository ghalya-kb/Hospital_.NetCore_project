using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ProjeOdevi.Models
{
    public class Randevu
    {
        [Key]
        public int Id { get; set; }
        [AllowNull]
        public int? HastaId { get; set; }
        [Required]
        public int DoktorId { get; set; }
        [Required]
        public DateTime Tarih { get; set; }
        [JsonIgnore]
        public Hasta Hasta { get; set; }
        [JsonIgnore]
        [Required]
        public Doktor Doktor { get; set; }
    }
}
