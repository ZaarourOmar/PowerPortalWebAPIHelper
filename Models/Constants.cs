using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPortalWebAPIHelper.Models
{
    public static class Constants
    {
        public const string ENTITY_FILTER_HINT = "Search for the entity name here..";
        public const string ENABLE_INNER_ERROR_TEXT = "Enable Inner Error Tracking for this website";
        public const string DISABLE_INNER_ERROR_TEXT = "Disable Inner Error Tracking for this website";
        public const string INNER_ERROR_CONFIRMATION_MESSAGE = "This command will either create or update the site setting(webapi/innererror/enabled) for the selected website.Do you want to continue?";
        public const string SAVE_CHANGES_CONFIRMATION_MESSAGE = "This command will either create or update the needed site settings for the selected entity and under the selected website, do you want to continue? ";

        public static string CREATE_ENTITY_PERMISSION_DIALOG = "This action will create a global entity permission for the selected entity. This is the least restrictive entity permission type so make sure to modify it based on your needs. The created permission will be associated with the \"Administrators\" web role (if found). Do you want to continue?";

        public static string WEBROLE_ADMIN_ID = "6f2449bf-9988-4db2-b4b7-42a52c931cc5";
        public static string WEBROLE_ADMIN_NAME= "Administrators";

        public static string WEBROLE_ADMIN_NOTFOUND = "The \"Administrators\" Webrole is not found for the selected website. The entity permission will be created without a role attached to it and it needs to be done manually.";

        public static string UNABLE_TO_CREATE_ENTITY_PERMISSION = "Unable to create entity permission, please restart the tool.";

        public static string ENTITY_PERMISSION_CREATED = "An entity permission is created for the selected entity.";

        public static string ENTITY_HAS_NO_ENTITYPERMISSION = "The selected entity has no related active entity permissions found. One is needed for the WebAPI calls to work.";

        public static string ENTITY_HAS_ENTITYPERMISSION = "An active entity permission is found for the selected entity. Make sure it allows for the required API actions (Create, Write, Delete ...)";
        public static string NO_PORTAL_WEBSITE_FOUND= "The target organization has no active portal setup. Please setup a portal first then restart this tool.";
    }
}
