using System.Collections.Generic;
using Grundfos.GeometryModel;
using Grundfos.TW.DataSourceMap;
using Grundfos.WG2TW.XML.ConsoleApp.Configuration;

namespace Grundfos.WG2TW.XML.ConsoleApp.ButtonBuilders
{
    public static class ButtonBuilderFactory
    {
        public static ButtonBuilderManager BuildButtonBuilderManager(Dictionary<int, List<DataSourceMapEntry>> dataSourceMap, ButtonFactoryConfiguration configuration)
        {
            var genericButtonBuilder = new ButtonBuilder(dataSourceMap, configuration);
            var builders = new Dictionary<ObjectTypes, ButtonBuilder>
            {
                { ObjectTypes.Pipe, new PipeButtonBuilder(dataSourceMap, configuration) },
                { ObjectTypes.Junction, genericButtonBuilder },
                { ObjectTypes.Tank, genericButtonBuilder },
                { ObjectTypes.IdahoHydrant, genericButtonBuilder },
                { ObjectTypes.Reservoir, genericButtonBuilder },
                { ObjectTypes.PBV, genericButtonBuilder },
                { ObjectTypes.FCV, genericButtonBuilder },
                { ObjectTypes.TCV, genericButtonBuilder },
                { ObjectTypes.GPV, genericButtonBuilder },
                { ObjectTypes.PRV, genericButtonBuilder },
                { ObjectTypes.PSV, genericButtonBuilder },
                { ObjectTypes.StandardPump, genericButtonBuilder },
                { ObjectTypes.IsolationValve, genericButtonBuilder },
                { ObjectTypes.VariableSpeedPumpBattery, genericButtonBuilder },
            };
            return new ButtonBuilderManager(builders);
        }
    }
}
