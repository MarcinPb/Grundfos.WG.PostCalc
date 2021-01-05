using System.Collections.Generic;
using System.Linq;
using Grundfos.GeometryModel;
using Grundfos.TW.XML;

namespace Grundfos.WG2TW.XML.ConsoleApp.ButtonBuilders
{
    public class ButtonBuilderManager
    {
        private readonly Dictionary<ObjectTypes, ButtonBuilder> builders;

        public ButtonBuilderManager()
        {
            this.builders = new Dictionary<ObjectTypes, ButtonBuilder>();
        }

        public ButtonBuilderManager(Dictionary<ObjectTypes, ButtonBuilder> builders)
        {
            this.builders = builders;
        }

        public void RegisterBuilder(ObjectTypes objectType, ButtonBuilder builder)
        {
            this.builders[objectType] = builder;
        }

        public List<ButtonDefinition> Build(IList<DomainObjectData> items)
        {
            var buttonDefinitions = items.Select(x => this.BuildButtonDefinition(x)).Where(x => x != null).ToList();
            return buttonDefinitions;
        }

        private ButtonDefinition BuildButtonDefinition(DomainObjectData item)
        {
            var builder = this.builders[item.ObjectType];
            var button = builder.BuildButtonDefinition(item);
            return button;
        }
    }
}
