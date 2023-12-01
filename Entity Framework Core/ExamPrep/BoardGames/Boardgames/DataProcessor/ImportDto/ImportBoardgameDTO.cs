using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Boardgames.DataProcessor.ImportDto
{
    [XmlType("Boardgame")]
    public class ImportBoardgameDTO
    {
        [XmlElement("Name")]
        [Required]
        [MaxLength(20)]
        [MinLength(10)]
        public string Name { get; set; } = null!;

        [XmlElement("Rating")]
        [Required]
        [Range(1.00, 10.00)]
        public double Rating { get; set; }

        [XmlElement("YearPublished")]
        [Required]
        [MaxLength(2023)]
        [MinLength(2018)]
        public int YearPublished { get; set; }

        [XmlElement("CategoryType")]
        [Required]
        [Range(0, 4)]
        public int CategoryType { get; set; }

        [XmlElement("Mechanics")]
        [Required]
        public string Mechanics { get; set; } = null!;
    }
}