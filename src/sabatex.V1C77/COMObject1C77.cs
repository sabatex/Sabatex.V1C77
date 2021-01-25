using sabatex.V1C77.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace sabatex.V1C77
{
    public class COMObject1C77 : ICOMObject1C77,IGlobalContext,ICatalog1C77,IDisposable,IDocument1C77
    {
        /// <summary>
        /// mark object then disposed
        /// </summary>
        private bool _disposed = false;
        /// <summary>
        /// chaild objects
        /// </summary>
        private List<COMObject1C77> _children = new List<COMObject1C77>();
        /// <summary>
        /// link to owner
        /// </summary>
        private COMObject1C77 _owner = null;
        /// <summary>
        /// COM object handler
        /// </summary>
        internal object _handle;
        private IGlobalContext globalContext;
        
        private COMObject1C77(COMObject1C77 owner, object handle)
        {
            _owner = owner;
            _handle = handle;
            var result = this;
                while (result._owner != null)
                {
                    result = result._owner;
                }
                globalContext = result as IGlobalContext;
        }

        private T OLE1C77Function<T>(string FuncName, BindingFlags invokeAttr, params object[] Args)
        {
            // convert args

            object[] normalArgs = new object[Args.Length];
            for (int i = 0; i < Args.Length; i++)
            {
                if (Args[i] == null)
                {
                    normalArgs[i] = null;
                    continue;
                }
                var comObject = Args[i] as COMObject1C77;
                if (comObject != null)
                    normalArgs[i] = comObject._handle;
                else
                {
                    var argType = Args[i].GetType();
                    if (argType == typeof(bool))
                    {
                        normalArgs[i] = (bool)Args[i]?1:0;
                        continue;
                    }

                    normalArgs[i] = Args[i];
                }
            }
            if (normalArgs.Length == 0) normalArgs = null;


            try
            {
                var obj = _handle.GetType().InvokeMember(FuncName, invokeAttr, null, _handle, normalArgs);
                if (obj == null) return (T)obj;
                if (Marshal.IsComObject(obj))
                {
                    var comObj = new COMObject1C77(this,obj);
                    _children.Add(comObj);
                    return (T)(object)comObj;
                }
                else
                {
                    if (typeof(T)== typeof(bool))
                    {
                        Type objectType = obj.GetType();
                        if (objectType == typeof(bool)) return (T)obj;
                        if (objectType == typeof(double))
                        {
                            if ((double)obj == 0)
                                return (T)(object)false;
                            else
                                return (T)(object)true;
                        }
                    }
                    return (T)obj;                   
                }

            }
            catch (Exception e)
            {
                StringBuilder errorStr = new StringBuilder($"Error OLE1C77Function with FuncName={FuncName}");
                if (Args.Length != 0)
                {
                    errorStr.Append(" and params:");
                    for (int i = 0; i < Args.Length; i++)
                        errorStr.Append($" Arg{i}={Args[i]}");
                }
                errorStr.Append($" Inner exception - {e.Message}");
                // FuncName == null
                throw new Exception(errorStr.ToString());
            }
        }

        public T Method<T>(string methodName, params object[] args)
        {
            try
            {
                return OLE1C77Function<T>(methodName, BindingFlags.InvokeMethod, args);
            }
            catch
            {
                string error = $"Error get property {methodName}";
                Trace.TraceError(error);
                throw new Exception(error);
            }
        }
        public T GetProperty<T>(string propertyName) => OLE1C77Function<T>(propertyName,BindingFlags.GetProperty);
        public void SetProperty(string propertyName, object value) => OLE1C77Function<object>(propertyName, BindingFlags.PutDispProperty, value);

        public IGlobalContext GlobalContext => globalContext;



        /// <summary>
        /// using chaild for remove self
        /// </summary>
        /// <param name="com"></param>
        internal void RemoveChild(COMObject1C77 com)
        {
            _children.Remove(com);
        }
        public void Dispose()
        {
            if (!_disposed)
            {
                // remove all child
                while (_children.Count > 0) _children[0].Dispose();

                // remove this object from owner
                _owner?.RemoveChild(this);

                // Free any unmanaged objects here.
                if (_handle != null)
                {
                    if (Marshal.IsComObject(_handle))
                        Marshal.ReleaseComObject(_handle);
                }
                _handle = null;
                _disposed = true;
            }
        }
    
    
        public static COMObject1C77 CreateConnection(Connection con)
        {
            if ((con.PlatformType & Connection.Platform1CV7) == 0)
            {
                string error = $"Помилка! Сплутані COM Class V77 with Object {con.PlatformType.ToString()} !";
                Trace.TraceError(error);
                throw new Exception(error);
            }

            string COMServerName = con.PlatformType.ToString() + ".Application";

            Trace.TraceInformation("З'єднання з COM Server {0}!", COMServerName);
            Type v1C77 = Type.GetTypeFromProgID(COMServerName);
            if (v1C77 == null)
            {
                string error = $"Помилка зєднання з COM Class Object {COMServerName} !";
                Trace.TraceError(error);
                throw new Exception(error);
            }

            object handle = Activator.CreateInstance(v1C77);

            if (handle == null)
            {
                string error = $"Instance of class {COMServerName} was not created";
                Trace.TraceError(error);
                v1C77 = null;
                throw new Exception(error);
            }

            string stringConnection = "/D" + con.DataBasePath
                                          + (con.Exclusive ? " /M" : "")
                                          + ((con.UserName != "") ? " /N" + con.UserName : "")
                                          + ((con.UserPass != "") ? " /P" + con.UserPass : "");

            var result = new COMObject1C77(null, handle);
            try
            {
                if (!result.Method<bool>("Initialize", result.Method<object>("RMTrade"), stringConnection, "NO_SPLASH_SHOW"))
                {
                    v1C77 = null;
                    result.Dispose();
                    string error = $"Помилка зєднання з базою {con.DataBasePath} !!!";
                    Trace.TraceError(error);
                    throw new Exception(error);
                }
            }
            catch
            {
                string error = $"Initialize of class {COMServerName} was not executed";
                Trace.TraceError(error);
                v1C77 = null;
                throw new Exception(error);
            }
            return result;
        }


    }
}
