using System;
using System.Globalization;
using System.Xml.Serialization;

namespace DataModel.Files.Ztm
{
    [Serializable]
    public class Stop
    {
        // stop_id,stop_code,stop_name,stop_desc,stop_lat,stop_lon,stop_url,location_type
        public Stop()
        {
        }

        public Stop(string id, string code, string name, string desc, string lat, string lon,string url, string locationType)
        {
            Id = Convert.ToInt64(id);
            Code = code;
            Name = name;
            Description = desc;
            Lat = Convert.ToDouble(lat, CultureInfo.InvariantCulture);
            Lon = Convert.ToDouble(lon, CultureInfo.InvariantCulture);
            Url = url;
            LocationType = Convert.ToInt32(locationType);
        }

        [XmlAttribute("stop_id")]
        public long Id { get; set; }

        [XmlAttribute("stop_code")]
        public string Code { get; set; }

        [XmlAttribute("stop_name")]
        public string Name { get; set; }

        [XmlAttribute("stop_desc")]
        public string Description { get; set; }

        [XmlAttribute("stop_lat")]
        public double Lat { get; set; }

        [XmlAttribute("stop_lon")]
        public double Lon { get; set; }

        [XmlAttribute("url")]
        public string Url { get; set; }

        [XmlAttribute("locationType")]
        public int LocationType { get; set; }


        public override string ToString()
        {
            return $"{Id} - {Code} - {Name} - {Lat} - {Lon}";
        }
    }
}
