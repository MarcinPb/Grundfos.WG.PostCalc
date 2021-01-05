using System.Collections.Generic;
using System.Linq;
using Grundfos.GeometryModel;
using Grundfos.GeometryModel.Builders.Utils;
using Grundfos.TW.DataSourceMap;
using Grundfos.TW.XML;
using Grundfos.WG2TW.XML.ConsoleApp.Configuration;
using NLog;

namespace Grundfos.WG2TW.XML.ConsoleApp.ButtonBuilders
{
    public class PipeButtonBuilder : ButtonBuilder
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        private readonly PathLenghtResolver pathLengthResolver;
        private readonly PathTraveller pathTraveller;

        public PipeButtonBuilder(Dictionary<int, List<DataSourceMapEntry>> dataSourceMap, ButtonFactoryConfiguration configuration)
            : base(dataSourceMap, configuration)
        {
            this.pathLengthResolver = new PathLenghtResolver();
            this.pathTraveller = new PathTraveller();
        }

        public override ButtonDefinition BuildButtonDefinition(DomainObjectData item)
        {
            if (!this.DataSourceMap.TryGetValue(item.ID, out List<DataSourceMapEntry> attributes))
            {
                log.Warn("Could not find button attrributes for object ID {0} ({1})", item.ID, item.Label);
                return null;
            }

            var pathLength = this.pathLengthResolver.GetLength(item.Geometry);
            var midPoint = this.pathTraveller.GetPointerAt(item.Geometry, pathLength / 2);
            var transformed = this.Transform(midPoint.Point);
            var buttonDefinition = new ButtonDefinition
            {
                ID = item.ID,
                Label = item.Label,
                PositionX = (int)transformed.X,
                PositionY = (int)transformed.Y,
                Width = this.Configuration.ButtonWidth,
                Height = this.Configuration.ButtonHeight,
                AttributeReplacements = attributes.ToDictionary(x => x.TwVariableName.Split('.').Last(), x => x.TwVariableName),
                TemplatePath = this.ButtonTemplates[item.ObjectType],
            };
            buttonDefinition.AttributeReplacements["ID"] = item.ID.ToString();
            buttonDefinition.AttributeReplacements["LABEL"] = item.Label;
            return buttonDefinition;
        }
    }
}
