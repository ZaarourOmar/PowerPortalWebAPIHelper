using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPortalWebAPIHelper.Models
{
    public class WebAPIHelperModel
    {
        public EntityItemModel SelectedEntityInfo { get; private set; } = new EntityItemModel();
        public List<WebsiteModel> Websites { get; private set; } = new List<WebsiteModel>();
        // a flag to control the select all atribute checkbox
        public bool ForceAllAttributeCheck = false;
        public List<EntityItemModel> AllEntitiesList { get; private set; } = new List<EntityItemModel>();
        public WebsiteModel SelectedWebsiteInfo { get; private set; } = new WebsiteModel();
        public Guid WebAPIEnabledSiteSettingId
        {
            get
            {
                return SelectedEntityInfo.WebAPIEnabledSiteSettingId;
            }
            set
            {
                SelectedEntityInfo.WebAPIEnabledSiteSettingId = value;
            }
        }

        public bool InnerErrorEnabled
        {
            get
            {
                return SelectedWebsiteInfo.InnerErrorEnabled;
            }
            set
            {
                SelectedWebsiteInfo.InnerErrorEnabled = value;
            }
        }
        public Guid InnerErrorSiteSettingsId
        {
            get
            {
                return SelectedWebsiteInfo.InnerErrorSiteSettingsId;
            }
            set
            {
                SelectedWebsiteInfo.InnerErrorSiteSettingsId = value;
            }
        }

        public string SelectedEntityLogicalName
        {
            get { return SelectedEntityInfo.LogicalName; }
            set
            {
                SelectedEntityInfo.LogicalName = value;
            }
        }

        public Guid SelectedWebSiteId
        {
            get { return SelectedWebsiteInfo.Id; }
            set { SelectedWebsiteInfo.Id = value; }
        }

        public Guid WebAPIFieldsSiteSettingId
        {
            get { return SelectedEntityInfo.WebAPIFieldsSiteSettingId; }
            set { SelectedEntityInfo.WebAPIFieldsSiteSettingId = value; }
        }

        public ManyToManyRelationshipMetadata[] SelectedEntityMtoMRelationships
        {
            get
            {
                return SelectedEntityInfo.MToMRelationships;
            }
            set { SelectedEntityInfo.MToMRelationships = value; }
        }

        public void ClearEntities()
        {
            AllEntitiesList?.Clear();
        }

        public void AddEntity(EntityItemModel entity)
        {
            AllEntitiesList?.Add(entity);
        }

        public void AddAttribute(WebAPIAttributeItemModel attribute)
        {
            SelectedEntityInfo?.AllAttributesList?.Add(attribute);
        }

        public void UpdateWebsite(WebsiteModel selectedWebsite)
        {
            SelectedWebsiteInfo = selectedWebsite;
        }

        public void UpdateSelectedEntity(EntityItemModel entityItemModel)
        {
            SelectedEntityInfo = entityItemModel;
        }
    }
}
