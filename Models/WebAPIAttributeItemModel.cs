using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPortalWebAPIHelper.Models
{
    public class WebAPIAttributeItemModel
    {
        public static Dictionary<string, string> RelatedEntitiesPluralNames = new Dictionary<string, string>();

        public WebAPIAttributeItemModel(AttributeMetadata metadata, EntityItemModel selectedEntityInfo, bool isCustomerAttribute=false, CustomerType customerType = CustomerType.Account)
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

            WebAPIName = LogicalName = metadata.LogicalName;

            DataType = metadata.AttributeType.Value;
            IsValidForCreate = metadata.IsValidForCreate.Value;
            IsValidForUpdate = metadata.IsValidForUpdate.Value;

            if(metadata.AttributeType== AttributeTypeCode.Customer)
            {
                var relationships = selectedEntityInfo.MToOneRelationships.Where(x => x.ReferencingAttribute == metadata.LogicalName).ToList();
                if (relationships.Count >1)
                {
                    
                    WebAPIName = relationships[(int)customerType].ReferencingEntityNavigationPropertyName;
                    RelatedEntityCollectionName = GetRelatedEntityCollecitonName(relationships[(int)customerType].ReferencedEntity);
                }
            }
            else if (metadata.AttributeType == AttributeTypeCode.Lookup)
            {
                if (metadata.LogicalName == "a51_testcase")
                {
                   
                }
                var relationships = selectedEntityInfo.MToOneRelationships.Where(x => x.ReferencingAttribute == metadata.LogicalName).ToList();
                if (relationships.Count > 0)
                {
                    WebAPIName = relationships[0].ReferencingEntityNavigationPropertyName;
                    RelatedEntityCollectionName = GetRelatedEntityCollecitonName(relationships[0].ReferencedEntity);
                }
            }

        }

        public AttributeMetadata Metadata { get; set; }
        public string DisplayName { get; set; }
        public string LogicalName { get; set; }
        public string WebAPIName { get; set; }
       
        public string RelatedEntityCollectionName { get; set; }

        public AttributeTypeCode DataType { get; set; }
        public bool IsValidForCreate { get; internal set; }
        public bool IsValidForUpdate { get; internal set; }
        public string ReferencingEntityNavigationPropertyName { get; }

        public string GetRelatedEntityCollecitonName(string entityLogicalName)
        {
            if (RelatedEntitiesPluralNames.Keys.Contains(entityLogicalName))
            {
                return RelatedEntitiesPluralNames[entityLogicalName];
            }
            else
            {
                return "not found";
            }
        }
        public override string ToString()
        {
            return $"{DisplayName} (WebAPI Exposed Name: {WebAPIName}, Type: {DataType})";
        }
    }
}
