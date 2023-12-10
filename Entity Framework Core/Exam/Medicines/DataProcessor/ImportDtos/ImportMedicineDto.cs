using Medicines.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Medicines.DataProcessor.ImportDtos
{
    [XmlType("Medicine")]
    public class ImportMedicineDto
    {
        [XmlElement("Name")]
        [Required]
        [MinLength(3)]
        [MaxLength(150)]
        public string Name { get; set; } = null!;

        [XmlAttribute("category")]
        [Required]
        [Range(0, 4)]
        public int Category { get; set; }

        [XmlElement("Price")]
        [Required]
        [Range(0.01, 1000)]
        public decimal Price { get; set; }

        [XmlElement("ProductionDate")]
        [Required]
        public DateTime ProductionDate { get; set; } 

        [XmlElement("ExpiryDate")]
        [Required]
        public DateTime ExpiryDate { get; set; } 

        [XmlElement("Producer")]
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Producer { get; set; } = null!;
    }
}