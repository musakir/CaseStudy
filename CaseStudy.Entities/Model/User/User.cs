using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Entities.Model.User
{
    public class User
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }

        public IEnumerable<string> UserRole { get; set; }
    }
}
