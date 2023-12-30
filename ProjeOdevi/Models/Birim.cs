namespace ProjeOdevi.Models
{
    public class Birim
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public IList<Doktor> doktorlar { get; set; }


    }
}
