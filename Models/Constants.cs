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
        public const string INNER_ERROR_CONFIRMATION_MESSAGE = "This command will either create or update the  sites etting(webapi/innererror/enabled) for the selected website.Do you want to continue?";
        public const string SAVE_CHANGES_CONFIRMATION_MESSAGE = "This command will either create or update the needed site settings for the selected entity and under the selected website, do you want to continue? ";
    }
}
