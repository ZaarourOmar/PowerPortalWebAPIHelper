using Microsoft.Xrm.Sdk.Metadata;
using PowerPortalWebAPIHelper.Models;
using System;
using System.Collections.Generic;

namespace PowerPortalWebAPIHelper
{
    public class MetadataValidator
    {

        // an attribute is valid if it has a display name 
        public static bool IsValidAttribute(AttributeMetadata attributeMetadata)
        {
            return attributeMetadata.DisplayName.LocalizedLabels.Count > 0;
        }

        // Microsoft docs doesn't clearly state which entities are good candidates to be exposed to the portal through the web api. The only thing I could find is that data entity (like account and contacts etc) and custom entities are what the users can expose. This validation function tries to limit the entities shown to users based on the above assumption but I can't guarntee that this list is fully inclusive or exclusive until microsoft provides more details on the conditions around the valid entities.
        public static bool IsValidEntity(EntityMetadata entityMetadata)
        {

            // neglect config entities and hidden entities that have no display name and any internal entity that doesn't expose a logical collection name.
            bool isConfigEntity = ExcludedEntitiesByLogicalName.Find(x => x == entityMetadata.LogicalName) != null;
            bool hasDisplayName = entityMetadata.DisplayName.LocalizedLabels.Count > 0;
            bool hasCollecitonName = !string.IsNullOrEmpty(entityMetadata.LogicalCollectionName);
            bool isMsDynEntity = entityMetadata.LogicalName.StartsWith("msdyn");
            bool isBpFEntity = entityMetadata.IsBPFEntity.Value;
            bool isDataEntity = entityMetadata.CanBeInCustomEntityAssociation.Value == true && entityMetadata.IsCustomizable.Value == true &&
                entityMetadata.IsImportable.Value == true;
            return !isConfigEntity && hasDisplayName && hasCollecitonName && !isMsDynEntity && isDataEntity && !isBpFEntity;
        }



        // this entity list can't be exposed via the web api (source https://docs.microsoft.com/en-us/powerapps/maker/portals/web-api-overview#configuration-entities)
        static List<string> ExcludedEntitiesByLogicalName = new List<string>()
        {
            "adx_contentaccesslevel","adx_redirect,adx_webpage_tag",
            "adx_contentsnippet","adx_setting","adx_webpageaccesscontrolrule",
            "adx_entityform","adx_shortcut","adx_webpageaccesscontrolrule_webrole",
            "adx_entityformmetadata","adx_sitemarker","adx_webpagehistory",
            "adx_entitylist","adx_sitesetting","adx_webpagelog",
            "adx_entitypermission_webrole","adx_webfile","adx_webrole_systemuser",
            "adx_externalidentity","adx_webfilelog","adx_website",
            "adx_pagealert","adx_webform","adx_website_list",
            "adx_pagenotification","adx_webformmetadata","adx_website_sponsor",
            "adx_pagetag","adx_webformsession","adx_websiteaccess",
            "adx_pagetag_webpage","adx_webformstep","adx_websiteaccess_webrole",
            "adx_pagetemplate","adx_weblink","adx_websitebinding",
            "adx_portallanguage","adx_weblinkset","adx_websitelanguage",
            "adx_publishingstate","adx_webnotificationentity","adx_webtemplate",
            "adx_publishingstatetransitionrule","adx_webnotificationurl","adx_urlhistory",
            "adx_publishingstatetransitionrule_webrole","adx_webpage","adx_entitypermission"
        };
    }
}