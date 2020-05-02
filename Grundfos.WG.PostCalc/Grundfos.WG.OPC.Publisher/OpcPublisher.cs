using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Grundfos.OPC;
using Grundfos.OPC.Model;
using Grundfos.WG.OPC.Publisher.Configuration;
using Haestad.Domain;
using Haestad.Support.OOP.Logging;
using Haestad.Support.Support;

namespace Grundfos.WG.OPC.Publisher
{
    public class OpcPublisher : IDisposable
    {
        private readonly OpcWriter publisher;

        public OpcPublisher(PublisherConfiguration configuration, ActionLogger logger)
        {
            this.Configuration = configuration;
            this.Logger = logger;
            this.publisher = new OpcWriter("Kepware.KEPServerEX.V6");
        }

        public PublisherConfiguration Configuration { get; }

        public ActionLogger Logger { get; }

        public virtual void PublishResults(IDomainDataSet domainDataSet, IScenario scenario)
        {
            try
            {
                // Log the start of the process with the "Info" level priority.
                // (Users can control the verbosity of log output to only see the level of detail they want).
                this.Logger?.WriteMessage(OutputLevel.Info, $"Reading {this.Configuration.ResultAttributeRecordName} ...");

                // Acquire the numerical engine name that supports Water Quality results.
                // This could also be hard coded as: StandardCalculationOptionFieldName.EpaNetEngine
                string engineName = scenario.GetActiveNumericalEngineTypeName(this.Configuration.ResultRecordName);

                // Acquire the relevant field or fields that we want to read results for.
                IResultTimeVariantField timeVariantField = domainDataSet.FieldManager.ResultField(
                    this.Configuration.ResultAttributeRecordName,
                    engineName,
                    this.Configuration.ResultRecordName) as IResultTimeVariantField;

                double[] timeSteps = domainDataSet.NumericalEngine(engineName).ResultDataConnection.TimeStepsInSeconds(scenario.Id);

                var elementTypes = new HmIDCollection(this.Configuration.ElementTypes.Select(x => (int)x).ToArray());

                var simulationValues = this.GetSimulationValues(timeVariantField, timeSteps, elementTypes, scenario.Id);

                this.PublishValuesToOpc(simulationValues);

                this.Logger?.WriteMessage(OutputLevel.Info, $"Published {simulationValues.Count} {this.Configuration.ResultAttributeRecordName} values to OPC.");
            }
            catch (Exception e)
            {
                this.Logger?.WriteMessage(OutputLevel.Errors, $"Error encountered when publishing {this.Configuration.ResultAttributeRecordName} results.");
                this.Logger?.WriteException(e, true);
            }
        }

        protected virtual void PublishValuesToOpc(Dictionary<int, double> simulationValues)
        {
            var writeValues = this.BuildRequests(simulationValues);
            publisher.Publish(writeValues.ToArray());
        }

        protected virtual List<OpcWriteValue> BuildRequests(Dictionary<int, double> simulationValues)
        {
            var writeValues = new List<OpcWriteValue>();
            foreach (var item in simulationValues)
            {
                if (!this.Configuration.Mappings.TryGetValue(item.Key, out OpcMappingEntry mapping))
                {
                    continue;
                }

                var writeValue = new OpcWriteValue { TagName = mapping.OpcTag, Value = item.Value };
                writeValues.Add(writeValue);
            }

            return writeValues;
        }

        protected virtual Dictionary<int, double> GetSimulationValues(IResultTimeVariantField timeVariantField, double[] timeSteps, HmIDCollection elementTypes, int scenarioID)
        {
            var result = new Dictionary<int, double>();
            var values = timeVariantField.GetValues(elementTypes, scenarioID, timeSteps.Length - 1);
            IDictionaryEnumerator enumerator = values.GetEnumerator();
            while (enumerator.MoveNext())
            {
                int elementID = (int)enumerator.Key;
                double doubleValue = Convert.ToDouble(enumerator.Value);
                result[elementID] = doubleValue;
            }

            return result;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.publisher.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~OpcPublisher() {
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
