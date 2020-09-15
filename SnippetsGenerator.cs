using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PowerPortalWebAPIHelper
{
    public class SnippetsGenerator
    {

        const string WRAPPER_FUNCTION = @"(function(webapi, $){
		function safeAjax(ajaxOptions) {
			var deferredAjax = $.Deferred();
	
			shell.getTokenDeferred().done(function (token) {
				// add headers for AJAX
				if (!ajaxOptions.headers) {
					$.extend(ajaxOptions, {
						headers: {
							'__RequestVerificationToken': token
						}
					}); 
				} else {
					ajaxOptions.headers['__RequestVerificationToken'] = token;
				}
				$.ajax(ajaxOptions)
					.done(function(data, textStatus, jqXHR) {
	validateLoginSession(data, textStatus, jqXHR, deferredAjax.resolve);
}).fail(deferredAjax.reject); //AJAX
			}).fail(function () {
	deferredAjax.rejectWith(this, arguments); // on token failure pass the token AJAX and args
});
	
			return deferredAjax.promise();	
		}
		webapi.safeAjax = safeAjax;
	})(window.webapi = window.webapi || {}, jQuery)";



        public static string GenerateWrapperFunction()
        {
            return WRAPPER_FUNCTION;
        }

        public static string GenerateCreateFunction(string collectionSchemaName,Dictionary<string,string> fields)
        {
            string json = GenerateJsonFromFields(fields);
            StringBuilder snipptTextBuilder = new StringBuilder();

            snipptTextBuilder.AppendLine("webapi.safeAjax({");
            snipptTextBuilder.AppendLine("\ttype: 'POST',");
            snipptTextBuilder.AppendLine("\turl: '/_api/" + collectionSchemaName + ",");
            snipptTextBuilder.AppendLine("\tcontentType:'application/json',");
            snipptTextBuilder.AppendLine("\tdata: JSON.stringify({'");
            snipptTextBuilder.AppendLine(json);
            snipptTextBuilder.AppendLine("\t}),");
            snipptTextBuilder.AppendLine("\tsuccess: function(res, status, xhr) {");
            snipptTextBuilder.AppendLine("\t\t//print id of newly created entity record");
            snipptTextBuilder.AppendLine("\t\tconsole.log('entityID: ' + xhr.getResponseHeader('entityid'))");
            snipptTextBuilder.AppendLine("\t}");
            snipptTextBuilder.AppendLine("});");

            
            return snipptTextBuilder.ToString();
        }

        private static string GenerateJsonFromFields(Dictionary<string, string> fields)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            foreach(KeyValuePair<string,string> kvp in fields)
            {
                jsonBuilder.AppendLine("\t\t'" + kvp.Key + "':'" + kvp.Value + "',");
            }
            return jsonBuilder.ToString();
        }
    }
}
