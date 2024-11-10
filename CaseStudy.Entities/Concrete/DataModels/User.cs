using System.Collections.Generic;

namespace CaseStudy.Entities.Concrete.DataModels
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string CompanyId { get; set; }

        public IEnumerable<string> role { get; set; }
    }
}
