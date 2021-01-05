using System;
using System.Collections.Generic;
using Grundfos.SHP;
using Grundfos.SHP.Constants;
using Grundfos.WO;
using Grundfos.WO.Constants;
using Grundfos.WO.ObjectReaders;
using NLog;

namespace Grundfos.WG2SVG.ConsoleApp
{
    public static class ShapeWriter
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        private static void WritePipes()
        {
            var progressReporter = new ProgressReporter(0.1);
            string sourceFileName = @"C:\2020-03-03\sandbox\Nowy model testowy\testOPC.wtg.sqlite";
            string targetFileName = @"C:\2020-03-03\sandbox\Nowy model testowy\eksport SHP\pipes-export.shp";
            log.Info("Exporting pipe data.");
            using (var dataSetProvider = new DomainDataSetProxy(sourceFileName))
            {
                var pipeReader = new PipeReader(dataSetProvider);
                pipeReader.ProgressChanged += (s, e) => progressReporter.HandleProgress(e.ProgressRatio, e.Message);
                var pipes = pipeReader.ReadObjects(new List<string>());
                log.Info("{0} items were read.", pipes.Count);

                var projection = new GisProjections();
                projection.Reproject(pipes, KnownProj4Projections.EPSG2176_ETRS89_PolandCS2000Zone5, KnownProj4Projections.EPSG3857_WGS84_PseudoMercator);

                var mapping = GetPipeFieldMapping();
                var shapeWriter = new ShapefileWriter(mapping);
                shapeWriter.WritePolylines(targetFileName, pipes);
            }

            log.Info("Finished exporting pipe data.");
        }

        private static void WriteNodes()
        {
            var progressReporter = new ProgressReporter(0.1);
            string sourceFileName = @"C:\2020-03-03\sandbox\Nowy model testowy\testOPC.wtg.sqlite";
            string targetFileName = @"C:\2020-03-03\sandbox\Nowy model testowy\eksport SHP\nodes-export.shp";
            log.Info("Exporting node data.");
            using (var dataSetProvider = new DomainDataSetProxy(sourceFileName))
            {
                var nodeReader = new GenericObjectReader(dataSetProvider, WO.Constants.ObjectTypes.Junction);
                nodeReader.ProgressChanged += (s, e) => progressReporter.HandleProgress(e.ProgressRatio, e.Message);
                var nodes = nodeReader.ReadObjects(new List<string>());
                log.Info("{0} items were read.", nodes.Count);

                var mapping = GetPipeFieldMapping();
                var shapeWriter = new ShapefileWriter(mapping);
                shapeWriter.WritePoints(targetFileName, nodes);
            }

            log.Info("Finished exporting node data.");
        }

        public static Dictionary<string, string> GetPipeFieldMapping()
        {
            var mapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { FieldNames.IsUserDefinedLength, ShapeAttributes.IsUserDefinedLength },
                { FieldNames.StatusInitial, ShapeAttributes.StatusInitial },
                { FieldNames.PipeMaterial, ShapeAttributes.PipeMaterial },
                { FieldNames.InstallationYear, ShapeAttributes.InstallationYear },
                { FieldNames.UserDefinedLength, ShapeAttributes.UserDefinedLength },
                { FieldNames.LengthScaled, ShapeAttributes.LengthScaled },
                { FieldNames.PipeDiameter, ShapeAttributes.PipeDiameter },
                { FieldNames.StartNodeID, ShapeAttributes.StartNodeID },
                { FieldNames.StartNodeLabel, ShapeAttributes.StartNodeLabel },
                { FieldNames.StopNodeID, ShapeAttributes.StopNodeID },
                { FieldNames.StopNodeLabel, ShapeAttributes.StopNodeLabel },
            };

            return mapping;
        }
    }
}
