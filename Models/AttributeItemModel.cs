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
        public AttributeItemModel(AttributeMetadata metadata, EntityItemModel selectedEntityInfo)
        {
            Metadata = metadata;
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
            IsValidForCreate = metadata.IsValidForCreate.Value;
            IsValidForUpdate = metadata.IsValidForUpdate.Value;


            if (metadata.LogicalName == "accountid")
            {
                var relationships = selectedEntityInfo.MToOneRelationships.Where(x => x.ReferencedAttribute == metadata.LogicalName);
                ReferencingEntityNavigationPropertyName = relationships.ElementAt(0).ReferencingEntityNavigationPropertyName;
            }
           
             
        }

        public AttributeMetadata Metadata { get; set; }
        public string DisplayName { get; set; }
        public string LogicalName { get; set; }


        public AttributeTypeCode DataType { get; set; }
        public bool IsValidForCreate { get; internal set; }
        public bool IsValidForUpdate { get; internal set; }
        public string ReferencingEntityNavigationPropertyName { get; }

        public override string ToString()
        {
            return $"{DisplayName} ({LogicalName}) ({ReferencingEntityNavigationPropertyName})";
        }
    }
}
