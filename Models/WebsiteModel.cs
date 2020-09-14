using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPortalWebAPIHelper.Models
{
    public class WebsiteModel
    {
     
        public WebsiteModel(string websiteName, Guid websiteId)
        {
            this.Name = websiteName;
            this.Id = websiteId;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;     
        }
    }
}
