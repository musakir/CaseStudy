using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Entities.Model.Tatil
{
    public class TatilList
    {
        public int Id { get; set; }
        public int TatilTurId { get; set; }
        public string TatilTur { get; set; }
        public DateTime? Tarih { get; set; }
        public string Aciklama { get; set; }
        public int Aktif { get; set; }
        public DateTime? CreateDate { get; set; }
        public int CreateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int UpdateUser { get; set; }
    }
}
