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
            types.Add(new OperationTypeInfo("Create new record with a POST operation. Provide the fields you want in the dataObject below.", APIOperationTypes.BasicCreate));
            types.Add(new OperationTypeInfo("Update multiple values in a record using PATCH operation. Make sure that the fields you add in the dataObject are updatable. This tool will check for that for most field types.", APIOperationTypes.BasicUpdate));
            types.Add(new OperationTypeInfo("Update a single value in a record using PUT Operation. This tool will use the first selected attribute as an example but you can change it to fit your needs.", APIOperationTypes.UpdateSingle));
            types.Add(new OperationTypeInfo("Delete a record by its ID. Make sure that the table permission exists and allows for deletes.", APIOperationTypes.BasicDelete));
            types.Add(new OperationTypeInfo("Delete a single field on a record. Make sure that a table permission exists and allows for delete", APIOperationTypes.DeleteSingle));
            //types.Add(new OperationTypeInfo("associate ", APIOperationTypes.AssociateDisassociate));

            //TBD
            return types;
        }
    }
}
