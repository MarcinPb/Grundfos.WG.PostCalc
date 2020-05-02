using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grundfos.OPC.Model;
using Hylasoft.Opc.Da;

namespace Grundfos.OPC
{
    public class OpcWriter : IDisposable
    {
        private readonly DaClient server;
        private const string GroupName = "OpcWriterGroupName";

        public OpcWriter(string baseAddress)
        {
            string address = "opcda://localhost/" + baseAddress;
            this.server = this.BuildProxy(address);
        }

        private DaClient BuildProxy(string baseAddress)
        {
            var server = new DaClient(new Uri(baseAddress));
            server.Connect();
            return server;
        }

        public void Publish(OpcWriteValue[] values)
        {
            // Create a group with items.
            foreach (var value in values)
            {
                this.server.Write(value.TagName, value.Value);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.server.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~OpcWriter() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
