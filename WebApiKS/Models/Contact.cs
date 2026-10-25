using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiKS.Models
{
    public class Contact
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public long Telefon { get; set; }
        public string Iletisim { get; set; }
        public string Cinsiyet { get; set; }
        public string Adres { get; set; }
    }
}