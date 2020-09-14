using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPortalWebAPIHelper.Models
{
    public class EntityInfo
    {
        public EntityInfo()
        {

        }

        public string LogicalName { get; set; }
        public string DisplayName { get; set; }
        public Guid WebAPIEnabledSiteSettingId { get; set; }
        public Guid WebAPIFieldsSiteSettingId { get; set; }

        public List<AttributeItemModel> SelectedAttributesList { get; set; } = new List<AttributeItemModel>();
        public List<AttributeItemModel> AllAttributesList { get; set; } = new List<AttributeItemModel>();


    }
}
