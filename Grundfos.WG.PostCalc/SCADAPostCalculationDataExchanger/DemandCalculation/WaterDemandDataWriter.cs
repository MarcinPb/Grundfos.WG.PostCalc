﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Schema;
using Grundfos.WaterDemandCalculation.Model;
using Grundfos.WG.PostCalc.DataExchangers;
//using Grundfos.WaterDemandCalculation.Model;
using Haestad.Domain;
using Haestad.Domain.ModelingObjects;
using Haestad.Domain.ModelingObjects.Water;
using Haestad.Support.OOP.Logging;
using Haestad.Support.Support;

namespace Grundfos.WG.PostCalc.DemandCalculation
{
    public class WaterDemandDataWriter
    {
        private string _testedZoneName = "1 - Przybków";
        private string _logFolder = @"C:\Users\Administrator\AppData\Local\Bentley\SCADAConnect\10";
        //private string _logFolder = @"C:\WG2TW\Grundfos.WG.PostCalc";
        private string dateFormat = "yyyy-MM-dd_HH-mm-ss_fffffff";

        public WaterDemandDataWriter(
            ActionLogger logger,
            IDomainDataSet domainDataSet,
            WaterDemandDataWriterConfiguration configuration,
            DataExchangerContext dataExchangerContext)
        {
            this.Logger = logger;
            this.DomainDataSet = domainDataSet;
            this.Configuration = configuration;
            this.DataExchangerContext = dataExchangerContext;
        }

        public ActionLogger Logger { get; }
        public IDomainDataSet DomainDataSet { get; }
        public WaterDemandDataWriterConfiguration Configuration { get; }
        public DataExchangerContext DataExchangerContext { get; }

        public bool WriteDemands(ZoneDemandData zoneDemandData)
        {
            try
            {
                if (zoneDemandData.ZoneName == _testedZoneName)
                {
                    this.Logger?.WriteMessage(OutputLevel.Info, $"# Start updating '{zoneDemandData.ZoneName}' zone.");
                    Helper.DumpToFile(zoneDemandData, Path.Combine(_logFolder, $"Dump_{DateTime.Now.ToString(dateFormat)}_ZoneDemandData.xml"));
                }

                var domainDataSet = this.DomainDataSet as IdahoDomainDataSet;
                this.UpdateCollectionDemands(Constants.NodeID, zoneDemandData);
                this.UpdateCollectionDemands(Constants.HydrantID, zoneDemandData);
                this.UpdateCustomerNodeMeterDemands(zoneDemandData);

                return true;
            }
            catch (Exception e)
            {
                this.Logger?.WriteMessage(OutputLevel.Errors, "Error encountered when updating base water demands.");
                this.Logger?.WriteException(e, true);

                return false;
            }
        }

        private void UpdateCollectionDemands(int elementTypeID, ZoneDemandData zoneDemandData)
        {
            if (zoneDemandData.ZoneName == _testedZoneName)
            {
                Logger.WriteMessage(OutputLevel.Info, $"## UpdateCollectionDemands for zone='{zoneDemandData.ZoneName}' and ElementTypeID={elementTypeID}");
            }
            var elements = zoneDemandData.Demands.Where(x => x.ObjectTypeID == elementTypeID).GroupBy(x => x.ObjectID).ToDictionary(x => x.Key, x => x.ToList());

            // elementTypeID = Constants.NodeID = 55 (Junction) or 54 (Hydrant) 
            var manager = this.DomainDataSet.DomainElementManager(elementTypeID);
            var supportedFields = manager.SupportedFields().Cast<IField>().ToDictionary(x => x.Name, x => x);
            var demandCollectionField = supportedFields["DemandCollection"];
            var editableDemandField = demandCollectionField as IEditField;

            foreach (var element in elements)
            {
                try
                {
                    //if (zoneDemandData.ZoneName == _testedZoneName)
                    //{
                    //    Logger.WriteMessage(OutputLevel.Info, $"## \telement: {element.Value.FirstOrDefault()}");
                    //}

                    if (this.Configuration.ExcludedObjectIDs.Contains(element.Key))
                    {
                        if (zoneDemandData.ZoneName == _testedZoneName)
                        {
                            Logger.WriteMessage(OutputLevel.Info, $"## \tExcludedObjectID: {element.Value.FirstOrDefault()?.ObjectID}");
                        }
                        continue;
                    }

                    bool wasModified = false;
                    DomainElementCollectionFieldListManager demandCollection;
                    try
                    {
                        demandCollection = editableDemandField.GetValue(element.Key) as DomainElementCollectionFieldListManager;
                    }
                    catch (ArgumentException)
                    {
                        this.Logger.WriteMessage(OutputLevel.Errors, $"The object {element.Key} does not exist in the model.");
                        continue;
                    }

                    foreach (var row in demandCollection.DataTable.Rows.Cast<System.Data.DataRow>())
                    {
                        // Constants.DemandCollectionPatternFieldName = "DemandCollection_DemandPattern"
                        var rawPatternID = row[Constants.DemandCollectionPatternFieldName];
                        // Constants.FixedPatternID = -1
                        int patternID = rawPatternID is int ? (int)rawPatternID : Constants.FixedPatternID;
                        if (this.Configuration.ExcludedDemandPatterns.Contains(patternID))
                        {
                            if (zoneDemandData.ZoneName == _testedZoneName)
                            {
                                Logger.WriteMessage(OutputLevel.Info, $"## \tExcludedDemandPatterns: {rawPatternID}");
                            }
                            continue;
                        }

                        var demandItem = element.Value.FirstOrDefault(x => x.DemandPatternID == patternID);
                        //var demandItem = element.Value.FirstOrDefault(x => x.DemandPatternID == 19126); // "Straty"
                        if (demandItem == null)
                        {
                            if (zoneDemandData.ZoneName == _testedZoneName)
                            {
                                Logger.WriteMessage(OutputLevel.Info, $"## \tdemandItem == null");
                            }
                            continue;
                        }

                        double value = demandItem.ActualDemandValue * zoneDemandData.DemandAdjustmentRatio * Constants.Flow_M3H_2_WG;
                        if (zoneDemandData.ZoneName == _testedZoneName)
                        {
                            Logger.WriteMessage(OutputLevel.Info, $"\t{element.Value.FirstOrDefault()?.ObjectID, 6}\t{demandItem.DemandPatternName,15}\t{value, 20}\t=\t{demandItem.ActualDemandValue, 20}\t*\t{zoneDemandData.DemandAdjustmentRatio, 20}\t*\t{Constants.Flow_M3H_2_WG, 20}");
                        }

                        // My: 
                        //if (zoneDemandData.ZoneName == _testedZoneName)
                        //{
                        //    this.Logger.WriteMessage(OutputLevel.Info, $"# BaseFlow of object {element.Key} was modified: {row[Constants.DemandCollectionBaseFlowFieldName]} <-- {value} .");
                        //    this.Logger.WriteMessage(OutputLevel.Info, $"# BaseFlow value: {value} = {demandItem.ActualDemandValue} * {zoneDemandData.DemandAdjustmentRatio} * {Constants.Flow_M3H_2_WG} .");
                        //}

                        // Constants.DemandCollectionBaseFlowFieldName = "DemandCollection_BaseFlow"
                        row[Constants.DemandCollectionBaseFlowFieldName] = value;
                        wasModified = true;
                    }

                    if (wasModified)
                    {
                        editableDemandField.SetValue(element.Key, demandCollection);
                    }

                }
                catch (Exception e)
                {
                    Logger.WriteMessage(OutputLevel.Info, $"## \terror: {e.Message}");
                    throw;
                }
            }
        }

