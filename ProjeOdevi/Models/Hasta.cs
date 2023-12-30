namespace ProjeOdevi.Models
{
    public class Hasta : Kullanici
    {
        public IList<Randevu> AlinanRandevular { get; set; }
    }
}
