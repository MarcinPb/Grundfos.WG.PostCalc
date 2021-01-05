using System.Linq;
using Grundfos.GeometryModel;
using Grundfos.TW2WG.AttributeService;
using Grundfos.WG2SVG.Configuration;
using NLog;

namespace Grundfos.WG2SVG.ConsoleApp
{
    public class DomainObjectDiagnostics
    {
        public const string CommandEntry = "-dump";
        public const string CommandSyntax = "-dump -objType <ObjectTypeID> -attr <AttributeID>";
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        private readonly Wg2SvgConfigurationSection configuration;

        public DomainObjectDiagnostics(Wg2SvgConfigurationSection configuration)
        {
            this.configuration = configuration;
        }

        public void DumpResults(Arguments arguments)
        {
            if (!arguments.TryGetValue("-objType", out int objectTypeInt))
            {
                log.Info("Object type was not provided");
                log.Info("Use the following syntax: {0}", CommandSyntax);
                return;
            }

            if (!arguments.TryGetValue("-attr", out int attribute))
            {
                log.Info("Argument type was not provided");
                log.Info("Use the following syntax: {0}", CommandSyntax);
                return;
            }

            ObjectTypes objectType = (ObjectTypes)objectTypeInt;
            this.DumpResults(objectType, attribute);
        }

        public void DumpResults(ObjectTypes objectType, int attributeID)
        {
            var domainObjects = Program.GetDomainObjects(configuration.DataSource);
            var dataSourceMap = Program.GetDataSourceMap(configuration.DataSource);
            var signals = Program.GetSignalValues(configuration.DataSource);
            var attributeService = new WgAttributeService(dataSourceMap, signals);
            var filteredObjects = domainObjects.Where(x => x.ObjectType == objectType).ToList();
            var results = filteredObjects
                .Select(x => attributeService.GetAttributeValue(x.ID, attributeID))
                .OrderByDescending(x => x)
                .ToList();
            results.ForEach(x => log.Trace(x));
        }
    }
}
