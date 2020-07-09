using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Grundfos.WG.Model;
using Haestad.Domain;
using Haestad.Domain.ModelingObjects;
using Haestad.Domain.ModelingObjects.Water;
using Haestad.Support.Support;

namespace Grundfos.WG.ObjectReaders
{
    public class SqliteProxy
    {
        private readonly string _sqliteFileName;
        //private DomainDataSetProxy _domainDataSetProxy;

        public SqliteProxy(string sqliteFileName)
        {
            _sqliteFileName = sqliteFileName;
        }

        public IList<WaterDemandData> GetJunctionList()
        {
            return GetDemandNodeList(Constants.NodeID);
        }

        public IList<WaterDemandData> GetHydrantList()
        {
            return GetDemandNodeList(Constants.HydrantID);
        }
        public IList<WaterDemandData> GetCustomerMeterList()
        {
            IList<WaterDemandData> resul = new List<WaterDemandData>();

            using (var dataSetProvider = new DomainDataSetProxy(_sqliteFileName))
            using (var dataSet = dataSetProvider.OpenDomainDataSet())
            {
                var demandPatternReader = new WaterDemandDataReader(dataSet);
                resul = demandPatternReader.GetCustomerMeterWaterDemands();
            }

            return resul;
        }


        public Dictionary<int, string> GetZoneList()
        {
            Dictionary<int, string> resul = new Dictionary<int, string>();

            using (var dataSetProvider = new DomainDataSetProxy(_sqliteFileName))
            using (var dataSet = dataSetProvider.OpenDomainDataSet())
            {
                var zoneReader = new ZoneReader(dataSet);
                resul = zoneReader.GetZones();
            }

            return resul;
        }

        public Dictionary<int, string> GetIdahoPatternList()
        {
            Dictionary<int, string> patterns = new Dictionary<int, string>();

            using (var dataSetProvider = new DomainDataSetProxy(_sqliteFileName))
            using (var dataSet = dataSetProvider.OpenDomainDataSet())
            {
                var demandPatternReader = new WaterDemandPatternCurveReader(dataSet);
                patterns = demandPatternReader.GetPatterns().ToDictionary(x => x.Value, x => x.Key);

                //FillPatternNames(demands, patterns.ToDictionary(x => x.Value, x => x.Key));
                //FillZoneNamesInWaterDemands(demands, zones);
            }

            return patterns;
        }


        public IList<IdahoPatternPatternCurve> GetIdahoPatternPatternCurveList()
        {
            var idahoPatternPatternCurveList = new List<IdahoPatternPatternCurve>();

            using (var dataSetProvider = new DomainDataSetProxy(_sqliteFileName))
            {
                using (var dataSet = dataSetProvider.OpenDomainDataSet())
                {
                    var demandPatternReader = new WaterDemandPatternCurveReader(dataSet);
                    var patterns = demandPatternReader.GetPatterns();
                    idahoPatternPatternCurveList = demandPatternReader
                        .GetIdahoPatternPatternCurveList(patterns.Where(x => x.Value >= 0).Select(x => x.Value).ToList())
                        .ToList();
                }
            }

            return idahoPatternPatternCurveList;
        }

        public void FillPatternNames(IList<WaterDemandData> demands, Dictionary<int, string> patterns)
        {
            foreach (var item in demands)
            {
                if (!patterns.TryGetValue(item.DemandPatternID, out string patternName))
                {
                    throw new Exception(string.Format("Could not find pattern definition for pattern ID: {0}.", item.DemandPatternID));
                }

                item.DemandPatternName = patternName;
            }
        }

        public void FillZoneNamesInWaterDemands(IList<WaterDemandData> demands, Dictionary<int, string> zones)
        {
            foreach (var item in demands.Where(x => x.ZoneID > 0))
            {
                if (!zones.TryGetValue(item.ZoneID, out string zoneName))
                {
                    throw new Exception(string.Format("Could not find pattern definition for pattern ID: {0}.", item.DemandPatternID));
                }

                item.ZoneName = zoneName;
            }
        }

