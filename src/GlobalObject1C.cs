using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace sabatex.V1C77
{
    public class GlobalObject1C77:COMObject
    {
        
        private Connection _connection;
        private GlobalObject1C77()
        {
                
        }

        public GlobalObject1C77(object handle, Connection connection) :base(null,handle)
        {
            _connection = connection;
               
        }

        public static void ReleaseComObject(ref object o)
        {
            if ((o != null))
            {
                try
                {
                    Marshal.ReleaseComObject(o);
                }
                catch
                {
                }
                finally
                {
                    o = null;
                }
            }
        }
        public Connection Connection { get => _connection; }
        public static GlobalObject1C77 GetConnection(Connection con)
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
 
            GlobalObject1C77 global = new GlobalObject1C77(handle,con);

            try
            {
                if (!(bool)global.Method("Initialize", global.Method("RMTrade"), stringConnection, "NO_SPLASH_SHOW"))
                {
                    v1C77 = null;
                    global.Dispose();
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

            return global;
        }


        //public void Connect(Connection con)
        //{
        //    if ((con.PlatformType & Connection.Platform1CV7) == 0)
        //    {
        //        string error = $"Помилка! Сплутані COM Class V77 with Object {con.PlatformType.ToString()} !";
        //        Trace.TraceError(error);
        //        throw new Exception(error);
        //    }
        //    string COMServerName = con.PlatformType.ToString() + ".Application"; 

        //    Trace.TraceInformation("З'єднання з COM Server {0}!", COMServerName);
        //    v1C77 = Type.GetTypeFromProgID(COMServerName);
        //    if (v1C77 == null)
        //    {
        //        string error = $"Помилка зєднання з COM Class Object {COMServerName} !";
        //        Trace.TraceError(error);
        //        Handle = null;
        //        throw new Exception(error);
        //    }
        //    dynamic handle = Activator.CreateInstance(v1C77);

        //    Handle = handle;
        //    if (Handle == null)
        //    {
        //        string error = $"Instance of class {COMServerName} was not created";
        //        Trace.TraceError(error);
        //        v1C77 = null;
        //        throw new Exception(error);
        //    }

        //    dynamic RMTrade = handle.RMTrade();
        //    string stringConnection = "/D" + con.DataBasePath
        //                                  + (con.Exclusive ? " /M" : "")
        //                                  + ((con.UserName != "") ? " /N" + con.UserName : "")
        //                                  + ((con.UserPass != "") ? " /P" + con.UserPass : "");

        //    if (!(Handle as dynamic).Initialize(RMTrade, stringConnection, "NO_SPLASH_SHOW"))
        //    {
        //        v1C77 = null;
        //        ReleaseComObject(ref Handle);
        //        string error = $"Помилка зєднання з базою {con.DataBasePath} !!!";
        //        Trace.TraceError(error);
        //        throw new Exception(error);
        //    }
        //    _Connection = con;
        //}
        public COMObject CreateObject(string ObjectName)
        {
            return Method("CreateObject", ObjectName) as COMObject;
        }
        public void SaveValue(string identifier,object value)
        {
            (this.Handle as dynamic).SaveValue(identifier, value);
        }

        public COMObject EvalExpr(string expr)
        {
            return Method("evalexpr", expr) as COMObject;
        }

        
     }
}
