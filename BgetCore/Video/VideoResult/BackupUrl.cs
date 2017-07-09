using System.Collections.Generic;
using System.Xml.Serialization;

namespace BgetCore.Video.VideoResult
{
    [XmlRoot(ElementName="backup_url")]
    public class BackupUrl 
    {
        [XmlElement(ElementName="url")]
        public List<string> Url { get; set; }
    }

}