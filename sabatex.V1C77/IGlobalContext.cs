// Copyright (c)  Serhiy Lakas.
// Licensed under the MIT license.
using sabatex.V1C77.Models.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace sabatex.V1C77
{
    public interface IGlobalContext:ICOMObject1C77
    {
        bool EmptyValue(ICOMObject1C77 obj);
        object EvalExpr(string expr);
        void SaveValue(string identifier, object value);

        COMObject1C77 CreateObject(string objectName);

        ICatalog1C77 CreateObjectCatalog(string catalogName);
        IDocument1C77 CreateObjectDocument(string documentName);
    }
}
