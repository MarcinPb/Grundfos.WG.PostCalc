using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grundfos.WG.Model
{
    public class Pipe
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public bool IsActive { get; set; }
        public string PipeStatus { get; set; }
    }
}
