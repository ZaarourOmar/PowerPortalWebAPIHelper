using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPortalWebAPIHelper.Models
{
   public class OperationTypeInfo
    {
        private OperationTypeInfo(string helpMessage, APIOperationTypes type)
        {
            Message = helpMessage;
            Type = type;
        }

        public string Message { get; set; }
        public APIOperationTypes Type { get; set; }

        public override string ToString()
        {
            return Type.ToString();
        }

        public static List<OperationTypeInfo> LoadAvailableTypes()
        {
            List<OperationTypeInfo> types = new List<OperationTypeInfo>();
            types.Add(new OperationTypeInfo("Create new record with a POST operation", APIOperationTypes.BasicCreate));
            types.Add(new OperationTypeInfo("Update multiple alues in a record using PATCH operation", APIOperationTypes.BasicUpdate));
            types.Add(new OperationTypeInfo("Update a single value in a record using PUT Operation", APIOperationTypes.UpdateSingle));
            types.Add(new OperationTypeInfo("Delete a record by its ID", APIOperationTypes.BasicDelete));
            types.Add(new OperationTypeInfo("Delete a single field on a record", APIOperationTypes.DeleteSingle));
            types.Add(new OperationTypeInfo("associate ", APIOperationTypes.Associate));

            //to be continued
            return types;
        }
    }
}
