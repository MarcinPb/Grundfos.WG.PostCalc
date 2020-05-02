using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Grundfos.WG.PostCalc.Persistence.Model;
using Grundfos.WG.PostCalc.Persistence.Repositories;
using Haestad.Domain;
using Haestad.Support.OOP.Logging;
using Haestad.Support.Support;

namespace Grundfos.WG.PostCalc.DataExchangers
{
    public class GenericDataExchanger : DataExchangerBase
    {
        public GenericDataExchanger(
            ActionLogger logger,
            IScenario scenario,
            IDomainDataSet domainDataSet,
            IPostCalcRepository repository,
            DataExchangerConfiguration configuration)
            : base(logger, scenario, domainDataSet)
        {
            this.Repository = repository;
            this.Configuration = configuration;
        }

        public IPostCalcRepository Repository { get; }
        public DataExchangerConfiguration Configuration { get; }

        public override bool DoDataExchange(object dataExchangeContext)
        {
            try
            {
                // Log the start of the process with the "Info" level priority.
                // (Users can control the verbosity of log output to only see the level of detail they want).
                this.Logger?.WriteMessage(OutputLevel.Info, $"Reading {this.Configuration.ResultAttributeRecordName} ...");

                // Acquire the numerical engine name that supports Water Quality results.
                // This could also be hard coded as: StandardCalculationOptionFieldName.EpaNetEngine
                string engineName = this.Scenario.GetActiveNumericalEngineTypeName(this.Configuration.ResultRecordName);

                // Acquire the relevant field or fields that we want to read results for.
                IResultTimeVariantField timeVariantField = this.DomainDataSet.FieldManager.ResultField(
                    this.Configuration.ResultAttributeRecordName,
                    engineName,
                    this.Configuration.ResultRecordName) as IResultTimeVariantField;

                // Acquire the "look ahead" time steps that have been simulated (we want to use the last time step).
                double[] timeSteps = this.DomainDataSet.NumericalEngine(engineName).ResultDataConnection.TimeStepsInSeconds(this.Scenario.Id);

                // Sspecify the element types that we want to load WQ data for.
                var elementTypes = new HmIDCollection
                {
                    (int)DomainElementType.IdahoJunctionElementManager,
                    (int)DomainElementType.IdahoTankElementManager
                };

                var simulationValues = this.GetSimulationValues(timeVariantField, timeSteps, elementTypes);
                var storedValues = this.Repository.GetResultsByAttribute(this.Configuration.ResultAttributeRecordName).ToDictionary(x => x.ObjectID, x => x.Value);
                bool processWasInterrupted = this.ProcessWasInterrupted(this.Configuration.ResultAttributeRecordName, out ProcessInfo processInfo);
                if (!processWasInterrupted)
                {
                    var data = simulationValues
                        .Where(x => !double.IsNaN(x.Value))
                        .Select(x => new Result { ObjectID = x.Key, Value = x.Value });
                    this.Repository.ClearResultsByAttribute(this.Configuration.ResultAttributeRecordName);
                    this.Repository.SetResults(this.Configuration.ResultAttributeRecordName, data);
                }
                else
                {
                    var resultTimestamp = new ResultTimestamp
                    {
                        Attribute = this.Configuration.ResultAttributeRecordName,
                        ProcessID = processInfo.ProcessID,
                        ProcessStartTime = processInfo.ProcessStartTime
                    };
                    this.Repository.SetResultTimestamp(resultTimestamp);
                }

                this.Repository.SaveChanges();

                // WRITING THE DATA ----
                this.Logger?.WriteMessage(OutputLevel.Info, $"Writing {this.Configuration.ResultAttributeRecordName}...");

                // Acquire the alternative (or alternatives) that we want to write the WQ data back to.
                int alternativeId = this.Scenario.AlternativeID((int)this.Configuration.Alternative);

                // Acquired the input field (or input fields) that we want to write the WQ data to.
                IEditField field = DomainDataSet.AlternativeManager((int)this.Configuration.Alternative).AlternativeField(
                    this.Configuration.FieldName,
                    (int)DomainElementType.BaseIdahoNodeElementManager,
                    alternativeId) as IEditField;

                int valuesWrittenCount = 0;

                // Loops through the values and write them into the model.
                foreach (var item in simulationValues)
                {
                    int elementID = item.Key;
                    double doubleValue;
                    if (processWasInterrupted)
                    {
                        if (!storedValues.TryGetValue(elementID, out doubleValue))
                        {
                            continue;
                        }
                    }
                    else
                    {
                        doubleValue = item.Value;
                    }

                    double valueToWrite = doubleValue * this.Configuration.ConversionFactor;
                    field.SetValue(elementID, valueToWrite);

                    this.Logger?.WriteMessage(OutputLevel.Debug, $"{this.Configuration.ResultAttributeRecordName}: ID={elementID}, Value={doubleValue}");

                    // Count the value
                    valuesWrittenCount++;
                }

                this.Logger?.WriteMessage(OutputLevel.Info, $"Wrote {valuesWrittenCount} {this.Configuration.ResultAttributeRecordName} values.");

                return true;
            }
            catch (Exception e)
            {
                this.Logger?.WriteMessage(OutputLevel.Errors, $"Error encountered during processing of {this.Configuration.ResultAttributeRecordName} results.");
                this.Logger?.WriteException(e, true);

                return false;
            }
        }

        private bool TryGetAtrributeValue(bool useDefault, int key, Dictionary<int, double> data, double defaultValue, out double result)
        {
            if (!useDefault)
            {
                result = defaultValue;
                return true;
            }

            return data.TryGetValue(key, out result);
        }

        private Dictionary<int, double> GetSimulationValues(IResultTimeVariantField timeVariantField, double[] timeSteps, HmIDCollection elementTypes)
        {
            var result = new Dictionary<int, double>();
            var values = timeVariantField.GetValues(elementTypes, this.Scenario.Id, timeSteps.Length - 1);
            IDictionaryEnumerator enumerator = values.GetEnumerator();
            while (enumerator.MoveNext())
            {
                int elementID = (int)enumerator.Key;
                double doubleValue = (double)enumerator.Value;
                result[elementID] = doubleValue;
            }

            return result;
        }

        private bool ProcessWasInterrupted(string attribute, out ProcessInfo currentProcessInfo)
        {
            currentProcessInfo = ProcessInfoService.GetCurrentProcessInfo();
            var dbProcessInfo = this.Repository.GetResultTimestamp(attribute);
            if (dbProcessInfo == null)
            {
                return true;
            }

            bool processWasInterrupted = currentProcessInfo.ProcessID != dbProcessInfo.ProcessID || currentProcessInfo.ProcessStartTime != dbProcessInfo.ProcessStartTime;
            return processWasInterrupted;
        }
    }
}
