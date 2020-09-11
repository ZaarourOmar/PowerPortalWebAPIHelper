using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPortalWebAPIHelper.Models
{
    public class EntityListItem
    {
        public EntityListItem(EntityMetadata metadata)
        {
            if(metadata.DisplayName.UserLocalizedLabel!=null && !string.IsNullOrEmpty(metadata.DisplayName.UserLocalizedLabel.Label))
            {
                Label = metadata.DisplayName.UserLocalizedLabel.Label;
            }
            else
            {
                Label = metadata.DisplayName.ToString();
            }

            LogicalName = metadata.LogicalName;
        }
        public string Label { get; set; }
        public string LogicalName { get; set; }

        public override string ToString()
        {
            return $"{Label} ({LogicalName})";
        }
    }
}
