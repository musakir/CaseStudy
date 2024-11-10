using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Entities.Filter.EyaletTatil
{
    public class EyaletTatilFilter
    {
        public int Id { get; set; }
        public DateTime? Tarih { get; set; }
        public int EyaletId { get; set; }
        public int UlkeId { get; set; }
        public int Yil { get; set; }
        public int Ay { get; set; }
        public int TatilTurId { get; set; }
    }
}