        private IList<WaterDemandData> GetDemandNodeList(int nodeTypeId)
        {
            var result = new List<WaterDemandData>();

            using (var dataSetProvider = new DomainDataSetProxy(_sqliteFileName))
            using (var dataSet = dataSetProvider.OpenDomainDataSet())
            {
                var waterDemandDataReader = new WaterDemandDataReader(dataSet);
                result = waterDemandDataReader.GetCollectionWaterDemands(nodeTypeId).ToList();
            }

            return result;
        }

        public void UpdateCustomerMeterZones(IList<WaterDemandData> customerMeterDemands, IList<WaterDemandData> nodeDemands)
        {
            //var nodeDictionary = new Dictionary<int, int>();
            //foreach (var item in nodeDemands)
            //{
            //    if (!nodeDictionary.ContainsKey(item.ObjectID))
            //    {
            //        nodeDictionary[item.ObjectID] = item.ZoneID;
            //    }
            //}

            //foreach (var customerMeter in customerMeterDemands)
            //{
            //    if (nodeDictionary.TryGetValue(customerMeter.AssociatedElementID, out int zoneID))
            //    {
            //        customerMeter.ZoneID = zoneID;
            //    }
            //    else
            //    {

            //    }
            //} 

            customerMeterDemands.ToList().ForEach(x =>
            {
                var obj = nodeDemands.FirstOrDefault(y => x.AssociatedElementID == y.ObjectID);
                if (obj == null)
                {
                    throw new Exception($"Could not find Junction ID: {x.AssociatedElementID} for CustomerMeter ID: {x.ObjectID}.");
                }
                else
                {
                    x.ZoneID = obj.ZoneID;
                    x.ZoneName = obj.ZoneName;
                }
            });
        }
        public IList<WaterDemandData> GetPipeList1()
        {
            var result = new List<WaterDemandData>();
            using (var dataSetProvider = new DomainDataSetProxy(_sqliteFileName))
            using (var dataSet = dataSetProvider.OpenDomainDataSet())
            {
                int elementTypeID = 69;     // Pipe //Constants.CustomerNodeMeterID;
                var manager = dataSet.DomainElementManager(elementTypeID);
                var elementIDs = manager.ElementIDs();

                var supportedFields = manager.SupportedFields().Cast<IField>().ToDictionary(x => x.Name, x => x);
                //var baseDemandField = supportedFields[Constants.DemandBaseFlowFieldName];
                //var patternField = supportedFields[Constants.DemandPatternFieldName];

                var associatedElementField = supportedFields[Constants.DemandAssociatedElementFieldName];

                var isActiveField = supportedFields["HMIActiveTopologyIsActive"];

                foreach (var elementID in elementIDs)
                {
                    var isActive = bool.Parse(isActiveField.GetValue(elementID).ToString());

                    //var rawPatternID = patternField.GetValue(elementID);
                    //int patternID = rawPatternID is int ? (int)rawPatternID : -1;

                    //var rawBaseDemand = baseDemandField.GetValue(elementID);
                    //double baseDemand = rawBaseDemand is double ? (double)rawBaseDemand : 0.0d;

                    var rawAssociatedElementID = associatedElementField.GetValue(elementID);
                    int associatedElementID = rawAssociatedElementID is int ? (int)rawAssociatedElementID : -1;


                    var demandInfo = new WaterDemandData
                    {
                        ObjectID = elementID,
                        ObjectTypeID = elementTypeID,
                        //BaseDemandValue = baseDemand * Constants.Flow_WG_2_M3H,
                        //DemandPatternID = patternID,
                        AssociatedElementID = associatedElementID,
                        IsActive = isActive
                    };

                    result.Add(demandInfo);
                }
            }
            return result;
        }

