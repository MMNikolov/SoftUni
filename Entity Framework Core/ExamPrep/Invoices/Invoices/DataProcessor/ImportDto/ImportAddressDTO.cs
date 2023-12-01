using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Invoices.DataProcessor.ImportDto
{
    [XmlType("Address")]
    public class ImportAddressDTO
    {
        [XmlElement("StreetName")]
        [Required]
        [MinLength(10)]
        [MaxLength(20)]
        public string StreetName { get; set; } = null!;

        [XmlElement("StreetNumber")]
        [Required]
        public int StreetNumber { get; set; } 

        [XmlElement("PostCode")]
        [Required]
        public string PostCode { get; set; } = null!;

        [XmlElement("City")]
        [Required]
        [MinLength(5)]
        [MaxLength(15)]
        public string City { get; set; } = null!;

        [XmlElement("Country")]
        [Required]
        [MinLength(5)]
        [MaxLength(15)]
        public string Country { get; set; } = null!;
    }
}