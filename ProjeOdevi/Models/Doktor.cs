namespace ProjeOdevi.Models
{
    public class Doktor : Kullanici
    {
        public Birim Birim { get; set; }
        public IList<Randevu> RandevuListesi { get; set; }
    }
}
