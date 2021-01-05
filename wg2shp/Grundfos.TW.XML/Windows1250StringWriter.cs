using System.IO;
using System.Text;

namespace Grundfos.TW.XML
{
    public class Windows1250StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.GetEncoding(1250);
    }
}
