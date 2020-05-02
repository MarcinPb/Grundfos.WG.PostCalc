using System;

namespace Grundfos.WG.PostCalc.Persistence
{
    public class PersistenceException : Exception
    {
        public PersistenceException(string message) : base(message)
        {
        }

        public PersistenceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