        public IList<Pipe> GetPipeList()
        {
            List<Pipe> result = new List<Pipe>();
            using (var dataSetProvider = new DomainDataSetProxy(_sqliteFileName))
            using (var dataSet = dataSetProvider.OpenDomainDataSet())
            {
                Dictionary<int, string> ids;
                ids = ((IdahoDomainDataSet)dataSet).IdahoPipeElementManager.Elements()
                    .Cast<ModelingElementBase>()
                    .ToDictionary(x => x.Id, x => x.Label);

                int elementTypeID = 69;     // Pipe //Constants.CustomerNodeMeterID;
                var manager = dataSet.DomainElementManager(elementTypeID);
                var elementIDs = manager.ElementIDs();

                var supportedFields = manager.SupportedFields().Cast<IField>().ToDictionary(x => x.Name, x => x);
                var pipeStatus = supportedFields["PipeStatus"];
                var isActiveField = supportedFields["HMIActiveTopologyIsActive"];

                foreach (var elementId in elementIDs)
                {
                    var isActive = bool.Parse(isActiveField.GetValue(elementId).ToString());
                    var pipeStatusValue = pipeStatus.GetValue(elementId);

                    result.Add(new Pipe()
                    {
                        Id = elementId,
                        Label = ids[elementId],
                        PipeStatus = pipeStatusValue.ToString(),
                        IsActive = isActive,
                    });
                }
            }

            return result;
        }




        public void GetNode(int nodeId, int nodeType)
        {
            var result = new List<WaterDemandData>();

            using (var dataSetProvider = new DomainDataSetProxy(_sqliteFileName))
            using (var dataSet = dataSetProvider.OpenDomainDataSet())
            {
                GetNodePropertyList(dataSet, nodeId, nodeType);
            }
        }

        private void GetNodePropertyList(IDomainDataSet dataSet, int nodeId, int nodeType)
        {
            int elementTypeID = nodeType;   // 23
            var result = new List<WaterDemandData>();

            var manager = dataSet.DomainElementManager(elementTypeID);
            var elementIDs = manager.ElementIDs();

            var supportedFields1 = manager.SupportedFields().Cast<IField>().ToList();
            //var supportedFields = manager.SupportedFields().Cast<IField>().ToDictionary(x => x.Name, x => x);
            //var demandCollectionField = supportedFields["DemandCollection"];
            //var editableDemandField = demandCollectionField as IEditField;
            //var zoneField = supportedFields["Physical_Zone"];

            //var isActiveField = supportedFields["HMIActiveTopologyIsActive"];

            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (var supportedField in supportedFields1)
            {
                string suppFieldName = supportedField.Name;
                var val = supportedField.GetValue(nodeId)?.ToString();
                dict.Add(suppFieldName, val);
            }

            IDomainElementManager conduitManager = dataSet.DomainElementManager((int)DomainElementType.ConduitElementManager);
            // Todo: here I get error.
            IResultTimeVariantField flowField = conduitManager.ResultField(
                    StandardResultRecordName.IdahoDemandNodeResults_NodeDemand,
                    //StandardCalculationOptionFieldName.SCADASimulationMode, 
                    StandardResultRecordName.IdahoDemandNodeResults
                ) as IResultTimeVariantField;

            ////Flows over time:
            //double[] flows = (double[])flowField.GetValuesOverTime(nodeId, dataSet.ScenarioManager.ActiveScenarioID);

            ////Flow for a specific time step of 5.The actual time for timeStepIndex 5 depends on the output increment and whether or not the scenario has pressure elements that could compute at “non - standard” time increments or what are called” intermediate” time steps.
            ////double flow = (double)flowField.GetValue(conduitID, dataSet.ScenarioManager.ActiveScenarioID, 5);


            var elementID = nodeId;
            //foreach (var elementID in elementIDs)
            //{

                //var isActive = bool.Parse(isActiveField.GetValue(elementID).ToString());

                //var rawZoneID = zoneField.GetValue(elementID);
                //int zoneID = rawZoneID is int ? (int)rawZoneID : -1;

                //var demandCollection = editableDemandField.GetValue(elementID) as DomainElementCollectionFieldListManager;
                //if (demandCollection.Count == 0)
                //{
                //    var demandInfo = new WaterDemandData
                //    {
                //        ObjectID = elementID,
                //        ObjectTypeID = elementTypeID,
                //        BaseDemandValue = 0,
                //        DemandPatternID = -1,
                //        ZoneID = zoneID,
                //    };
                //    result.Add(demandInfo);
                //}
                //else
                //{
                //    foreach (var row in demandCollection.DataTable.Rows.Cast<System.Data.DataRow>())
                //    {
                //        var rawPatternID = row[Constants.DemandCollectionPatternFieldName];
                //        int patternID = rawPatternID is int ? (int)rawPatternID : -1;
                //        var rawBaseDemand = row[Constants.DemandCollectionBaseFlowFieldName];
                //        double baseDemand = rawBaseDemand is double ? (double)rawBaseDemand : 0.0d;
                //        var demandInfo = new WaterDemandData
                //        {
                //            ObjectID = elementID,
                //            ObjectTypeID = elementTypeID,
                //            BaseDemandValue = baseDemand * Constants.Flow_WG_2_M3H,
                //            DemandPatternID = patternID,
                //            ZoneID = zoneID,
                //            IsActive = isActive,
                //        };

                //        result.Add(demandInfo);
                //    }
                //}
            //}
        }

