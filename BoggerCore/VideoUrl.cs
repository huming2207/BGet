using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace BoggerCore
{
    [XmlRoot(ElementName="backup_url")]
    public class BackupUrl 
    {
        [XmlElement(ElementName="url")]
        public List<string> Url { get; set; }
    }

    [XmlRoot(ElementName="durl")]
    public class Durl 
    {
        [XmlElement(ElementName="order")]
        public string Order { get; set; }
        
        [XmlElement(ElementName="length")]
        public string Length { get; set; }
        
        [XmlElement(ElementName="size")]
        public string Size { get; set; }
        
        [XmlElement(ElementName="url")]
        public string Url { get; set; }
        
        [XmlElement(ElementName="backup_url")]
        public BackupUrl BackupUrl { get; set; }
    }

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