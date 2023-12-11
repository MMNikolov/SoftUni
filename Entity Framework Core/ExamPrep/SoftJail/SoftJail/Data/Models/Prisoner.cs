using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks.Sources;

namespace SoftJail.Data.Models
{
    public class Prisoner
    {
        public Prisoner()
        {
            Mails = new List<Mail>();
            OfficersPrisoners = new List<OfficerPrisoner>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string FullName { get; set; }

        [Required]
        public string NickName { get; set; }

        [Required]
        [MaxLength(65)]
        public int Age { get; set; }

        [Required]
        public DateTime IncarcerationDate { get; set; }


        public DateTime ReleaseDate { get; set; }


        public decimal Bail { get; set; }

        [ForeignKey(nameof(Cell))]
        public int CellId { get; set; }
        public Cell Cell { get; set; }


        public virtual ICollection<Mail> Mails { get; set; }
        public virtual ICollection<OfficerPrisoner> OfficersPrisoners { get; set; }
    }
}