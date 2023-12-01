using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Trucks.DataProcessor.ImportDto
{
    [XmlType("Truck")]
    public class ImportTruckDTO
    {
        [XmlElement("RegistrationNumber")]
        [MaxLength(8)]
        [MinLength(8)]
        [RegularExpression(@"^[A-Z]{2}\d{4}[A-Z]{2}$")]
        public string? RegistrationNumber { get; set; }

        [XmlElement("VinNumber")]
        [MaxLength(17)]
        [MinLength(17)]
        [Required]
        public string VinNumber { get; set; } = null!;

        [XmlElement("TankCapacity")]
        [Range(950, 1420)]
        public int TankCapacity { get; set; }

        [XmlElement("CargoCapacity")]
        [Range(5000, 29000)]
        public int CargoCapacity { get; set; }

        [XmlElement("CategoryType")]
        [Required]
        [Range(0, 3)]
        public int CategoryType { get; set; }

        [XmlElement("MakeType")]
        [Required]
        [Range(0, 4)]
        public int MakeType { get; set; }
    }
}
