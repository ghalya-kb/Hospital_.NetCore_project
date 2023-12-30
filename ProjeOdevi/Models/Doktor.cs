using System.Text.Json.Serialization;

namespace ProjeOdevi.Models
{
    public class Doktor : Kullanici
    {
        public int BirimId { get; set; }
        [JsonIgnore]
        public Birim Birim { get; set; }
        public IList<Randevu> RandevuListesi { get; set; }
    }
}
