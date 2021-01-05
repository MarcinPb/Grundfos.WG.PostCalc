using System.Configuration;
using System.Linq;
using Grundfos.Imaging;
using Grundfos.TW.LegendBuilder.Configuration;
using Grundfos.WG2SVG.Configuration;
using NLog;

namespace Grundfos.TW.LegendBuilder
{
    class Program
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            try
            {
                var tw2svgConfigPath = ConfigurationManager.AppSettings[Constants.Wg2SvgConfigPath];
                ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap
                {
                    ExeConfigFilename = tw2svgConfigPath,
                };
                log.Trace("Reading app.config from: {0}.", tw2svgConfigPath);
                var config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                log.Trace("Reading wg2svg config section.");
                var section = config.GetSection("wg2svg") as Wg2SvgConfigurationSection;
                var targets = section.Targets.Cast<Target>().Where(x => x.AttributeColorRules.Count > 0 && !string.IsNullOrWhiteSpace(x.Legend?.FileName)).ToList();
                log.Info("{0} target definitions were read.", targets.Count);

                var settingsManager = new AppSettingsManager();
                var symmetricalRangeDetection = new SymmetricalRangeDetection(settingsManager);
                var imageBuilderFactory = new ImageBuilderFactory();
                var imageBuilder = imageBuilderFactory.Build();
                var legendBuilder = new LegendBuilder(imageBuilder, ConfigurationManager.AppSettings[Constants.LegendEntryFormat]);
                var legendBuilderManager = new LegendBuilderManager(symmetricalRangeDetection, legendBuilder);
                log.Trace("Building legend files for targets.");
                legendBuilderManager.BuildTargetLegends(targets);
            }
            catch (System.Exception ex)
            {
                log.Error(ex, ex.Message);
            }
        }

    }
}
