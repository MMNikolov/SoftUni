using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Boardgames.DataProcessor.ExportDto
{
    [XmlType("Creator")]
    public class ExportCreatorDTO
    {
        [XmlElement("CreatorName")]
        public string CreatorName { get; set; }

        [XmlAttribute("BoardgamesCount")]
        public int BoardgamesCount { get; set; }

        [XmlArray("Boardgames")]
        public ExportBoardgameDTO[] Boardgames { get; set; }
    }
}