        // Made based on GetSimulationResult method.
        public void GetSimulationResult()
        {
            try
            {
                var ResultRecordName = StandardResultRecordName.IdahoPipeResults;
                var ResultAttributeRecordName = StandardResultRecordName.PipeResultControlStatus;
                var FieldName = StandardFieldName.PipeStatus;
                //ConversionFactor = 1,
                var ElementTypes = new DomainElementType[]
                {
                    DomainElementType.IdahoPipeElementManager,
                };

                using (var dataSetProvider = new DomainDataSetProxy(_sqliteFileName))
                using (var domainDataSet = dataSetProvider.OpenDomainDataSet())
                {
                    int scenarioId = domainDataSet.ScenarioManager.ActiveScenarioID;
                    IScenario scenario = domainDataSet.ScenarioManager.Element(scenarioId) as IScenario;

                    // Log the start of the process with the "Info" level priority.
                    // (Users can control the verbosity of log output to only see the level of detail they want).
                    //this.Logger?.WriteMessage(OutputLevel.Info, $"Reading {this.Configuration.ResultAttributeRecordName} ...");

                    // Acquire the numerical engine name that supports Water Quality results.
                    // This could also be hard coded as: StandardCalculationOptionFieldName.EpaNetEngine
                    string engineName = scenario.GetActiveNumericalEngineTypeName(ResultRecordName);

                    // Acquire the relevant field or fields that we want to read results for.
                    IResultTimeVariantField timeVariantField = domainDataSet.FieldManager.ResultField(
                        ResultAttributeRecordName,
                        engineName,
                        //StandardCalculationOptionFieldName.EpaNetEngine,
                        ResultRecordName
                    ) as IResultTimeVariantField;

                    double[] timeSteps = domainDataSet.NumericalEngine(engineName).ResultDataConnection.TimeStepsInSeconds(scenario.Id);

                    var elementTypes = new HmIDCollection(ElementTypes.Select(x => (int)x).ToArray());

                    var simulationValues = this.GetSimulationValues(timeVariantField, timeSteps, elementTypes, scenario.Id);
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Error encountered when publishing results.");
                //this.Logger?.WriteException(e, true);
            }
        }

        private Dictionary<int, double> GetSimulationValues(IResultTimeVariantField timeVariantField, double[] timeSteps, HmIDCollection elementTypes, int scenarioID)
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

    }
}
