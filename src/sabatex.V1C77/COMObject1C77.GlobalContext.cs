using sabatex.V1C77.Models.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace sabatex.V1C77
{
    public partial class COMObject1C77:IGlobalContext
    {

        #region IGlobalContext
 
        bool IGlobalContext.EmptyValue(ICOMObject1C77 obj)
        {
            return Method<bool>("EmptyValue", obj);
        }
        object IGlobalContext.EvalExpr(string expr)
        {
            return Method<object>("evalexpr", expr);
        }
        void IGlobalContext.SaveValue(string identifier, object value)
        {
            Method<object>("SaveValue", identifier, value);
        }
        COMObject1C77 IGlobalContext.CreateObject(string objectName)
        {
            return CreateObject(objectName);
        }
        ICatalog1C77 IGlobalContext.CreateObjectCatalog(string catalogName)
        {
            return CreateObject($"Справочник.{catalogName}") as ICatalog1C77;
        }
        IDocument1C77 IGlobalContext.CreateObjectDocument(string documentName)
        {
            return CreateObject($"Документ.{documentName}") as IDocument1C77;
        }



        #endregion IGlobalContext

    }
}
