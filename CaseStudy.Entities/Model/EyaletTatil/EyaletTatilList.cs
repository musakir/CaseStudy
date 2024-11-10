using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Entities.Model.EyaletTatil
{
    public class EyaletTatilList
    {
        public int TatilTurId { get; set; }
        public string TatilTur { get; set; }
        public DateTime? TatilTarih { get; set; }
        public string TatilAciklama { get; set; }
        public string EyaletAdi { get; set; }
    }
}
