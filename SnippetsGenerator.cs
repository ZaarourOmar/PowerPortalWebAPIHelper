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


        public static string GenerateWrapperFunction()
        {
            StringBuilder snippetTextBuilder = new StringBuilder();
            snippetTextBuilder.AppendLine("//This is the wrapper ajax function to execute the API calls. Make sure that this piece of code is loaded before you call the APIs");
            snippetTextBuilder.AppendLine();
            snippetTextBuilder.AppendLine("(function(webapi, $){");
            snippetTextBuilder.AppendLine("\tfunction safeAjax(ajaxOptions) {");
            snippetTextBuilder.AppendLine("\t\tvar deferredAjax = $.Deferred();");
            snippetTextBuilder.AppendLine("\t\tshell.getTokenDeferred().done(function (token) {");
            snippetTextBuilder.AppendLine("\t\t\t// add headers for AJAX");
            snippetTextBuilder.AppendLine("\t\t\tif (!ajaxOptions.headers) {");
            snippetTextBuilder.AppendLine("\t\t\t\t$.extend(ajaxOptions, {");
            snippetTextBuilder.AppendLine("\t\t\t\t\theaders: {");
            snippetTextBuilder.AppendLine("\t\t\t\t\t\t\"__RequestVerificationToken\": token");
            snippetTextBuilder.AppendLine("\t\t\t\t\t}");
            snippetTextBuilder.AppendLine("\t\t\t\t});");
            snippetTextBuilder.AppendLine("\t\t\t} else {");
            snippetTextBuilder.AppendLine("\t\t\t\tajaxOptions.headers[\"__RequestVerificationToken\"] = token;");
            snippetTextBuilder.AppendLine("\t\t\t}");
            snippetTextBuilder.AppendLine("\t\t\t$.ajax(ajaxOptions)");
            snippetTextBuilder.AppendLine("\t\t\t\t.done(function(data, textStatus, jqXHR) {");
            snippetTextBuilder.AppendLine("\t\t\t\t\tvalidateLoginSession(data, textStatus, jqXHR, deferredAjax.resolve);");
            snippetTextBuilder.AppendLine("\t\t\t\t.}).fail(deferredAjax.reject); //AJAX");
            snippetTextBuilder.AppendLine("\t\t}).fail(function () {");
            snippetTextBuilder.AppendLine("\t\t\tdeferredAjax.rejectWith(this, arguments); // on token failure pass the token AJAX and args");
            snippetTextBuilder.AppendLine("\t\t});");


            return snippetTextBuilder.ToString();
        }

        public static string GenerateCreateSnippet(string collectionSchemaName, List<AttributeItemModel> selectedAttributes)
        {
            string json = GenerateJsonFromFields(selectedAttributes, OperationTypes.Create);
            StringBuilder snippetTextBuilder = new StringBuilder();

            snippetTextBuilder.AppendLine("// This is a sample create function using the POST operator. Use this when you want to create a record");
            snippetTextBuilder.AppendLine("webapi.safeAjax({");
            snippetTextBuilder.AppendLine("\ttype: 'POST',");
            snippetTextBuilder.AppendLine("\turl: '/_api/" + collectionSchemaName + ",");
            snippetTextBuilder.AppendLine("\tcontentType:'application/json',");
            snippetTextBuilder.AppendLine("\tdata: JSON.stringify({");
            snippetTextBuilder.AppendLine(json);
            snippetTextBuilder.AppendLine("\t}),");
            snippetTextBuilder.AppendLine("\tsuccess: function(res, status, xhr) {");
            snippetTextBuilder.AppendLine("\t\t//print id of newly created entity record");
            snippetTextBuilder.AppendLine("\t\tconsole.log('entityID: ' + xhr.getResponseHeader('entityid'))");
            snippetTextBuilder.AppendLine("\t}");
            snippetTextBuilder.AppendLine("});");


            return snippetTextBuilder.ToString();
        }


        public static string GenerateUpdateSnippet(string collectionSchemaName, List<AttributeItemModel> selectedAttributes)
        {
            StringBuilder snippetTextBuilder = new StringBuilder();
            snippetTextBuilder.AppendLine("// This is a sample update function using the PATCH operator. Use this when you want to update many attributes in the record. You need to update the ID of the record and the attributes in the json object to your needs. A sample list of attributes and dummy values are provided for you.");
            snippetTextBuilder.Append(GenerateBasicUpdateFunction(collectionSchemaName, selectedAttributes));
            snippetTextBuilder.AppendLine();
            snippetTextBuilder.AppendLine();
            snippetTextBuilder.AppendLine();
            snippetTextBuilder.AppendLine();
            snippetTextBuilder.AppendLine("// This is a sample update function using the PUT operator. Use this when you want to update a single attribute in the record. You need to update the Id of the record and the logical name of the attribute you wish to update. Also, a new value for the attribute must be provided in the json object.");
            snippetTextBuilder.AppendLine(GenerateSinglePropertyUpdateFunction(collectionSchemaName));

            return snippetTextBuilder.ToString();
        }
        private static string GenerateBasicUpdateFunction(string collectionSchemaName, List<AttributeItemModel> selectedAttributes)
        {
            string json = GenerateJsonFromFields(selectedAttributes, OperationTypes.Update);
            StringBuilder snippetTextBuilder = new StringBuilder();

            snippetTextBuilder.AppendLine("webapi.safeAjax({");
            snippetTextBuilder.AppendLine("\ttype: 'PATCH',");
            snippetTextBuilder.AppendLine("\turl: '/_api/" + collectionSchemaName + "(YOUR_GUID_HERE),");
            snippetTextBuilder.AppendLine("\tcontentType:'application/json',");
            snippetTextBuilder.AppendLine("\tdata: JSON.stringify({");
            snippetTextBuilder.AppendLine(json);
            snippetTextBuilder.AppendLine("\t}),");
            snippetTextBuilder.AppendLine("\tsuccess: function(res) {");
            snippetTextBuilder.AppendLine("\t\tconsole.log(res)");
            snippetTextBuilder.AppendLine("\t}");
            snippetTextBuilder.AppendLine("});");


            return snippetTextBuilder.ToString();
        }

        private static string GenerateSinglePropertyUpdateFunction(string collectionSchemaName)
        {
            StringBuilder snippetTextBuilder = new StringBuilder();
            snippetTextBuilder.AppendLine("webapi.safeAjax({");
            snippetTextBuilder.AppendLine("\ttype: 'PUT',");
            snippetTextBuilder.AppendLine("\turl: '/_api/" + collectionSchemaName + "(YOUR_GUID_HERE)/ATTRIBUTE_LOGICAL_NAME_HERE,");
            snippetTextBuilder.AppendLine("\tcontentType:'application/json',");
            snippetTextBuilder.AppendLine("\tdata: JSON.stringify({");
            snippetTextBuilder.AppendLine("\\t\t'value':'NEW VALUE HERE'");
            snippetTextBuilder.AppendLine("\t}),");
            snippetTextBuilder.AppendLine("\tsuccess: function(res) {");
            snippetTextBuilder.AppendLine("\t\tconsole.log(res)");
            snippetTextBuilder.AppendLine("\t}");
            snippetTextBuilder.AppendLine("});");


            return snippetTextBuilder.ToString();
        }

        public static string GenerateDeleteSnippet(string collectionSchemaName)
        {

            StringBuilder snippetTextBuilder = new StringBuilder();

            snippetTextBuilder.AppendLine("// This is a sample delete function using the DELETE operator. Use this when you want to delete a record. You need to specify the ID of the record between the brackets");

            snippetTextBuilder.AppendLine("webapi.safeAjax({");
            snippetTextBuilder.AppendLine("\ttype: 'DELETE',");
            snippetTextBuilder.AppendLine("\turl: '/_api/" + collectionSchemaName + "(YOUR_GUID_HERE),");
            snippetTextBuilder.AppendLine("\tcontentType:'application/json',");
            snippetTextBuilder.AppendLine("\tsuccess: function(res) {");
            snippetTextBuilder.AppendLine("\t\tconsole.log(res)");
            snippetTextBuilder.AppendLine("\t}");
            snippetTextBuilder.AppendLine("});");

            return snippetTextBuilder.ToString();
        }


        private static string GenerateJsonFromFields(List<AttributeItemModel> selectedAttributes, OperationTypes operationType)
        {

            StringBuilder jsonBuilder = new StringBuilder();
            foreach (var attribute in selectedAttributes)
            {
                if (operationType == OperationTypes.Create && !attribute.IsValidForCreate)
                    continue;
                if (operationType == OperationTypes.Update && !attribute.IsValidForUpdate) 
                    continue;

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
                        jsonBuilder.AppendLine("\t\t'" + attribute.LogicalName + "':");
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

    public enum OperationTypes { Create, Update, Delete }
}
