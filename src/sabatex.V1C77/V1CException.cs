using System;
using System.Collections.Generic;
using System.Text;

namespace sabatex.V1C77
{
    public class V1CException : Exception
    {
        public V1CException(){}
        public V1CException(string message) : base($"V1C77.module: {message}") { }
    }
}
