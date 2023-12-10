using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicines.DataProcessor.ImportDtos
{
    public class ImportPatientsDTO
    {
        [JsonProperty("FullName")]
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string FullName { get; set; } = null!;


        [JsonProperty("AgeGroup")]
        [Required]
        [Range(0, 2)]
        public int AgeGroup { get; set; }

        [JsonProperty("Gender")]
        [Required]
        [Range(0, 1)]
        public int Gender { get; set; }

        [JsonProperty("Medicines")]
        public int[] MedicineIds { get; set; }
    }
}
