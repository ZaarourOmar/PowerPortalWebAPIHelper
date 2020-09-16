using PowerPortalWebAPIHelper.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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

        public static string GenerateCreateSnippet(string collectionSchemaName, List<AttributeItemModel> selectedAttributes)
        {
            string json = GenerateJsonFromFields(selectedAttributes);
            StringBuilder snipptTextBuilder = new StringBuilder();

            snipptTextBuilder.AppendLine("// This is a sample create function using the POST operator. Use this when you want to create a record");
            snipptTextBuilder.AppendLine("webapi.safeAjax({");
            snipptTextBuilder.AppendLine("\ttype: 'POST',");
            snipptTextBuilder.AppendLine("\turl: '/_api/" + collectionSchemaName + ",");
            snipptTextBuilder.AppendLine("\tcontentType:'application/json',");
            snipptTextBuilder.AppendLine("\tdata: JSON.stringify({");
            snipptTextBuilder.AppendLine(json);
            snipptTextBuilder.AppendLine("\t}),");
            snipptTextBuilder.AppendLine("\tsuccess: function(res, status, xhr) {");
            snipptTextBuilder.AppendLine("\t\t//print id of newly created entity record");
            snipptTextBuilder.AppendLine("\t\tconsole.log('entityID: ' + xhr.getResponseHeader('entityid'))");
            snipptTextBuilder.AppendLine("\t}");
            snipptTextBuilder.AppendLine("});");


            return snipptTextBuilder.ToString();
        }


        public static string GenerateUpdateSnippet(string collectionSchemaName, List<AttributeItemModel> selectedAttributes)
        {
            StringBuilder snipptTextBuilder = new StringBuilder();
            snipptTextBuilder.AppendLine("// This is a sample update function using the PATCH operator. Use this when you want to update many attributes in the record. You need to update the ID of the record and the attributes in the json object to your needs. A sample list of attributes and dummy values are provided for you.");
            snipptTextBuilder.Append(GenerateBasicUpdateFunction(collectionSchemaName, selectedAttributes));
            snipptTextBuilder.AppendLine();
            snipptTextBuilder.AppendLine();
            snipptTextBuilder.AppendLine();
            snipptTextBuilder.AppendLine();
            snipptTextBuilder.AppendLine("// This is a sample update function using the PUT operator. Use this when you want to update a single attribute in the record. You need to update the Id of the record and the logical name of the attribute you wish to update. Also, a new value for the attribute must be provided in the json object.");
            snipptTextBuilder.AppendLine(GenerateSinglePropertyUpdateFunction(collectionSchemaName));

            return snipptTextBuilder.ToString();
        }
        private static string GenerateBasicUpdateFunction(string collectionSchemaName, List<AttributeItemModel> selectedAttributes)
        {
            string json = GenerateJsonFromFields(selectedAttributes);
            StringBuilder snipptTextBuilder = new StringBuilder();

            snipptTextBuilder.AppendLine("webapi.safeAjax({");
            snipptTextBuilder.AppendLine("\ttype: 'PATCH',");
            snipptTextBuilder.AppendLine("\turl: '/_api/" + collectionSchemaName + "(YOUR_GUID_HERE),");
            snipptTextBuilder.AppendLine("\tcontentType:'application/json',");
            snipptTextBuilder.AppendLine("\tdata: JSON.stringify({");
            snipptTextBuilder.AppendLine(json);
            snipptTextBuilder.AppendLine("\t}),");
            snipptTextBuilder.AppendLine("\tsuccess: function(res) {");
            snipptTextBuilder.AppendLine("\t\tconsole.log(res)");
            snipptTextBuilder.AppendLine("\t}");
            snipptTextBuilder.AppendLine("});");


            return snipptTextBuilder.ToString();
        }

        private static string GenerateSinglePropertyUpdateFunction(string collectionSchemaName)
        {
            StringBuilder snipptTextBuilder = new StringBuilder();
            snipptTextBuilder.AppendLine("webapi.safeAjax({");
            snipptTextBuilder.AppendLine("\ttype: 'PUT',");
            snipptTextBuilder.AppendLine("\turl: '/_api/" + collectionSchemaName + "(YOUR_GUID_HERE)/ATTRIBUTE_LOGICAL_NAME_HERE, // Add the record Guid between the brackets and the attribute you wish to update");
            snipptTextBuilder.AppendLine("\tcontentType:'application/json',");
            snipptTextBuilder.AppendLine("\tdata: JSON.stringify({");
            snipptTextBuilder.AppendLine("\\t\t'value':'NEW VALUE HERE'//Add your new value here");
            snipptTextBuilder.AppendLine("\t}),");
            snipptTextBuilder.AppendLine("\tsuccess: function(res) {");
            snipptTextBuilder.AppendLine("\t\tconsole.log(res)");
            snipptTextBuilder.AppendLine("\t}");
            snipptTextBuilder.AppendLine("});");


            return snipptTextBuilder.ToString();
        }

        public static string GenerateDeleteSnippet(string collectionSchemaName)
        {

            StringBuilder snipptTextBuilder = new StringBuilder();

            snipptTextBuilder.AppendLine("// This is a sample delete function using the DELETE operator. Use this when you want to delete a record. You need to specify the ID of the record between the brackets");

            snipptTextBuilder.AppendLine("webapi.safeAjax({");
            snipptTextBuilder.AppendLine("\ttype: 'DELETE',");
            snipptTextBuilder.AppendLine("\turl: '/_api/" + collectionSchemaName + "(YOUR_GUID_HERE),");
            snipptTextBuilder.AppendLine("\tcontentType:'application/json',");
            snipptTextBuilder.AppendLine("\tsuccess: function(res) {");
            snipptTextBuilder.AppendLine("\t\tconsole.log(res)");
            snipptTextBuilder.AppendLine("\t}");
            snipptTextBuilder.AppendLine("});");

            return snipptTextBuilder.ToString();
        }

        private static string GenerateJsonFromFields(List<AttributeItemModel> selectedAttributes)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            foreach (var attribute in selectedAttributes)
            {
                switch (attribute.DataType)
                {
                    case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.Integer:
                    case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.BigInt:
                    case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.Picklist:
                    case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.State:
                    case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.Status:
                        jsonBuilder.AppendLine("\t\t'" + attribute.LogicalName + "':'" + 0 + "',");
                        break;
                    case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.Double:
                    case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.Decimal:
                    case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.Money:
                        jsonBuilder.AppendLine("\t\t'" + attribute.LogicalName + "':'" + 0.0 + "',");
                        break;

                    case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.Memo:
                    case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.String:
                        jsonBuilder.AppendLine("\t\t'" + attribute.LogicalName + "':'Some String Value',");
                        break;

                    case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.Uniqueidentifier:
                        jsonBuilder.AppendLine("\t\t'" + attribute.LogicalName + "':'" + Guid.Empty.ToString() + "',");
                        break;

                    case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.Lookup:
                        jsonBuilder.AppendLine("\t\t'"+attribute.LogicalName+"':");
                        jsonBuilder.AppendLine("\t\t{");
                        jsonBuilder.AppendLine("\t\t\t //Add related entity fields");
                        jsonBuilder.AppendLine("\t\t},");

                        break;

                    default:
                        jsonBuilder.AppendLine("\t\t'" + attribute.LogicalName + "':'Some String Value',");
                        break;
                }
            }
            return jsonBuilder.ToString();
        }
    }
}
