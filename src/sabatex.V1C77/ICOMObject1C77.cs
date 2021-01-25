using System;
using System.Collections.Generic;
using System.Text;

namespace sabatex.V1C77
{
    public interface ICOMObject1C77:IDisposable
    {
        T Method<T>(string methodName, params object[] args);
        T GetProperty<T>(string propertyName);
        void SetProperty(string propertyName, object value);
        IGlobalContext GlobalContext { get; }
    }
}
