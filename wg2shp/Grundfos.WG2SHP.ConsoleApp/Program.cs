using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Grundfos.GeometryModel;
using Grundfos.GeometryModel.Builders;
using Grundfos.GeometryModel.Builders.Painters;
using Grundfos.GeometryModel.ExtensionMethods;
using Grundfos.SVG;
using Grundfos.SVG.Builders;
using Grundfos.TW.DataSourceMap;
using Grundfos.TW.DataSourceMap.Tw2Opc;
using Grundfos.TW.DataSourceMap.Wg2Opc;
using Grundfos.TW.SQL;
using Grundfos.TW2WG.AttributeService;
using Grundfos.WG2SVG.Configuration;
using Grundfos.WG2SVG.ConsoleApp.Painters;
using Grundfos.WO;
using Grundfos.WO.ObjectReaders;
using NLog;

namespace Grundfos.WG2SVG.ConsoleApp
{
    class Program
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            var arguments = new Arguments(args);
            if (arguments.ContainsKey(DomainObjectDiagnostics.CommandEntry))
            {
                var config = ConfigurationManager.GetSection("wg2svg") as Wg2SvgConfigurationSection;
                var diagnistics = new DomainObjectDiagnostics(config);
                diagnistics.DumpResults(arguments);
                return;
            }

            log.Info("================================================================================");
            log.Info("Starting the application.");
            RunAsync().Wait();

            // Only for testing.
            //Run();

