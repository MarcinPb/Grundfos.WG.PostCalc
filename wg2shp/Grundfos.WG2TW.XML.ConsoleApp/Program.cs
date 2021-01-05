using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using AutoMapper;
using Grundfos.GeometryModel;
using Grundfos.TW.DataSourceMap;
using Grundfos.TW.DataSourceMap.Tw2Opc;
using Grundfos.TW.DataSourceMap.Wg2Opc;
using Grundfos.TW.XML;
using Grundfos.WG2SVG.Configuration;
using Grundfos.WG2TW.XML.ConsoleApp.Configuration;
using Grundfos.WO;
using NLog;

namespace Grundfos.WG2TW.XML.ConsoleApp
{
    class Program
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            try
            {
                log.Info("================================================");
                log.Info("Starting the application");
                bool diagnose = args.Any(x => x.Equals("-diag", StringComparison.OrdinalIgnoreCase));
                if (diagnose)
                {
                    log.Info("Running in diagnostic mode. Only 10 buttons for each object type will be generated.");
                }

                var config = ConfigurationManager.GetSection("wg2svg") as Wg2TwConfigurationSection;
                var mapper = BuildMapper();
                var sheetProcessorConfig = mapper.Map<SheetXmlProcessorConfiguration>(config.XmlProcessorConfiguration);
                log.Info("Getting result mapping information.");
                var dataSourceMap = GetDataSourceMap(config.DataSource, mapper).GroupBy(x => x.WgObjectID).ToDictionary(x => x.Key, x => x.ToList());
                var buttonBuilder = ButtonBuilders.ButtonBuilderFactory.BuildButtonBuilderManager(dataSourceMap, config.ButtonFactoryConfiguration);
                log.Info("Getting WaterGEMS objects from: {0}.", config.DataSource.WaterGemsFileName);
                var wgObjects = config.ButtonFactoryConfiguration.ButtonTemplates.Cast<ButtonTemplate>()
                    .ToDictionary(x => x.ObjectType, x => GetWgObjects(config.DataSource, x.ObjectType));
                log.Info("{0} WaterGEMS objects were retrieved.", wgObjects.Sum(x => x.Value.Count));
                log.Info("Building button definitions.");
                var buttonDefinitions = wgObjects.ToDictionary(x => x.Key, x => buttonBuilder.Build(x.Value));
                log.Info("{0} buttons were built.", buttonDefinitions.Sum(x => x.Value.Count));

                var twXmlProcessor = new SheetXmlProcessor(sheetProcessorConfig);
                if (diagnose)
                {
                    foreach (var item in buttonDefinitions)
                    {
                        item.Value.RemoveRange(0, Math.Max(0, item.Value.Count - 10));
                    }
                }

                log.Info("Building target files.");
                foreach (var target in config.XmlProcessorConfiguration.Targets.Cast<Configuration.Target>())
                {
                    var targetConfiguration = mapper.Map<TargetConfiguration>(target);
                    var targetButtons = target.ObjectTypes.SelectMany(x => buttonDefinitions[x]).ToList();
                    twXmlProcessor.Process(targetButtons, targetConfiguration);
                    log.Info("Successfully built the target file: {0}.", target.DestinationFileName);
                }
            }
            catch (System.Exception ex)
            {
                log.Error(ex, ex.Message);
            }

            log.Info("================================================");
        }

        private static IMapper BuildMapper()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(typeof(Wg2OpcMapReader).Assembly);
                cfg.AddProfiles(typeof(Program).Assembly);
            });
            var mapper = new Mapper(mapperConfig);
            return mapper;
        }

        public static IList<DomainObjectData> GetWgObjects(DataSource config, ObjectTypes objectType)
        {
            using (var dataSetProvider = new DomainDataSetProxy(config.WaterGemsFileName))
            {
                var provider = new WgObjectsProvider(dataSetProvider);
                var wgItems = provider.GetWgObjects(objectType);
                return wgItems;
            }
        }

        public static List<DataSourceMapEntry> GetDataSourceMap(DataSource config, IMapper mapper)
        {
            var builder = new DataSourceMapBuilder(
                new Wg2OpcMapReader(config.DataSourceMapConfiguration.Wg2OpcMapFileName, mapper),
                new TwVar2OpcMapReader(config.DataSourceMapConfiguration.TwVar2OpcMapFileName, mapper),
                new TwVar2IDMapReader(config.DataSourceMapConfiguration.TwVar2IDMapFileName)
            );

            var dataSourceMap = builder.Build();
            return dataSourceMap;
        }
    }
}
