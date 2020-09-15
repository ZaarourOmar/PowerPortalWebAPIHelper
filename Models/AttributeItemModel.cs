using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPortalWebAPIHelper.Models
{
    public class AttributeItemModel
    {
        public AttributeItemModel(AttributeMetadata metadata)
        {
            if (metadata.DisplayName.UserLocalizedLabel != null && !string.IsNullOrEmpty(metadata.DisplayName.UserLocalizedLabel.Label))
            {
                DisplayName = metadata.DisplayName.UserLocalizedLabel.Label;
            }
            else
            {
                DisplayName = metadata.DisplayName.ToString();
            }

            LogicalName = metadata.LogicalName;

            DataType = metadata.AttributeType.Value;
        }
        public string DisplayName { get; set; }
        public string LogicalName { get; set; }


        public AttributeTypeCode DataType { get; set; }
        public override string ToString()
        {
            return $"{DisplayName} ({LogicalName})";
        }
    }
}
