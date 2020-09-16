using Microsoft.Xrm.Sdk.Metadata;
using System;

namespace PowerPortalWebAPIHelper
{
    public class MetadataValidator
    {
        public static bool IsValidAttribute(AttributeMetadata attributeMetadata)
        {
            return attributeMetadata.DisplayName.LocalizedLabels.Count > 0;
        }
        public static bool IsValidEntity(EntityMetadata entityMetadata)
        {
            // this check needs to be replaced with a list of config entities and currently for simplicity, I neglect all entities that are prefxied with adx
            return !entityMetadata.LogicalName.StartsWith("adx_") &&entityMetadata.DisplayName.LocalizedLabels.Count>0 && !string.IsNullOrEmpty(entityMetadata.LogicalCollectionName);
        }
    }
}