using CaseStudy.Core.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Entities.Concrete.DataModels
{
    public class AppLog:IEntity
    {
        public int Id { get; set; }
        public int LogInfo { get; set; }
        public string Data { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