            log.Info("Exiting the application.");
        }

        private async static Task RunAsync()
        {
            try
            {
                var config = ConfigurationManager.GetSection("wg2svg") as Wg2SvgConfigurationSection;
                var domainObjects = Task.Run(() => GetDomainObjects(config.DataSource).GroupBy(x => x.ObjectType).ToDictionary(x => x.Key, x => x.ToList()));
                var dataSourceMap = Task.Run(() => GetDataSourceMap(config.DataSource));
                var signals = Task.Run(() => GetSignalValues(config.DataSource));
                var attributeService = Task.Run(async () => { return new WgAttributeService(await dataSourceMap, await signals); });

                WriteStaticSvg(config.Targets[0], await domainObjects);
                for (int i = 1; i < config.Targets.Count; i++)
                {
                    WriteDynamicSvg(config.Targets[i], await domainObjects, await attributeService);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, ex.Message);
            }
        }

        // Only for testing.
        private static void Run()
        {
            try
            {
                var config = ConfigurationManager.GetSection("wg2svg") as Wg2SvgConfigurationSection;

                // Get data from SqlLite to List<DomainObjectData>
                // 13 [Pipe 3542, Junction 2987, IdahoHydrant 1, ...]
                var domainObjects = GetDomainObjects(config.DataSource)
                    .GroupBy(x => x.ObjectType)
                    .ToDictionary(x => x.Key, x => x.ToList());

                // Get data from "TestData\KepEx.xlsx", "TestData\Tw.xlsx", "TestData\TelSrv.map" files 
                // to List<DataSourceMapEntry>
                // 32298 rec.
                var dataSourceMap = GetDataSourceMap(config.DataSource);
                //var pipe_p_2_070_4922_List = dataSourceMap.Where(x => x.WgObjectLabel == "p-2-070").ToList(); 


                // Get data (36461 rec.) from: mssql.TWDB database 
                // "SELECT D_NAME, D_TIME, D_VALUE_INT, D_VALUE_FLO, D_TYPE FROM TELWIN_CUR WHERE D_TIME > @time" 
                // to List<TW.SQL.Model.SignalValue>
                // where @time = now - intervalSeconds ([App.config].intervalSeconds = 86400 sek. = 24 hours)
                var signals = GetSignalValues(config.DataSource);

                // Just two lists; dataSourceMap and signals
                var attributeService = new WgAttributeService(dataSourceMap, signals);

                // Create  map-network-static.svg (path) in C:\temp
                WriteStaticSvg(config.Targets[0], domainObjects);
                //WriteDynamicSvg(config.Targets[9], domainObjects, attributeService);

                // Create 9 (from 1 to 9) svg files in C:\temp 
                // circles: map-disabled-objects.svg, map-junction-pressure.svg, ... 
                // path: map-pipe-flow.svg, map-pipe-velocity.svg
                for (int i = 1; i < config.Targets.Count; i++)
                {
                    WriteDynamicSvg(config.Targets[i], domainObjects, attributeService);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, ex.Message);
            }
        }

        private static void WriteDynamicSvg(Target target, Dictionary<ObjectTypes, List<DomainObjectData>> domainObjects, IWgAttributeService attributeService)
        {
            var geometry = new List<Geometry>();
            foreach (var item in target.AttributeColorRules.Cast<AttributeColorRule>())
            {
                var objectType = item.ObjectTypeID;
                var objectsOfType = domainObjects[(ObjectTypes)item.ObjectTypeID];
                var strokeWidthServices = BuildStrokeWidthServices(target);

                var colorServices = target.AttributeColorRules.Cast<AttributeColorRule>().Select(x => (IColorSelectorService)new ColorSelectorService(attributeService, x)).ToList();
                var attributeColorService = new AttributeColorService(colorServices);
                var staticLayerPipePainter = new StaticLayerPipePainter(strokeWidthServices, attributeColorService);
                var geometryBuilder = BuildDynamicGeometryBuilder(staticLayerPipePainter, attributeService);
                geometry.AddRange(geometryBuilder.BuildGeometry(objectsOfType));
            }

            var svgBuilder = BuildDynamicSvgBuilder();
            svgBuilder.Draw(geometry);
            Transform(svgBuilder, target);

            svgBuilder.Save(target.FileName);
            log.Info("Saved the file to: {0}", target.FileName);
        }

        public static List<TW.SQL.Model.SignalValue> GetSignalValues(DataSource config)
        {
            List<TW.SQL.Model.SignalValue> signals;
            using (var connection = new SqlConnection(config.ConnectionString))
            {
                var signalValueService = new SignalValueService2(connection, config.TableDiscoveryConfiguration, config.SignalDiscoveryConfiguration);
                signals = signalValueService.GetSignalValues();
            }

            return signals;
        }

        public static List<DataSourceMapEntry> GetDataSourceMap(DataSource config)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(typeof(Wg2OpcMapReader).Assembly);
            });
            var mapper = new Mapper(mapperConfig);

            var builder = new DataSourceMapBuilder(
                new Wg2OpcMapReader(config.DataSourceMapConfiguration.Wg2OpcMapFileName, mapper),
                new TwVar2OpcMapReader(config.DataSourceMapConfiguration.TwVar2OpcMapFileName, mapper),
                new TwVar2IDMapReader(config.DataSourceMapConfiguration.TwVar2IDMapFileName)
            );

            var dataSourceMap = builder.Build();
            return dataSourceMap;
        }

        private static void WriteStaticSvg(Target target, Dictionary<ObjectTypes, List<DomainObjectData>> domainObjects)
        {
            var strokeWidthServices = BuildStrokeWidthServices(target);
            var labelColorRules = target.LabelColorRules.Cast<LabelColorRule>();
            var allGeometries = new List<Geometry>();
            foreach (var labelColorRule in labelColorRules)
            {
                if (!domainObjects.TryGetValue((ObjectTypes)labelColorRule.ObjectTypeID, out List<DomainObjectData> objects))
                {
                    continue;
                }

                var labelToColorService = new LabelToColorService(Color.FromArgb(63, 72, 204), labelColorRule);
                var staticLayerPipePainter = new StaticLayerPipePainter(strokeWidthServices, labelToColorService);
                var geometryBuilder = BuildStaticGeometryBuilder(staticLayerPipePainter);
                var geometry = geometryBuilder.BuildGeometry(objects);
                allGeometries.AddRange(geometry);
            }

            var svgBuilder = BuildStaticSvgBuilder();
            svgBuilder.Draw(allGeometries);
            Transform(svgBuilder, target);

            svgBuilder.Save(target.FileName);
            log.Info("Saved the file to: {0}", target.FileName);
        }

        public static void Transform(SvgBuilder builder, Target target)
        {
            foreach (var transformation in target.Transformations.Cast<Transformation>())
            {
                Transform(builder, transformation);
            }
        }

        public static void Transform(SvgBuilder builder, Transformation transformation)
        {
            switch (transformation.TransformationType)
            {
                case TransformationTypes.Move:
                    builder.MoveBy((float)transformation.MoveX, (float)transformation.MoveY);
                    break;
                case TransformationTypes.Rotate:
                    builder.RotateBy((float)transformation.RotateByDegrees.DegreesToRadians());
                    break;
                case TransformationTypes.Scale:
                    builder.Scale((float)transformation.ScaleX, (float)transformation.ScaleY);
                    break;
                case TransformationTypes.ResizeCanvas:
                    float w = (float)transformation.CanvasWidth;
                    float h = (float)transformation.CanvasHeight;
                    builder.SetCanvasSize(w, h);
                    var extremePoints = new[]
                    {
                        new Dot { ID = 201905081, Label = "201905081", Center = new Point2D(0, 0), StrokeColor = new Color(), StrokeWidthPoints = 0.00001f },
                        new Dot { ID = 201905082, Label = "201905082", Center = new Point2D(w, -h), StrokeColor = new Color(), StrokeWidthPoints = 0.00001f },
                    };
                    builder.Draw(extremePoints);
                    break;
                case TransformationTypes.ResizeCanvasToContent:
                    builder.ResizeCanvas();
                    break;
                default:
                    throw new NotSupportedException("Unknown transformation type: " + transformation.TransformationType);
            }
        }

        private static SvgBuilder BuildStaticSvgBuilder()
        {
            var colorServer = new ColorServer();
            var pathBuilder = new PathBuilder(colorServer);
            var closedPathBuilder = new ClosedPathBuilder(colorServer);
            var builders = new List<IVisualElementBuilder>
            {
                new PathBuilder(colorServer),
                new CircleBuilder(colorServer),
                new SVG.WG.Builders.HydrantBuilder(colorServer, closedPathBuilder, pathBuilder),
                new SVG.WG.Builders.ReservoirBuilder(colorServer, closedPathBuilder, pathBuilder),
                new SVG.WG.Builders.TankBuilder(colorServer, closedPathBuilder, pathBuilder),
                new SVG.WG.Builders.PumpBuilder(colorServer),
                new SVG.WG.Builders.PumpBatteryBuilder(colorServer),
                new SVG.WG.Builders.PbvBuilder(colorServer),
                new SVG.WG.Builders.TcvBuilder(colorServer),
                new SVG.WG.Builders.FcvBuilder(colorServer),
                new SVG.WG.Builders.GpvBuilder(colorServer),
                new SVG.WG.Builders.PrvBuilder(colorServer),
                new SVG.WG.Builders.PsvBuilder(colorServer),
            };

            var groupTransformations = new GroupTransformations();
            var transformations = new SVG.Transformations(groupTransformations);
            var svg = new SvgBuilder(builders, transformations);
            return svg;
        }

        private static SvgBuilder BuildDynamicSvgBuilder()
        {
            var colorServer = new ColorServer();
            var pathBuilder = new PathBuilder(colorServer);
            var closedPathBuilder = new ClosedPathBuilder(colorServer);
            var builders = new List<IVisualElementBuilder>
            {
                new CircleBuilder(colorServer),
                new SVG.WG.Builders.PipeBuilder(colorServer),
                new SVG.WG.Builders.HydrantBuilder(colorServer, closedPathBuilder, pathBuilder),
                new SVG.WG.Builders.ReservoirBuilder(colorServer, closedPathBuilder, pathBuilder),
                new SVG.WG.Builders.TankBuilder(colorServer, closedPathBuilder, pathBuilder),
                new SVG.WG.Builders.PumpBuilder(colorServer),
                new SVG.WG.Builders.PumpBatteryBuilder(colorServer),
                new SVG.WG.Builders.PbvBuilder(colorServer),
                new SVG.WG.Builders.TcvBuilder(colorServer),
                new SVG.WG.Builders.FcvBuilder(colorServer),
                new SVG.WG.Builders.GpvBuilder(colorServer),
                new SVG.WG.Builders.PrvBuilder(colorServer),
                new SVG.WG.Builders.PsvBuilder(colorServer),
            };

            var groupTransformations = new GroupTransformations();
            var transformations = new SVG.Transformations(groupTransformations);
            var svg = new SvgBuilder(builders, transformations);
            return svg;
        }


        private static GeometryBuilder BuildStaticGeometryBuilder(StaticLayerPipePainter staticLayerPipePainter)
        {
            var builderDictionary = new List<IGeometryBuilder>
            {
                new PolylineBuilder(staticLayerPipePainter),
                new DotBuilder(staticLayerPipePainter),
                new HydrantBuilder(staticLayerPipePainter),
                new ReservoirBuilder(staticLayerPipePainter),
                new TankBuilder(staticLayerPipePainter),
                new PumpBuilder(staticLayerPipePainter),
                new PumpBatteryBuilder(staticLayerPipePainter),
                new PbvBuilder(staticLayerPipePainter),
                new TcvBuilder(staticLayerPipePainter),
                new FcvBuilder(staticLayerPipePainter),
                new GpvBuilder(staticLayerPipePainter),
                new PrvBuilder(staticLayerPipePainter),
                new PsvBuilder(staticLayerPipePainter),
            };
            var geometryBuilder = new GeometryBuilder(builderDictionary);
            return geometryBuilder;
        }

        private static GeometryBuilder BuildDynamicGeometryBuilder(StaticLayerPipePainter staticLayerPipePainter, IWgAttributeService attributeService)
        {
            var builderDictionary = new List<IGeometryBuilder>
            {
                new PipeBuilder(staticLayerPipePainter, attributeService),
                new DotBuilder(staticLayerPipePainter),
                new HydrantBuilder(staticLayerPipePainter),
                new ReservoirBuilder(staticLayerPipePainter),
                new TankBuilder(staticLayerPipePainter),
                new PumpBuilder(staticLayerPipePainter),
                new PumpBatteryBuilder(staticLayerPipePainter),
                new PbvBuilder(staticLayerPipePainter),
                new TcvBuilder(staticLayerPipePainter),
            };
            var geometryBuilder = new GeometryBuilder(builderDictionary);
            return geometryBuilder;
        }

        public static List<DomainObjectData> GetDomainObjects(DataSource config)
        {
            List<DomainObjectData> domainObjects;
            using (var dataSetProvider = new DomainDataSetProxy(config.WaterGemsFileName))
            {
                domainObjects = GetWgObjects(dataSetProvider);
            }

            return domainObjects;
        }

        private static Dictionary<ObjectTypes, IStrokeWidthService> BuildStrokeWidthServices(Target config)
        {
            var pipeStrokeWidthService = new PipeStrokeWidthService
            {
                DefaultWidth = (float)config.StrokeWidthSettings.DefaultWidth,
                PipeDiameterToWidthFactor = (float)config.StrokeWidthSettings.PipeDiameterToWidthFactor,
                DiameterFieldName = config.StrokeWidthSettings.DiameterFieldName,
            };
            var symbolStrokeWidthService = new SymbolStrokeWidthService((float)config.StrokeWidthSettings.SymbolWidth);
            var strokeWidthServices = new Dictionary<ObjectTypes, IStrokeWidthService>
            {
                { ObjectTypes.Pipe, pipeStrokeWidthService },
                { ObjectTypes.Junction, pipeStrokeWidthService },
                { ObjectTypes.IdahoHydrant, pipeStrokeWidthService },
                { ObjectTypes.Reservoir, symbolStrokeWidthService },
                { ObjectTypes.Tank, symbolStrokeWidthService },
                { ObjectTypes.StandardPump, symbolStrokeWidthService },
                { ObjectTypes.VariableSpeedPumpBattery, symbolStrokeWidthService },
                { ObjectTypes.PBV, symbolStrokeWidthService },
                { ObjectTypes.TCV, symbolStrokeWidthService },
                { ObjectTypes.FCV, symbolStrokeWidthService },
                { ObjectTypes.GPV, symbolStrokeWidthService },
                { ObjectTypes.PRV, symbolStrokeWidthService },
                { ObjectTypes.PSV, symbolStrokeWidthService },
            };
            return strokeWidthServices;
        }

        private static List<DomainObjectData> GetWgObjects(DomainDataSetProxy dataSetProvider)
        {
            log.Info("Reading WaterGEMS objects.");
            var pipeReader = new PipeReader(dataSetProvider);
            var pipes = pipeReader.ReadObjects(new List<string>());

            var junctionReader = new GenericObjectReader(dataSetProvider, WO.Constants.ObjectTypes.Junction);
            var junctions = junctionReader.ReadObjects(new List<string>());

            var hydrantReader = new GenericObjectReader(dataSetProvider, WO.Constants.ObjectTypes.IdahoHydrant);
            var hydrants = hydrantReader.ReadObjects(new List<string>());

            var reservoirReader = new GenericObjectReader(dataSetProvider, WO.Constants.ObjectTypes.Reservoir);
            var reservoirs = reservoirReader.ReadObjects(new List<string>());

            var tankReader = new GenericObjectReader(dataSetProvider, WO.Constants.ObjectTypes.Tank);
            var tanks = tankReader.ReadObjects(new List<string>());

            var pumpReader = new GenericObjectReader(dataSetProvider, WO.Constants.ObjectTypes.StandardPump);
            var pumps = pumpReader.ReadObjects(new List<string>());

            var pumpBatteryReader = new GenericObjectReader(dataSetProvider, WO.Constants.ObjectTypes.VariableSpeedPumpBattery);
            var batteries = pumpBatteryReader.ReadObjects(new List<string>());

            var pbvReader = new GenericObjectReader(dataSetProvider, WO.Constants.ObjectTypes.PBV);
            var pbvs = pbvReader.ReadObjects(new List<string>());

            var tcvReader = new GenericObjectReader(dataSetProvider, WO.Constants.ObjectTypes.TCV);
            var tcvs = tcvReader.ReadObjects(new List<string>());

            var prvReader = new GenericObjectReader(dataSetProvider, WO.Constants.ObjectTypes.PRV);
            var prvs = prvReader.ReadObjects(new List<string>());

            var psvReader = new GenericObjectReader(dataSetProvider, WO.Constants.ObjectTypes.PSV);
            var psvs = psvReader.ReadObjects(new List<string>());

            var fcvReader = new GenericObjectReader(dataSetProvider, WO.Constants.ObjectTypes.FCV);
            var fcvs = fcvReader.ReadObjects(new List<string>());

            var gpvReader = new GenericObjectReader(dataSetProvider, WO.Constants.ObjectTypes.GPV);
            var gpvs = gpvReader.ReadObjects(new List<string>());

            var allObjects = pipes
                .Union(junctions)
                .Union(hydrants)
                .Union(reservoirs)
                .Union(tanks)
                .Union(pumps)
                .Union(batteries)
                .Union(pbvs)
                .Union(tcvs)
                .Union(prvs)
                .Union(psvs)
                .Union(fcvs)
                .Union(gpvs)
                .ToList();
            log.Info("{0} WaterGEMS objects were read.", allObjects.Count);
            return allObjects;
        }
    }
}
