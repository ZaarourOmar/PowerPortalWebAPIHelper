using Microsoft.Xrm.Sdk.Metadata;
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

        private static string GenerateJsonFromFields( List<WebAPIAttributeItemModel> selectedAttribute, APIOperationTypes operationType)
        {

            StringBuilder jsonBuilder = new StringBuilder();
            foreach (var attribute in selectedAttribute)
            {
                if (operationType == APIOperationTypes.BasicCreate && !attribute.IsValidForCreate)
                    continue;
                if ((operationType == APIOperationTypes.UpdateSingle || operationType == APIOperationTypes.BasicUpdate) && !attribute.IsValidForUpdate)
                    continue;

                switch (attribute.DataType)
                {

                    case AttributeTypeCode.Boolean:
                        jsonBuilder.Append("\t\t\"" + attribute.LogicalName + ":false");
                        break;
                    case AttributeTypeCode.Integer:
                    case AttributeTypeCode.BigInt:
                    case AttributeTypeCode.Picklist:
                    case AttributeTypeCode.State:
                    case AttributeTypeCode.Status:
                        jsonBuilder.Append("\t\t\"" + attribute.LogicalName + "\":0");
                        break;
                    case AttributeTypeCode.Double:
                    case AttributeTypeCode.Decimal:
                    case AttributeTypeCode.Money:
                        jsonBuilder.Append("\t\t\"" + attribute.LogicalName + "\":0.0");
                        break;
                    case AttributeTypeCode.Memo:
                    case AttributeTypeCode.String:

                        jsonBuilder.Append("\t\t\"" + attribute.LogicalName + "\":\"Some String Value\"");
                        break;
                    case AttributeTypeCode.DateTime:
                        jsonBuilder.Append("\t\t\"" + attribute.LogicalName + "\":\"Some Date Value\"");

                        break;
                    case AttributeTypeCode.Uniqueidentifier:
                        jsonBuilder.Append("\t\t\"" + attribute.LogicalName + "\":\"" + Guid.Empty.ToString() + "\"");
                        break;
                    case AttributeTypeCode.Lookup:
                    case AttributeTypeCode.Customer:

                        if (operationType == APIOperationTypes.BasicUpdate || operationType == APIOperationTypes.UpdateSingle)
                        {
                            var relatedEntityColelcitonName = attribute.RelatedEntityCollectionName;
                            jsonBuilder.Append("\t\t\"" + attribute.WebAPIName + "@odata.bind\":portalurl+\"/_api/"+ relatedEntityColelcitonName + "(11111111-1111-1111-1111-111111111111)" + "\"");
                        }
                        else
                        {
                            jsonBuilder.AppendLine("\t\t\"" + attribute.WebAPIName + "\":");
                            jsonBuilder.AppendLine("\t\t{");
                            jsonBuilder.AppendLine("\t\t\t //Add related entity fields. Make sure to add the required fields for the related entity. Also, make sure that the related entity is enabled for Web API and has proper entity permissions");
                            jsonBuilder.Append("\t\t}");
                        }
                       

                        break;
                    case AttributeTypeCode.Virtual:
                        jsonBuilder.Append("\t\t\"" + attribute.LogicalName + "\":\"This is a virtual field. An example of this field is entity image on the contact entity and it needs to be a base64 string. For other virtual fields values, please check Microsoft documentation.\"");
                        break;
                    default:
                        jsonBuilder.Append("\t\t\"" + attribute.LogicalName + "\":\"Unknown field type. please check Microsoft documentation for the proper format of the this field value.\"");
                        break;
                }

                if (selectedAttribute.IndexOf(attribute) < selectedAttribute.Count - 1)
                {
                    jsonBuilder.AppendLine(",");
                }
               
            }
            jsonBuilder.AppendLine("");
            return jsonBuilder.ToString();
        }

        public static string GenerateSnippet(EntityItemModel selectedEntityInfo, List<WebAPIAttributeItemModel> selectedAttributes, APIOperationTypes opType, AssociationInfo association, bool addFields)
        {
            switch (opType)
            {
                case APIOperationTypes.BasicCreate:
                    return GenerateBasicCreateSnippet(selectedEntityInfo, selectedAttributes, opType, association, addFields);
                case APIOperationTypes.UpdateSingle:
                    return GenerateUpdateSingleSnippet(selectedEntityInfo,selectedAttributes, addFields);
                case APIOperationTypes.BasicUpdate:
                    return GenerateBasicUpdateSnippet(selectedEntityInfo, selectedAttributes, opType, association, addFields);
                case APIOperationTypes.BasicDelete:
                    return GenerateBasicDeleteSnippet(selectedEntityInfo, selectedAttributes);
                case APIOperationTypes.DeleteSingle:
                    return GenerateDeleteSingleSnippet(selectedEntityInfo, selectedAttributes, addFields);
                case APIOperationTypes.AssociateDisassociate:

                    break;
                default:
                    throw new NotImplementedException("This operation is not implemented yet");
            }

            return "";
        }

        private static string GenerateDeleteSingleSnippet(EntityItemModel selectedEntityInfo, List<WebAPIAttributeItemModel> selectedAttributes,bool addFields)
        {
            StringBuilder snippetTextBuilder = new StringBuilder();
            snippetTextBuilder.AppendLine("// This is a sample delete snippet to delete a single property using the DELETE operator. Use this when you want to clear out a single property value and not the whole record. You need to specify the ID of the record between the brackets and the logical name of the propery");

            snippetTextBuilder.AppendLine("webapi.safeAjax({");
            snippetTextBuilder.AppendLine("\ttype: \"DELETE\",");
            snippetTextBuilder.AppendLine("\turl: \"/_api/" + selectedEntityInfo.CollectionName + "(00000000-0000-0000-0000-000000000000)/ATTRIBUTE_NAME_HERE\",");
            snippetTextBuilder.AppendLine("\tcontentType:\"application/json\",");
            snippetTextBuilder.AppendLine("\tsuccess: function(res) {");
            snippetTextBuilder.AppendLine("\t\tconsole.log(res)");
            snippetTextBuilder.AppendLine("\t}");
            snippetTextBuilder.AppendLine("});");

            return snippetTextBuilder.ToString();
        }

        private static string GenerateBasicDeleteSnippet(EntityItemModel selectedEntityInfo, List<WebAPIAttributeItemModel> selectedAttributes)
        {
            StringBuilder snippetTextBuilder = new StringBuilder();

            snippetTextBuilder.AppendLine("// This is a sample delete snippet using the DELETE operator. Use this when you want to delete a record. You need to specify the ID of the record between the brackets.");

            snippetTextBuilder.AppendLine("webapi.safeAjax({");
            snippetTextBuilder.AppendLine("\ttype: \"DELETE\",");
            snippetTextBuilder.AppendLine("\turl: \"/_api/" + selectedEntityInfo.CollectionName + "(00000000-0000-0000-0000-000000000000)\",");
            snippetTextBuilder.AppendLine("\tcontentType:\"application/json\",");
            snippetTextBuilder.AppendLine("\tsuccess: function(res) {");
            snippetTextBuilder.AppendLine("\t\tconsole.log(res)");
            snippetTextBuilder.AppendLine("\t}");
            snippetTextBuilder.AppendLine("});");

            return snippetTextBuilder.ToString();
        }

        private static string GenerateBasicUpdateSnippet(EntityItemModel selectedEntityInfo, List<WebAPIAttributeItemModel> selectedAttributes, APIOperationTypes opType, AssociationInfo association, bool addFields)
        {

            StringBuilder snippetTextBuilder = new StringBuilder();
            snippetTextBuilder.AppendLine("// This is a sample basic update snippet using the PATCH operator. You need to specify the ID of the record being updated and the JSON data object for the fields you want to update.");

            string json = addFields ? GenerateJsonFromFields(selectedAttributes, APIOperationTypes.BasicUpdate) : "";
            StringBuilder jsonObject = new StringBuilder();
            jsonObject.AppendLine("var dataObject={");
            jsonObject.AppendLine(json);
            jsonObject.AppendLine("};");


            snippetTextBuilder.AppendLine(jsonObject.ToString());


            snippetTextBuilder.AppendLine("webapi.safeAjax({");
            snippetTextBuilder.AppendLine("\ttype: \"PATCH\",");
            snippetTextBuilder.AppendLine("\turl: \"/_api/" + selectedEntityInfo.CollectionName + "(00000000-0000-0000-0000-000000000000)\",");
            snippetTextBuilder.AppendLine("\tcontentType:\"application/json\",");
            snippetTextBuilder.AppendLine("\tdata: JSON.stringify(dataObject),");
            snippetTextBuilder.AppendLine("\tsuccess: function(res) {");
            snippetTextBuilder.AppendLine("\t\tconsole.log(res)");
            snippetTextBuilder.AppendLine("\t}");
            snippetTextBuilder.AppendLine("});");


            return snippetTextBuilder.ToString();
        }
        private static string GenerateUpdateSingleSnippet(EntityItemModel selectedEntityInfo, List<WebAPIAttributeItemModel> selectedAttributes,bool addFields)
        {
            StringBuilder snippetTextBuilder = new StringBuilder();
            snippetTextBuilder.AppendLine("// This is a sample single field update snippet using the PUT operator. You need to specify the ID of the record being updated, the logical name of the attribute and the value of the targeted attribute.");

            snippetTextBuilder.AppendLine("webapi.safeAjax({");
            snippetTextBuilder.AppendLine("\ttype: \"PUT\",");
            snippetTextBuilder.AppendLine("\turl: \"/_api/" + selectedEntityInfo.CollectionName + "(00000000-0000-0000-0000-000000000000)/ATTRIBUTE_NAME_HERE\",");
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

        private static string GenerateBasicCreateSnippet(EntityItemModel selectedEntityInfo, List<WebAPIAttributeItemModel> selectedAttributes, APIOperationTypes opType, AssociationInfo association, bool addFields)
        {
            string json = addFields ? GenerateJsonFromFields(selectedAttributes, opType) : "";
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
