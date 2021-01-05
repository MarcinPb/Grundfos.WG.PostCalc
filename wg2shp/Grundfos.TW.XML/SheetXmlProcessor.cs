using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Grundfos.TW.XML
{
    public class SheetXmlProcessor
    {
        private readonly SheetXmlProcessorConfiguration configuration;
        private readonly Dictionary<string, XElement> buttonTemplateCache;

        public SheetXmlProcessor(SheetXmlProcessorConfiguration configuration)
        {
            this.configuration = configuration;
            this.buttonTemplateCache = new Dictionary<string, XElement>();
        }

        public void Process(List<ButtonDefinition> buttonDefinitions, TargetConfiguration targetConfiguration)
        {
            XElement document = XElement.Load(targetConfiguration.TemplateFileName, LoadOptions.PreserveWhitespace);

            var elementsNode = document.XPathSelectElement(this.configuration.ElementsXPath);
            var xPathElements = document.XPathSelectElements(this.configuration.ElementsXPath);

            RemoveOldButtons(xPathElements);

            foreach (var buttonDefinition in buttonDefinitions)
            {
                XElement buttonTemplate = this.GetButtonTemplate(buttonDefinition.TemplatePath);
                var button = new XElement(buttonTemplate);
                UpdateName(button, buttonDefinition);
                UpdateChangeList(button, buttonDefinition);
                UpdatePointBoxes(button, buttonDefinition);

                elementsNode.Add(button);
            }

            using (var writer = new Windows1250StringWriter())
            {
                document.Save(writer, SaveOptions.DisableFormatting);
                var encoding = Encoding.GetEncoding(1250);
                using (var file = new StreamWriter(targetConfiguration.DestinationFileName, false, encoding))
                {
                    var sb = writer.GetStringBuilder();
                    file.Write(sb.ToString().ToCharArray());
                }
            }
        }

        private XElement GetButtonTemplate(string templatePath)
        {
            if (this.buttonTemplateCache.TryGetValue(templatePath, out XElement buttonTemplate))
            {
                return buttonTemplate;
            }

            buttonTemplate = XElement.Load(templatePath);
            this.buttonTemplateCache[templatePath] = buttonTemplate;
            return buttonTemplate;
        }

        private void RemoveOldButtons(IEnumerable<XElement> xPathElements)
        {
            var buttons = xPathElements.Descendants()
                .Where(x =>
                    x.Name == "Button" &&
                    x.Descendants().Any(desc => desc.Name == "Name" && desc.Value.StartsWith(this.configuration.ButtonPrefix, System.StringComparison.OrdinalIgnoreCase))
                ).ToList();
            buttons.Remove();
        }

        private void UpdateName(XElement button, ButtonDefinition buttonDefinition)
        {
            var nameNode = button.Descendants().FirstOrDefault(x => x.Name == "Name");
            if (nameNode == null)
            {
                return;
            }

            string buttonName = $"{this.configuration.ButtonPrefix} ({buttonDefinition.Label}) ({buttonDefinition.ID})";
            nameNode.SetValue(buttonName);
        }

        private void UpdateChangeList(XElement button, ButtonDefinition replacementDefinition)
        {
            var changeList = button.XPathSelectElements(this.configuration.ChangeListXPath);
            foreach (var replacement in replacementDefinition.AttributeReplacements)
            {
                var node = changeList.Elements().FirstOrDefault(x => x.Attributes().Any(a => a.Name == "changeFrom" && a.Value == replacement.Key));
                if (node != null)
                {
                    node.SetAttributeValue(XName.Get("changeTo"), replacement.Value);
                }
            }
        }

        private void UpdatePointBoxes(XElement button, ButtonDefinition replacementDefinition)
        {
            var pointBoxes = button.XPathSelectElements(this.configuration.PointBoxesXPath).ToList();
            pointBoxes[0].XPathSelectElement("x").SetValue(replacementDefinition.PositionX - (replacementDefinition.Width / 2));
            pointBoxes[0].XPathSelectElement("y").SetValue(replacementDefinition.PositionY - (replacementDefinition.Height / 2));
            pointBoxes[1].XPathSelectElement("x").SetValue(replacementDefinition.PositionX + (replacementDefinition.Width / 2));
            pointBoxes[1].XPathSelectElement("y").SetValue(replacementDefinition.PositionY + (replacementDefinition.Height / 2));
        }
    }
}