        private void UpdateCustomerNodeMeterDemands(ZoneDemandData zoneDemandData)
        {
            if (zoneDemandData.ZoneName == _testedZoneName)
            {
                Logger.WriteMessage(OutputLevel.Info, $"## UpdateCustomerNodeMeterDemands for zone='{zoneDemandData.ZoneName}'.");
            }
            var elements = zoneDemandData.Demands.Where(x => x.ObjectTypeID == Constants.CustomerNodeMeterID).ToList();

            // domainElementTypeID = Constants.CustomerNodeMeterID = 73(Customer Node)
            var manager = this.DomainDataSet.DomainElementManager(Constants.CustomerNodeMeterID);
            var supportedFields = manager.SupportedFields().Cast<IField>().ToDictionary(x => x.Name, x => x);
            // Constants.DemandBaseFlowFieldName = "Demand_BaseFlow"
            var baseFlowField = supportedFields[Constants.DemandBaseFlowFieldName] as IEditField;
            // Constants.DemandPatternFieldName = "Demand_DemandPattern"
            var patternField = supportedFields[Constants.DemandPatternFieldName] as IEditField;

            foreach (var element in elements)
            {
                if (this.Configuration.ExcludedObjectIDs.Contains(element.ObjectID))
                {
                    continue;
                }

                object rawPatternID;
                try
                {
                    // "Demand_DemandPattern"
                    rawPatternID = patternField.GetValue(element.ObjectID);
                }
                catch (Exception)
                {
                    this.Logger.WriteMessage(OutputLevel.Errors, $"The object {element.ObjectID} does not exist in the model.");
                    continue;
                }

                int patternID = rawPatternID is int ? (int)rawPatternID : Constants.FixedPatternID;
                if (this.Configuration.ExcludedDemandPatterns.Contains(patternID))
                {
                    continue;
                }

                double value = element.ActualDemandValue * zoneDemandData.DemandAdjustmentRatio * Constants.Flow_M3H_2_WG;
                if (zoneDemandData.ZoneName == _testedZoneName)
                {
                    Logger.WriteMessage(OutputLevel.Info, $"\t{element.ObjectID,6}\t{value,20}\t=\t{element.ActualDemandValue,20}\t*\t{zoneDemandData.DemandAdjustmentRatio,20}\t*\t{Constants.Flow_M3H_2_WG,20}");
                }

                // My: 
                //if (zoneDemandData.ZoneName == _testedZoneName)
                //{
                //    this.Logger.WriteMessage(OutputLevel.Info, $"# BaseFlow of object {element.ObjectID} was modified: oldValue <-- {value} .");
                //    this.Logger.WriteMessage(OutputLevel.Info, $"# BaseFlow value: {value} = {element.ActualDemandValue} * {zoneDemandData.DemandAdjustmentRatio} * {Constants.Flow_M3H_2_WG} .");
                //}

                baseFlowField.SetValue(element.ObjectID, value);
            }
        }
    }
}
