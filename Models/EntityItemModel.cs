using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPortalWebAPIHelper.Models
{
    public class EntityItemModel
    {
        public EntityItemModel()
        {

        }
        public EntityItemModel(EntityMetadata metadata)
        {
            if(metadata.DisplayName.UserLocalizedLabel!=null && !string.IsNullOrEmpty(metadata.DisplayName.UserLocalizedLabel.Label))
            {
                DisplayName = metadata.DisplayName.UserLocalizedLabel.Label;
            }
            else
            {
                DisplayName = metadata.DisplayName.ToString();
            }

            LogicalName = metadata.LogicalName;

            CollectionName = metadata.CollectionSchemaName;
        }

        public string CollectionName { get; set; }
        public string DisplayName { get; set; }
        public string LogicalName { get; set; }

        public Guid WebAPIEnabledSiteSettingId { get; set; }
        public Guid WebAPIFieldsSiteSettingId { get; set; }

        public List<AttributeItemModel> SelectedAttributesList { get; set; } = new List<AttributeItemModel>();
        public List<AttributeItemModel> AllAttributesList { get; set; } = new List<AttributeItemModel>();

        public override string ToString()
        {
            return $"{DisplayName} ({LogicalName})";
        }

    }
}
