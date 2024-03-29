﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trucks.DataProcessor.ImportDto
{
    public class ImportClientDTO
    {
        [Required]
        [MaxLength(40)]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [MaxLength(40)]
        [MinLength(2)]
        public string Nationality { get; set; }

        [Required]
        public string Type { get; set; }
        public int[] Trucks { get; set; }
    }
}
