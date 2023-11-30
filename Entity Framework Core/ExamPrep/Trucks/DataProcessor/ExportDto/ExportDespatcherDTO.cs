using System.Xml.Serialization;

namespace Trucks.DataProcessor.ExportDto
{
    [XmlType("Despatcher")]
    public class ExportDespatcherDTO
    {
        [XmlElement("DespatcherName")]
        public string DespatcherName { get; set; } = null!;

        [XmlAttribute("TrucksCount")]
        public int TrucksCount { get; set; }

        [XmlArray("Trucks")]
        public ExportTruckDTO[] Trucks { get; set; } = null!;
    }
}
