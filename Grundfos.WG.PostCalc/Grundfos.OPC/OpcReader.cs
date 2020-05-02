using System;
using System.Collections.Generic;
using System.Linq;
using TitaniumAS.Opc.Client.Common;
using TitaniumAS.Opc.Client.Da;

namespace Grundfos.OPC
{
    public class OpcReader : IDisposable
    {
        private bool disposedValue = false;
        private readonly OpcDaServer server;

        public OpcReader(string baseAddress)
        {
            this.server = this.BuildProxy(baseAddress);
        }

        public double GetDouble(string signal)
        {
            var signals = new List<string> { signal };
            var ageList = new List<TimeSpan> { TimeSpan.MaxValue };
            OpcDaVQTE[] wrappedValues = this.server.Read(signals, ageList);
            if (wrappedValues.Length != 1)
            {
                throw new Exception("Unexpected number of returned results.");
            }

            var wrappedResult = wrappedValues[0];
            if (wrappedResult.Error.Failed)
            {
                throw new Exception(string.Format("Error when reading values for signal: {0}.", signal));
            }

            var result = Convert.ToDouble(wrappedResult.Value);
            return result;
        }

        private OpcDaServer BuildProxy(string baseAddress)
        {
            var server = new OpcDaServer(baseAddress);
            server.Connect();
            return server;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.server.Disconnect();
                    this.server.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
    }
}
