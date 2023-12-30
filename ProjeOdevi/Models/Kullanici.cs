namespace ProjeOdevi.Models
{
    public abstract class Kullanici
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string TCNo { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
    }
}
