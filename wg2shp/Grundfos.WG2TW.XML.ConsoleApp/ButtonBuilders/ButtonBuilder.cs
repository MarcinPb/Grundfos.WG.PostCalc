using System;
using System.Collections.Generic;
using System.Linq;
using Grundfos.GeometryModel;
using Grundfos.GeometryModel.ExtensionMethods;
using Grundfos.TW.DataSourceMap;
using Grundfos.TW.XML;
using Grundfos.WG2SVG.Configuration;
using Grundfos.WG2TW.XML.ConsoleApp.Configuration;
using NLog;

namespace Grundfos.WG2TW.XML.ConsoleApp.ButtonBuilders
{
    public class ButtonBuilder
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        public ButtonBuilder(Dictionary<int, List<DataSourceMapEntry>> dataSourceMap, ButtonFactoryConfiguration configuration)
        {
            this.DataSourceMap = dataSourceMap;
            this.Configuration = configuration;
            this.Transformations = configuration.Transformations.Cast<Transformation>().OrderBy(x => x.SequenceNumber).ToList();
            this.ButtonTemplates = configuration.ButtonTemplates.Cast<ButtonTemplate>().ToDictionary(x => x.ObjectType, x => x.ButtonTemplatePath);
        }

        public Dictionary<int, List<DataSourceMapEntry>> DataSourceMap { get; private set; }
        public List<Transformation> Transformations { get; private set; }
        public Dictionary<Grundfos.GeometryModel.ObjectTypes, string> ButtonTemplates { get; }
        public ButtonFactoryConfiguration Configuration { get; private set; }

        public virtual ButtonDefinition BuildButtonDefinition(DomainObjectData item)
        {
            if (!this.DataSourceMap.TryGetValue(item.ID, out List<DataSourceMapEntry> attributes))
            {
                log.Warn("Could not find button attrributes for object ID {0} ({1})", item.ID, item.Label);
                return null;
            }

            try
            {
                var transformed = this.Transform(item.Geometry[0]);
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
            catch (Exception ex)
            {
                log.Error("Could not build button definition for object ID {0} ({1})", item.ID, item.Label);
                log.Error(ex, ex.Message);
                throw;
            }
        }

        protected Point2D Transform(Point2D source)
        {
            foreach (var transformation in this.Transformations)
            {
                source = this.Transform(source, transformation);
            }

            return source;
        }

        protected Point2D Transform(Point2D source, Transformation transformation)
        {
            switch (transformation.TransformationType)
            {
                case TransformationTypes.Move:
                    return source.Move(transformation.MoveX, transformation.MoveY);
                case TransformationTypes.Rotate:
                    return source.Rotate(transformation.RotateByDegrees.DegreesToRadians());
                case TransformationTypes.Scale:
                    return source.Scale(transformation.ScaleX, transformation.ScaleY);
                default:
                    throw new NotSupportedException("Unknown transformation type: " + transformation.TransformationType);
            }
        }
    }
}
