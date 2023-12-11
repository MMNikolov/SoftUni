using Medicines.DataProcessor.ImportDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Medicines.DataProcessor.ExportDtos
{
    [XmlType("Patient")]
    public class ExportPatientDTO
    {
        [XmlElement("Name")]
        public string FullName { get; set; } = null!;

        [XmlElement("AgeGroup")]
        public string AgeGroup { get; set; } = null!;

        [XmlAttribute("Gender")]
        public string Gender { get; set; } = null!;

        [XmlArray("Medicines")]
        public ExportMedicineDTO[] Medicines { get; set; }
    }
}
