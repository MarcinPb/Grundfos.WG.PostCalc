using System.Collections.Generic;
using System.Linq;

namespace Grundfos.WG.Model
{
    public class WaterDemandPattern
    {
        private IList<WaterDemandPatternEntry> _profile;

        public string Name { get; set; }

        public IList<WaterDemandPatternEntry> Profile
        {
            get => _profile;
            set => _profile = value.OrderBy(x => x.TimeshiftMinutes).ToList();
        }

        public override string ToString()
        {
            return $"{Name} ({this.Profile.Count})";
        }
    }
}
