using System;

namespace Grundfos.OPC.Exceptions
{
    public class OpcException : Exception
    {
        public OpcException(string message) : base(message)
        {
        }
    }
}
