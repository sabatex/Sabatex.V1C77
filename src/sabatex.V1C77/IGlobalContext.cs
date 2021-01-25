using System;
using System.Collections.Generic;
using System.Text;

namespace sabatex.V1C77
{
    public interface IGlobalContext:ICOMObject1C77
    {
        public bool EmptyValue(ICOMObject1C77 obj)
        {
            return Method<bool>("EmptyValue", obj);
        }
        public object EvalExpr(string expr)
        {
            return Method<object>("evalexpr", expr);
        }
        public void SaveValue(string identifier, object value)
        {
            Method<object>("SaveValue", identifier, value);
        }

        public COMObject1C77 CreateObject(string objectName)
        {
            return Method<COMObject1C77>("CreateObject", objectName);
        }

        public ICatalog1C77 CreateObjectCatalog(string catalogName)
        {
            return CreateObject($"Справочник.{catalogName}") as ICatalog1C77;
        }
        public IDocument1C77 CreateObjectDocument(string documentName)
        {
            return CreateObject($"Документ.{documentName}") as IDocument1C77;
        }



    }
}
