using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Entities.Model.IdentityToken
{
    public class IUserLocation
    {
        public int Id { get; set; }
        public long IUserId { get; set; }
        public string UserName { get; set; }
        public long ILocationId { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
    }
}
