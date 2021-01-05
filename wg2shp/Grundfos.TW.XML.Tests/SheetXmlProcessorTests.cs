using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using NUnit.Framework;

namespace Grundfos.TW.XML.Tests
{
    [TestFixture]
    public class SheetXmlProcessorTests
    {
        [Test]
        public void Process_Test()
        {
            var configuration = new SheetXmlProcessorConfiguration
            {
                ButtonPrefix = "Przycisk_Dynamiczny",
                ElementsXPath = "Elements",
                ChangeListXPath = "Function/ChangeList",
                PointBoxesXPath = "PointBoxes/PointBox",
            };

            var targetConfiguration = new TargetConfiguration
            {
                TemplateFileName = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData\MapWithDynamicButtons.xml"),
                DestinationFileName = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData\MapWithDynamicButtons_Destination.xml"),
            };

            List<ButtonDefinition> buttons = BuildButtonDefinitions();

            var reader = new SheetXmlProcessor(configuration);
            reader.Process(buttons, targetConfiguration);

            XElement document = XElement.Load(targetConfiguration.DestinationFileName, LoadOptions.PreserveWhitespace);
            var actualPointBoxes = document.XPathSelectElements("Elements/Button[Name='Przycisk_Dynamiczny']/PointBoxes/PointBox").ToList();
            for (int i = 0; i < buttons.Count; i++)
            {
                int pointBoxIndex = i * 2;
                var button = buttons[i];
                Assert.AreEqual(button.PositionX - (button.Width / 2), int.Parse(actualPointBoxes[pointBoxIndex].XPathSelectElement("x").Value));
                Assert.AreEqual(button.PositionY - (button.Height / 2), int.Parse(actualPointBoxes[pointBoxIndex].XPathSelectElement("y").Value));
                Assert.AreEqual(button.PositionX + (button.Width / 2), int.Parse(actualPointBoxes[pointBoxIndex + 1].XPathSelectElement("x").Value));
                Assert.AreEqual(button.PositionY + (button.Height / 2), int.Parse(actualPointBoxes[pointBoxIndex + 1].XPathSelectElement("y").Value));
            }

            var actualChangeLists = document.XPathSelectElements("Elements/Button[Name='Przycisk_Dynamiczny']/Function/ChangeList/Def").ToList();
            for (int i = 0; i < buttons.Count; i++)
            {
                var button = buttons[i];
                var replacementKeys = button.AttributeReplacements.Keys.ToList();
                for (int j = 0; j < replacementKeys.Count; j++)
                {
                    var expectedChangeTo = button.AttributeReplacements[replacementKeys[j]];
                    var actualChangeListItem = actualChangeLists[i * 2 + j].Attribute(XName.Get("changeTo")).Value;
                    Assert.AreEqual(expectedChangeTo, actualChangeListItem);
                }
            }
        }

        private static List<ButtonDefinition> BuildButtonDefinitions()
        {
            return new List<ButtonDefinition>
            {
                new ButtonDefinition
                {
                    AttributeReplacements = new Dictionary<string, string>
                    {
                        { "VAR_ID", "VAR_ID_1" },
                        { "PROP_NAME", "PROP_NAME_1" },
                    },
                    PositionX = 1500,
                    PositionY = 3900,
                    Height = 4,
                    Width = 4,
                    TemplatePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData\Button.xml"),
                },
                new ButtonDefinition
                {
                    AttributeReplacements = new Dictionary<string, string>
                    {
                        { "VAR_ID", "VAR_ID_2" },
                        { "PROP_NAME", "PROP_NAME_2" },
                    },
                    PositionX = 1502,
                    PositionY = 3902,
                    Height = 4,
                    Width = 4,
                    TemplatePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData\Button.xml"),
                },
            };
        }
    }
}
