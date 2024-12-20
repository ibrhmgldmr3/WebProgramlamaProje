using System.ComponentModel.DataAnnotations.Schema;
using WebProgramlamaProje.Models;

namespace  WebProgramlamaProje.Models{
public class Uzmanlik
{
    public int UzmanlikId { get; set; }
    public string Ad { get; set; }
    public List<Calisan> Calisanlar { get; set; } = new List<Calisan>();
    public List<IslemUzmanlik> IslemUzmanliklar { get; set; } = new List<IslemUzmanlik>();

    // Formdan gelen seçili işlemleri tutmak için
    [NotMapped]
    public int[] SelectedIslemler { get; set; }
}
}