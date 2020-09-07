using System;
using System.Collections.Generic;
using System.Linq;
using Grundfos.OPC.Model;
using TitaniumAS.Opc.Client.Common;
using TitaniumAS.Opc.Client.Da;
using TitaniumAS.Opc.Client.Da.Browsing;

namespace Grundfos.OPC
{
    public class OpcReader : IDisposable
    {
        OpcDaServer server;

        public OpcReader(string address)
        {
            Uri url = UrlBuilder.Build(address);
            this.server = new OpcDaServer(url);
            server.Connect();
        }

        public ICollection<OpcValue> GetValues(ICollection<string> tags)
        {
            var guid = Guid.NewGuid();
            OpcDaGroup group = server.AddGroup(guid.ToString());
            group.IsActive = true;

            var requests = tags.Select(x => new OpcDaItemDefinition { ItemId = x, IsActive = true }).ToArray();
            OpcDaItemResult[] results = group.AddItems(requests);
            this.ValidateOpcItems(results);
            
            OpcDaItemValue[] rawValues = group.Read(group.Items, OpcDaDataSource.Device);
            var values = rawValues.Select(x => new OpcValue { Tag = x.Item.ItemId, Value = x.Value }).ToList();
            return values;
        }

        public void WriteValues(ICollection<OpcValue> values)
        {
            var guid = Guid.NewGuid();
            OpcDaGroup group = server.AddGroup(guid.ToString());
            group.IsActive = true;

            var requests = values.Select(x => new OpcDaItemDefinition { ItemId = x.Tag, IsActive = true }).ToArray();
            OpcDaItemResult[] results = group.AddItems(requests);
            this.ValidateOpcItems(results);

            var join = values.Join(
                group.Items,
                (v) => v.Tag,
                (v) => v.ItemId,
                (opcVal, item) => new { OpcVal = opcVal, Item = item },
                StringComparer.Ordinal
            ).ToList();

            if (join.Count != values.Count)
            {
                throw new Exception("The number of OPC group items did not match the number of requests.");
            }

            OpcDaItem[] items = join.Select(x => x.Item).ToArray();
            object[] vals = join.Select(x => x.OpcVal.Value).ToArray();

            HRESULT[] result = group.Write(items, vals);
            if (result == null || result.Length != values.Count || result.Any(x => x.Failed))
            {
                throw new Exception("Could not write values to OPC server.");
            }
        }

        public List<string> Browse(string node = null, int depth = int.MaxValue)
        {
            var browser = new OpcDaBrowserAuto(server);
            var items = BrowseChildren(browser, node, depth);
            return items;
        }

        private List<string> BrowseChildren(IOpcDaBrowser browser, string itemId = null, int depth = int.MaxValue, int indent = 0)
        {
            var itemNames = new List<string>();

            OpcDaBrowseElement[] elements = browser.GetElements(itemId);
            var items = elements.Where(x => x.IsItem).ToList();

            // Output elements.
            foreach (OpcDaBrowseElement element in items)
            {
                // Output the element.
                itemNames.Add(element.ItemId);

                // Skip elements without children.
                if (!element.HasChildren || indent >= depth)
                {
                    continue;
                }

                var childNames = BrowseChildren(browser, element.ItemId, indent + 1);
                itemNames.AddRange(childNames);
            }

            return itemNames;
        }

        private void ValidateOpcItems(OpcDaItemResult[] results)
        {
            var failures = results.Where(x => x.Error.Failed).ToList();
            if (failures.Count > 0)
            {
                string messages = string.Join(", ", failures.Select(x => x.Error.ToString()).Distinct());
                string message = string.Format("Could not get results for OPC tags due to the following problems: {0}.", messages);
                throw new Exception(message);
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
        // ~OpcReader() {
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
