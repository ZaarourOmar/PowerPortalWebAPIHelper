using Microsoft.Xrm.Sdk.Metadata;
using System;

namespace PowerPortalWebAPIHelper
{
    public class AttributeValidator
    {
        public static bool IsValidAttribute(AttributeMetadata attribute)
        {
            return string.IsNullOrEmpty(attribute.AttributeOf) && attribute.DisplayName.LocalizedLabels.Count > 0;
        }
    }
}