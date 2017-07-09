using System.Collections.Generic;
using System.Xml.Serialization;

namespace BgetCore.Video.VideoResult
{
    [XmlRoot(ElementName="video")]
    public class VideoUrl
    {
        [XmlElement(ElementName="result")]
        public string Result { get; set; }
        
        [XmlElement(ElementName="timelength")]
        public string Timelength { get; set; }
        
        [XmlElement(ElementName="format")]
        public string Format { get; set; }
        
        [XmlElement(ElementName="accept_format")]
        public string AcceptFormat { get; set; }
        
        [XmlElement(ElementName="accept_quality")]
        public string AcceptQuality { get; set; }
        
        [XmlElement(ElementName="from")]
        public string From { get; set; }
        
        [XmlElement(ElementName="seek_param")]
        public string SeekParam { get; set; }
        
        [XmlElement(ElementName="seek_type")]
        public string SeekType { get; set; }
        
        [XmlElement(ElementName="durl")]
        public List<Durl> Durl { get; set; }
    }

}