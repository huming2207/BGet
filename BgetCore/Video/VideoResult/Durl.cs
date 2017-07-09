using System.Xml.Serialization;
using BgetCore.Video.VideoResult;

namespace BgetCore.Video.VideoResult
{
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

}