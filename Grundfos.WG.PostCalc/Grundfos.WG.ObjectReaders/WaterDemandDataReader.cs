using System.Collections.Generic;
using System.Linq;
using Grundfos.WaterDemandCalculation.Model;
using Haestad.Domain;
using Haestad.Domain.ModelingObjects;
using Haestad.Support.Support;

namespace Grundfos.WG.ObjectReaders
{
    public class WaterDemandDataReader
    {
        public WaterDemandDataReader(IDomainDataSet domainDataSet)
        {
            this.DomainDataSet = domainDataSet;
        }

        public IDomainDataSet DomainDataSet { get; }

        public IList<WaterDemandData> GetWaterDemands()
        {
            var hydrantDemands = this.GetCollectionWaterDemands(Constants.HydrantID);
            var nodeDemands = this.GetCollectionWaterDemands(Constants.NodeID);
            var customerMeterDemands = this.GetCustomerMeterWaterDemands();
            this.SetCustomerMeterZones(nodeDemands, customerMeterDemands);
            var result = hydrantDemands.Union(nodeDemands).Union(customerMeterDemands).ToList();
            return result;
        }

        public IList<WaterDemandData> GetCollectionWaterDemands(int elementTypeID)
        {
            var result = new List<WaterDemandData>();

            var manager = this.DomainDataSet.DomainElementManager(elementTypeID);
            var elementIDs = manager.ElementIDs();

            var supportedFields = manager.SupportedFields().Cast<IField>().ToDictionary(x => x.Name, x => x);
            var demandCollectionField = supportedFields["DemandCollection"];
            var editableDemandField = demandCollectionField as IEditField;
            var zoneField = supportedFields["Physical_Zone"];

            foreach (var elementID in elementIDs)
            {
                var rawZoneID = zoneField.GetValue(elementID);
                int zoneID = rawZoneID is int ? (int)rawZoneID : -1;

                var demandCollection = editableDemandField.GetValue(elementID) as DomainElementCollectionFieldListManager;
                if (demandCollection.Count == 0)
                {
                    var demandInfo = new WaterDemandData
                    {
                        ObjectID = elementID,
                        ObjectTypeID = elementTypeID,
                        BaseDemandValue = 0,
                        DemandPatternID = -1,
                        ZoneID = zoneID,
                    };
                    result.Add(demandInfo);
                }
                else
                {
                    foreach (var row in demandCollection.DataTable.Rows.Cast<System.Data.DataRow>())
                    {
                        var rawPatternID = row[Constants.DemandCollectionPatternFieldName];
                        int patternID = rawPatternID is int ? (int)rawPatternID : -1;
                        var rawBaseDemand = row[Constants.DemandCollectionBaseFlowFieldName];
                        double baseDemand = rawBaseDemand is double ? (double)rawBaseDemand : 0.0d;
                        var demandInfo = new WaterDemandData
                        {
                            ObjectID = elementID,
                            ObjectTypeID = elementTypeID,
                            BaseDemandValue = baseDemand * Constants.Flow_WG_2_M3H,
                            DemandPatternID = patternID,
                            ZoneID = zoneID,
                        };

                        result.Add(demandInfo);
                    }
                }
            }

            return result;
        }

        public IList<WaterDemandData> GetCustomerMeterWaterDemands()
        {
            var result = new List<WaterDemandData>();

            int elementTypeID = Constants.CustomerNodeMeterID;
            var manager = this.DomainDataSet.DomainElementManager(elementTypeID);
            var elementIDs = manager.ElementIDs();

            var supportedFields = manager.SupportedFields().Cast<IField>().ToDictionary(x => x.Name, x => x);
            var baseDemandField = supportedFields[Constants.DemandBaseFlowFieldName];
            var patternField = supportedFields[Constants.DemandPatternFieldName];
            var associatedElementField = supportedFields[Constants.DemandAssociatedElementFieldName];

            foreach (var elementID in elementIDs)
            {
                var rawPatternID = patternField.GetValue(elementID);
                int patternID = rawPatternID is int ? (int)rawPatternID : -1;

                var rawAssociatedElementID = associatedElementField.GetValue(elementID);
                int associatedElementID = rawAssociatedElementID is int ? (int)rawAssociatedElementID : -1;

                var rawBaseDemand = baseDemandField.GetValue(elementID);
                double baseDemand = rawBaseDemand is double ? (double)rawBaseDemand : 0.0d;

                var demandInfo = new WaterDemandData
                {
                    ObjectID = elementID,
                    ObjectTypeID = elementTypeID,
                    BaseDemandValue = baseDemand * Constants.Flow_WG_2_M3H,
                    DemandPatternID = patternID,
                    AssociatedElementID = associatedElementID,
                };

                result.Add(demandInfo);
            }

            return result;
        }

        private void SetCustomerMeterZones(IList<WaterDemandData> nodeDemands, IList<WaterDemandData> customerMeterDemands)
        {
            var nodeDictionary = new Dictionary<int, int>();
            foreach (var item in nodeDemands)
            {
                if (!nodeDictionary.ContainsKey(item.ObjectID))
                {
                    nodeDictionary[item.ObjectID] = item.ZoneID;
                }
            }
            
            foreach (var customerMeter in customerMeterDemands)
            {
                if (nodeDictionary.TryGetValue(customerMeter.AssociatedElementID, out int zoneID))
                {
                    customerMeter.ZoneID = zoneID;
                }
                else
                {

                }
            }
        }
    }
}
