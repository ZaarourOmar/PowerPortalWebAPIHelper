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
            snippetTextBuilder.AppendLine("//This is the wrapper ajax snippet to execute the API calls. Make sure that this piece of code is loaded before you call the APIs. You can paste this wrapper in the page html content (in a script tag) or you can store it in a webfile and expose it in the header or footer web templates.  depending on your use case.");
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
            snippetTextBuilder.AppendLine("\t\t\t\t}).fail(deferredAjax.reject); //AJAX");
            snippetTextBuilder.AppendLine("\t\t}).fail(function () {");
            snippetTextBuilder.AppendLine("\t\t\tdeferredAjax.rejectWith(this, arguments); // on token failure pass the token AJAX and args");
            snippetTextBuilder.AppendLine("\t\t});");
            snippetTextBuilder.AppendLine("\t\treturn deferredAjax.promise();");
            snippetTextBuilder.AppendLine("\t}");
            snippetTextBuilder.AppendLine("\twebapi.safeAjax = safeAjax;");
            snippetTextBuilder.AppendLine("})(window.webapi = window.webapi || {}, jQuery)");


            return snippetTextBuilder.ToString();
        }

        private static string GenerateJsonFromFields(List<AttributeItemModel> selectedAttributes, APIOperationTypes operationType)
        {

            StringBuilder jsonBuilder = new StringBuilder();
            foreach (var attribute in selectedAttributes)
            {
                if (operationType == APIOperationTypes.BasicCreate && !attribute.IsValidForCreate)
                    continue;
                if ((operationType == APIOperationTypes.UpdateSingle || operationType == APIOperationTypes.BasicUpdate) && !attribute.IsValidForUpdate)
                    continue;

                switch (attribute.DataType)
                {
                    case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.Integer:
                    case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.BigInt:
                    case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.Picklist:
                    case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.State:
                    case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.Status:
                        jsonBuilder.AppendLine("\t\t\"" + attribute.LogicalName + "\":" + 0 + ",");
                        break;
                    case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.Double:
                    case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.Decimal:
                    case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.Money:
                        jsonBuilder.AppendLine("\t\t\"" + attribute.LogicalName + "\":" + 0.0 + ",");
                        break;

                    case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.Memo:
                    case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.String:

                        jsonBuilder.AppendLine("\t\t\"" + attribute.LogicalName + "\":\"Some String Value\",");
                        break;
                    case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.DateTime:
                        jsonBuilder.AppendLine("\t\t\"" + attribute.LogicalName + "\":\"Some Date Value\",");

                        break;
                    case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.Uniqueidentifier:
                        jsonBuilder.AppendLine("\t\t\"" + attribute.LogicalName + "\":\"" + Guid.Empty.ToString() + "\",");
                        break;

                    case Microsoft.Xrm.Sdk.Metadata.AttributeTypeCode.Lookup:
                        jsonBuilder.AppendLine("\t\t\"" + attribute.LogicalName + "\":");
                        jsonBuilder.AppendLine("\t\t{");
                        jsonBuilder.AppendLine("\t\t\t //Add related entity fields");
                        jsonBuilder.AppendLine("\t\t},");

                        break;

                    default:
                        jsonBuilder.AppendLine("\t\t\"" + attribute.LogicalName + "\":\"Some String Value\",");
                        break;
                }


            }
            return jsonBuilder.ToString();
        }

        public static string GenerateSnippet(EntityItemModel selectedEntityInfo, APIOperationTypes opType, AssociationInfo association, bool addFields)
        {
            switch (opType)
            {
                case APIOperationTypes.BasicCreate:
                    return GenerateBasicCreateSnippet(selectedEntityInfo, opType, association, addFields);
                case APIOperationTypes.UpdateSingle:
                    return GenerateUpdateSingleSnippet(selectedEntityInfo);
                case APIOperationTypes.BasicUpdate:
                    return GenerateBasicUpdateSnippet(selectedEntityInfo, opType, association, addFields);
                case APIOperationTypes.BasicDelete:
                    return GenerateBasicDeleteSnippet(selectedEntityInfo);
                case APIOperationTypes.DeleteSingle:
                    return GenerateDeleteSingleSnippet(selectedEntityInfo);
                case APIOperationTypes.Associate:

                    break;
                case APIOperationTypes.Disassociate:

                    break;

                default:
                    throw new NotImplementedException("This operation is not implemented yet");
            }

            return "";
        }

        private static string GenerateDeleteSingleSnippet(EntityItemModel selectedEntityInfo)
        {
            StringBuilder snippetTextBuilder = new StringBuilder();
            snippetTextBuilder.AppendLine("// This is a sample delete snippet to delete a single property using the DELETE operator. Use this when you want to clear out a single property value and not the whole record. You need to specify the ID of the record between the brackets and the logical name of the propery");
            snippetTextBuilder.AppendLine("webapi.safeAjax({");
            snippetTextBuilder.AppendLine("\ttype: \"DELETE\",");
            snippetTextBuilder.AppendLine("\turl: \"/_api/" + selectedEntityInfo.CollectionName + "(YOUR_GUID_HERE)/ATTRIBUTE_LOGICAL_NAME\",");
            snippetTextBuilder.AppendLine("\tcontentType:\"application/json\",");
            snippetTextBuilder.AppendLine("\tsuccess: function(res) {");
            snippetTextBuilder.AppendLine("\t\tconsole.log(res)");
            snippetTextBuilder.AppendLine("\t}");
            snippetTextBuilder.AppendLine("});");

            return snippetTextBuilder.ToString();
        }

        private static string GenerateBasicDeleteSnippet(EntityItemModel selectedEntityInfo)
        {
            StringBuilder snippetTextBuilder = new StringBuilder();

            snippetTextBuilder.AppendLine("// This is a sample delete snippet using the DELETE operator. Use this when you want to delete a record. You need to specify the ID of the record between the brackets.");

            snippetTextBuilder.AppendLine("webapi.safeAjax({");
            snippetTextBuilder.AppendLine("\ttype: \"DELETE\",");
            snippetTextBuilder.AppendLine("\turl: \"/_api/" + selectedEntityInfo.CollectionName + "(YOUR_GUID_HERE)\",");
            snippetTextBuilder.AppendLine("\tcontentType:\"application/json\",");
            snippetTextBuilder.AppendLine("\tsuccess: function(res) {");
            snippetTextBuilder.AppendLine("\t\tconsole.log(res)");
            snippetTextBuilder.AppendLine("\t}");
            snippetTextBuilder.AppendLine("});");

            return snippetTextBuilder.ToString();
        }

        private static string GenerateBasicUpdateSnippet(EntityItemModel selectedEntityInfo, APIOperationTypes opType, AssociationInfo association, bool addFields)
        {

            StringBuilder snippetTextBuilder = new StringBuilder();
            snippetTextBuilder.AppendLine("// This is a sample basic update snippet using the PATCH operator. You need to specify the ID of the record being updated and the JSON data object for the fields you want to update.");

            string json = addFields ? GenerateJsonFromFields(selectedEntityInfo.SelectedAttributesList, APIOperationTypes.BasicUpdate) : "";
            StringBuilder jsonObject = new StringBuilder();
            jsonObject.AppendLine("var dataObject={");
            jsonObject.AppendLine(json);
            jsonObject.AppendLine("};");


            snippetTextBuilder.AppendLine(jsonObject.ToString());


            snippetTextBuilder.AppendLine("webapi.safeAjax({");
            snippetTextBuilder.AppendLine("\ttype: \"PATCH\",");
            snippetTextBuilder.AppendLine("\turl: \"/_api/" + selectedEntityInfo.CollectionName + "(YOUR_GUID_HERE)\",");
            snippetTextBuilder.AppendLine("\tcontentType:\"application/json\",");
            snippetTextBuilder.AppendLine("\tdata: JSON.stringify(dataObject),");
            snippetTextBuilder.AppendLine("\tsuccess: function(res) {");
            snippetTextBuilder.AppendLine("\t\tconsole.log(res)");
            snippetTextBuilder.AppendLine("\t}");
            snippetTextBuilder.AppendLine("});");


            return snippetTextBuilder.ToString();
        }
        private static string GenerateUpdateSingleSnippet(EntityItemModel selectedEntityInfo)
        {
            StringBuilder snippetTextBuilder = new StringBuilder();

            snippetTextBuilder.AppendLine("// This is a sample single field update snippet using the PUT operator. You need to specify the ID of the record being updated, the logical name of the attribute and the value of the targeted attribute");

            snippetTextBuilder.AppendLine("webapi.safeAjax({");
            snippetTextBuilder.AppendLine("\ttype: \"PUT\",");
            snippetTextBuilder.AppendLine("\turl: \"/_api/" + selectedEntityInfo.CollectionName + "(YOUR_GUID_HERE)/ATTRIBUTE_LOGICAL_NAME_HERE\",");
            snippetTextBuilder.AppendLine("\tcontentType:\"application/json\",");
            snippetTextBuilder.AppendLine("\tdata: JSON.stringify({");
            snippetTextBuilder.AppendLine("\t\t\"value\":\"NEW VALUE HERE\"");
            snippetTextBuilder.AppendLine("\t}),");
            snippetTextBuilder.AppendLine("\tsuccess: function(res) {");
            snippetTextBuilder.AppendLine("\t\tconsole.log(res)");
            snippetTextBuilder.AppendLine("\t}");
            snippetTextBuilder.AppendLine("});");


            return snippetTextBuilder.ToString();
        }

        private static string GenerateBasicCreateSnippet(EntityItemModel selectedEntityInfo, APIOperationTypes opType, AssociationInfo association, bool addFields)
        {
            string json = addFields ? GenerateJsonFromFields(selectedEntityInfo.SelectedAttributesList, opType) : "";
            StringBuilder jsonObject = new StringBuilder();
            StringBuilder snippetTextBuilder = new StringBuilder();

            snippetTextBuilder.AppendLine("// This is a sample create snippet using the POST operator. Use this snippet inside your record creation logic. Replace or modify the data object properties per your needs. Make sure that these properies are enabled for web api.");


            jsonObject.AppendLine("var dataObject={");
            jsonObject.AppendLine(json);
            jsonObject.AppendLine("};");


            snippetTextBuilder.AppendLine(jsonObject.ToString());


            snippetTextBuilder.AppendLine("webapi.safeAjax({");
            snippetTextBuilder.AppendLine("\ttype: \"POST\",");
            snippetTextBuilder.AppendLine("\turl: \"/_api/" + selectedEntityInfo.CollectionName + "\",");
            snippetTextBuilder.AppendLine("\tcontentType:\"application/json\",");
            snippetTextBuilder.AppendLine("\tdata: JSON.stringify(dataObject),");
            snippetTextBuilder.AppendLine("\tsuccess: function(res, status, xhr) {");
            snippetTextBuilder.AppendLine("\t\t//print id of newly created entity record");
            snippetTextBuilder.AppendLine("\t\tconsole.log(\"entityID: \" + xhr.getResponseHeader(\"entityid\"))");
            snippetTextBuilder.AppendLine("\t}");
            snippetTextBuilder.AppendLine("});");


            return snippetTextBuilder.ToString();
        }
    }

}
